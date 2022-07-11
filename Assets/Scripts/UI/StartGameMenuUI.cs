using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class StartGameMenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField playerNameText;
    [SerializeField] Button startGameButton;
    [SerializeField] Button quitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        ShowBestScore();
        startGameButton.onClick.AddListener(StartGame);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowBestScore()
    {
        DataStorage dataStorage = DataStorage.Instance;
        DataStorage.Score bestPlayer = dataStorage.BestPlayer;
        if (bestPlayer != null)
        {
            bestScoreText.text = "Best Score : " + bestPlayer.playerName + " : " + bestPlayer.score;
        }
        else
        {
            bestScoreText.text = "No best score recorded yet";
        }
    }

    void StartGame()
    {
        DataStorage dataStorage = DataStorage.Instance;
        dataStorage.CreateNewPlayer(playerNameText.text);

        SceneManager.LoadScene(1);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
