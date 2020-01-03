using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleat : MonoBehaviour
{

    [SerializeField]
    private Ring_Deleat Parent;//親オブジェクトを格納

    [SerializeField]
    private ParticleSystem[] Ring_Particle;//パーティクルを格納する

    private int Par_Count;//現在のパーティクルの数字

    private bool F_Deleat;//消去するフラグ

    private bool F_NB;//成功かどうかの判定

    private void Update()
    {
        //フラグを取得
        F_Deleat = DunkManager.Instance.F_Under;
        F_NB = DunkManager.Instance.F_Nice_Bad;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突したゲームオブジェクトタグがPlayerだった場合
        if (collision.gameObject.tag == "Player")
        {
            //消去するかどうか
            if(F_Deleat == true)
            {
                //パーティクルを生成
                Instantiate(Ring_Particle[0], this.transform.position, Quaternion.identity);
                //成功かどうか
                if (F_NB == true)
                {
                    //パーティクルを全て生成
                    for(Par_Count = 1; Par_Count <Ring_Particle.Length; Par_Count++)
                    {
                        //パーティクル生成
                        Instantiate(Ring_Particle[Par_Count], this.transform.position, Quaternion.identity);
                    }
                    
                }
                //親を消去
                Parent.Deleat_Ring();
            }

            
        }


    }
}
