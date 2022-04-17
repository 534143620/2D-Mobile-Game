using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    private Button TryAgain_Button;
    private void Awake()
    {
        TryAgain_Button = transform.Find("TryAgain").GetComponent<Button>();
        TryAgain_Button.onClick.AddListener(OnTryAgainButtonClick);
    }

    private void OnTryAgainButtonClick()
    {
        GameManager.instance.RestartScene();
    }
}