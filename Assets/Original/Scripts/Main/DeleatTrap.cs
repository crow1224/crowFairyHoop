using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleatTrap : MonoBehaviour
{
    [SerializeField]
    private Ring_Deleat Parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突したオブジェクトのタグがOutofscreenだった場合
        if (collision.gameObject.tag == "Outofscreen")
        {

            Parent.Deleat_Ring();
        }


    }
}
