using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start Game");
    }

    public void LoadGameSceneOnStart()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadGameScene()
    {
        
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadGameOverScene()
    {
        StartCoroutine(LoadSceneAfterTime());
          
    }
    IEnumerator LoadSceneAfterTime()
    {

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Game Over");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
