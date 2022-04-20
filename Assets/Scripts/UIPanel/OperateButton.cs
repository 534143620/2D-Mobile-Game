using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OperateButton : MonoBehaviour
{
    private Button Operate_Button;
    private PlayerController player;
    private void Awake()
    {
        Operate_Button = transform.GetComponent<Button>();
        player = FindObjectOfType<PlayerController>();
        if (transform.name == "Jump")
        {
            Operate_Button.onClick.AddListener(OnJumpButtonClick);
        }
        else
        {
            Operate_Button.onClick.AddListener(OnAttackButtonClick);
        }
    }

    private void OnAttackButtonClick()
    {
        player.GetComponent<PlayerController>().Attack();
    }

    private void OnJumpButtonClick()
    {
        player.GetComponent<PlayerController>().doJump();
    }

}
