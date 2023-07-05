using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private InputAction lookAction;
    private Vector2 lookInput;

    public float rotationSpeed = 5f;

    private void OnEnable()
    {
        //lookAction.Enable();
    }

    private void OnDisable()
    {
        //lookAction.Disable();
    }

    private void Awake()
    {
        //lookAction = new InputAction("LookAction", binding: "<Gamepad>/rightStick");
        //lookAction.performed += OnLook;
        //lookAction.canceled += OnLook;
    }

    private void Update()
    {
        Vector3 lookDirection = new Vector3(lookInput.x, 0f, lookInput.y);
        if (lookDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
