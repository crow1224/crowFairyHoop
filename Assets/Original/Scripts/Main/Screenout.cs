using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenout : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //画面外にリングが画面外に行ってしまった場合
        if (collision.gameObject.tag == "Score")
        {
            //ゲームオーバー関数を呼び出す
            DunkManager.Instance.GameOver();

        }
    }
}
