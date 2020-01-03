using UnityEngine;

public class Deleat : MonoBehaviour
{
    // パーティクルを格納する
    [SerializeField] private ParticleSystem[] Ring_Particle = new ParticleSystem[4];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したゲームオブジェクトタグがPlayerだった場合
        // 消去するかどうか
        if (collision.gameObject.tag == "Player" &&
            DunkManager.Instance.F_Under &&
            DunkManager.Instance.F_Nice_Bad)
        {
            // パーティクルを全て生成
            for (int i = 0; i < Ring_Particle.Length; i++)
            {
                // パーティクル生成
                Instantiate(Ring_Particle[i], this.transform.position, Quaternion.identity);
            }
            // 親を消去
            Destroy(this.transform.parent.gameObject);
        }
    }
}
