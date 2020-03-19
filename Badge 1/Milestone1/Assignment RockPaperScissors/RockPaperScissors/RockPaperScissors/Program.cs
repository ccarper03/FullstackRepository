using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            int playerChoice = 0;
            int gameAi_Choice = 0;
            bool playAgain = false;
            do
            {
                int rounds = 0;
                int playerWinCount = 0;
                int gameAiWinCount = 0;
                int tieCount = 0;
                Random rand = new Random();
                rounds = GetRoundCount(); // rounds to play
                for (int i = 0; i < rounds; i++)
                {
                    Console.WriteLine("Round " + (i+1));
                    Console.WriteLine("---------------------------------------------------------");
                    playerChoice = GetPlayerChoice();
                    gameAi_Choice = GetAiChoice(rand);
                    // Determine winner
                    // 1 = Rock 2 = Paper 3 = Scissors 
                    if (playerChoice == gameAi_Choice) 
                    {
                        tieCount++;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You both have selected the same choice, rounds tied, Tied Game Count: {0}\n", tieCount);
                        Console.ResetColor();
                    }
                    else if(playerChoice == 1 && gameAi_Choice == 2 || playerChoice == 2 && gameAi_Choice == 3 || playerChoice == 3 && gameAi_Choice == 1)
                    {
                        gameAiWinCount++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Game AI Wins this round. Player {0} Game AI {1}\n", playerWinCount, gameAiWinCount);
                        Console.ResetColor();
                    }// just leave as an else, only other option
                    else if(gameAi_Choice == 1 && playerChoice == 2 || gameAi_Choice == 2 && playerChoice == 3 || gameAi_Choice == 3 && playerChoice == 1)
                    {
                        playerWinCount++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Player Wins this round. Player {0} Game AI {1}\n", playerWinCount, gameAiWinCount);
                        Console.ResetColor();
                    }
                }
                DisplayResults(playerWinCount, gameAiWinCount, tieCount);
                playAgain = PlayAgain();
            } while (playAgain);
            ClosingMessage();
        }
        /// <summary>
        /// Displays a goodbye message
        /// </summary>
        private static void ClosingMessage()
        {
            Console.WriteLine("Thanks for playing.");
            Console.ReadLine();
        }
        /// <summary>
        /// Determines the winner and displays the stats
        /// </summary>
        /// <param name="player"></param>
        /// <param name="GameAI"></param>
        /// <param name="ties"></param>
        private static void DisplayResults(int player, int GameAI, int ties)
        {
            Console.WriteLine(@" /$$$$$$$                                /$$   /$$              
| $$__  $$                              | $$  | $$              
| $$  \ $$  /$$$$$$   /$$$$$$$ /$$   /$$| $$ /$$$$$$   /$$$$$$$ 
| $$$$$$$/ /$$__  $$ /$$_____/| $$  | $$| $$|_  $$_/  /$$_____/ 
| $$__  $$| $$$$$$$$|  $$$$$$ | $$  | $$| $$  | $$   |  $$$$$$  
| $$  \ $$| $$_____/ \____  $$| $$  | $$| $$  | $$ /$$\____  $$ 
| $$  | $$|  $$$$$$$ /$$$$$$$/|  $$$$$$/| $$  |  $$$$//$$$$$$$/ 
|__/  |__/ \_______/|_______/  \______/ |__/   \___/ |_______/  
                                                                                                        
 /$$$$$$ /$$$$$$ /$$$$$$ /$$$$$$ /$$$$$$ /$$$$$$ /$$$$$$ /$$$$$$
|______/|______/|______/|______/|______/|______/|______/|______/
");
            if (ties > player && player == GameAI)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Tied Game: {0}.", ties);
                Console.WriteLine("Your score: {0}.", player);
                Console.WriteLine("Game AI's score: {0} ", GameAI);                
                Console.ResetColor();
            }
            else if (player > GameAI)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You are the winner with a score of {0}.", player);
                Console.WriteLine("Game AI has: {0} ", GameAI);
                Console.WriteLine("There was {0} ties\n", ties);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game AI is the winner with a score of {0}.", GameAI);
                Console.WriteLine("Your score was {0}.", player);
                Console.WriteLine("There was {0} ties.", ties);
                Console.ResetColor();
            }
        }
        /// <summary>
        /// Get GameAI's Choice
        /// </summary>
        /// <param name="rand"></param>
        /// <returns>Returns an int</returns>
        private static int GetAiChoice(Random rand)
        {
            return rand.Next(1, 4);
        }
        /// <summary>
        /// Get's the players choice
        /// </summary>
        /// <returns>returns an Int</returns>
        private static int GetPlayerChoice()
        {
            int number;
            while (true)
            {
                Console.WriteLine("Select a number for your choice: 1 = Rock, 2 = Paper, 3 = Scissors");
                string input = Console.ReadLine()+"\n";
                if (int.TryParse(input, out number))
                {
                    if (number < 1 || number > 3)
                    {
                        continue;
                    }
                    else
                    {
                        break; // output has value and not lower than 1 or higher than 3
                    }
                }
                else
                {
                    Console.WriteLine("Select a number for your choice: 1 = Rock, 2 = Paper, 3 = Scissors");
                }
            }
            return number;
        }
        /// <summary>
        /// Ask player for input to play again
        /// </summary>
        /// <returns>returns a bool</returns>
        private static bool PlayAgain()
        {
            Console.WriteLine("\nWould you like to play again? [Y/N]: ");
            while (true)
            {
                string name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("You did not enter anything!");
                }
                else
                {
                    if (name.Equals("Y")||name.Equals("y"))
                    {
                        return true;
                    }
                    else if (name.Equals("N") || name.Equals("n"))
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Sorry... I cannot compute... ");
                        Console.WriteLine("Would you like to play again? [Y/N]: ");
                    }
                }
            }
        }
        /// <summary>
        /// Gets amount of rounds from the user
        /// </summary>
        /// <returns>returns an int</returns>
        private static int GetRoundCount()
        {
            int number;
            while (true)
            {
                Console.Write("From 1 - 10, How many rounds would you like to play? ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out number))
                {
                    if (number > 10 || number < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong... BAD... Go sit in the corner!!");
                        Console.ResetColor();
                        Console.WriteLine("Press enter to close");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    else
                    {
                        break; // output has value and is not higher then 10 or below 1
                    }
                    
                }
                else
                {
                    Console.WriteLine("Please enter a number!");
                }
            }
            return number;
        }
    }
}
