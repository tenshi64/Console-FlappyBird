using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Console_Flappy_Bird
{
    internal class Program
    {
        protected static bool isGameStarted;
        protected static bool gotInput;
        private static bool isRendered;
        protected static int score;
        public const int drawRate = 8;
        protected static int playersY = 0;
        protected static Dictionary<string, List<string>> AxisY = new Dictionary<string, List<string>>();

        static void Main()
        {
            Game.CreateIfNoSaveFile();

            //--rendering lines
            List<string> AxisX1 = new List<string>();
            List<string> AxisX2 = new List<string>();
            List<string> AxisX3 = new List<string>();
            List<string> AxisX4 = new List<string>();
            List<string> AxisX5 = new List<string>();
            List<string> AxisX6 = new List<string>();
            List<string> AxisX7 = new List<string>();

            AxisY["1"] = AxisX1;
            AxisY["2"] = AxisX2;
            AxisY["3"] = AxisX3;
            AxisY["4"] = AxisX4;
            AxisY["5"] = AxisX5;
            AxisY["6"] = AxisX6;
            AxisY["7"] = AxisX7;
            //-rendering lines

            Game.StartInfo();

            if (!isGameStarted)
            {
                while(true)
                {
                    Console.SetWindowSize(80, 32);
                    Console.Write("\rPress SPACE to play...");
                    if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                    {
                        isGameStarted = true;
                        break;
                    }
                    Thread.Sleep(500);
                }
                if (isGameStarted)
                {
                    while (true)
                    {
                        Console.SetWindowSize(80, 32);
                        if (isGameStarted)
                        {
                            if(!isRendered)
                            {
                                for (int i = 1; i <= AxisY.Count(); i++)
                                {
                                    if (AxisY[i.ToString()].Count <= 20)
                                    {
                                        AxisY[i.ToString()] = World.CreateElements(drawRate);
                                        isRendered = true;
                                    }
                                }
                            }

                            bool pointAdded = false;
                            World.RandomWorld(AxisY, drawRate);
                            for (int i = 1; i <= AxisY.Count(); i++)
                            {
                                //--rendering
                                Transform.MoveWorld(AxisY[i.ToString()], "█", " ", drawRate);
                                Transform.SpawnPlayer(AxisY, 2, 3 + playersY);
                                if(AxisY.ContainsKey(i.ToString()))
                                {
                                    string renderedObjects = String.Join("", AxisY[i.ToString()]);
                                    Console.SetCursorPosition(0, 18 + i);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\r" + renderedObjects);
                                }
                                //--rendering

                                if (!pointAdded)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Game.AddPoint(2);
                                    pointAdded = true;
                                    if (score > Game.GetHighestScore())
                                    {
                                        Game.SaveHighestScore(score);
                                        Console.SetCursorPosition(0, 17);
                                        Console.WriteLine("Highest score: " + Game.GetHighestScore() + " (New record!)");
                                    }
                                }
                                Console.ForegroundColor = ConsoleColor.White;

                                if (AxisY.Count() == 0)
                                {
                                    Game.GameOver();
                                }
                            }
                            if (AxisY.Count() > 0)
                            {
                                //--ground
                                Console.SetCursorPosition(0, 26);
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("--------------------------------------------------------------------------------");
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("--------------------------------------------------------------------------------");
                                Console.WriteLine("--------------------------------------------------------------------------------");
                                Console.WriteLine("--------------------------------------------------------------------------------");
                                Console.WriteLine("--------------------------------------------------------------------------------");
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                //--ground

                                Task.Factory.StartNew(() => Game.GetKey()).Wait(TimeSpan.FromSeconds(0.35));
                                Game.keyClicked = false;

                                if (!gotInput)
                                {
                                    if ((3 + playersY) <= AxisY.Count())
                                    {
                                        Transform.ClearOldPosition(AxisY, 2, 3 + playersY);
                                        playersY++;
                                    }
                                    else
                                    {
                                        isGameStarted = false;
                                        AxisY.Clear();
                                        Console.Clear();
                                        Game.GameOver();
                                    }
                                }
                                gotInput = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
