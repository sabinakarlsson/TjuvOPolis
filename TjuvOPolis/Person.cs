using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TjuvOPolis.Person;

namespace TjuvOPolis
{
    public class Person
    {
        public int PlacementX { get; set; }

        public int PlacementY { get; set; }

        public int MovementDirectionY { get; set; }

        public int MovementDirectionX { get; set; }

        public int PrisonPlacementX { get; set; }

        public int PrisonPlacementY { get; set; }


        public Person()
        {
            PlacementY = Random.Shared.Next(1, 15);
            PlacementX = Random.Shared.Next(1, 60);
            MovementDirectionY = Random.Shared.Next(-1, 2);
            MovementDirectionX = Random.Shared.Next(-1, 2);

        }

        public virtual void Move(int moveX, int moveY, string[,] myCity)
        {

        }

        public void MovePrisoners(int moveX, int moveY, string[,] myPrison)
        {
            PrisonPlacementY = Random.Shared.Next(1, 7);
            PrisonPlacementX = Random.Shared.Next(1, 15);

            if (MovementDirectionX == 0 && MovementDirectionY == 0)
            {
                MovementDirectionY = Random.Shared.Next(-1, 2);
                MovementDirectionX = Random.Shared.Next(-1, 2);
            }

            PrisonPlacementY += MovementDirectionY;
            PrisonPlacementX += MovementDirectionX;

            if (PrisonPlacementX < 0) PrisonPlacementX = myPrison.GetLength(1) - 1;
            if (PrisonPlacementX >= myPrison.GetLength(1)) PrisonPlacementX = 0;
            if (PrisonPlacementY < 0) PrisonPlacementY = myPrison.GetLength(0) - 1;
            if (PrisonPlacementY >= myPrison.GetLength(0)) PrisonPlacementY = 0;
        }



        public class Police : Person
        {
            public List<Thing> SeizedProperty { get; set; }

            public string Name { get; set; }

            public Police(string name) : base()
            {
                List<Thing> seizedProperty = new List<Thing>();

                SeizedProperty = seizedProperty;
                Name = name;
            }

            public override void Move(int moveX, int moveY, string[,] myCity)
            {
                if (MovementDirectionX == 0 && MovementDirectionY == 0)
                {
                    MovementDirectionY = Random.Shared.Next(-1, 2);
                    MovementDirectionX = Random.Shared.Next(-1, 2);
                }
                PlacementY += MovementDirectionY;
                PlacementX += MovementDirectionX;

                if (PlacementX < 0) PlacementX = myCity.GetLength(1) - 1;
                if (PlacementX >= myCity.GetLength(1)) PlacementX = 0;
                if (PlacementY < 0) PlacementY = myCity.GetLength(0) - 1;
                if (PlacementY >= myCity.GetLength(0)) PlacementY = 0;
            }

            public static void Confiscate(Person police, Person thief)
            {

                ((Police)police).SeizedProperty.AddRange(((Thief)thief).StolenProperty);
                ((Thief)thief).StolenProperty.Clear();
                Console.WriteLine(((Police)police).Name + " genomsökte " + ((Thief)thief).Name + ", och beslagstog alla stulna värdesaker. " + ((Thief)thief).Name + " fick åka i fängelse för sitt brott...");

            }
        }

        public class Thief : Person
        {
            public List<Thing> StolenProperty { get; set; }

            public string Name { get; set; }


            public Thief(string name) : base()
            {
                StolenProperty = new List<Thing>();
                Name = name;

            }

            public override void Move(int moveX, int moveY, string[,] myCity)
            {
                if (MovementDirectionX == 0 && MovementDirectionY == 0)
                {
                    MovementDirectionY = Random.Shared.Next(-1, 2);
                    MovementDirectionX = Random.Shared.Next(-1, 2);
                }

                PlacementY += MovementDirectionY;
                PlacementX += MovementDirectionX;

                if (PlacementX < 0) PlacementX = myCity.GetLength(1) - 1;
                if (PlacementX >= myCity.GetLength(1)) PlacementX = 0;
                if (PlacementY < 0) PlacementY = myCity.GetLength(0) - 1;
                if (PlacementY >= myCity.GetLength(0)) PlacementY = 0;
            }

            public static void Steal(Person citizen, Person thief)
            {

                int randomItemNr = Random.Shared.Next(((Citizen)citizen).PropertyInPossession.Count);

                var randomItemRemoved = ((Citizen)citizen).PropertyInPossession[randomItemNr];
                ((Citizen)citizen).PropertyInPossession.RemoveAt(randomItemNr);

                ((Thief)thief).StolenProperty.Add(randomItemRemoved);
                Console.WriteLine("Denna tjuv rånade " + ((Citizen)citizen).Name + " på " + randomItemRemoved.Things);

            }
        }




        public class Citizen : Person
        {
            public List<Thing> PropertyInPossession { get; set; }

            public string Name { get; set; }


            public Citizen(string name) : base()
            {
                PropertyInPossession = new List<Thing> { new Thing("sina nycklar"), new Thing("sin plånbok"), new Thing("sin mobiltelefon"), new Thing("sin klocka") };
                Name = name;
            }

            public override void Move(int moveX, int moveY, string[,] myCity)
            {
                if (MovementDirectionX == 0 && MovementDirectionY == 0)
                {
                    MovementDirectionY = Random.Shared.Next(-1, 2);
                    MovementDirectionX = Random.Shared.Next(-1, 2);
                }

                PlacementY += MovementDirectionY;
                PlacementX += MovementDirectionX;

                if (PlacementX < 0) PlacementX = myCity.GetLength(1) - 1;
                if (PlacementX >= myCity.GetLength(1)) PlacementX = 0;
                if (PlacementY < 0) PlacementY = myCity.GetLength(0) - 1;
                if (PlacementY >= myCity.GetLength(0)) PlacementY = 0;
            }
        }



        public class PrisonSize
        {
            public int PrisonPlacementX { get; set; }

            public int PrisonPlacementY { get; set; }

            public int MovePrisonY { get; set; }

            public int MovePrisonX { get; set; }

            public PrisonSize()
            {
                PrisonPlacementX = Random.Shared.Next(1, 10);
                PrisonPlacementY = Random.Shared.Next(1, 20);
                MovePrisonY = Random.Shared.Next(-1, 2);
                MovePrisonX = Random.Shared.Next(-1, 2);

            }
        }





        public static string GetRandomOfficer()
        {
            string[] allOfficers =
            {
                "Konstapel 1",
                "Konstapel 2",
                "Konstapel 3",
                "Konstapel 4",
                "Konstapel 5",
                "Konstapel 6",
                "Konstapel 7",
                "Konstapel 8",
                "Konstapel 9",
                "Konstapel 10",
            };



            Random random = new Random();
            int rnd = random.Next(0, allOfficers.Length - 1);


            return allOfficers[rnd];
        }

        public static string GetRandomThief()
        {
            string[] allThiefs =
            {
                "Tjuv 1",
                "Tjuv 2",
                "Tjuv 3",
                "Tjuv 4",
                "Tjuv 5",
                "Tjuv 6",
                "Tjuv 7",
                "Tjuv 8",
                "Tjuv 9",
                "Tjuv 10",
                "Tjuv 11",
                "Tjuv 12",
                "Tjuv 13",
                "Tjuv 14",
                "Tjuv 15",
                "Tjuv 16",
                "Tjuv 17",
                "Tjuv 18",
                "Tjuv 19",
                "Tjuv 20",
            };



            Random random = new Random();
            int rnd = random.Next(0, allThiefs.Length - 1);


            return allThiefs[rnd];
        }

        public static string GetRandomCitizen()
        {
            string[] allCitizens =
            {
                "Medborgare 1",
                "Medborgare 2",
                "Medborgare 3",
                "Medborgare 4",
                "Medborgare 5",
                "Medborgare 6",
                "Medborgare 7",
                "Medborgare 8",
                "Medborgare 9",
                "Medborgare 10",
                "Medborgare 11",
                "Medborgare 12",
                "Medborgare 13",
                "Medborgare 14",
                "Medborgare 15",
                "Medborgare 16",
                "Medborgare 17",
                "Medborgare 18",
                "Medborgare 19",
                "Medborgare 20",
                "Medborgare 21",
                "Medborgare 22",
                "Medborgare 23",
                "Medborgare 24",
                "Medborgare 25",
                "Medborgare 26",
                "Medborgare 27",
                "Medborgare 28",
                "Medborgare 29",
                "Medborgare 30",
            };



            Random random = new Random();
            int rnd = random.Next(0, allCitizens.Length - 1);


            return allCitizens[rnd];
        }
    }
}
