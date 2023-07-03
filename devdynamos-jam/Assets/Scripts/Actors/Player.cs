using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] private int playerLife;
    [SerializeField] private int playerMaxLife = 3;

    public bool PlayerDead => isDead;

    [SerializeField] private AudioClip[] audio;


    [SerializeField] private bool isDead = false;

    [SerializeField] private Image lifeBar;
    public Rigidbody2D rig;
    private Vector2 _direction;
    public float speed;

    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public CameraShake _cameraShake;

    public List<MonoBehaviour> _scripsParaAtivarDepoisDoGameStart = new List<MonoBehaviour>();
    private bool alreadyActivated = false;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        SetupScripts();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLife = playerMaxLife;
        _cameraShake = FindFirstObjectByType<CameraShake>();
        StartCoroutine(StartShakeRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneManage.Instance.GameStarted) return;
        else if (!alreadyActivated)
        {
            alreadyActivated = true;
            ActivateScripts();
        }
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
        if (!SceneManage.Instance.GameStarted) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rig.velocity = movement * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!SceneManage.Instance.GameStarted) return;

        if (collision.gameObject.tag == "EnemyBullet" && isDead == false)
        {
            playerLife -= 1;
            lifeBar.fillAmount = ((float)playerLife / playerMaxLife);
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

    private void SetupScripts()
    {
        _scripsParaAtivarDepoisDoGameStart.Clear();
        _scripsParaAtivarDepoisDoGameStart.Add(GetComponent<SpawnEnemy>());
        _scripsParaAtivarDepoisDoGameStart.Add(GetComponent<Oxygen>());
    }

    private void ActivateScripts()
    {
        foreach(var scr in _scripsParaAtivarDepoisDoGameStart)
        {
            scr.enabled = true;
        }
    }

    private IEnumerator StartShakeRoutine()
    {
        yield return new WaitForSeconds(SceneManage.Instance.InitialAnimationDelay);
        StartCoroutine(_cameraShake.Shake(2));
        yield return new WaitForSeconds(2);
    }

}