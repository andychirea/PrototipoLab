using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _movementSpeed = 1.8f;
    [SerializeField] private float _rotationSpeed = 0.1f;
    [SerializeField] private float _gravity = 10f;


    private CharacterController _characterController;

    private Vector3 _movementDirection;
    private float _xRotation;
    private float _yRotation;
    private float _timeInAir;

    private void Awake() => Initialize();

    private void Initialize()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        if (_characterController.isGrounded)
            _timeInAir = 0.0f;
        else
            _timeInAir += deltaTime;

        var gravityDirection = _gravity * _timeInAir * deltaTime * Vector3.down;
        var walkDirection = transform.rotation * _movementDirection * deltaTime;

        _characterController.Move(walkDirection + gravityDirection);
    }

    public void Move(Vector2 direction)
    {
        _movementDirection = new Vector3(direction.x, 0, direction.y) * _movementSpeed;
    }

    public void RotateX(float value)
    {
        _xRotation += value * _rotationSpeed;
        var current = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(current.x, _xRotation, current.z);
    }

    public void RotateY(float value)
    {
        _yRotation -= value * _rotationSpeed;
        _yRotation = Mathf.Clamp(_yRotation, -90f, 90f);
        var current = _cameraTransform.rotation.eulerAngles;
        _cameraTransform.rotation = Quaternion.Euler(_yRotation,current.y, current.z);
    }
}