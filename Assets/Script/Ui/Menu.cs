using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _helpPanel;
    [SerializeField] private Button _menu;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _help;
    [SerializeField] private Button _exit;

    private void Awake()
    {
        _continue.interactable = false;
    }

    private void OnEnable()
    {
        _menu.onClick.AddListener(OnButtonClickMenu);
        _continue.onClick.AddListener(OnButtonClickContinue);
        _help.onClick.AddListener(OnButtonClickHelp);
        _exit.onClick.AddListener(OnButtonClickExit);
        ButtonNewGame.NewGame += Close;
    }

    private void OnDisable()
    {
        _menu.onClick.RemoveListener(OnButtonClickMenu);
        _continue.onClick.RemoveListener(OnButtonClickContinue);
        _help.onClick.RemoveListener(OnButtonClickHelp);
        _exit.onClick.RemoveListener(OnButtonClickExit);
        ButtonNewGame.NewGame -= Close;
    }

    public void Open()
    {
        Time.timeScale = 0;
        _menuPanel.SetActive(true);
    }

    private void Close()
    {
        Time.timeScale = 1;
        _menuPanel.SetActive(false);
    }

    private void OnButtonClickMenu()
    {
        _continue.interactable = true;
        Open();
    }

    private void OnButtonClickContinue()
    {
        Close();
    }

    private void OnButtonClickHelp()
    {
        _helpPanel.SetActive(true);
    }

    private void OnButtonClickExit()
    {
        Application.Quit();
    }
}
