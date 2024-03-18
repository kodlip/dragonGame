using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : MonoBehaviour
{
    private void Start()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out ICanFly canFly))
        {
            canFly.Fly();
        }
    }
}
