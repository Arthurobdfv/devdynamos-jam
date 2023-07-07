using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public Leaderboard leaderboard;
    public TMP_InputField playerNameInputfild;
    public static PlayerManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        if(playerNameInputfild.text.Length >= 0)
        {
            LootLockerSDKManager.SetPlayerName(playerNameInputfild.text, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Succesfully set player name");
                }
                else
                {
                    Debug.Log("Could not set player name" + response.Error);
                }
            });
        }

    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Seção não iniciada");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
