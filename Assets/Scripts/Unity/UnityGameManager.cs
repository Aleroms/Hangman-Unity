using Hangman.AI;
using Hangman.CloudInfrastructure;
using Hangman.GameCore;
using Hangman.GameInterface;
using Hangman.Local;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UnityGameManager : MonoBehaviour
{
    private GameDifficulty _gameDifficulty;
    // temporarily set masterword to public for debugging
    [SerializeField]
    private string _masterWord;
    private string _guessedWords = "";
    private char[] _displayWord;
    private bool _playAgain = false;

    private UnitySetupManager _setupManager;
    private UIManager _uiManager;
    private IWordGenerator _wordGenerator;
    private IPlayerInputHandler _playerInputHandler;
    private IHangman _hangman;
    private IStorage _storage;

    private void Awake()
    {
        AWSBedrock.FoundationalModel = FoundationalModel.ANTHROPIC_CLAUDE1;
        
        _setupManager = this.GetComponent<UnitySetupManager>();
        _uiManager = this.GetComponent<UIManager>();
        _wordGenerator = this.GetComponent<IWordGenerator>();
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

    private bool IsMasterWordGuessed() => _masterWord == new string(_displayWord);

    public void FetchWord()
    {
        StartCoroutine(FetchWordCoroutine(_gameDifficulty));
        
        /*
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
        */

    }
    
    public void Reset()
    {
        throw new NotImplementedException();
    }
    public void Run()
    {
        _hangman.DisplayState(_playerInputHandler.Lives, _displayWord, _guessedWords);
        if(!IsGameOver(_playerInputHandler.Lives))
        {

        }
    }
    public void StartGame()
    {
        Setup();
        FetchWord();
        
        //Run();
    }

    public void Setup()
    {

        //load victories
        _playerInputHandler.Victories = int.Parse(_storage.Read("victories.txt"));
        _gameDifficulty = _setupManager.GameMode;
    }
    private IEnumerator FetchWordCoroutine(GameDifficulty game)
    {
        string uri = "https://6owlahqw42.execute-api.us-east-1.amazonaws.com/dev/" 
            + AWSBedrock.ModelPrompt(game.ToString());
        UnityWebRequest request = UnityWebRequest.Get(uri);
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

            string word = response.body.completion;
            // Process the word as needed
            Debug.Log(word);
            _masterWord = AWSBedrock.Sanitize(word);
            Debug.Log(_masterWord);

        }

        _uiManager.ToggleGameMode();
    }


}
