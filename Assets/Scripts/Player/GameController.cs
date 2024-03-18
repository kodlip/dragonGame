using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController>
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayerController.OnPlayerPointChanged += PlayerControllerOnOnPlayerPointChanged;
    }

    private void PlayerControllerOnOnPlayerPointChanged(int obj)
    {
        _scoreText.text = obj.ToString();
        UserData.Instance.HighScore = obj;
    }

    private void Start()
    {
        _scoreText.text = UserData.Instance.HighScore.ToString();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name.StartsWith("level"))
        {
            _scoreText.gameObject.SetActive(false); 
        }
        else
        {
            _scoreText.gameObject.SetActive(true);
        }
    }

    public void LoadCurrentLevelScene()
    {
        SceneManager.LoadScene("level" + UserData.Instance.CurrentLevel);
    }
    
    public void LoadNextLevelScene()
    {
        UserData.Instance.CurrentLevel++;
        SceneManager.LoadScene("level" + UserData.Instance.CurrentLevel);
    }
}
