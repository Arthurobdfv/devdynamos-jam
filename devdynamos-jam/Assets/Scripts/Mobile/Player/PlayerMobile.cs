using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMobile : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    Rigidbody2D rb;
    Animator anim;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
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
    }

    public void Move(InputAction.CallbackContext value)
    {
        movement = value.ReadValue<Vector2>();
    }

}
