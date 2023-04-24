using System.Collections.Generic;

namespace Console_Flappy_Bird
{
    internal static class Transform
    {
        private const string player = "@";
        private static string bufferedData;

        public static Dictionary<string, List<string>> SpawnPlayer(Dictionary<string, List<string>> AxisY, int X, int Y)
        {
            if (Y <= AxisY.Count && Y >= 1)
            {
                if (AxisY[Y.ToString()][X] != "█")
                {
                    if (AxisY[Y.ToString()][X] != "@")
                    {
                        bufferedData = AxisY[Y.ToString()][X];
                    }
                    AxisY[Y.ToString()][X] = player;
                    return AxisY;
                }
                else
                {
                    AxisY.Clear();
                    Game.GameOver();
                }
                AxisY.Clear();
            }
            return AxisY;
        }

        public static Dictionary<string, List<string>> ClearOldPosition(Dictionary<string, List<string>> AxisY, int X, int Y)
        {
            if (Y <= AxisY.Count && Y >= 1)
            {
                AxisY[Y.ToString()][X] = bufferedData;
            }
            else
            {
                AxisY.Clear();
                Game.GameOver();
                return AxisY;
            }
            return AxisY;
        }

        public static void MoveWorld(List<string> AxisX, string wallTexture, string emptyField, int drawRate)
        {
            for (int i = 0; i <= AxisX.Count - 1; i++)
            {
                if (i < AxisX.Count - 1)
                {
                    string bufferedData2 = AxisX[i + 1];
                    if (AxisX[i + 1] == player)
                    {
                        AxisX[i] = bufferedData;
                    }
                    else
                    {
                        if (bufferedData2 != player)
                        {
                            AxisX[i + 1] = AxisX[i];
                            AxisX[i] = bufferedData2;
                        }
                    }
                }
                else
                {
                    int check = 0;
                    for (int a = 1; a < drawRate; a++)
                    {
                        if (AxisX[i - a] == emptyField)
                        {
                            check++;
                        }
                    }
                    if (check == drawRate - 1)
                    {
                        AxisX[i] = wallTexture;
                    }
                    else
                    {
                        AxisX[i] = emptyField;
                    }
                }
            }
        }
    }
}
