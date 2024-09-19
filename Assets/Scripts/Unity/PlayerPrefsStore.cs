using Hangman.GameCore;
using UnityEngine;

public class PlayerPrefsStore : MonoBehaviour, IStorage
{
    public string Read(string filePath) =>
        PlayerPrefs.GetString(filePath, "0");

    public void Write(string filePath, string data)
    {
        PlayerPrefs.SetString(filePath, data);
    }
}
