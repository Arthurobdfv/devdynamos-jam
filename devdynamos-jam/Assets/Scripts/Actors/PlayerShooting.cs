using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private AudioClip[] audio;

    SpriteRenderer sprite;

    void Start()
    {
        firePoint = transform;
        sprite = GetComponent<SpriteRenderer>();
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

    private void Shoot()
    {
        if (!SceneManage.Instance.GameStarted) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = bullet.transform.up * bulletSpeed;

        AudioManager.PlayFromRandomClips(audio);
    }
}
