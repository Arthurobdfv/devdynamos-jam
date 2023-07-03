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
        StartCoroutine(_cameraShake.Shake(.3f));
    }

}