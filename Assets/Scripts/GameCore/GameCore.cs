namespace Hangman.GameCore
{
    public enum GameDifficulty { EASY, MEDIUM, HARD }
    public struct WordSettings
    {
        public int wordLength;
        public GameDifficulty difficulty;

        public WordSettings(int length, GameDifficulty difficulty)
        {
            this.wordLength = length;
            this.difficulty = difficulty;
        }
    }

    public interface IWordGenerator
    {
        string GenerateWord(GameDifficulty game);
    }
    public interface IPlayerInputHandler
    {
        char GetPlayerGuess();
        bool GetPlayAgain();
        void ResetLives();
        int Lives { get; set; }
        int Victories { get; set; }
    }
    public interface IStorage
    {
        string Read(string filePath);
        void Write(string filePath, string data);
    }
}