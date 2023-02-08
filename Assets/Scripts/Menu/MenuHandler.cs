using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonSettings;
    [SerializeField] private Button _buttonExit;
    [SerializeField] private Button _buttonCancel;
    [SerializeField] private Button _buttonSave;

    [SerializeField] private GameObject _mainMenuScreen;
    [SerializeField] private GameObject _settingsScreen;

    [SerializeField] private Match _match;

    [SerializeField] private float _matchToggleSeconds = 1f;

    private FadeInOut _fadeInOutController;

    void Start()
    {
        _buttonStart.onClick.AddListener(GameStart);
        _buttonExit.onClick.AddListener(ExitGame);
        _buttonSettings.onClick.AddListener(OpenSettingsScreen);
        _buttonCancel.onClick.AddListener(OpenMainMenuScreen);
        _buttonSave.onClick.AddListener(SaveSettings);
    }
    //---------------------

    public void GameStart()
    {
        StartCoroutine(GameStartCoroutine());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //Временно
    public void OpenSettingsScreen()
    {
        _mainMenuScreen.SetActive(false);
        _settingsScreen.SetActive(true);
        _match.Burn(false, _matchToggleSeconds);
    }

    //Временно
    public void OpenMainMenuScreen()
    {
        _settingsScreen.SetActive(false);
        _mainMenuScreen.SetActive(true);
        _match.Burn(true, _matchToggleSeconds);
    }

    //Временно
    public void SaveSettings()
    {
        Debug.Log("Вы сохранили настройки");
    }

    private IEnumerator GameStartCoroutine()
    {
        _mainMenuScreen.SetActive(false);
        _match.Burn(false, _matchToggleSeconds);
        yield return new WaitForSeconds(_matchToggleSeconds);
        SceneManager.LoadScene(1);
    }
}
