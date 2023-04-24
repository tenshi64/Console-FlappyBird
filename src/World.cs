using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Flappy_Bird
{
    internal static class World
    {
        public static Dictionary<string, List<string>> RandomWorld(Dictionary<string, List<string>> AxisY, int drawRate)
        {
            Random random = new Random();
            for (int i = 2; i < AxisY.Count()-1; i++)
            {
                for (int j = 0; j < AxisY[i.ToString()].Count(); j++)
                {
                    if (j % drawRate == 0)
                    { 
                        if(!CheckIfAnyHole(AxisY, j))
                        {
                            if (i > 2)
                            {
                                if (AxisY[(i - 1).ToString()][j] != " " && AxisY[(i - 2).ToString()][j] != " ")
                                {
                                    int randomInt = random.Next(3, 6);
                                    AxisY[randomInt.ToString()][j] = " ";
                                    if (i < AxisY.Count())
                                    {
                                        int value = randomInt + 1;
                                        AxisY[value.ToString()][j] = " ";
                                    }
                                    else
                                    {
                                        int value = randomInt - 1;
                                        AxisY[value.ToString()][j] = " ";
                                    }
                                }
                            }
                            else
                            {
                                int randomInt = random.Next(0,5);

                                if (randomInt == 1)
                                {
                                    AxisY[i.ToString()][j] = " ";
                                    int value = i + 1;
                                    AxisY[value.ToString()][j] = " ";
                                }
                            }
                        }
                    }
                }
            }
            return AxisY;
        }

        public static List<string> CreateElements(int drawRate)
        {
            List<string> AxisX = new List<string>();
            for (int i = 80; i > 0; i--)
            {
                if (i % drawRate == 0)
                {
                    AxisX.Add("█");
                }
                else
                {
                    AxisX.Add(" ");
                }
            }
            return AxisX;
        }

        private static bool CheckIfAnyHole(Dictionary<string, List<string>> AxisY, int index)
        {
            for(int i = 1; i < AxisY.Count(); i++)
            {
                if(AxisY[i.ToString()][index] == " ")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
