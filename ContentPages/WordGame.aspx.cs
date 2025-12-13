using Fricke_ITM_325_Assignment_4.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Fricke_ITM_325_Assignment_4.ContentPages
{
    public partial class WordGame : System.Web.UI.Page
    {
        private Random random = new Random();

        private string originalWordCookieName = "Original Word";
        private string wordCookieName = "Game Word";
        private string answerCookieName = "Answers";
        private string logicCookieName = "Game Logic";
        private string missingLettersCookieName = "Missing Letters";
        private string attemptsCookieName = "Attempts";
        private string gameOverCookieName = "Game Over";
        private string messageCookieName = "Game Message";

        private const int MaxAttempts = 7;

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie wordCookie = Request.Cookies[wordCookieName];

            if (wordCookie == null || string.IsNullOrEmpty(wordCookie.Value))
            {
                int index = random.Next(WordCollection.Instance.words.Count);
                HandleCookies(WordCollection.Instance.words[index]);
                SetAttempts(0);
                SetGameOver("");
                SetMessage(WordCollection.Instance.responses["Start"]);
                wordCookie = Response.Cookies[wordCookieName];
            }

            Word.InnerHtml = wordCookie?.Value ?? "";

            RenderGuesses();

            if (!IsPostBack)
            {
                if (IsGameOver())
                {
                    string state = Request.Cookies[gameOverCookieName]?.Value ?? "";
                    string original = Request.Cookies[originalWordCookieName]?.Value ?? "";
                    int attempts = GetAttempts();
                    int remaining = MaxAttempts - attempts;

                    if (state == "Win")
                    {
                        Message.InnerText = string.Format(WordCollection.Instance.responses["Win"], original, remaining);
                    }
                    else
                    {
                        Message.InnerText = string.Format(WordCollection.Instance.responses["Lose"], original, remaining);
                    }    
                }
                else
                {
                    string last = GetMessage();
                    Message.InnerText = string.IsNullOrWhiteSpace(last) ? WordCollection.Instance.responses["Start"] : last;
                }
            }

            Answer.Enabled = true;
            Submit.Enabled = true;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string input = (Answer.Text ?? "").Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(input)) return;

            if (IsGameOver())
            {
                if (input == "!")
                {
                    ResetGame();
                    return;
                }

                SetMessage(WordCollection.Instance.responses["GameOver"]);
                Response.Redirect(Request.RawUrl);
                return;
            }

            if (input.Length != 1) return;

            string original = Request.Cookies[originalWordCookieName]?.Value;
            if (string.IsNullOrEmpty(original)) return;

            char g = input[0];

            var guessedSet = ParseLetterSet(Request.Cookies[answerCookieName]?.Value ?? "");
            if (guessedSet.Contains(g))
            {
                SetMessage(string.Format(WordCollection.Instance.responses["RepeatGuess"], g));
                Answer.Text = "";
                Response.Redirect(Request.RawUrl);
                return;
            }

            guessedSet.Add(g);
            HttpCookie answers = new HttpCookie(answerCookieName, string.Join(", ", guessedSet));
            answers.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(answers);

            var missingSet = ParseLetterSet(Request.Cookies[missingLettersCookieName]?.Value ?? "");
            bool isCorrect = missingSet.Contains(g);

            if (isCorrect)
            {
                SetMessage(WordCollection.Instance.responses["Correct"]);
                RevealLetterInWordCookieAndMaybeWin(g);
                return;
            }

            int attempts = GetAttempts() + 1;
            SetAttempts(attempts);
            SetMessage($"{WordCollection.Instance.responses["Incorrect"]} ({attempts}/{MaxAttempts})");

            if (attempts >= MaxAttempts)
            {
                SetGameOver("Lose");
                string solved = Request.Cookies[originalWordCookieName]?.Value ?? "";
                int remaining = MaxAttempts - attempts;
                SetMessage(string.Format(WordCollection.Instance.responses["Lose"], solved, remaining));
                Response.Redirect(Request.RawUrl);
                return;
            }

            Answer.Text = "";
            Response.Redirect(Request.RawUrl);
        }

        private void RenderGuesses()
        {
            Answers.InnerHtml = "";

            var guessed = ParseLetterSet(Request.Cookies[answerCookieName]?.Value ?? "")
                .OrderBy(c => c)
                .ToList();

            foreach (char c in guessed)
            {
                var li = new HtmlGenericControl("span");
                li.InnerText = c.ToString() + " ";
                Answers.Controls.Add(li);
            }
        }

        protected void HandleCookies(string word)
        {
            string logic = GenerateGameLogic(word);
            HandleLogicCookie(logic);
            HandleWordCookies(word, logic);
            HandleMissingLettersCookie(word, logic);
        }

        protected void HandleLogicCookie(string logic)
        {
            HttpCookie logicCookie = new HttpCookie(logicCookieName, logic);
            logicCookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(logicCookie);
        }

        protected void HandleWordCookies(string word, string logic)
        {
            HttpCookie originalCookie = new HttpCookie(originalWordCookieName, word);
            HttpCookie wordCookie = new HttpCookie(wordCookieName, UpdateWordWithUnderscores(word, logic));
            HttpCookie answersCookie = new HttpCookie(answerCookieName, "");

            originalCookie.Expires = DateTime.Now.AddDays(30);
            wordCookie.Expires = DateTime.Now.AddDays(30);
            answersCookie.Expires = DateTime.Now.AddDays(30);

            Response.Cookies.Add(originalCookie);
            Response.Cookies.Add(wordCookie);
            Response.Cookies.Add(answersCookie);
        }

        protected void HandleMissingLettersCookie(string word, string logic)
        {
            var set = new HashSet<char>();
            foreach (string part in logic.Split(','))
            {
                if (int.TryParse(part.Trim(), out int index))
                {
                    if (index >= 0 && index < word.Length)
                        set.Add(char.ToLowerInvariant(word[index]));
                }
            }

            HttpCookie missingCookie = new HttpCookie(missingLettersCookieName, string.Join(", ", set));
            missingCookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(missingCookie);
        }

        protected string GenerateGameLogic(string word)
        {
            return $"{random.Next(0, word.Length)}, {random.Next(0, word.Length)}, {random.Next(0, word.Length)}";
        }

        protected string UpdateWordWithUnderscores(string word, string logic)
        {
            char[] chars = word.ToCharArray();
            foreach (string part in logic.Split(','))
            {
                if (int.TryParse(part.Trim(), out int index))
                {
                    if (index >= 0 && index < chars.Length)
                        chars[index] = '_';
                }
            }
            return new string(chars);
        }

        private HashSet<char> ParseLetterSet(string cookieValue)
        {
            var set = new HashSet<char>();
            if (string.IsNullOrWhiteSpace(cookieValue)) return set;

            foreach (string part in cookieValue.Split(','))
            {
                string s = part.Trim();
                if (s.Length > 0 && char.IsLetter(s[0]))
                    set.Add(char.ToLowerInvariant(s[0]));
            }

            return set;
        }

        private void RevealLetterInWordCookieAndMaybeWin(char guess)
        {
            string original = Request.Cookies[originalWordCookieName]?.Value ?? Response.Cookies[originalWordCookieName]?.Value;
            string current = Request.Cookies[wordCookieName]?.Value ?? Response.Cookies[wordCookieName]?.Value;

            if (string.IsNullOrEmpty(original) || string.IsNullOrEmpty(current))
                return;

            char[] display = current.ToCharArray();
            char g = char.ToLowerInvariant(guess);

            for (int i = 0; i < original.Length && i < display.Length; i++)
            {
                if (display[i] == '_' && char.ToLowerInvariant(original[i]) == g)
                    display[i] = original[i];
            }

            string updatedWord = new string(display);

            HttpCookie wordCookie = new HttpCookie(wordCookieName, updatedWord);
            wordCookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(wordCookie);

            if (!updatedWord.Contains("_"))
            {
                SetGameOver("Win");
                int attempts = GetAttempts();
                int remaining = MaxAttempts - attempts;
                SetMessage(string.Format(WordCollection.Instance.responses["Win"], original, remaining));
            }

            Response.Redirect(Request.RawUrl, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private int GetAttempts()
        {
            int attempts = 0;
            int.TryParse(Request.Cookies[attemptsCookieName]?.Value, out attempts);
            return attempts;
        }

        private void SetAttempts(int attempts)
        {
            HttpCookie c = new HttpCookie(attemptsCookieName, attempts.ToString());
            c.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(c);
        }

        private bool IsGameOver()
        {
            string v = Request.Cookies[gameOverCookieName]?.Value;
            return v == "Win" || v == "Lose";
        }

        private void SetGameOver(string state)
        {
            HttpCookie c = new HttpCookie(gameOverCookieName, state);
            c.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(c);
        }

        private void SetMessage(string text)
        {
            HttpCookie c = new HttpCookie(messageCookieName, text ?? "");
            c.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(c);
            Message.InnerText = text ?? "";
        }

        private string GetMessage()
        {
            return Request.Cookies[messageCookieName]?.Value ?? "";
        }

        private void ResetGame()
        {
            ExpireCookie(originalWordCookieName);
            ExpireCookie(wordCookieName);
            ExpireCookie(answerCookieName);
            ExpireCookie(logicCookieName);
            ExpireCookie(missingLettersCookieName);
            ExpireCookie(attemptsCookieName);
            ExpireCookie(gameOverCookieName);
            ExpireCookie(messageCookieName);
            Response.Redirect(Request.RawUrl);
        }

        private void ExpireCookie(string name)
        {
            HttpCookie c = new HttpCookie(name, "");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(c);
        }
    }
}
