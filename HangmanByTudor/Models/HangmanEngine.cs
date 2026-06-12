namespace HangmanByTudor.Models
{
    public class HangmanEngine
    {
        public GameStateModel ProcessGuess(GameStateModel game, char letter)
        {
            if (!game.GuessedLetters.Contains(letter) && !game.IsGameOver)
            {
                game.GuessedLetters.Add(letter);
                if (!game.SecretWord.Contains(letter))
                {
                    game.Lives--;
                }
            }

            return game;
        }
    }
}
