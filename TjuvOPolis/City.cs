using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TjuvOPolis.Person;

namespace TjuvOPolis
{
    public class City
    {
        public static void ShowCity(string[,] myCity, List<Person> myTown)
        {
            //sätter ut punkter i min stad
            for (int i = 0; i < myCity.GetLength(0); i++)
            {
                for (int j = 0; j < myCity.GetLength(1); j++)
                {
                    myCity[i, j] = ".";
                }
            }

            //för varje person, skriv bokstav och flytta den
            foreach (Person person in myTown)
            {
                if (person is Police)
                {
                    myCity[person.PlacementY, person.PlacementX] = "P";
                    person.Move(person.MovementDirectionX, person.MovementDirectionY, myCity);
                }

                else if (person is Thief)
                {
                    myCity[person.PlacementY, person.PlacementX] = "T";
                    person.Move(person.MovementDirectionX, person.MovementDirectionY, myCity);
                }

                else if (person is Citizen)
                {
                    myCity[person.PlacementY, person.PlacementX] = "M";
                    person.Move(person.MovementDirectionX, person.MovementDirectionY, myCity);
                }

            }


            //visar spelplanen
            for (int i = 0; i < myCity.GetLength(0); i++)
            {
                for (int j = 0; j < myCity.GetLength(1); j++)
                {
                    Console.Write(myCity[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
