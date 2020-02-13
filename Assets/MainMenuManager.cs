using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    Fader fader;

    public void StartGame()
    {
        DontDestroyOnLoad(gameObject);
        fader = new Fader();
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int sceneNumber)
    {
        yield return StartCoroutine(fader.FadeOut(2));
        yield return SceneManager.LoadSceneAsync(sceneNumber);
        Debug.Log("SceneLoaded");
        yield return StartCoroutine(fader.FadeIn(2));
    }
}

class Fader
{
    public IEnumerator FadeOut(float time)
    {

        Debug.Log("fadingOut");
        float temp = 0;
        while (temp < 1)

        {

            temp += Time.deltaTime / time;

            yield return null;

        }
        //yield return new WaitForSeconds(1.0f);
        Debug.Log("fadedOut");
        //StartCoroutine(FadeIn(1));

    }

    public IEnumerator FadeIn(float time)
    {
        Debug.Log("fadingIn");
        float temp = 1;
        while (temp > 0)

        {

            temp -= Time.deltaTime / time;

            yield return null;

        }
        Debug.Log("fadedIn");
    }
}