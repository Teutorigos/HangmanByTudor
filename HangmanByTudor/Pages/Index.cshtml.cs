using HangmanByTudor.Extensions;
using HangmanByTudor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HangmanByTudor.Pages
{
    public class IndexModel : PageModel
    {
        public GameStateModel Game { get; set; }

        public void OnGet()
        {
            // Load game from session or initialize new one
            Game = HttpContext.Session.GetJson<GameStateModel>("Game") ?? NewGame();
        }

        public IActionResult OnPostGuess(char letter)
        {
            Game = HttpContext.Session.GetJson<GameStateModel>("Game");

            var engine = new HangmanEngine();

            Game = engine.ProcessGuess(Game, letter);

            HttpContext.Session.SetJson("Game", Game);

            return Page();
        }

        private GameStateModel NewGame() {
            // Define your word list
            string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "words.txt");
            var words = System.IO.File.ReadAllLines(filePath).ToList();
            var random = new Random();

            // Create and return the initial state
            var newGame = new GameStateModel
            {
                SecretWord = words[random.Next(words.Count)],
                GuessedLetters = new List<char>(),
                Lives = 6
            };

            // Save it to session immediately so the next request can find it
            HttpContext.Session.SetJson("Game", newGame);

            return newGame;
        }

        public IActionResult OnPostReset()
        {
            // Remove the current game state from the session
            HttpContext.Session.Remove("Game");

            // Redirect back to the Index page to start fresh
            return RedirectToPage("Index");
        }
    }
}
