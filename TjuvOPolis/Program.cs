using static TjuvOPolis.Person;


//RIKTIGA, inte test
namespace TjuvOPolis

{
    using System.Threading;

    public class Program 
    {
        static string[,] myCity = new string[15, 60];

        static string[,] myPrison = new string[10, 20];

        static void Main(string[] args)
        {
            int numberPoliceOfficers = 10;
            int numberThiefs = 20;
            int numberCitizens = 30;

            Arrest arrest = new Arrest();
            Robbed robbed = new Robbed();

            List<Person> myTown = new List<Person>();
            List<Person> myPrisoners = new List<Person>();



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

                //Sätter ut punkter i min stad, skriver ut bokstäver samt visar min stad:
                City.ShowCity(myCity, myTown);
                Console.WriteLine();


                //Sätter ut punkter i mitt fängelse, skriver ut bokstav samt visar mitt fängelse:
                Prison.ShowPrison(myPrison, myPrisoners);

                Console.WriteLine();
                Console.WriteLine("-------------------------");

                City.MeetingLetters(myCity, myTown, myPrison, myPrisoners, arrest, robbed);

                

                Console.WriteLine();
                Console.ReadLine();
                //Thread.Sleep(1000);

            }

        }

        
    }
}
