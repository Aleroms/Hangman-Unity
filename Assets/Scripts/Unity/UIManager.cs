using Hangman.GameInterface;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPopup;
    [SerializeField] private GameObject _setupGO;
    [SerializeField] private GameObject _gameGO;
    [SerializeField] private GameObject _winGO;
    [SerializeField] private GameObject _loseGO;

    private IHangman _unityHangman;
    private bool toggleGameModes = true;

    private void Start()
    {
        _unityHangman = GetComponent<IHangman>();
        Toggle();
        _loadingPopup.SetActive(false);
        _loseGO.SetActive(false);
        _winGO.SetActive(false);
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
    public void DidPlayerLose(bool playerLost, string word)
    {
        Debug.Log(playerLost + word);
        if (playerLost)
        {
            _loseGO.SetActive(true);
            GameObject.Find("wordReveal").GetComponent<TextMeshProUGUI>()
                .text = word;
        }
        else 
        { 
            _winGO.SetActive(true) ;
        }

        GameObject.Find("gamesWon").GetComponent<TextMeshProUGUI>()
                .text = "Games Won: " + 0;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StopKeyboard()
    {
        var keys = GameObject.FindGameObjectsWithTag("keys");
        foreach(var key in keys)
        {
            key.GetComponent<Button>().interactable = false;
        }
    }
}
