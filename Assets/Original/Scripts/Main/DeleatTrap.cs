using UnityEngine;

public class DeleatTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突したオブジェクトのタグがOutofscreenだった場合
        if (collision.gameObject.tag == "Outofscreen")
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
