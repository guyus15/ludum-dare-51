using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private GameObject _ammoCountTextObject;
    private TMPro.TextMeshProUGUI _ammoCountText;

    private WeaponManager _playerWeaponManager;
    private WeaponController _weaponController;

    private bool _reduceFontSize;

    private float _startFontSize;
    [SerializeField] private int _characterLengthBeforeOverflow = 5;
    [SerializeField] private float _perCharacterFontSize = 3;

    private void Start()
    {
        _ammoCountText = _ammoCountTextObject.GetComponent<TMPro.TextMeshProUGUI>();

        _playerWeaponManager = FindObjectOfType<WeaponManager>();

        _startFontSize = _ammoCountText.fontSize;
    }

    private void Update()
    {
        _weaponController = _playerWeaponManager.ActiveWeapon;

        if (_weaponController == null)
            return;

        string ammoText = _weaponController.GetAmmoText();

        float fontSizeDelta = _perCharacterFontSize * (ammoText.Length - _characterLengthBeforeOverflow);

        if (ammoText.Length > _characterLengthBeforeOverflow && !_reduceFontSize)
        {
            _ammoCountText.fontSize -= fontSizeDelta;
            _reduceFontSize = true;
        }
        else if (ammoText.Length <+ _characterLengthBeforeOverflow && _reduceFontSize)
        {
            _ammoCountText.fontSize = _startFontSize;
            _reduceFontSize = false;
        }

        _ammoCountText.text = ammoText;
    }
}
