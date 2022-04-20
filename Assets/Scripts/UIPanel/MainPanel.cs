using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    private Button NewGame_Button;
    private Button Continue_Button;
    private Button Quit_Button;

    private void Awake()
    {
        NewGame_Button = transform.Find("NEWGAME").GetComponent<Button>();
        Continue_Button = transform.Find("CONTINUE").GetComponent<Button>();
        Quit_Button = transform.Find("QUIT").GetComponent<Button>();
        NewGame_Button.onClick.AddListener(OnNewGameButtonClick);
        Continue_Button.onClick.AddListener(OnContinueButtonClick);
        Quit_Button.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnNewGameButtonClick()
    {
        Debug.Log("新游戏");
        GameManager.instance.NewGame();
    }

    private void OnContinueButtonClick()
    {
        Debug.Log("继续游戏");
        GameManager.instance.ContinueGame();
    }

    private void OnQuitButtonClick()
    {
        Debug.Log("游戏退出");
        GameManager.instance.QuitGame();
    }

}