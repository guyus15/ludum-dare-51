using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb2d;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * _moveSpeed, Input.GetAxis("Vertical") * _moveSpeed);

        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePosition.x -= playerScreenPos.x;
        mousePosition.y -= playerScreenPos.y;

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
     }
}
