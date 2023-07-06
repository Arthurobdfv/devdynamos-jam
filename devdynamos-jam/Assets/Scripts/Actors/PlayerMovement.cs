using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int playerLife;
    [SerializeField] private int playerMaxLife = 3;

    [SerializeField] public bool PlayerDead => isDead;
    [SerializeField] private bool isDead = false;

    [SerializeField] private Image lifeBar;
    [SerializeField] private AudioClip _hitSound;


    [SerializeField] private float speed = 5;

    Rigidbody2D rb;
    Animator anim;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        playerLife = playerMaxLife;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!SceneManage.Instance.GameStarted) return;

        rb.velocity = movement * speed;

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", speed);

        if (movement.x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        playerLife = Mathf.Clamp(playerLife, 0, playerMaxLife);
        lifeBar.fillAmount = ((float)playerLife / playerMaxLife);

        if (playerLife == 0)
        {
            isDead = true;
        }
    }

    public void Move(InputAction.CallbackContext value)
    {
        movement = value.ReadValue<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" && isDead == false)
        {
            AudioManager.PlaySound(_hitSound);
            playerLife -= 1;
        }
    }

    public void HPup()
    {
        playerLife += 1;
    }

}
