namespace Hangman.GameInterface
{
    public interface IHangman
    {
        public void DisplayState(int lives, char[] displayedWords, string guessedWords);
    }
}
