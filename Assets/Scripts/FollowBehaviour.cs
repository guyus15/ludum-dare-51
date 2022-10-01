using System.Linq;
using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;

    [SerializeField] private float _lookAheadFactor = 3;
    [SerializeField] private float _damping;

    [SerializeField] protected Transform trackingTarget;

    private Vector2 _lastTargetPosition;
    private Vector2 _currentVelocity;
    private Vector2 _lookAheadPosition;

    private void Start()
    {
        _lastTargetPosition = trackingTarget.position;
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 moveDelta = mousePosition - _lastTargetPosition;

        bool updateLookAheadFactor = moveDelta.x > 0 || moveDelta.y > 0;

        if (updateLookAheadFactor)
        {
            float[] deltas;
            deltas = new float[] { moveDelta.x, moveDelta.y };

            _lookAheadPosition = _lookAheadFactor * mousePosition.normalized * Mathf.Sign(deltas.Max());
        }

        Vector2 aheadTargetPos = (Vector2)trackingTarget.position + _lookAheadPosition;
        aheadTargetPos = new Vector2(aheadTargetPos.x + _offsetX, aheadTargetPos.y + _offsetY);
        Vector2 newPos = Vector2.SmoothDamp(transform.position, aheadTargetPos, ref _currentVelocity, _damping);

        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        _lastTargetPosition = mousePosition;
    }
}