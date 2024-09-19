using Hangman.GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityWordGenerator : MonoBehaviour, IWordGenerator
{
    private UIManager uiManager;
    public string GenerateWord(GameDifficulty game)
    {
        StartCoroutine(GenerateWord(game));
        return "test";
    }


    private void Start()
    {
        uiManager = GetComponent<UIManager>();
    }
    private IEnumerator GenerateWordCoroutine(GameDifficulty game)
    {
        //FIX THIS PART
        yield return null;
    }
}
