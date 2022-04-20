using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerController player;
    private Door doorExit;
    public List<Enemy> enemies = new List<Enemy>();

    public bool GameOver;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void Update()
    {
        if (player != null)
            GameOver = player.isDead;
        UIManager.instance.GameOverUI(GameOver);
    }

    public void IsPlayer(PlayerController controller)
    {
        player = controller;
    }

    public void IsExitDoor(Door door)
    {
        doorExit = door;
    }

    public void IsEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void EnemyDead(Enemy enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count == 0)
        {
            doorExit.OpenDoor();
            SaveData();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteKey("playerHealth");
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("sceneIndex"))
            SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
        else
            NewGame();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public float LoadHealth()
    {
        if (!PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerPrefs.SetFloat("playerHealth", 3f);
        }

        float currentHealth = PlayerPrefs.GetFloat("playerHealth");
        
        return currentHealth;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("playerHealth", player.health);
        PlayerPrefs.SetInt("sceneIndex", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.Save();
    }
}
