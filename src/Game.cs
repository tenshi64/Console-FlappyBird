using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Flappy_Bird
{
    internal class Game : Program
    {
        private static readonly string SaveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Console Flappy Bird\save.ini";
        public static bool keyClicked;

        public static void AddPoint(int X)
        {
            bool pointAdded = false; //checking if point has been added
            for (int i = 2; i < AxisY.Count() - 1; i++)
            {
                if(!pointAdded)
                {
                    if (AxisY[i.ToString()][X] == "█")
                    {
                        score++;
                        Console.SetCursorPosition(0, 16);
                        Console.WriteLine($"Score: {score}");
                        pointAdded = true;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void GetKey()
        {
            if(!keyClicked)
            {
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    keyClicked = true;
                    if ((3 + playersY) <= AxisY.Count())
                    {
                        Transform.ClearOldPosition(AxisY, 2, 3 + playersY);
                        playersY--;
                    }
                    else
                    {
                        isGameStarted = false;
                        GameOver();
                    }
                    Program.gotInput = true;
                }
            }
        }

        public static void GameOver()
        {
            Console.Clear();
            StartInfo();
            Console.WriteLine("Game Over!\nrestart the game, to play more.");
        }

        public static void StartInfo()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"   _____                      _      ");
            Console.WriteLine(@"  / ____|                    | |     ");
            Console.WriteLine(@" | |     ___  _ __  ___  ___ | | ___");
            Console.WriteLine(@" | |    / _ \| '_ \/ __|/ _ \| |/ _ \");
            Console.WriteLine(@" | |___| (_) | | | \__ \ (_) | |  __/");
            Console.WriteLine(@"  \_____\___/|_| |_|___/\___/|_|\___|");

            Console.WriteLine(@"  ______ _                           ____  _         _ ");
            Console.WriteLine(@" |  ____| |                         |  _ \(_)       | |");
            Console.WriteLine(@" | |__  | | __ _ _ __  _ __  _   _  | |_) |_ _ __ __| |");
            Console.WriteLine(@" |  __| | |/ _` | '_ \| '_ \| | | | |  _ <| | '__/ _` |");
            Console.WriteLine(@" | |    | | (_| | |_) | |_) | |_| | | |_) | | | | (_| |");
            Console.WriteLine(@" |_|    |_|\__,_| .__/| .__/\__,| | |____/|_|_|  \__,_|");
            Console.WriteLine(@"                | |   | |    __/ |                    ");
            Console.WriteLine(@"                |_|   |_|   |___/                     ");
            Console.WriteLine("Console FlappyBird. Michał Narożny 2023");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Score: {score}");
            Console.WriteLine("Highest score: {0}", GetHighestScore());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        public static void CreateIfNoSaveFile()
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Console Flappy Bird";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(SaveFilePath))
            {
                File.WriteAllText(SaveFilePath, "0");
            }
        }

        public static void SaveHighestScore(int score)
        {
            if (File.Exists(SaveFilePath))
            {
                File.WriteAllText(SaveFilePath, score.ToString());
            }
            else
            {
                CreateIfNoSaveFile();
                File.WriteAllText(SaveFilePath, score.ToString());
            }
        }

        public static int GetHighestScore()
        {
            string content = File.ReadAllText(SaveFilePath);

            try
            {
                return int.Parse(content);
            }
            catch(Exception)
            {
                return 0;
            }
        }
    }
}
