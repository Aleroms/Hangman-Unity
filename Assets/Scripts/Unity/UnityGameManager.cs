using Hangman.AI;
using Hangman.CloudInfrastructure;
using Hangman.GameCore;
using Hangman.GameInterface;
//using Hangman.Local;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UnityGameManager : MonoBehaviour
{
    private GameDifficulty _gameDifficulty;
    private string _masterWord;
    private string _guessedWords = "";
    private char[] _displayWord;

    private UnitySetupManager _setupManager;
    private UIManager _uiManager;
    private IPlayerInputHandler _playerInputHandler;
    private IHangman _hangman;
    private IStorage _storage;

    private void Awake()
    {
        AWSBedrock.FoundationalModel = FoundationalModel.ANTHROPIC_CLAUDE1;
        
        _setupManager = this.GetComponent<UnitySetupManager>();
        _uiManager = this.GetComponent<UIManager>();
        _hangman = this.GetComponent<IHangman>();
        _playerInputHandler = this.GetComponent<IPlayerInputHandler>();
        _storage = this.GetComponent<IStorage>();

       
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

    private bool IsMasterWordGuessed() => _masterWord.ToLower() == new string(_displayWord).ToLower();

    public void FetchWord()
    {
        StartCoroutine(FetchWordCoroutine(_gameDifficulty));
    }
    private void SetDisplayWord()
    {
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
    public void PlayerTurn(int index)
    {
        char c = (char)((int)'a' + index - 1);

        //check if user has already entered that guess
        if (_guessedWords.Contains(c))
            return;

        if (!EvaluateGuess(c))
            _playerInputHandler.Lives--;
        
        _hangman.DisplayState(_playerInputHandler.Lives, _displayWord, _guessedWords);
        
        if (IsGameOver(_playerInputHandler.Lives))
        {
            //remove interactivity with keyboard
            _uiManager.StopKeyboard();

            if (_playerInputHandler.Lives < 1)
            {
                _uiManager.DidPlayerLose(true, _masterWord, _playerInputHandler.Victories);
            } 
            else
            {
                // save new record
                _playerInputHandler.Victories++;
                _storage.Write("victories",_playerInputHandler.Victories.ToString());

                _uiManager.DidPlayerLose(false, _masterWord, _playerInputHandler.Victories);
            }
        }

    }
    public void StartGame()
    {
        Setup();
        FetchWord();
    }

    public void Setup()
    {

        //load victories
        _playerInputHandler.Victories = int.Parse(_storage.Read("victories"));
        _gameDifficulty = _setupManager.GameMode;
    }
    private IEnumerator FetchWordCoroutine(GameDifficulty game)
    {
        string uri = "https://6owlahqw42.execute-api.us-east-1.amazonaws.com/dev/" 
            + AWSBedrock.ModelPrompt(game.ToString());
        UnityWebRequest request = UnityWebRequest.Get(uri);

        _uiManager.IsLoading(true);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
        request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error fetching word: {request.error}");
        }
        else
        {
            // Get the response text
            var response = JsonUtility.FromJson<Root_Anthropic>(request.downloadHandler.text);

            // Process the word as needed
            _masterWord = AWSBedrock.Sanitize(response.body.completion);
            SetDisplayWord();
        }

        _hangman.DisplayState(_playerInputHandler.Lives, _displayWord, _guessedWords);
        _uiManager.IsLoading(false);
        _uiManager.ToggleGameMode();
    }


}
