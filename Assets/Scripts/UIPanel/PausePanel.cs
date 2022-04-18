using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    private Button Resume_Button;
    private Button PlayAgain_Button;
    private Button Quit_Button;
    private void Awake()
    {
        Resume_Button = transform.Find("Resume").GetComponent<Button>();
        PlayAgain_Button = transform.Find("PlayAgain").GetComponent<Button>();
        Quit_Button = transform.Find("Quit").GetComponent<Button>();
        Resume_Button.onClick.AddListener(OnResumeButtonClick);
        PlayAgain_Button.onClick.AddListener(OnPlayAgainButtonClick);
        Quit_Button.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnResumeButtonClick()
    {
        Debug.Log("游戏继续");
        UIManager.instance.ResumeGame();
    }

    private void OnPlayAgainButtonClick()
    {
        Debug.Log("游戏重启");
        GameManager.instance.RestartScene();
    }

    private void OnQuitButtonClick()
    {
        Debug.Log("游戏退出");
        GameManager.instance.QuitGame();
    }

}