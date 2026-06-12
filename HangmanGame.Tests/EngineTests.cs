using HangmanByTudor.Models;

namespace HangmanGame.Tests
{
    // HangmanGame.Tests/EngineTests.cs
    public class EngineTests
    {
        [Fact]
        public void ProcessGuess_CorrectLetter_ShouldNotReduceLives()
        {
            // Arrange
            var engine = new HangmanEngine();
            var game = new GameStateModel { SecretWord = "TEST", Lives = 6 };

            // Act
            engine.ProcessGuess(game, 'T');

            // Assert
            Assert.Equal(6, game.Lives);
            Assert.Contains('T', game.GuessedLetters);
        }

        [Fact]
        public void ProcessGuess_WrongLetter_ShouldReduceLives()
        {
            // Arrange
            var engine = new HangmanEngine();
            var game = new GameStateModel { SecretWord = "TEST", Lives = 6 };

            // Act
            engine.ProcessGuess(game, 'Z');

            // Assert
            Assert.Equal(5, game.Lives);
        }
    }
}
