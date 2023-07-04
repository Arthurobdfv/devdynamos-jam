using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] private int playerLife;
    [SerializeField] private int playerMaxLife = 3;

    [SerializeField] public bool PlayerDead => isDead;

    [SerializeField] private Animator anim;

    [SerializeField] private AudioClip[] audio;


    [SerializeField] private bool isDead = false;

    [SerializeField] private Image lifeBar;
    [SerializeField] private AudioClip _hitSound;
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
        anim = GetComponentInChildren<Animator>();
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

        playerLife = Mathf.Clamp(playerLife,  0,  playerMaxLife);
        lifeBar.fillAmount = ((float)playerLife / playerMaxLife);

        if(playerLife == 0)
        {
            isDead = true;
        }
    }
    private void FixedUpdate()
    {
        if (!SceneManage.Instance.GameStarted) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rig.velocity = movement * speed;

        anim.SetFloat("Horizontal", moveHorizontal);
        anim.SetFloat("Vertical", moveVertical);
        anim.SetFloat("Speed", speed);

        if (moveHorizontal < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (moveHorizontal > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!SceneManage.Instance.GameStarted) return;

        if (collision.gameObject.tag == "EnemyBullet" && isDead == false)
        {
            AudioManager.PlaySound(_hitSound);
            playerLife -= 1;
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

    public void HPup()
    {
        playerLife += 1;

    }

}