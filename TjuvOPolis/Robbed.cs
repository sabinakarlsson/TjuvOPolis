using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOPolis
{
    public class Robbed
    {
        public int NumberRobbed { get; set; }

        public Robbed()
        {
            NumberRobbed = 0;
        }


        public void ShowRobbed(Person person)
        {
            //hur många som blivit rånade
            NumberRobbed++;
            Console.WriteLine("Antal rånade medborgare: " + NumberRobbed);

        }
    }
}
