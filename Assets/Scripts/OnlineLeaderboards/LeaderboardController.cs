using System.Collections;
using LootLocker.Requests;
using UI;
using UnityEngine;

namespace OnlineLeaderboards
{
    public class LeaderboardController : MonoBehaviour
    {
        private int _leaderboardID = 11355;
        private event PlayerInfoInstantiated _playerInfo;


        private void Start()
        {
            StartCoroutine(LoginRoutine());
        }

        public void SubscribeInstantiate(PlayerInfoInstantiated playerInfoInstantiated)
        {
            _playerInfo += playerInfoInstantiated;
        }
    
        IEnumerator LoginRoutine()
        {
            bool done = false;

            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    Debug.Log("player was logged in");
                    PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                    done = true;
                }
                else
                {
                    Debug.Log("cannot login");
                    done = true;
                }
            });
            yield return new WaitWhile(() => done == false);
            yield return FetchTopHighScoresRoutine();
        }

        public void SetPlayerName()
        {
            LootLockerSDKManager.SetPlayerName(PlayerPrefs.GetString("PlayerID"), (response) =>
                {
                    if (response.success)
                    {
                        Debug.Log("Successfully set player name");
                    }
                    else
                    {
                        Debug.Log("could not set player name" + response.Error);
                    }
                }
            );
            StartCoroutine(FetchTopHighScoresRoutine());
        }


        public IEnumerator SubmitScoreRoutine(int scoreToUpload)
        {
            bool done = false;
            string playerID = PlayerPrefs.GetString("PlayerID");
            LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, _leaderboardID, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Score Uploaded");
                    done = true;
                }
                else
                {
                    Debug.Log("Failed" + response.Error);
                    done = true;
                }
            });
            yield return new WaitWhile(() => done == false);
            yield return FetchTopHighScoresRoutine();
        }

        public IEnumerator FetchTopHighScoresRoutine()
        {
            bool done = false;
            LootLockerSDKManager.GetScoreListMain(_leaderboardID, 10, 0, (response) =>
            {
                if (response.success)
                {
                    string tempPlayerNames = "Names\n";
                    string tempPlayerScores = "Die Count\n";

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
                    _playerInfo?.Invoke(tempPlayerNames, tempPlayerScores);
                }
                else
                {
                    Debug.Log("failed" + response.Error);
                    done = true;
                }
            });
            yield return new WaitWhile(() => done == false);
        }


    }
}