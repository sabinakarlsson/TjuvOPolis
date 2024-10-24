using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOPolis
{
    public class Prison
    {
        public static void ShowPrison(string[,] myPrison, List<Person> myPrisoners)
        {
            //sätter ut punkter i mitt fängelse
            for (int i = 0; i < myPrison.GetLength(0); i++)
            {
                for (int j = 0; j < myPrison.GetLength(1); j++)
                {
                    myPrison[i, j] = ".";
                }
            }

            //lägger till personer i fängelset
            foreach (Person person in myPrisoners)
            {
                myPrison[person.PrisonPlacementY, person.PrisonPlacementX] = "T";
                person.MovePrisoners(person.MovementDirectionX, person.MovementDirectionY, myPrison);

            }


            Console.WriteLine("Fängelse: ");
            //skriver ut mitt fängelse

            for (int s = 0; s < myPrison.GetLength(0); s++)
            {
                for (int t = 0; t < myPrison.GetLength(1); t++)
                {
                    Console.Write(myPrison[s, t]);
                }
                Console.WriteLine();
            }
        }
    }
}
