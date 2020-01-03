using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 100;//プレイヤースピード

    private float StartSpeed = 100;//プレイヤーの初期値

    [SerializeField]
    private float AddSpeed = 0.05f;//プラスするスピード

    [SerializeField]
    private float jumpPower = 300;//ジャンプ力

    private bool isJump = false;//ジャンプ可能かどうか

    private Rigidbody2D rbody;//リキッドボディ


    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのリキッドボディを取得
        rbody = GetComponent<Rigidbody2D>();
        //ポーズを開始
        DunkManager.Instance.Start_Pose();
 
    }

    void Update()
    {
        //プレイ続行可能な場合
        if(DunkManager.Instance.F_Play == true)
        {


                //左マウスクリックした場合
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                   //フラグが許可されている場合
                   if(DunkManager.Instance.F_Pose == true)
                    {
                    　　//ポーズを解除
                        DunkManager.Instance.RePose();
                        
                        
                    }
                    //ジャンプを許可する
                    isJump = true;
                }
            

        }



    }

    private void FixedUpdate()
    {
        //ジャンプ可能な場合
        if (isJump)
        {
            //速度をクリアして2回目以降のジャンプも一回目と同じ共同にする
            rbody.velocity = Vector2.zero;

            //ジャンプ
            rbody.AddForce(Vector2.up * jumpPower);
            rbody.AddForce(Vector2.right * MoveSpeed);

            //ジャンプを許可する
            isJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //衝突したオブジェクトのタグがGameOverだった場合
        if (collision.gameObject.tag == "GameOver")
        {
            //ゲームオーバー関数を呼び出す
            DunkManager.Instance.GameOver();
        }

        //衝突したオブジェクトのタグがBadだった場合
        if(collision.gameObject.tag == "Bad")
        {
            //Bad関数を呼び出す
            DunkManager.Instance.Bad();
            

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突したオブジェクトのタグがScoreの場合
        if (collision.gameObject.tag == "Clear")
        {
            
            //Flag_Under関数を呼び出す
            DunkManager.Instance.Flag_Under();

        }


        //衝突したオブジェクトのタグがScoreの場合
        if (collision.gameObject.tag == "Score")
        {
            //Point関数を呼び出す
            DunkManager.Instance.Point();
            MoveSpeed =  DunkManager.Instance.Score * AddSpeed + StartSpeed;

        }
    }

}
