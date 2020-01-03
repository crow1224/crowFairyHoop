using UnityEngine;

public class RingDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Score")
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
