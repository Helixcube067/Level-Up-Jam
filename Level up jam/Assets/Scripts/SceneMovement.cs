using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMovement : MonoBehaviour
{
    /* Contains all of the scene loading functions to move form scene to scene
     */ 
    public GameObject loadingScreen;
    public Slider slider;
    public GameObject otherScreen;
    public Text progressText;

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
            slider.value = progress;
            progressText.text = "Loading..." + progress * 100f + "%";
            yield return null;
        }
    }
}
