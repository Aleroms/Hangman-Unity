using System;

namespace Hangman.AI
{
    [System.Serializable]
    public class Body_Anthropic
    {
        public string type;
        public string completion;
        public string stop_reason;
        public string stop;
    }

    [System.Serializable]
    public class Root_Anthropic
    {
        public int statusCode;
        public Body_Anthropic body;
    }

    [System.Serializable]
    public class Body_Meta
    {
        public string AIResponse;
        public int PromptTokenCount;
        public int GenerationTokenCount;
        public string StopReason;
    }
    [System.Serializable]  
    public class Root_Meta
    {
        public int StatusCode;
        public Body_Meta Body;
    }
}