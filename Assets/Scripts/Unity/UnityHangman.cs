using Hangman.GameInterface;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnityHangman : MonoBehaviour, IHangman
{
    [SerializeField]
    private List<Sprite> _state;
    [SerializeField]
    private Image _hangman;
    [SerializeField]
    private TextMeshProUGUI _guessedLetters;
    [SerializeField]
    private TextMeshProUGUI _displayWords;

    public void DisplayState(int lives, char[] displayedWords, string guessedWords)
    {
        string displayGuessedLetters = string.Join(" ", guessedWords);
        string word = "";
        foreach (char c in displayedWords) word += c;
        Debug.Log($"lives:{lives} displayWords:{word} guessedWords:{guessedWords}");

        _hangman.sprite = _state[lives];
        _displayWords.text = word;
        _guessedLetters.text = displayGuessedLetters;
    }

}
