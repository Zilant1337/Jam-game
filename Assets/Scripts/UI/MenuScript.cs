using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static MenuScript Instance;
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    GameObject pauseMenuObject;
    [SerializeField]
    GameObject gameOverMenuObject;
    [SerializeField]
    GameObject victoryMenuObject;
    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    public void StartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "MainMenu")
        {
            SceneManager.LoadSceneAsync("Level1");
        }
        else
        {
            Debug.LogError("Can't start the game from outside of main menu");
        }
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void GameOver()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "MainMenu")
        {
            Time.timeScale = 0;
            playerInput.SwitchCurrentActionMap("UI");
            CursorManager.instance.SetMenuCursor();
            gameOverMenuObject.SetActive(true);
        }
    }
    public void Victory()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "MainMenu")
        {
            Time.timeScale = 0;
            playerInput.SwitchCurrentActionMap("UI");
            CursorManager.instance.SetMenuCursor();
            victoryMenuObject.SetActive(true);
        }
    }
    public void Pause()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "MainMenu")
        {
            Time.timeScale = 0;
            playerInput.SwitchCurrentActionMap("UI");
            CursorManager.instance.SetMenuCursor();
            pauseMenuObject.SetActive(true);
        }
    }
    public void Unpause()
    {
        pauseMenuObject.SetActive(false);
        CursorManager.instance.SetGameCursor();
        playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Can't have more than one MenuScript");
        }
        Time.timeScale = 1;
    }
}
