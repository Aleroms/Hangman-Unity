using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hangman.GameCore;
using UnityEngine.UI;

public class UnitySetupManager : MonoBehaviour
{
    public GameDifficulty GameMode { get; private set; }

    public void SetGameDifficulty(int difficulty)
    {
        GameMode = (GameDifficulty)difficulty;
        //Debug.Log($"Game Difficulty: {GameMode}");
    }
}
