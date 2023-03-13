using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class NavigationInput : MonoBehaviour, Controls.INavigationActions
{
    [SerializeField] private UnityEvent<Vector2> _onMovement;
    [SerializeField] private UnityEvent<float> _onRotationX;
    [SerializeField] private UnityEvent<float> _onRotationY;

    private Controls _controls;

    private void Awake()
    {
        _controls = new Controls();
        _controls.Navigation.SetCallbacks(this);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _controls.Navigation.Enable();
    }

    private void OnDisable()
    {
        _controls.Navigation.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        var vector = context.ReadValue<Vector2>();
        _onMovement.Invoke(vector);
    }

    public void OnXRotation(InputAction.CallbackContext context)
    {
        var x = context.ReadValue<float>();
        _onRotationX.Invoke(x);
    }

    public void OnYRotation(InputAction.CallbackContext context)
    {
        var y = context.ReadValue<float>();
        _onRotationY.Invoke(y);
    }
}