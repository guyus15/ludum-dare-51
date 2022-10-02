using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private Volume _globalVolume;

    [Header("Transition")]
    [SerializeField] private float _fadeInDuration = 0.5f;
    [SerializeField] private float _fadeOutDuration = 0.5f;

    private float _startFadeTime;

    private bool _isFadingIn;
    private bool _isFadingOut;

    [SerializeField] private float _vignetteBaseIntensity = 0.3f;
    private float _vignetteIntensity;

    private Vignette _vignetteComponent;

    private void Start()
    {
        EventManager.AddListener<PlayerHitEvent>(OnPlayerHit);
    }

    private void OnPlayerHit(PlayerHitEvent evt)
    {
        // Finding the Vignette component of the global volume
        foreach (var volumeComponent in _globalVolume.profile.components)
        {
            if (volumeComponent is Vignette vignette)
            {
                _vignetteComponent = vignette;
            }
        }

        float vignetteIntensityAmount = Mathf.Clamp(((float)evt.damageInflicted / 100), 0, 1 - _vignetteBaseIntensity);
        _vignetteIntensity = _vignetteBaseIntensity + vignetteIntensityAmount;

        StartEffect();
    }

    private void StartEffect()
    {
        _startFadeTime = Time.time;

        _isFadingIn = true;
        _isFadingOut = false;
    }

    private void Update()
    {
        float timeSinceFadeStarted = Time.time - _startFadeTime;

        if (_isFadingIn && !_isFadingOut)
        {
            if (timeSinceFadeStarted < _fadeInDuration)
            {
                _vignetteComponent.intensity.value = _vignetteIntensity - (timeSinceFadeStarted / _fadeInDuration);
            }
            else
            {
                _vignetteComponent.intensity.value = 0f;

                _isFadingIn = false;
            }
        }
        else if (_isFadingOut)
        {
            if (timeSinceFadeStarted < _fadeOutDuration)
            {
                _vignetteComponent.intensity.value = (timeSinceFadeStarted / _fadeOutDuration) * _vignetteIntensity;
            }
            else
            {
                _vignetteComponent.intensity.value = _vignetteIntensity;

                _isFadingOut = false;
            }
        }
    }
}
