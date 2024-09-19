﻿using Hangman.GameCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Hangman.Local
{
    public class LocalWordGenerator : MonoBehaviour, IWordGenerator
    {
        private readonly Dictionary<GameDifficulty, List<string>> _wordList;
        private string fetchedWord = "";
        [SerializeField] private GameObject fetchWordText;

        public LocalWordGenerator()
        {
            _wordList = new()
            {
                //EVENTUALLY IMPLEMENT ISTORAGE TO STORE THESE WORDS
                {
                    GameDifficulty.EASY,
                    new List<string>
                    {
                        "cat",
                        "sun",
                        "book",
                        "tree",
                        "milk",
                        "kite",
                        "ball",
                        "duck",
                        "fish",
                        "moon",
                    }
                },
                {
                    GameDifficulty.MEDIUM,
                    new List<string>
                    {
                        "penguin",
                        "kitchen",
                        "blanket",
                        "pumpkin",
                        "lantern",
                        "picture",
                        "glasses",
                        "chicken",
                        "monster",
                        "diamond",
                    }
                },
                {
                    GameDifficulty.HARD,
                    new List<string>
                    {
                        "crocodile",
                        "astronaut",
                        "zucchini",
                        "javascript",
                        "kangaroo",
                        "hierarchy",
                        "xylophone",
                        "microwave",
                        "quarantine",
                        "philosopher",
                    }
                }
            };
        }
        private void Start()
        {
            fetchWordText.SetActive(false);
        }
        public string GenerateWord(GameDifficulty game)
        {
            var matchingWords = _wordList[game];

            if (matchingWords.Count == 0)
                fetchedWord = "";

            var random = new System.Random();
            fetchedWord = matchingWords[random.Next(matchingWords.Count)];
            return fetchedWord;
        }


    }
    public class LocalStore : IStorage
    {
        public string Read(string filePath)
        {
            if (File.Exists(filePath))
            {
                var fileContents = File.ReadAllText(filePath);
                return fileContents;
            }
            return "0";
        }

        public void Write(string filePath, string data)
        {
            File.WriteAllText(filePath, data);
        }
    }
}
