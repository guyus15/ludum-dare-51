using UnityEngine;
using UnityEngine.UI;

public class NotificationToast : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _titleTextContent;
    [SerializeField] private TMPro.TextMeshProUGUI _descriptionTextContent;

    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private HorizontalOrVerticalLayoutGroup _layoutGroup;

    [Header("Duration")]
    [SerializeField] private float _notificationDuration = 1.0f;

    private float _currentNotificationDuration;

    [Header("Transitions")]
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private float _fadeOutDuration;

    [Header("Movement")]
    [SerializeField] private float _moveInDuration;
    [SerializeField] private float _moveOutDuration;

    [SerializeField] private AnimationCurve _moveInCurve;
    [SerializeField] private AnimationCurve _moveOutCurve;

    private float _startFadeTime;

    private bool _isFadingIn;
    private bool _isFadingOut;
    private bool _isMovingIn;
    private bool _isMovingOut;


    private RectTransform _rectTransform;

    public void Initialise(string titleText, string descText, float delay)
    {
        Canvas.ForceUpdateCanvases();

        _titleTextContent.text = titleText;
        _descriptionTextContent.text = descText;

        if (GetComponent<RectTransform>())
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        _startFadeTime = Time.time + delay;

        _isFadingIn = true;
        _isFadingOut = true;
    }

    private void Complete()
    {
        _startFadeTime = Time.time;

        _isFadingIn = false;
        _isMovingIn = false;

        _isFadingOut = true;
        _isMovingOut = true;
    }

    private void Update()
    {
        if (_currentNotificationDuration >= _notificationDuration)
        {
            Complete();

            _currentNotificationDuration = 0;
        }

        _currentNotificationDuration += Time.deltaTime;

        float timeSinceFadeStarted = Time.time - _startFadeTime;

        if (_isFadingIn && !_isFadingOut)
        {
            if (timeSinceFadeStarted < _fadeInDuration)
            {
                _canvasGroup.alpha = timeSinceFadeStarted / _fadeInDuration;
            }
            else
            {
                _canvasGroup.alpha = 1f;

                _isFadingIn = false;
            }
        }

        if (_isMovingIn && !_isMovingOut)
        {
            if (timeSinceFadeStarted < _moveInDuration)
            {
                _layoutGroup.padding.left = (int)_moveInCurve.Evaluate(timeSinceFadeStarted / _moveInDuration);

                if (GetComponent<RectTransform>())
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
                }
            }
            else
            {
                _layoutGroup.padding.left = 0;

                if (GetComponent<RectTransform>())
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
                }

                _isMovingIn = false;
            }
        }

        if (_isFadingOut)
        {
            if (timeSinceFadeStarted < _fadeOutDuration)
            {
                _canvasGroup.alpha = 1 - (timeSinceFadeStarted / _fadeOutDuration);
            }
            else
            {
                _canvasGroup.alpha = 0f;

                _isFadingOut = false;
                Destroy(gameObject);
            }
        }

        if (_isMovingOut)
        {
            if (timeSinceFadeStarted < _moveOutDuration)
            {
                _layoutGroup.padding.left = (int)_moveOutCurve.Evaluate(timeSinceFadeStarted / _moveOutDuration);

                if (GetComponent<RectTransform>())
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
                }
            }
            else
            {
                _isMovingOut = false;
            }
        }
    }
}
