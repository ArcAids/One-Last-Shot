using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform winScreen;
    [SerializeField] PlayerController player;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void YouWin()
    {
        winScreen.gameObject.SetActive(true);
        player.DisableControls();
    }
}
