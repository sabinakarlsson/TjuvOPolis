using static TjuvOPolis.Person;

namespace TjuvOPolis
{
    public class Program 
    {
        static string[,] myCity = new string[25, 100];
        static void Main(string[] args)
        {
            int numberPoliceOfficers = 10;
            int numberThiefs = 20;
            int numberCitizens = 30;

            Arrest arrest = new Arrest();
            Robbed robbed = new Robbed();

            List<Person> myTown = new List<Person>();

            //lägger till poliser, utifrån antalet ovanför
            for (int i = 0; i < numberPoliceOfficers; i++)
            {
                myTown.Add(new Police(GetRandomOfficer()));
            }

            //tjuvar
            for (int i = 0; i < numberThiefs; i++)
            {
                myTown.Add(new Thief(GetRandomThief()));
            }

            //medborgare
            for (int i = 0; i < numberCitizens; i++)
            {
                myTown.Add(new Citizen(GetRandomCitizen()));
            }


            while (true)
            {
                Console.Clear();
                
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

                
                // Kolla om någon bokstav möts
                for (int i = 0; i < myTown.Count; i++)
                {
                    for (int j = i + 1; j < myTown.Count; j++)
                    {
                        if (myTown[i].PlacementY == myTown[j].PlacementY && myTown[i].PlacementX == myTown[j].PlacementX)
                        {
                            if (myTown[i] is Police && myTown[j] is Thief)
                            {
                                Console.WriteLine(((Police)myTown[i]).Name + " och " + ((Thief)myTown[j]).Name + " möttes. Andra blev tillfångatagen.");
                            }

                            if (myTown[i] is Thief && myTown[j] is Citizen)
                            {
                                Console.WriteLine(((Thief)myTown[i]).Name + " och " + ((Citizen)myTown[j]).Name + " möttes. Andra blev rånad");
                            }

                            if (myTown[i] is Citizen && myTown[j] is Police)
                            {
                                Console.WriteLine(((Citizen)myTown[i]).Name + " och " + ((Police)myTown[j]).Name + " möttes. De hälsade på varandra");
                            }

                        }
                    }
                }




                Thread.Sleep(2000);

                /*
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("Händeleser i staden: ");
                Console.WriteLine("Antal gripna: " + arrest.NumberArrested);
                Console.WriteLine("Antal rånade: " + robbed.NumberRobbed);*/
                

            }

        }

        
    }
}
