using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearOnGameStart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.gameObject.SetActive(!SceneManage.Instance.GameStarted);
    }
}
