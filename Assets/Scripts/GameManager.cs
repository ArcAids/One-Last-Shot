using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform winScreen;
    [SerializeField] PlayerController player;
    [SerializeField] Transform pauseMenu;

    public static bool gameOn;
    public void YouWin()
    {
        gameOn = false;
        winScreen.gameObject.SetActive(true);
        player.DisableControls();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameOn = false;
        pauseMenu.gameObject.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        gameOn = true;
        pauseMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameOn)
                UnPause();
            else
                Pause();
        }
    }
}
