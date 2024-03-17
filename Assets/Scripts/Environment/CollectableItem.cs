using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerController player))
        {
            player.AddPoint(1);
            DelayedDestroy().Forget();
        }
    }

    private async UniTaskVoid DelayedDestroy()
    {
        await UniTask.DelayFrame(1);
        Destroy(gameObject);
    }
}
