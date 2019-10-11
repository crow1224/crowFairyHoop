﻿using System.Collections;
using UnityEngine;

public class CameraShake : SingletonMonoBehaviour<CameraShake>
{

    [SerializeField]
    private float duration = 0.1f;

    [SerializeField]
    private float magnitude = 0.1f;

    public void Shake()
    {
        StartCoroutine(DoShake());
    }

    private IEnumerator DoShake()
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = pos;
    }
}
