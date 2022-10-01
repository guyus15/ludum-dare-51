using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPrefab;

    [SerializeField] private Transform _weaponParentSocket;
    [SerializeField] private Transform _defaultWeaponPosition;

    public bool hasFired;

    [SerializeField] private float _maxRecoilDistance = 0.5f;
    [SerializeField] private float _recoilSharpness = 50f;
    [SerializeField] private float _returnRecoilSharpness = 10f;

    private Vector2 _weaponRecoilLocalPosition;
    private Vector2 _accumulatedRecoil;

    private GameObject _weapon;

    [SerializeField] private LayerMask weaponLayer;

    public bool IsPointingAtEnemy { get; set; }

    private void Start()
    {
        _weapon = Instantiate(_weaponPrefab, _weaponParentSocket.transform.position, _weaponParentSocket.transform.rotation);
        _weapon.transform.parent = _weaponParentSocket.transform;
    }

    private void Update()
    {
        WeaponController activeWeapon = _weapon.GetComponent<WeaponController>();

        if (activeWeapon == null) return;

        if (activeWeapon.IsReloading) return;

        if (activeWeapon.CanReload())
        {
            if (!activeWeapon.automaticReload && InputManager.GetReloadInputDown())
            {
                // Handle manual reloading here

                activeWeapon.IsReloading = true;

                PlayerReloadEvent evt = Events.s_PlayerReloadEvent;
                evt.reloadTime = activeWeapon.reloadTime;
                evt.onReloadInstance = activeWeapon.ReloadWeapon;
                EventManager.Broadcast(evt);
            }

            if (activeWeapon.automaticReload && activeWeapon.NeedsToReload)
            {
                // Handle automatic reloading here

                activeWeapon.IsReloading = true;

                PlayerReloadEvent evt = Events.s_PlayerReloadEvent;
                evt.reloadTime = activeWeapon.reloadTime;
                evt.onReloadInstance = activeWeapon.ReloadWeapon;
                EventManager.Broadcast(evt);
            }
        }

        // Handle weapon shooting
        hasFired = activeWeapon.HandleShootInputs(
            InputManager.GetFireInputDown(),
            InputManager.GetFireInputHeld(),
            InputManager.GetFireInputUp()
        );

        // Handle weapon recoil
        if (hasFired)
        {
            _accumulatedRecoil += Vector2.left * activeWeapon.recoilForce;
            _accumulatedRecoil = Vector2.ClampMagnitude(_accumulatedRecoil, _maxRecoilDistance);
        }
    }

    private void LateUpdate()
    {
        UpdateWeaponRecoil();

        _weaponParentSocket.localPosition = (Vector2)_defaultWeaponPosition.localPosition + _weaponRecoilLocalPosition;
    }

    private void UpdateWeaponRecoil()
    {
        if (_weaponRecoilLocalPosition.x >= _accumulatedRecoil.x * 0.99f)
            _weaponRecoilLocalPosition = Vector2.Lerp(_weaponRecoilLocalPosition, _accumulatedRecoil, _recoilSharpness * Time.deltaTime);
        else
        {
            _weaponRecoilLocalPosition = Vector2.Lerp(_weaponRecoilLocalPosition, Vector2.zero, _returnRecoilSharpness * Time.deltaTime);
            _accumulatedRecoil = _weaponRecoilLocalPosition;
        }
    }
}
