using UnityEngine;

public class BodyTypeDetector : MonoBehaviour
{
    public bool ShouldChange { get; set; }

    private void Start()
    {
        ShouldChange = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ShouldChange = true;
        }
    }
}
