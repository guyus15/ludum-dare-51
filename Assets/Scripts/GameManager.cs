using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [field: Header("Pause Menu")]
    public static bool IsPaused { get; set; }

    [SerializeField] private bool canPauseHere = false;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        EventManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
    }    

    private void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);      
    }

    private void Update()
    {
        if (InputManager.GetPauseInputDown() && canPauseHere)
        {
            if (IsPaused) Resume(); else Pause();
        }
    }

    private void Pause()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        Time.timeScale = 0;
        IsPaused = true;
    }

    public void Resume()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        Time.timeScale = 1;
        IsPaused = false;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);

        // Make sure scene isn't paused
        Resume();
    }

    public void UnloadScene(int sceneIndex)
    {
        SceneManager.UnloadSceneAsync(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnPlayerDeath(PlayerDeathEvent evt)
    {
        // Load the game over screen
        LoadScene(2);
    }
}
