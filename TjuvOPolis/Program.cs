using static TjuvOPolis.Person;

namespace TjuvOPolis
{
    public class Program
    {
        static string[,] myCity = new string[25, 100];
        static void Main(string[] args)
        {
            int numberPoliceOfficers = 1;
            int numberThiefs = 1;
            int numberCitizens = 1;

            Arrest arrest = new Arrest();
            Robbed robbed = new Robbed();

            List<Person> myTown = new List<Person>();


            //sätter punkter på varje ruta i min spelplan
            for (int x = 0; x < myCity.GetLength(0); x++)
            {
                for (int y = 0; y < myCity.GetLength(1); y++)
                {
                    myCity[x, y] = ".";
                }
            }


            //lägger till poliser, utifrån antalet ovanför
            for (int i = 0; i < numberPoliceOfficers; i++)
            {
                myTown.Add(new Police());
            }

            //tjuvar
            for (int i = 0; i < numberThiefs; i++)
            {
                myTown.Add(new Thief());
            }

            //medborgare
            for (int i = 0; i < numberCitizens; i++)
            {
                myTown.Add(new Citizen());
            }





            while (true)
            {
                Console.Clear();

                //sätter ut personerna i min stad
                foreach (Person person in myTown)
                {

                    if (person is Police)
                    {

                        myCity[person.PlacementY, person.PlacementX] = "P";
                        myCity[person.PlacementY, person.PlacementX] = ".";

                        person.Move(person.PlacementX, person.PlacementY, person.MovementDirectionX, person.MovementDirectionY, myCity);

                        myCity[person.PlacementY, person.PlacementX] = "P";

                    }

                    else if (person is Thief)
                    {
                        myCity[person.PlacementY, person.PlacementX] = "T";
                        myCity[person.PlacementY, person.PlacementX] = ".";

                        person.Move(person.PlacementX, person.PlacementY, person.MovementDirectionX, person.MovementDirectionY, myCity);

                        myCity[person.PlacementY, person.PlacementX] = "T";
                    }

                    else if (person is Citizen)
                    {
                        myCity[person.PlacementY, person.PlacementX] = "M";
                        myCity[person.PlacementY, person.PlacementX] = ".";

                        person.Move(person.PlacementX, person.PlacementY, person.MovementDirectionX, person.MovementDirectionY, myCity);

                        myCity[person.PlacementY, person.PlacementX] = "M";

                    }


                    //visar spelplanen
                    for (int i = 0; i < myCity.GetLength(0); i++)
                    {
                        for (int j = 0; j < myCity.GetLength(1); j++)
                        {
                            Console.Write(myCity[i, j]);


                            //if one person is thief, and the other person is police
                            if (((Thief)person).PlacementX == ((Police)person).PlacementX && ((Thief)person).PlacementY == ((Police)person).PlacementY)
                            {
                                Console.WriteLine("Polis och tjuv möttes. " + ((Police)person).Name + " tog allt som " + ((Thief)person).Name + " hade på sig.");
                                Police.Confiscate(((Police)person), ((Thief)person));
                                arrest.ShowArrest(((Thief)person));

                            }

                            if (person is Thief && person is Citizen)
                            {
                                Console.WriteLine("Tjuv och medborgare möttes");
                                Thief.Steel(person thief, person citizen); //paus, sedan fortsätta ändra som ovan
                            }

                        }
                        Console.WriteLine();
                    }




                    Console.WriteLine("-----------------------------------------");


                }

                Console.WriteLine("Händeleser i staden: ");
                Console.WriteLine("Antal gripna: " + arrest.NumberArrested);
                Console.WriteLine("Antal rånade: " + robbed.NumberRobbed);
                Thread.Sleep(200);

            }

        }
    }
}
