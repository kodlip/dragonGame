using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
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

    [SerializeField] private Fireball _fireballPrefab;
    [SerializeField] private GameObject _earthCubePrefab;
    [SerializeField] private float _dashForce;

    private int _userPoint;
    
    private GameObject _earthCube;
    
    private Animator _currentAnimator;
    private PlayerForms _playerForm;
    private Vector2 _moveVector;
    private bool _lookLeft;
    private bool _isJumping;
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private Camera _camera;

    private float _abilityCooldown = 0.5f;
    private float _timeRemaining;
    private Vector3 _playerStartPosition;
    private bool _onDash;
    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Start()
    {
        _userPoint = 0;
        _playerStartPosition = transform.position;
        SetPlayerForm(PlayerForms.Earth);
    }

    private void Update()
    {
        if (_timeRemaining >= 0)
        {
            _timeRemaining -= Time.deltaTime;
        }
        
        if (_camera != null)
        {
            var position = transform.position;
            var transform1 = _camera.transform;
            transform1.position = new Vector3(position.x, position.y, transform1.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _timeRemaining <= 0)
        {
            UseAbility();
        }
        
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

        if (transform.position.y < -50)
        {
            transform.position = _playerStartPosition;
        }
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
        if (_onDash)
        {
            return;
        }
        
        _moveVector.x = Input.GetAxis("Horizontal");
        _currentAnimator.SetFloat(MoveX, Mathf.Abs(_moveVector.x));
        playerRigidbody.velocity = new Vector2(_moveVector.x * _moveSpeed, playerRigidbody.velocity.y);
        
        if ((_moveVector.x < 0 && !_lookLeft) || (_moveVector.x > 0 && _lookLeft))
        {
            transform.localScale *= new Vector2(-1, 1);
            _lookLeft = !_lookLeft;
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

    private void UseAbility()
    {
        _timeRemaining = _abilityCooldown;
        
        switch (_playerForm)
        {
            case PlayerForms.Earth:
                CreateEarthCube();
                break;
            case PlayerForms.Air:
                CreateHurricane();
                break;
            case PlayerForms.Fire:
                CreateFireball();
                break;
        }
    }

    private void CreateFireball()
    {
        var fireball = Instantiate(_fireballPrefab, transform.position, Quaternion.identity);
        fireball.Init(_lookLeft ? Vector2.left : Vector2.right, 1);
        var vector = _lookLeft ? Vector2.right : Vector2.left;
        
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(vector * _dashForce);
        _onDash = true;
        WaitDash().Forget();
    }

    private async UniTaskVoid WaitDash()
    {
        await UniTask.Delay(500);
        _onDash = false;
    }
    
    // public void Walk(int Value, int Friction, float Speed)
    // {
    //     //PlayerRigidBody.velocity = new Vector2(Value * Speed * Friction, PlayerRigidBody.velocity.y); Deprecated
    //     if (PlayerRigidBody.velocity.x > 0)
    //         PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x - Time.deltaTime * 10, PlayerRigidBody.velocity.y);
    //     if (PlayerRigidBody.velocity.x < 0)
    //         PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x + Time.deltaTime * 10, PlayerRigidBody.velocity.y);
    //
    //
    //     if (Friction == 0) PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x / 2, PlayerRigidBody.velocity.y);
    //     if(Value > 0 && PlayerRigidBody.velocity.x < Speed)
    //         PlayerRigidBody.AddForce(new Vector2(Value, 0) * 10000 * Time.deltaTime);
    //     if(Value < 0 && PlayerRigidBody.velocity.x > -Speed) 
    //         PlayerRigidBody.AddForce(new Vector2(Value, 0) * 10000 * Time.deltaTime);
    // }
    
    private void CreateEarthCube()
    {
        if (_earthCube != null)
        {
            Destroy(_earthCube);
        }   
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -_camera.transform.position.z;
        
        var position = _camera.ScreenToWorldPoint(mousePos);
        _earthCube = Instantiate(_earthCubePrefab, position, Quaternion.identity);
    }

    private void CreateHurricane()
    {
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

    public void AddPoint(int i)
    {
        _userPoint += i;
    }
}