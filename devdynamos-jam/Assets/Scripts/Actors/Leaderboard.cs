using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    int leaderboardID = 15897;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [System.Obsolete]
    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PalyerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Sucessfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    [System.Obsolete]
    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderboardID, 10, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Name\n";
                string tempPlayerScores = "Score\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScore.text = tempPlayerScores;

            }
            else
            {
                Debug.Log("Faild" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }


}
