using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }
    private CharacterController _controller;
    private GameObject _player;
    [SerializeField]
    private float _enemySpeed = 8.0f;
    private Vector3 _velocity;
    [SerializeField]
    private float _gravity = 20.0f;
    [SerializeField]
    private float _attackDelay = 2;
    private float _nextAttack = -1;
    [SerializeField]
    private EnemyState _currentState = EnemyState.Chase;
    private UniversalHealth _playerHealth;
    [SerializeField]
    GameObject _AttackCollider;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("No CharacterController");
        }
        _player = GameObject.Find("Player").gameObject;
        if (_player == null)
        {
            Debug.LogError("No Player");
        }
        _playerHealth = _player.transform.GetComponent<UniversalHealth>();
        if (_playerHealth == null)
        {
            Debug.LogError("No Player Health");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(_currentState)
        {
            case EnemyState.Attack:
            if (Time.time > _nextAttack)
                {
                    if (_playerHealth != null)
                    {
                        _playerHealth.Damage(1);
                        _nextAttack = Time.time + _attackDelay;
                    }

                }
                break;
            case EnemyState.Chase:
                CalculateMovement();
                break;

        }    
            
    }

    private void CalculateMovement()
    {
        _velocity.y -= _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        if (_controller.isGrounded)
        {
            Vector3 direction = _player.transform.position - transform.position;
            direction = direction.normalized;
            direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _enemySpeed;
        }
    }
    public void AttackTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _currentState = EnemyState.Attack;
        }
    }

    public void AttackTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _currentState = EnemyState.Chase;
        }
    }

}
