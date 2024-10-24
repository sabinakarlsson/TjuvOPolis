﻿using static TjuvOPolis.Person;


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


                Console.WriteLine("-------------------------");
                Console.WriteLine();

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
                Console.WriteLine();
                Console.WriteLine("-------------------------");
                


                // Kolla om någon bokstav möts
                for (int i = 0; i < myTown.Count; i++)
                {
                    for (int j = i + 1; j < myTown.Count; j++)
                    {
                        if (myTown[i].PlacementY == myTown[j].PlacementY && myTown[i].PlacementX == myTown[j].PlacementX)
                        {
                            //om polisraden möter tjuvkolumnen
                            if (myTown[i] is Police && myTown[j] is Thief)
                            {
                                Console.Write(((Police)myTown[i]).Name + " och " + ((Thief)myTown[j]).Name + " möttes.");
                                if (((Thief)myTown[j]).StolenProperty.Count > 0)
                                {
                                    Police.Confiscate((Police)myTown[i], (Thief)myTown[j]);
                                    arrest.ShowArrest((Thief)myTown[j]);
                                    myTown.RemoveAt(j);
                                    myPrisoners.Add(myTown[j]);
                                }

                                else
                                {
                                    Console.WriteLine(" Polisen genomsökte " + ((Thief)myTown[j]).Name + ", men hen hade inga stulna värdesaker på sig (denna gång..)");
                                }
                            }

                            //NÖDVÄNDIG?? om tjuvraden möter poliskolumnen
                            if (myTown[j] is Police && myTown[i] is Thief)
                            {
                                Console.Write(((Police)myTown[j]).Name + " och " + ((Thief)myTown[i]).Name + " möttes.");

                                if (((Thief)myTown[i]).StolenProperty.Count > 0)
                                {
                                    Police.Confiscate((Police)myTown[j], (Thief)myTown[i]);
                                    arrest.ShowArrest((Thief)myTown[i]);
                                    myTown.RemoveAt(i);
                                    myPrisoners.Add((Thief)myTown[i]);
                                }

                                else
                                {
                                    Console.WriteLine(" Polisen genomsökte " + ((Thief)myTown[i]).Name + ", men hen hade inga stulna värdesaker på sig (denna gång..)");
                                }
                            }

                            //om tjuvraden möter medborgarkolumnen
                            if (myTown[i] is Thief && myTown[j] is Citizen)
                            {
                                Console.Write(((Thief)myTown[i]).Name + " och " + ((Citizen)myTown[j]).Name + " möttes.");

                                if (((Citizen)myTown[j]).PropertyInPossession.Count > 0)
                                {
                                    Thief.Steal((Citizen)myTown[j], (Thief)myTown[i]);
                                    robbed.ShowRobbed((Thief)myTown[i]);
                                }

                                else
                                {
                                    Console.WriteLine(((Thief)myTown[i]).Name + " försökte råna " + ((Citizen)myTown[j]).Name + ", men hen har redan blivit bestulen på alla sina värdesaker..");
                                }
                            }

                            //NÖDVÄNDIG?? om tjuvkolumnen möter medborgarraden
                            if (myTown[j] is Thief && myTown[i] is Citizen)
                            {
                                Console.Write(((Thief)myTown[j]).Name + " och " + ((Citizen)myTown[i]).Name + " möttes.");

                                if (((Citizen)myTown[i]).PropertyInPossession.Count > 0)
                                {
                                    Thief.Steal((Citizen)myTown[i], (Thief)myTown[j]);
                                    robbed.ShowRobbed((Thief)myTown[j]);
                                }

                                else
                                {
                                    Console.WriteLine(((Thief)myTown[j]).Name + " försökte råna " + ((Citizen)myTown[i]).Name + ", men hen har redan blivit bestulen på alla sina värdesaker..");
                                }
                            }

                            //om medborgarraden möter poliskolumnen
                            if (myTown[i] is Citizen && myTown[j] is Police)
                            {
                                Console.WriteLine(((Citizen)myTown[i]).Name + " och " + ((Police)myTown[j]).Name + " möttes. De hälsade på varandra");
                            }

                            //NÖDVÄNDIG?? om medborgarkolumnen möter polisraden
                            if (myTown[j] is Citizen && myTown[i] is Police)
                            {
                                Console.WriteLine(((Citizen)myTown[j]).Name + " och " + ((Police)myTown[i]).Name + " möttes. De hälsade på varandra");
                            }

                        }
                    }
                }
                
                


                Console.WriteLine();
                Console.WriteLine("Antal gripna: " + arrest.NumberArrested);
                Console.WriteLine("Antal rånade: " + robbed.NumberRobbed);
                Thread.Sleep(1000);

            }

        }

        
    }
}
