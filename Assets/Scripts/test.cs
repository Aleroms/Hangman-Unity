using Hangman.CloudInfrastructure;
using Hangman.GameCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class test : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject panel1;
    public GameObject panel2;
    public void CallAPI()
    {
        StartCoroutine(FetchWordCoroutine(GameDifficulty.EASY) );
    }
   
    private IEnumerator FetchWordCoroutine(GameDifficulty game)
    {
        text.text = "LOADING...";

        string uri = "https://6owlahqw42.execute-api.us-east-1.amazonaws.com/dev/"
            + AWSBedrock.ModelPrompt(game.ToString());
        Debug.Log(uri);
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();

        panel1.SetActive(false);
        panel2.SetActive(true);

        if (request.result == UnityWebRequest.Result.ConnectionError ||
        request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error fetching word: {request.error}");
        }
        else
        {
            // Get the response text
            text.text = request.downloadHandler.text;


        }
    }
}
