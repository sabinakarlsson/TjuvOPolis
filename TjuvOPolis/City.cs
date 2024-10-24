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




        public static void MeetingLetters(string[,] myCity, List<Person> myTown, string[,] myPrison, List<Person> myPrisoners, Arrest arrest, Robbed robbed)
        {


            // Kolla om någon bokstav möts
            for (int i = 0; i < myTown.Count; i++)
            {
                for (int j = i + 1; j < myTown.Count; j++)
                {
                    if (myTown[i].PlacementY == myTown[j].PlacementY && myTown[i].PlacementX == myTown[j].PlacementX)
                    {
                        //om polisRaden möter tjuvKolumnen
                        if (myTown[i] is Police && myTown[j] is Thief)
                        {
                            Console.Write(((Police)myTown[i]).Name + " och " + ((Thief)myTown[j]).Name + " möttes.");
                            if (((Thief)myTown[j]).StolenProperty.Count > 0)
                            {
                                Police.Confiscate((Police)myTown[i], (Thief)myTown[j]);
                                arrest.ShowArrest((Thief)myTown[j]);
                                myTown.RemoveAt(j);
                                myPrisoners.Add(myTown[j]);
                                Console.WriteLine();
                            }

                            else if (((Thief)myTown[j]).StolenProperty.Count == 0)
                            {
                                Console.WriteLine(" Polisen genomsökte " + ((Thief)myTown[j]).Name + ", men hen hade inga stulna värdesaker på sig (denna gång..)");
                                Console.WriteLine();
                            }
                        }

                        //NÖDVÄNDIG?? Exakt som ovan men istället om tjuvRaden möter polisKolumnen
                        if (myTown[j] is Police && myTown[i] is Thief)
                        {
                            Console.Write(((Police)myTown[j]).Name + " och " + ((Thief)myTown[i]).Name + " möttes.");

                            if (((Thief)myTown[i]).StolenProperty.Count > 0)
                            {
                                Police.Confiscate((Police)myTown[j], (Thief)myTown[i]);
                                arrest.ShowArrest((Thief)myTown[i]);
                                myTown.RemoveAt(i);
                                myPrisoners.Add((Thief)myTown[i]);
                                Console.WriteLine();
                            }

                            else if (((Thief)myTown[i]).StolenProperty.Count == 0)
                            {
                                Console.WriteLine(" Polisen genomsökte " + ((Thief)myTown[i]).Name + ", men hen hade inga stulna värdesaker på sig (denna gång..)");
                                Console.WriteLine();
                            }
                        }

                        //om tjuvRaden möter medborgarKolumnen
                        if (myTown[i] is Thief && myTown[j] is Citizen)
                        {
                            Console.Write(((Thief)myTown[i]).Name + " och " + ((Citizen)myTown[j]).Name + " möttes.");

                            if (((Citizen)myTown[j]).PropertyInPossession.Count > 0)
                            {
                                Thief.Steal((Citizen)myTown[j], (Thief)myTown[i]);
                                robbed.ShowRobbed((Thief)myTown[i]);
                                Console.WriteLine();
                            }

                            else if (((Citizen)myTown[j]).PropertyInPossession.Count == 0)
                            {
                                Console.WriteLine(((Thief)myTown[i]).Name + " försökte råna " + ((Citizen)myTown[j]).Name + ", men hen har redan blivit bestulen på alla sina värdesaker..");
                                Console.WriteLine();
                            }
                        }

                        //NÖDVÄNDIG?? Exakt som ovan men istället om tjuvKolumnen möter medborgarRaden
                        if (myTown[j] is Thief && myTown[i] is Citizen)
                        {
                            Console.Write(((Thief)myTown[j]).Name + " och " + ((Citizen)myTown[i]).Name + " möttes.");

                            if (((Citizen)myTown[i]).PropertyInPossession.Count > 0)
                            {
                                Thief.Steal((Citizen)myTown[i], (Thief)myTown[j]);
                                robbed.ShowRobbed((Thief)myTown[j]);
                                Console.WriteLine();
                            }

                            else if (((Citizen)myTown[i]).PropertyInPossession.Count == 0)
                            {
                                Console.WriteLine(((Thief)myTown[j]).Name + " försökte råna " + ((Citizen)myTown[i]).Name + ", men hen har redan blivit bestulen på alla sina värdesaker..");
                                Console.WriteLine();
                            }
                        }

                        //om medborgarRaden möter polisKolumnen
                        if (myTown[i] is Citizen && myTown[j] is Police)
                        {
                            Console.WriteLine(((Citizen)myTown[i]).Name + " och " + ((Police)myTown[j]).Name + " möttes. De hälsade på varandra");
                            Console.WriteLine();
                        }

                        //NÖDVÄNDIG?? Exakt som ovan men istället om medborgarKolumnen möter polisRaden
                        if (myTown[j] is Citizen && myTown[i] is Police)
                        {
                            Console.WriteLine(((Citizen)myTown[j]).Name + " och " + ((Police)myTown[i]).Name + " möttes. De hälsade på varandra");
                            Console.WriteLine();
                        }

                    }
                }
            }
            Console.WriteLine("-------------------------");
            Console.WriteLine("Antal gripna: " + arrest.NumberArrested);
            Console.WriteLine("Antal rånade: " + robbed.NumberRobbed);
        }
    }
}
