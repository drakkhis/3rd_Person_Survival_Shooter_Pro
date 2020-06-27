using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController _controller;
    private GameObject _player;
    [SerializeField]
    private float _enemySpeed = 8.0f;
    private Vector3 _velocity;
    [SerializeField]
    private float _gravity = 20.0f;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _velocity.y -= _gravity * Time.deltaTime;

        

        _controller.Move(_velocity * Time.deltaTime);
        if (_controller.isGrounded)
        {
            Vector3 direction = new Vector3();
            direction = _player.transform.position - transform.position;
            direction = direction.normalized;
            direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _enemySpeed;
        }
        
    }
}
