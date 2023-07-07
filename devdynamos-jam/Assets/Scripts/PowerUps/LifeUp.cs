using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    private PlayerMovement player;

    [SerializeField] private AudioClip audio;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            player.HPup();
            AudioManager.PlaySound(audio);
            Destroy(gameObject);
        }
    }

}
