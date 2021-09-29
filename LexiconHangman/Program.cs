using System;
using System.Text;

namespace LexiconHangman
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuePlaying = true;
            string selectedOption;

            while (continuePlaying)
            {
                selectedOption = DisplayPlayOrQuitMenu();

                if (selectedOption == "p")
                    Startgame();
                else
                    continuePlaying = false;
            }
            Console.WriteLine("Application has stopped.");
        }


        private static void Startgame()
        {
            int numberOfGuessesLeft = 10;
            StringBuilder incorrectGuesses = new StringBuilder();
            
            int randomWordIndex = new Random().Next(0, 9);
            string wordToGuess = WordCollection.GetStringFromWordArrayByIndex(randomWordIndex).ToUpper();

            char[] guessedLettersArray = new char[wordToGuess.Length];
            for (int i = 0; i < wordToGuess.Length; i++)
                guessedLettersArray[i] = '_';

            bool wordIsSolved = false;
            bool guessWasCorrect;

            while (!wordIsSolved && (numberOfGuessesLeft > 0))
            {
                guessWasCorrect = false;
                DisplayCurrentGameStatus(numberOfGuessesLeft, incorrectGuesses, guessedLettersArray);
                string guess = Console.ReadLine().ToUpper();

                if (guess.Length == 1) // If guess is a single letter.
                   wordIsSolved =  HandleInputWhenGuessIsALetter(ref guessWasCorrect, incorrectGuesses, wordToGuess, guessedLettersArray, guess);
                else // If guess is multiple letters (a word).
                    wordIsSolved = HandleInputWhenGuessIsAWord(ref guessWasCorrect, wordToGuess, guess);

                if (!guessWasCorrect && !incorrectGuesses.ToString().Contains(guess))
                {
                    numberOfGuessesLeft--;
                    if(guess.Length == 1)
                        incorrectGuesses.Append($"{guess} ");
                }

                Console.WriteLine($"{Environment.NewLine}Your guess was {(guessWasCorrect ? "correct" : "incorrect")}.");
            }

            Console.WriteLine($"Game {(wordIsSolved ? "Won" : "Lost")}! The secret word was {wordToGuess}");
            Console.ReadLine();
        }

        
        private static bool HandleInputWhenGuessIsALetter(ref bool guessWasCorrect, StringBuilder incorrectGuesses, string wordToGuess, char[] guessedLettersArray, string guess)
        {
            if (incorrectGuesses.ToString().Contains(guess))
            {
                Console.WriteLine("You have already guessed this letter before, this guess will not count.");
            }
            else
            {
                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i] == char.Parse(guess))
                    {
                        guessedLettersArray[i] = wordToGuess[i];
                        guessWasCorrect = true;
                    }
                }
            }
            return new string(guessedLettersArray) == wordToGuess; //Compares the accumulated correct guesses to the secret word.
        }

        
        private static bool HandleInputWhenGuessIsAWord(ref bool guessWasCorrect, string wordToGuess, string guess)
        {
            if (guess == wordToGuess)
            {
                guessWasCorrect = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        
        private static void DisplayCurrentGameStatus(int numberOfGuessesLeft, StringBuilder incorrectGuesses, char[] guessedLettersArray)
        {
            Console.WriteLine($"{Environment.NewLine}Correct guessed letters: {new string(guessedLettersArray)}");
            Console.WriteLine($"Incorrect guessed letters: {incorrectGuesses}");
            Console.WriteLine($"You have {numberOfGuessesLeft} guesses left.");
            Console.Write("Guess a letter in the word or guess the whole word: ");
        }

        
        private static string DisplayPlayOrQuitMenu()
        {
            Console.Clear();
            Console.Write($"Welcome to the Hangman Game. {Environment.NewLine}Press p to play or q to quit: ");  
            string selOpt = Console.ReadLine();

            if (selOpt != "p" && selOpt != "q")
            {
                Console.Write($"Entered character is not a valid selection. {Environment.NewLine}Press enter to select again.");
                Console.ReadLine();
                selOpt = DisplayPlayOrQuitMenu();
            }

            return selOpt;
        } 
    }
}
