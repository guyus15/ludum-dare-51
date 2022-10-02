using UnityEngine;
using UnityEngine.UI;

public class ReloadProgressIndicator : MonoBehaviour
{
    private float _startReloadTime;
    private float _reloadTime;

    private bool _isProgressing;

    private Image _reloadProgressIndicator;

    private PlayerReloadEvent _reloadEvent;

    private void Awake()
    {
        EventManager.AddListener<PlayerReloadEvent>(OnReload);

        Debug.Log("I am awake");

        _reloadProgressIndicator = GetComponent<Image>();
        _reloadProgressIndicator.enabled = false;
    }

    private void OnReload(PlayerReloadEvent evt)
    {
        _reloadEvent = evt;

        _startReloadTime = Time.time;
        _reloadTime = _reloadEvent.reloadTime;

        if (_reloadProgressIndicator != null)
            _reloadProgressIndicator.enabled = true;

        _isProgressing = true;
    }

    private void Update()
    {
        float timeSinceReloadStarted = Time.time - _startReloadTime;

        if (_reloadProgressIndicator == null) return;

        if (_isProgressing)
        {
            if (timeSinceReloadStarted < _reloadTime)
            {
                _reloadProgressIndicator.fillAmount = (timeSinceReloadStarted / _reloadTime);
            }
            else
            {
                _reloadProgressIndicator.fillAmount = 0;

                _isProgressing = false;

                _reloadEvent.onReloadInstance.Invoke();
            }
        }
        else
        {
            _reloadProgressIndicator.enabled = false;
        }
    }
}
