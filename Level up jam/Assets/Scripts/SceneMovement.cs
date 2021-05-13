using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMovement : MonoBehaviour
{
    /* Contains all of the scene loading functions to move form scene to scene
     */ 
    public GameObject loadingScreen;
    public GameObject otherScreen;

    public void Exit()
    {
        Debug.Log("Exited");
        Application.Quit();
    }

    public void LoadLevel(string sceneName)
    {
        AkSoundEngine.PostEvent("Stop_Forest_Biom", gameObject);
        StartCoroutine(LoadAsyncly(sceneName));
    }

    IEnumerator LoadAsyncly(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        otherScreen.SetActive(false);
        Time.timeScale = 0f;
        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            //Debug.Log(operation.progress);
            GameObject.Find("Loading Slider").GetComponent<Slider>().value = progress;
            GameObject.Find("Progress Text").GetComponent<Text>().text = "Loading..." + progress * 100f + "%";
            yield return null;
        }
    }
}
