using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string loadLevel;

    public GameObject loadingScreen;

        

   
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadScene()
    {
        loadingScreen.SetActive(true);
        //SceneManager.LoadScene(loadLevel);

        StartCoroutine(LoadAsync());
        Time.timeScale = 1f;
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadLevel);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
