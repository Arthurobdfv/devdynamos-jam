using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

<<<<<<<< HEAD:devdynamos-jam/Assets/Scripts/Player/PlayerProjetil.cs
public class PlayerProjetil : MonoBehaviour
========
public class ShootEnemy : MonoBehaviour
>>>>>>>> develop:devdynamos-jam/Assets/Scripts/Enemy/ShootEnemy.cs
{
    public GameObject projetil;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bala = Instantiate(projetil);
        }

    }
}
