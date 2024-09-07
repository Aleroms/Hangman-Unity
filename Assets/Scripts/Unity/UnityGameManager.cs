using Hangman.GameCore;
using Hangman.GameInterface;
using Hangman.Local;
using System;
using UnityEngine;

public class UnityGameManager : MonoBehaviour
{
    private GameDifficulty _gameDifficulty;
    private string _masterWord;
    private char[] _displayWord;
    private string _guessedWords = "";

    private UnitySetupManager _setupManager; //
    private IWordGenerator _wordGenerator; //
    private IPlayerInputHandler _playerInputHandler;
    private IHangman _hangman;
    private IStorage _storage;

    private void Start()
    {
        _setupManager = this.GetComponent<UnitySetupManager>();
        _wordGenerator = this.GetComponent<IWordGenerator>();
        _hangman = this.GetComponent<IHangman>();
    }


    private bool IsGameOver(int playerLives) => playerLives == 0 || IsMasterWordGuessed();

    private bool EvaluateGuess(char guess)
    {
        bool isCorrectGuess = false;
        if (!_guessedWords.Contains(guess))
            _guessedWords += guess;

        for (int i = 0; i < _masterWord.Length; i++)
        {
            if (_masterWord[i] == guess)
            {
                _displayWord[i] = guess;
                isCorrectGuess = true;
            }
        }
        return isCorrectGuess;
    }

    private bool IsMasterWordGuessed() => _masterWord == new string(_displayWord);

    public void FetchWord()
    {
        _masterWord = _wordGenerator.GenerateWord(_gameDifficulty);

        if (string.IsNullOrEmpty(_masterWord))
        {
            Debug.LogError("failed to load word");
            return;
        }

        _displayWord = new char[_masterWord.Length];

        for (int i = 0; i < _masterWord.Length; i++)
        {
            _displayWord[i] = '_';
        }
    }
    public void Reset()
    {
        throw new NotImplementedException();
    }
    public void Run() { throw new NotImplementedException(); }
    public void Setup()
    {

        //load victories
        _playerInputHandler.Victories = int.Parse(_storage.Read("victories.txt"));
        _gameDifficulty = _setupManager.GameMode;
    }


}
