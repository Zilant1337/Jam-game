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
    // Перезапуск сцены
    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    // Запуск игры из главного меню
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
    // Переключение на указанную сцену
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    // Открытие меню, предлагающего перезапустить игру при смерти игрока
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
    // Включение меню, завершающего уровень
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
    // Постановка игры на паузу
    public void Pause()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "MainMenu")
        {
            // Время не движется во время паузы
            Time.timeScale = 0;
            playerInput.SwitchCurrentActionMap("UI");
            CursorManager.instance.SetMenuCursor();
            pauseMenuObject.SetActive(true);
        }
    }
    // Снятие с паузы
    public void Unpause()
    {
        pauseMenuObject.SetActive(false);
        CursorManager.instance.SetGameCursor();
        playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;

    }
    // Выход из игры
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
