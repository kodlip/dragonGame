using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void Start()
    {
        if (_startButton != null)
        {
            _startButton.onClick.AddListener(StartGame);
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
