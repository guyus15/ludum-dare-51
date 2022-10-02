using UnityEngine;

public class ModifierManager : MonoBehaviour
{
    [SerializeField] private float _modifierTimeInterval = 10.0f;
    private float _currentTimeInterval;

    private IModifier _currentModifier;

    private Modifiers _modifiers;

    private void Start()
    {
        _modifiers = GetComponent<Modifiers>();
    }

    private void Update()
    {
        if (_currentTimeInterval >= _modifierTimeInterval)
        {
            if (_currentModifier != null)
                _currentModifier.Deactivate();

            int randomIndex = Random.Range(0, _modifiers.AllModifiers.Count);
            IModifier selectedModifier = _modifiers.AllModifiers[randomIndex];

            // Activate the selected modifier.
            selectedModifier.Activate();

            _currentModifier = selectedModifier;

            _currentTimeInterval = 0;
        }

        _currentTimeInterval += Time.deltaTime;
    }
}