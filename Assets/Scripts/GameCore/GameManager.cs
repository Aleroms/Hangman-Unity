namespace Hangman.GameCore
{


    public abstract class GameManager
    {
        protected GameDifficulty gameDifficulty;
        protected string masterWord;
        protected char[] displayWord;
        protected string guessedWords = "";


        public abstract void Run();
        public abstract void Setup();
        public abstract void FetchWord();
        public abstract void Reset();

        protected bool IsGameOver(int playerLives) => playerLives == 0 || IsMasterWordGuessed();

        protected bool EvaluateGuess(char guess)
        {
            bool isCorrectGuess = false;
            if (!guessedWords.Contains(guess))
                guessedWords += guess;

            for (int i = 0; i < masterWord.Length; i++)
            {
                if (masterWord[i] == guess)
                {
                    displayWord[i] = guess;
                    isCorrectGuess = true;
                }
            }
            return isCorrectGuess;
        }

        private bool IsMasterWordGuessed() => masterWord == new string(displayWord);

    }
}