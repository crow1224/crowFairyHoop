using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;//プレイヤーオブジェクト

    [SerializeField]
    private float ShiftPoint = 5;//どれぐらいカメラを動かすか

    private float Camerapoint;//カメラのx座標目標位置

    // Update is called once per frame
    void Update()
    {
        //移動するカメラの目標位置を設定
        Camerapoint = player.transform.position.x+ShiftPoint;

        //カメラを移動する
        transform.position = new Vector3(Camerapoint, 0, -10);
    }
}
