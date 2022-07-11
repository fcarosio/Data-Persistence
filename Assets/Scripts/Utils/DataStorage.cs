using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public class Score
    {
        public string playerName;
        public int score;

        public Score(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }

    public static DataStorage Instance = null;

    public Score CurrentPlayer { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreateNewPlayer(string playerName)
    {
        Debug.Log("New player " + playerName + " created");
        CurrentPlayer = new Score(playerName, 0);
    }

    public void RegisterCurrentScore(int score)
    {
        CurrentPlayer.score = score;
        Debug.Log("Registered score for " + CurrentPlayer.playerName + ": " + CurrentPlayer.score);
    }
}
