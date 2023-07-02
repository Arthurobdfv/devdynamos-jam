using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig;
    private Vector2 _direction;
    public float speed;

    public GameObject bulletPrefab;
    public float bulltetSpeed;

    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        OnInput();
        OnMove();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Vector3 shootDirection = targetPosition - transform.position;
            bullet.transform.rotation = Quaternion.LookRotation(shootDirection);

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>(); 
            if(bulletRigidbody != null )
            {
                bulletRigidbody.velocity = shootDirection.normalized * bulltetSpeed;
            }
        }
    }
}

//Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
//RaycastHit hit;

//if (Physics.Raycast(ray, out hit))
//{
//    Vector3 targetPosition = hit.point;
//    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

//    Vector3 shootDirection = targetPosition - transform.position;
//    bullet.transform.rotation = Quaternion.LookRotation(shootDirection);

//    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
//    if (bulletRigidbody != null)
//    {
//        bulletRigidbody.velocity = shootDirection.normalized * bulletSpeed;
//    }
//}