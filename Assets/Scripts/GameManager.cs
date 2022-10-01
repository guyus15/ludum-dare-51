using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField, Header("Pause Menu")]
    public static bool IsPaused { get; set; }

    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);      
    }

    private void Update()
    {
        if (InputManager.GetPauseInputDown())
        {
            if (IsPaused) Resume(); else Pause();
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }
}
