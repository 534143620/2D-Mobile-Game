using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    private Button Resume_Button;
    private void Awake()
    {
        Resume_Button = transform.Find("Resume").GetComponent<Button>();
        Resume_Button.onClick.AddListener(OnResumeButtonClick);
    }

    private void OnResumeButtonClick()
    {
        Debug.Log("游戏继续");
        UIManager.instance.ResumeGame();
    }
}