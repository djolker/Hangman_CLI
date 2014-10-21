using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman_Lib_test
{
    /// <summary>
    /// Hangman Game Object
    /// </summary>
    class Hangman
    {
        #region variables
        /// <summary>
        /// Goal word
        /// </summary>
        private char[] word {get; set;}

        /// <summary>
        /// players progress towards the goal word
        /// </summary>
        public List<char> fillingWord = new List<char>();

        /// <summary>
        /// letters already guessed along the way
        /// </summary>
        public List<char> guessedLetters = new List<char>();

        /// <summary>
        /// int16 to represent number of strikes
        /// </summary>
        public Int16 strikes = new short();

        /// <summary>
        /// Dictionary pulled from dictionary.txt file
        /// </summary>
        private List<string> dictionary = new List<string>();

        /// <summary>
        /// Status of the game was lost or not. 
        /// </summary>
        private bool gameOver = false;

        #endregion

        /// <summary>
        /// Start new game of Hangman
        /// </summary>
        /// <param name="difficulty"></param>
        public Hangman(int difficulty)
        {
            gameOver = false;
            loadDictionary();
            initNewWord(difficulty);
        }

        public bool getGameOverStatus()
        {
            return gameOver;
        }

        /// <summary>
        /// Takes in int representing difficulty and finds a word that suits that choice, then applies it to variable word
        /// </summary>
        /// <param name="difficulty"></param>
        private void initNewWord(int difficulty)
        {
            Random rnd = new Random();

            int min = new int();
            int max = new int();

            switch (difficulty)
            {
                case 1:
                    min = 0;
                    max = 4;
                    break;
                case 2:
                    min = 5;
                    max = 6;
                    break;
                case 3:
                    min = 7;
                    max = 8;
                    break;
                case 4:
                    min = 9;
                    max = 10000;
                    break;
            }

            while (true)
            {
                int randomNum = rnd.Next(0, dictionary.Count);
                if (min <= dictionary[randomNum].Length && dictionary[randomNum].Length <= max)
                {
                    word = dictionary[randomNum].ToCharArray();
                    break;
                }
            }

            for (int i = 0; i < word.Count(); i++)
            {
                fillingWord.Add('_');
            }
        }

        /// <summary>
        /// loads dictionary file into list
        /// </summary>
        /// <returns></returns>
        private void loadDictionary()
        {
            dictionary = File.ReadAllLines("dictionary.txt").ToList();
        }

        /// <summary>
        /// Letter is taken in, checked against the goal word, puts letters into fillingWords array
        /// </summary>
        /// <param name="letter"></param>
        public void guessLetter(char letter)
        {
            if (!guessedLetters.Contains(letter))
            {
                if (word.Contains(letter))
                {
                    for (int i = 0; i < word.Count(); i++)
                    {
                        if (word[i] == letter)
                        {
                            fillingWord[i] = letter;
                        }

                        guessedLetters.Add(letter);
                    }
                }
                else
                {
                    strikes++;
                }
            }
            else
            {
                Console.WriteLine("Letter " + letter.ToString() + " has already been guessed");
                Console.Read();
            }
        }

        /// <summary>
        /// starts new round with current progress
        /// </summary>
        public void newRound()
        {
            Console.Clear();
            
            if (checkForCompletion())
            {
                Console.WriteLine("Grats you win");
                Console.ReadKey();
            }
            else if (checkForGameOver())
            {
                Console.WriteLine("Too many strikes, Game Over");

                StringBuilder completeword = new StringBuilder();
                foreach (char letter in word)
                {
                    completeword.Append(letter.ToString());  
                }
                
                Console.WriteLine("The word was " + completeword.ToString());
                Console.WriteLine("Press Enter to Continue...");
                Console.Read();
            }
            else
            {
                showProgress();

                Console.WriteLine("What is your next guess?");
                guessLetter(Console.ReadKey().KeyChar);
            }
        }

        /// <summary>
        /// Checks for strikes based GameOver
        /// </summary>
        /// <returns></returns>
        private bool checkForGameOver()
        {
            if (strikes >= 6)
            {
                gameOver = true;
                
            }
            return gameOver;
        }

        /// <summary>
        /// Checks for a GameWin
        /// </summary>
        /// <returns>GameWin Bool</returns>
        public bool checkForCompletion()
        {
            bool gameWon = true;

            for (int i = 0; i < word.Count(); i++)
            {
                if (!word[i].Equals(fillingWord[i]))
                {
                    gameWon = false;
                    break;
                }
            }

            return gameWon;
        }

        /// <summary>
        /// Prints current game status
        /// </summary>
        private void showProgress()
        {
            string currentProgress = ""; //recreated each cycle to show the current players progress
            foreach (char letter in fillingWord)
            {
                if (!(letter.ToString() == @"\0"))
                {
                    currentProgress += " " + letter.ToString();
                }
                else
                {
                    currentProgress += " _ ";
                }
            }
            Console.WriteLine("Current Progress: " + currentProgress);
            Console.WriteLine("Strikes: " + strikes + "/6");
        }
    }
}