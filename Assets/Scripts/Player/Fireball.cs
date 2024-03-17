using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Fireball : MonoBehaviour
{
    private int _fireballDamage = 1;
    private Vector2 _moveVector;
    private float _moveSpeed = 30f;

    public void Init(Vector2 moveVector, int damage)
    {
        if (moveVector == Vector2.left)
        {
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            transform1.localScale = localScale;
        }
        
        SetFireballDamage(damage);
        _moveVector = moveVector;
        StartCoroutine(DelayedDestroy());
    }
    
    private void Update()
    {
        transform.Translate(_moveVector * _moveSpeed * Time.deltaTime);
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void SetFireballDamage(int damage)
    {
        _fireballDamage = damage;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(1);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

internal interface IDamageable
{
    void TakeDamage(int damage);
}
