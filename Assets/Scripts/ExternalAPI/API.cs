//using Hangman.AI;
using Hangman.CloudInfrastructure;
using Hangman.GameCore;
using System;
using System.Collections;
using System.Net.Http;
//using System.Text.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Hangman.ExternalAPI
{

    public class APIWordGenerator : IWordGenerator
    {
        //private static readonly string _uri = "https://6owlahqw42.execute-api.us-east-1.amazonaws.com/dev/";


        public APIWordGenerator(FoundationalModel model)
        {
            AWSBedrock.FoundationalModel = model;
        }
        public string GenerateWord(GameDifficulty game)
        {
            //string difficulty = game.ToString();


            //using HttpResponseMessage response = _client.GetAsync(AWSBedrock.ModelPrompt(difficulty))
            //    .GetAwaiter().GetResult();

            //response.EnsureSuccessStatusCode();
            //var word = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            //var root = JsonSerializer.Deserialize<Root_Anthropic>(word);

            //if (root == null)
            //    return "";

            //// sanitize response 
            //return AWSBedrock.Sanitize(root.Body.AIResponse);
            return "";
        }
        



    }


}
