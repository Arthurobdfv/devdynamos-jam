using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] private int playerLife;
    [SerializeField] private int playerMaxLife = 3;

    public bool PlayerDead => isDead;


    [SerializeField] private bool isDead = false;

    [SerializeField] private Image lifeBar;
    public Rigidbody2D rig;
    private Vector2 _direction;
    public float speed;

    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLife = playerMaxLife;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 lookDirection = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
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
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rig.velocity = movement * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && isDead == false)
        {
            playerLife -= 1;
            lifeBar.fillAmount = ((float)playerLife / playerMaxLife);
        }
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = bullet.transform.up * bulletSpeed;
    }

}