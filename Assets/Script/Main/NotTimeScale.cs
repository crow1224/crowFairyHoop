using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotTimeScale : MonoBehaviour
{
    [SerializeField]
    Animator animator;//アニメータを格納

    // Update is called once per frame
    void Update()
    {
        // タイムスケールを無視する
        
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

    }
}
