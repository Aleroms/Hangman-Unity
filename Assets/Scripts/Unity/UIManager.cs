using Hangman.GameInterface;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPopup;
    [SerializeField] private GameObject _setupGO;
    [SerializeField] private GameObject _gameGO;

    private IHangman _unityHangman;
    private bool toggleGameModes = true;

    private void Start()
    {
        _unityHangman = GetComponent<IHangman>();
        Toggle();
        _loadingPopup.SetActive(false);
    }
    public void ToggleGameMode()
    {
        toggleGameModes = !toggleGameModes;
        Toggle();
    }
    private void Toggle()
    {
        _setupGO.SetActive(toggleGameModes);
        _gameGO.SetActive(!toggleGameModes);
    }
    public void IsLoading(bool isLoading)
    {
        _loadingPopup.SetActive(isLoading);
    }
}
