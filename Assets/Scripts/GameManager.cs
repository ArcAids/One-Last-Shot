using ArcAid.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] PlayerController player;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameHUD;
    [SerializeField] GameObject journal;

    public static bool gameOn=true;
    bool gameOver=false;
    public void YouWin()
    {
        gameOver = true;
        winScreen.SetActive(true);
        player.DisableControls();
    }

    public void YouLose()
    {
        gameOver = true;
        loseScreen.SetActive(true);
        player.DisableControls();
    }

    public void Pause()
    {
        if (gameOver)
            return;
        gameOn = false;
        pauseMenu.SetActive(true,true);
        gameHUD.SetActive(false,true);
        Time.timeScale = 0;
        player.DisableControls();
    }

    public void UnPause()
    {

        if (journal.activeSelf)
        {
            journal.SetActive(false);
            return;
        }
        Time.timeScale = 1;
        gameOn = true;
        pauseMenu.SetActive(false,true);
        gameHUD.SetActive(true,true);
        player.EnableControls();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (!gameOn)
                UnPause();
            else
                Pause();
        }
    }
}
