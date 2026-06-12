namespace HangmanByTudor.Models
{
    public class GameStateModel
    {
        public string SecretWord { get; set; } = "";
        public List<char> GuessedLetters { get; set; } = new();
        public int Lives { get; set; } = 6;
        public bool IsGameOver => Lives <= 0 || IsWinner;
        public bool IsWinner => SecretWord.All(c => GuessedLetters.Contains(c));
    }
}
