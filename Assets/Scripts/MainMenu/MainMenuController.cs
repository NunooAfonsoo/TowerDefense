using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button exit;
    [SerializeField] private Slider slider;

    [SerializeField] private IntEventSO gameStartedEvent;

    private void Awake()
    {
        play.onClick.AddListener(Play);
        exit.onClick.AddListener(ExitGame);
    }

    private void Play()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        Time.timeScale = 1f;
        SceneManager.LoadScene(Constants.MAIN_SCENE_NAME);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        gameStartedEvent.RaiseEvent((int)slider.value);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
