using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring_Move : MonoBehaviour
{
    private Rigidbody2D rb;//Rigidbody2Dを格納する変数
    private Vector3 defaultPos;//自分の座標を格納する変数

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
        //現在の座標を取得
        defaultPos = transform.position;
        
    }

    private void FixedUpdate()
    {
        //上下移動
        rb.MovePosition(new Vector3(defaultPos.x+ Mathf.PingPong(Time.time,2), defaultPos.y + Mathf.PingPong(Time.time, 2), defaultPos.z));
    }
}
