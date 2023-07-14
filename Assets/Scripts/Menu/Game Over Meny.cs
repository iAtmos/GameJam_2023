using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameOverMeny : MonoBehaviour
{
    public void OnDeath()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting game");
        Application.Quit();
    }
}
