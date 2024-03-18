using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : MonoBehaviour
{
    // private void Start()
    // {
    //     Invoke(nameof(SelfDestroy), 2f);
    // }

    // private void SelfDestroy()
    // {
    //     Destroy(gameObject);
    // }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out ICanFly canFly))
        {
            canFly.Fly();
        }
    }
}
