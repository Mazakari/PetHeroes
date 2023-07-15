using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformInput : MonoBehaviour
{
    public float MoveInputX { get; private set; }

    private IInputService _inputServie;

    private void OnEnable()
    {
        _inputServie = AllServices.Container.Single<IInputService>();

        _inputServie.InputActions.Platform.Move.performed += SetMove;
        _inputServie.InputActions.Platform.Move.canceled += SetMove;
    }

    
    private void OnDisable()
    {
        _inputServie.InputActions.Platform.Move.performed -= SetMove;
        _inputServie.InputActions.Platform.Move.canceled -= SetMove;
    }

    private void SetMove(InputAction.CallbackContext context) => 
        MoveInputX = context.ReadValue<Vector2>().x;
}
