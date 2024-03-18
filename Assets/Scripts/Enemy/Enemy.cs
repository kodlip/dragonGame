using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Fireball _fireballPrefab;
    [SerializeField] private int _health = 1;
    [SerializeField] private float _reloadTime = 2f;
    
    private PlayerController _player;
    private float _remainingTime;
    private void Start()
    {
        _remainingTime = _reloadTime;
        _player = FindObjectOfType<PlayerController>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
        }
        else
        {
            _remainingTime = _reloadTime;
            CreateFireball();
        }
    }

    private void CreateFireball()
    {
        var position = transform.position;
        var fireball = Instantiate(_fireballPrefab, position, Quaternion.identity);
        var lookLeft = position.x > _player.transform.position.x;
        
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), fireball.GetComponent<Collider2D>());
        
        fireball.Init(lookLeft ? Vector2.left : Vector2.right, 1);
        var vector = lookLeft ? Vector2.right : Vector2.left;
        
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(vector * 100000);
    }
}
