using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Vector2 lookDirection;
    [SerializeField] private AudioClip[] audio;
    [SerializeField] private float _fireRate;
    private Camera _camera;

    private float _timeSinceLastShot = 0.3f;

    SpriteRenderer sprite;

    void Start()
    {
        _camera = Camera.main;
        firePoint = transform;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastShot += Time.deltaTime;
        if (lookDirection.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == UnityEngine.TouchPhase.Ended && _timeSinceLastShot >= _fireRate && Application.isMobilePlatform)
                {
                    Shoot();
                    _timeSinceLastShot = 0f;
                }
            }

            if (Input.GetMouseButtonDown(0) && _timeSinceLastShot >= _fireRate && !Application.isMobilePlatform)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (!SceneManage.Instance.GameStarted) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = bullet.transform.up * bulletSpeed;

        AudioManager.PlayFromRandomClips(audio);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.control.ToString().Contains("Mouse/position"))
        {
            lookDirection = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>()) - transform.position; 
        }
        else
        {
            lookDirection = context.ReadValue<Vector2>();
        }
    }
}
