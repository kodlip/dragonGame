using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]

public class FlyItem : MonoBehaviour, ICanFly
{
    [SerializeField] private Rigidbody2D _itemRigidbody;
    
    public void Fly()
    {
        if (_itemRigidbody != null)
        {
            _itemRigidbody.AddForce(Vector2.up * (180000 * 2f));   
        }
    }
}
