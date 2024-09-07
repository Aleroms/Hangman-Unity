using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hangman.GameCore;

public class UnityPlayer : MonoBehaviour, IPlayerInputHandler
{
    private static readonly int _defaultLives = 6;
    public int Lives { get; set; }
    public int Victories { get; set; }


    private void Start()
    {
        Lives = _defaultLives;
    }

    public bool GetPlayAgain()
    {
        throw new System.NotImplementedException();
    }

    public char GetPlayerGuess()
    {
        throw new System.NotImplementedException();
    }

    public void ResetLives()
    {
        throw new System.NotImplementedException();
    }

    
}
