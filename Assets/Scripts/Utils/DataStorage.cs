using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataStorage : MonoBehaviour
{
    [System.Serializable]
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
    public Score BestPlayer { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestPlayer();
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

        if (BestPlayer == null)
        {
            BestPlayer = new Score(CurrentPlayer.playerName, CurrentPlayer.score);
            SavePlayer(BestPlayer);
        }
        else
        {
            if (CurrentPlayer.score > BestPlayer.score)
            {
                BestPlayer.playerName = CurrentPlayer.playerName;
                BestPlayer.score = CurrentPlayer.score;
                SavePlayer(BestPlayer);
            }
        }
    }

    void SavePlayer(Score player)
    {
        string path = Application.persistentDataPath + "/best_score.json";
        Debug.Log("Saving file " + path);

        string json = JsonUtility.ToJson(player);

        File.WriteAllText(path, json);
    }

    void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "/best_score.json";
        Debug.Log("Loading file " + path);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestPlayer = JsonUtility.FromJson<Score>(json);

            Debug.Log("[LOADED] " + BestPlayer.playerName + ": " + BestPlayer.score);
        }
    }
}
