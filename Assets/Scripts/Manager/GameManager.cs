using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;

    public bool GameOver;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        player = FindObjectOfType<PlayerController>();
    }

    public void Update()
    {
        GameOver = player.isDead;
        UIManager.instance.GameOverUI(GameOver);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
