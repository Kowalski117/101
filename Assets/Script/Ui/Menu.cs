using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _helpPanel;
    [SerializeField] private Button _buttonMenu;
    [SerializeField] private Button _buttonContinue;
    [SerializeField] private Button _buttonHelp;
    [SerializeField] private Button _buttonExit;

    private void Awake()
    {
        _buttonContinue.interactable = false;
    }

    private void OnEnable()
    {
        _buttonMenu.onClick.AddListener(OnButtonClickMenu);
        _buttonContinue.onClick.AddListener(OnButtonClickContinue);
        _buttonHelp.onClick.AddListener(OnButtonClickHelp);
        _buttonExit.onClick.AddListener(OnButtonClickExit);
        ButtonNewGame.NewGame += Close;
    }

    private void OnDisable()
    {
        _buttonMenu.onClick.RemoveListener(OnButtonClickMenu);
        _buttonContinue.onClick.RemoveListener(OnButtonClickContinue);
        _buttonHelp.onClick.RemoveListener(OnButtonClickHelp);
        _buttonExit.onClick.RemoveListener(OnButtonClickExit);
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
        _buttonContinue.interactable = true;
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
