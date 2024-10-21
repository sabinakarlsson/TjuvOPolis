using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOPolis
{
    public class Person
    {
        public int PlacementX { get; set; }

        public int PlacementY { get; set; }

        public int MovementDirectionY { get; set; }

        public int MovementDirectionX { get; set; }


        public Person()
        {
            PlacementY = Random.Shared.Next(1, 25);
            PlacementX = Random.Shared.Next(1, 100);
            MovementDirectionY = Random.Shared.Next(-1, 2);
            MovementDirectionX = Random.Shared.Next(-1, 2);

        }

        public virtual void Move(int placeX, int placeY, int moveX, int moveY, string[,] myCity)
        {

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

            public override void Move(int placeX, int placeY, int moveX, int moveY, string[,] myCity)
            {
                PlacementX += MovementDirectionX;
                PlacementY += MovementDirectionY;


                if (PlacementX < 0) PlacementX = myCity.GetLength(0) - 1;
                if (PlacementX >= myCity.GetLength(0)) PlacementX = 0;
                if (PlacementY < 0) PlacementY = myCity.GetLength(1) - 1;
                if (PlacementY >= myCity.GetLength(1)) PlacementY = 0;
            }

            public static void Confiscate(Person police, Person thief)
            {
                if (((Thief)thief).StolenProperty.Count > 0)
                {
                    ((Police)police).SeizedProperty.AddRange(((Thief)thief).StolenProperty);
                    ((Thief)thief).StolenProperty.Clear();
                }

                else
                {
                    Console.WriteLine(((Police)police) + " genomsökte " + ((Thief)thief) + ", men hen hade inga stulna värdesaker på sig (denna gång..)");
                }
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

            public override void Move(int placeX, int placeY, int moveX, int moveY, string[,] myCity)
            {
                PlacementX += MovementDirectionX;
                PlacementY += MovementDirectionY;


                if (PlacementX < 0) PlacementX = myCity.GetLength(0) - 1;
                if (PlacementX >= myCity.GetLength(0)) PlacementX = 0;
                if (PlacementY < 0) PlacementY = myCity.GetLength(1) - 1;
                if (PlacementY >= myCity.GetLength(1)) PlacementY = 0;
            }

            public static void Steel(Person citizen, Person thief)
            {
                if (((Citizen)citizen).PropertyInPossession.Count > 0)
                {
                    int randomItemNr = Random.Shared.Next(((Citizen)citizen).PropertyInPossession.Count);

                    var randomItemRemoved = ((Citizen)citizen).PropertyInPossession[randomItemNr];
                    ((Citizen)citizen).PropertyInPossession.RemoveAt(randomItemNr);

                    ((Thief)thief).StolenProperty.Add(randomItemRemoved);

                }

                else
                {
                    Console.WriteLine(((Thief)thief) + " försökte råna " + ((Citizen)citizen) + ", men hen har redan blivit bestulen på alla sina värdesaker..");
                }

            }
        }




        public class Citizen : Person
        {
            public List<Thing> PropertyInPossession { get; set; }

            public string Name { get; set; }


            public Citizen(string name) : base()
            {
                PropertyInPossession = new List<Thing> { new Thing("nycklar"), new Thing("plånbok"), new Thing("mobiltelefon"), new Thing("klocka") };
                Name = name;
            }

            public override void Move(int placeX, int placeY, int moveX, int moveY, string[,] myCity)
            {
                PlacementX += MovementDirectionX;
                PlacementY += MovementDirectionY;


                if (PlacementX < 0) PlacementX = myCity.GetLength(0) - 1;
                if (PlacementX >= myCity.GetLength(0)) PlacementX = 0;
                if (PlacementY < 0) PlacementY = myCity.GetLength(1) - 1;
                if (PlacementY >= myCity.GetLength(1)) PlacementY = 0;
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
