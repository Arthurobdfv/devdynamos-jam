using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject player;
    private InputAction miraAction;
    private Vector3 lookDirection;

    void Start()
    {
        mainCamera = Camera.main;
        player = transform.parent.gameObject;
        miraAction = new InputAction(binding: "<Gamepad>/rightStick");
        miraAction.performed += context => SetValorMira(context, context.ReadValue<Quaternion>());
        miraAction.Enable();
    }

    void Update()
    {
        Vector3 mousePosition = mainCamera.WorldToScreenPoint(player.transform.position) + lookDirection;
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = worldMousePosition - player.transform.position;
        lookDirection.z = 0f;

        if (lookDirection.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void SetValorMira(InputAction.CallbackContext context, Quaternion mira)
    {
        lookDirection = mira * Vector3.up;
    }

    void Shoot()
    {
        // Lógica de disparo da arma
    }
}
