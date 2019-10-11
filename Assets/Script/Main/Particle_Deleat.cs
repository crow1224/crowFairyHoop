using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Deleat : MonoBehaviour
{
    [SerializeField]
    private float timer = 1.1f;//消去までの時間
    // Start is called before the first frame update
    void Start()
    {
        //自分を消去
        Destroy(gameObject, timer);
    }


}
