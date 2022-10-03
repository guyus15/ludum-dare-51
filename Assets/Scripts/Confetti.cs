using UnityEngine;

public class Confetti : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5.0f;
    [SerializeField] private float _explosionForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, _explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb2d = hit.GetComponent<Rigidbody2D>();

            if (rb2d != null)
            {
                Debug.Log("Exerting force");

                Vector2 force = ((Vector2)hit.gameObject.transform.position - explosionPos).normalized * _explosionForce;
                rb2d.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
