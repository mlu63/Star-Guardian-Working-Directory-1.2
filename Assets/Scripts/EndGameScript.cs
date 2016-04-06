// handles pause menu aspects

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGameScript : MonoBehaviour
{

    // variable declarations
    public Canvas endGameMenu;
    public Button mainMenuBtn;
    public Button playAgainBtn;
    public Text endLabel;
    public Text scoreLabel;
    private player gamer;

    // Use this for initialization
    void Start()
    {
        endGameMenu = endGameMenu.GetComponent<Canvas>();
        mainMenuBtn = mainMenuBtn.GetComponent<Button>();
        playAgainBtn = playAgainBtn.GetComponent<Button>();
        endGameMenu.enabled = false;
        gamer = (player)FindObjectOfType(typeof(player));
    }

    // return to main button
    public void ReturnToMainMenuButtonPressed()
    {
        Time.timeScale = 1.0f;
        endGameMenu.enabled = false;
        GlobalControl.Instance.gameOver = false;
        SceneManager.LoadScene("StartMenu");
    }

    // replay current
    public void ReplayButtonPressed()
    {
        GlobalControl.Instance.gameOver = false;
        Time.timeScale = 1.0f;
        endGameMenu.enabled = false;
        SceneManager.LoadScene("Play");
        gamer.init();
    }
}
