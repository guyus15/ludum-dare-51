using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    [SerializeField] private float moveSpeed;

    private Rigidbody2D _rb2d;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();      
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);
    }
}
