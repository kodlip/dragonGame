using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerForms
{
    Earth,
    Air,
    Fire,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private GameObject playerFireForm;
    [SerializeField] private GameObject playerEarthForm;
    [SerializeField] private GameObject playerAirForm;

    [SerializeField] private Animator _fireAnimator;
    [SerializeField] private Animator _earthAnimator;
    [SerializeField] private Animator _airAnimator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private Animator _currentAnimator;
    private PlayerForms _playerForm;
    private Vector2 _moveVector;
    private bool _lookRight;
    private bool _isJumping;
    private static readonly int MoveX = Animator.StringToHash("moveX");

    private void Start()
    {
        SetPlayerForm(PlayerForms.Earth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetPlayerForm(PlayerForms.Earth);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetPlayerForm(PlayerForms.Air);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetPlayerForm(PlayerForms.Fire);
        }

        Walk();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.TryGetComponent(out Ground ground))
        {
            _isJumping = false;
        }
    }

    private void Walk()
    {
        _moveVector.x = Input.GetAxis("Horizontal");
        _currentAnimator.SetFloat(MoveX, Mathf.Abs(_moveVector.x));
        playerRigidbody.velocity = new Vector2(_moveVector.x * _moveSpeed, playerRigidbody.velocity.y);

        if ((_moveVector.x < 0 && !_lookRight) || (_moveVector.x > 0 && _lookRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            _lookRight = !_lookRight;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            playerRigidbody.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void SetPlayerForm(PlayerForms form)
    {
        switch (form)
        {
            case PlayerForms.Earth:
                playerEarthForm.SetActive(true);
                playerAirForm.SetActive(false);
                playerFireForm.SetActive(false);
                _playerForm = PlayerForms.Earth;
                _currentAnimator = _earthAnimator;
                break;

            case PlayerForms.Air:
                playerEarthForm.SetActive(false);
                playerAirForm.SetActive(true);
                playerFireForm.SetActive(false);
                _playerForm = PlayerForms.Air;
                _currentAnimator = _airAnimator;
                break;

            case PlayerForms.Fire:
                playerEarthForm.SetActive(false);
                playerAirForm.SetActive(false);
                playerFireForm.SetActive(true);
                _playerForm = PlayerForms.Fire;
                _currentAnimator = _fireAnimator;
                break;
        }
    }
}