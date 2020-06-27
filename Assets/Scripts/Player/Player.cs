using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, InputActions.IPlayerActions
{
    private CharacterController _controller;
    private InputActions _playerControls;
    [Header("Controller Settings")]
    [SerializeField]
    private float _playerSpeed = 6.0f;
    [SerializeField]
    private float _jumpHeight = 8.0f;
    [SerializeField]
    private float _gravity = 20.0f;
    private Vector3 _direction;
    private float _jump;
    private Vector3 _velocity;
    private bool _canJump;
    private float _horizontal;
    private float _vertical;
    private Camera _camera;
    [Header("Camera Settings")]
    [SerializeField]
    private float _mouseSensativity = 0.25f;

    private void Awake()
    {
        _playerControls = new InputActions();
        _playerControls.Player.SetCallbacks(this);
    }
    private void OnEnable()
    {
        _playerControls.Player.Enable();

    }
    private void OnDisable()
    {
        _playerControls.Player.Disable();

    }


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("No CharacterController");
        }
        _camera = Camera.main;
        if (_camera == null)
        {
            Debug.LogError("No Main Camera");
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _velocity.y -= _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        if (_controller.isGrounded)
        {
            _direction = new Vector3(_horizontal, 0, _vertical);
            _velocity = _direction * _playerSpeed;
            _velocity = transform.TransformDirection(_velocity);
            _canJump = true;
        }
        PlayerLook();

    }

    private void PlayerLook()
    {
        Vector2 lookInput = _playerControls.Player.Look.ReadValue<Vector2>();
        lookInput *= _mouseSensativity;

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += lookInput.x;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        Vector3 currentCameraRotation = _camera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= lookInput.y;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0, 27);
        _camera.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

    private void LateUpdate()
    {

    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (_canJump)
        {
            _jump = context.ReadValue<float>();
            _velocity.y = _jump * _jumpHeight;
            _canJump = false;
        }

    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        
        _horizontal = context.ReadValue<Vector2>().x;
        _vertical = context.ReadValue<Vector2>().y;

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            this.transform.GetComponent<PlayerShoot>().Shoot();
        }
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnUnlockCursor(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
