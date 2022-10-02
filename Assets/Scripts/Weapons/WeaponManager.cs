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

    public WeaponController ActiveWeapon { get; private set; }
    public bool IsPointingAtEnemy { get; set; }

    private void Awake()
    {
        EventManager.AddListener<PlayerPickupEvent>(OnPickup);
    }

    private void Start()
    {
        _weapon = Instantiate(_weaponPrefab, _weaponParentSocket.transform.position, _weaponParentSocket.transform.rotation);
        _weapon.transform.parent = _weaponParentSocket.transform;
    }

    private void Update()
    {
        ActiveWeapon = _weapon.GetComponent<WeaponController>();

        if (ActiveWeapon == null) return;

        if (ActiveWeapon.IsReloading) return;

        if (ActiveWeapon.CanReload())
        {
            if (!ActiveWeapon.automaticReload && InputManager.GetReloadInputDown())
            {
                // Handle manual reloading here

                ActiveWeapon.IsReloading = true;

                PlayerReloadEvent evt = Events.s_PlayerReloadEvent;
                evt.reloadTime = ActiveWeapon.reloadTime;
                evt.onReloadInstance = ActiveWeapon.ReloadWeapon;
                EventManager.Broadcast(evt);
            }

            if (ActiveWeapon.automaticReload && ActiveWeapon.NeedsToReload)
            {
                // Handle automatic reloading here

                ActiveWeapon.IsReloading = true;

                PlayerReloadEvent evt = Events.s_PlayerReloadEvent;
                evt.reloadTime = ActiveWeapon.reloadTime;
                evt.onReloadInstance = ActiveWeapon.ReloadWeapon;
                EventManager.Broadcast(evt);
            }
        }

        // Handle weapon shooting
        hasFired = ActiveWeapon.HandleShootInputs(
            InputManager.GetFireInputDown(),
            InputManager.GetFireInputHeld(),
            InputManager.GetFireInputUp()
        );

        // Handle weapon recoil
        if (hasFired)
        {
            _accumulatedRecoil += Vector2.left * ActiveWeapon.recoilForce;
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

    private void OnPickup(PlayerPickupEvent evt)
    {
        switch (evt.type)
        {
            case PlayerPickupEvent.PickupType.AMMO:
                Debug.Log("Picking up ammo.");
                ActiveWeapon.AddAmmo(GameConstants.AMMO_PICKUP_AMOUNT);
                break;
        }
    }
}
