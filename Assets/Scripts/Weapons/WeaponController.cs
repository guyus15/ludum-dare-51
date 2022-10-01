using System.Collections;
using UnityEngine;

public enum WeaponShootType
{
    SINGLE,
    AUTO
}

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Parameters")]
    public float recoilForce = 1.0f;
    [SerializeField] private GameObject _weaponRoot;
    [SerializeField] private WeaponShootType _weaponShootType;
    [SerializeField] private int _damagePerBullet = 1;
    [SerializeField] private int _bulletsPerShot = 1;
    [SerializeField] private float _weaponShotDelay = 0.5f;
    [SerializeField] private float _bulletVisibilityDuration = 0.02f;
    [SerializeField] private float _bulletSpreadAngle = 0.0f;
    [SerializeField] private float _bulletForce = 1.0f;
    [SerializeField] private bool _wantsToShoot;

    [Header("Ammunition Parameters")]
    public bool automaticReload = true;
    public float reloadTime = 1.0f;
    [SerializeField] private int _startingAmmoInStockpile = 10;
    [SerializeField] private int _ammoPerClip = 3;

    private int _currentAmmoInStockpile;
    private int _currentAmmoInClip;
    private float _lastTimeShot = Mathf.NegativeInfinity;

    [Header("Weapon Effects")]
    [SerializeField] private Transform _muzzlePosition;
    [SerializeField] private GameObject _muzzleFlashEffect;
    [SerializeField] private GameObject _impactParticleEffect;
    [SerializeField] private float _lineRendersMaxLength = 100.0f;

    private LineRenderer[] _lineRenderers;
    private Player _player;

    public bool IsWeaponActive { get; private set; }
    public bool IsReloading { get; set; }
    public bool NeedsToReload { get; private set; }

    private void Awake()
    {
        _currentAmmoInStockpile = _startingAmmoInStockpile;
        _currentAmmoInClip = _ammoPerClip;

        _lineRenderers = FindObjectsOfType<LineRenderer>();
        _player = FindObjectOfType<Player>();
    }

    public bool CanReload()
    {
        return _currentAmmoInStockpile > 0 && _currentAmmoInClip < _ammoPerClip;
    }

    public void ReloadWeapon()
    {
        _currentAmmoInStockpile -= (_ammoPerClip - _currentAmmoInClip);

        int ammoToLoad;

        if (_currentAmmoInStockpile < 0)
            ammoToLoad = _ammoPerClip + _currentAmmoInStockpile;
        else
            ammoToLoad = _ammoPerClip;

        if (ammoToLoad > 0)
            NeedsToReload = false;

        _currentAmmoInStockpile = Mathf.Clamp(_currentAmmoInStockpile, 0, int.MaxValue);

        _currentAmmoInClip = ammoToLoad;

        IsReloading = false;
    }

    public string GetAmmoText()
    {
        int shotsPerClip = (int)Mathf.Floor(_currentAmmoInClip / _bulletsPerShot);
        int shotsPerStockpile = (int)Mathf.Floor(_currentAmmoInStockpile / _bulletsPerShot);

        if (_currentAmmoInClip % _bulletsPerShot > 0)
            shotsPerClip++;

        if (_currentAmmoInStockpile % _bulletsPerShot > 0)
            shotsPerStockpile++;

        return $"{shotsPerClip}/{shotsPerStockpile}";
    }

    public bool HandleShootInputs(bool inputDown, bool inputHeld, bool inputUp)
    {
        _wantsToShoot = inputDown || inputHeld;

        return _weaponShootType switch
        {
            WeaponShootType.SINGLE => inputDown && TryShoot(),
            WeaponShootType.AUTO => inputHeld && TryShoot(),
            _ => false,
        };
    }

    public void ClearAllLineRenderers()
    {
        // Disables all line renderers
        if (_lineRenderers == null) return;
        
        foreach (LineRenderer lineRenderer in _lineRenderers)
        {
            lineRenderer.enabled = false;
        }
    }

    private bool TryShoot()
    {
        if (_currentAmmoInClip < 1 || !(_lastTimeShot + _weaponShotDelay < Time.time) || GameManager.IsPaused)
            return false;

        StartCoroutine(HandleShoot());

        return true;
    }

    private IEnumerator HandleShoot()
    {
        int bulletsPerThisShot = UseAmmo(_bulletsPerShot);

        for (int i = 0; i < bulletsPerThisShot; i++)
        {
            Vector2 shotDirection = GetShotDirectionWithinSpread(_muzzlePosition);

            // Checks to see if the hit object is a physics object (i.e. has a Rigidbody2D component).
            RaycastHit2D hitInfo = Physics2D.Raycast(_muzzlePosition.position, shotDirection);

            if (hitInfo)
            {
                Rigidbody2D objectRigidbody = hitInfo.collider.GetComponent<Rigidbody2D>() ?? null;

                if (objectRigidbody != null)
                    objectRigidbody.AddForce(shotDirection.normalized * _bulletForce, ForceMode2D.Impulse);

                IDamagable objectDamagable = hitInfo.collider.GetComponent<IDamagable>();

                // Removes health to any IDamagable object we might hit.
                objectDamagable?.RemoveHealth(_damagePerBullet);

                if (_impactParticleEffect != null)
                    Instantiate(_impactParticleEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

                Vector2 recoilDirection = -shotDirection.normalized;
                _player.Recoil(recoilForce, recoilDirection);

                _lineRenderers[i].SetPosition(0, _muzzlePosition.position);
                _lineRenderers[i].SetPosition(1, hitInfo.point);
            }
            else
            {
                _lineRenderers[i].SetPosition(0, _muzzlePosition.position);
                _lineRenderers[i].SetPosition(1, _muzzlePosition.position + (Vector3)shotDirection.normalized * _lineRendersMaxLength);
            }

            if (_muzzleFlashEffect != null)
                Instantiate(_muzzleFlashEffect, _muzzlePosition.position, _muzzlePosition.rotation);

            _lineRenderers[i].enabled = true;
        }

        yield return new WaitForSeconds(_bulletVisibilityDuration);

        ClearAllLineRenderers();
    }

    private int UseAmmo(int amount)
    {
        int thisBulletsPerShot = amount;

        _currentAmmoInClip -= amount;

        if (_currentAmmoInClip < 0)
            thisBulletsPerShot = amount + _currentAmmoInClip;

        _currentAmmoInClip = Mathf.Clamp(_currentAmmoInClip, 0, _ammoPerClip);

        if (_currentAmmoInClip == 0)
            NeedsToReload = true;

        _lastTimeShot = Time.time;

        return thisBulletsPerShot;
    }

    private Vector2 GetShotDirectionWithinSpread(Transform shootTransform)
    {
        float spreadAngleRatio = _bulletSpreadAngle / 180.0f;

        Vector2 spreadWorldDirection = Vector3.Slerp(shootTransform.right, Random.insideUnitCircle, spreadAngleRatio);

        return spreadWorldDirection;
    }
}
