using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOPolis
{
    public class Arrest
    {
        public int NumberArrested { get; set; }

        public Arrest()
        {
            NumberArrested = 0;
        }


        public void ShowArrest(Person person)
        {
            //hur många som blivit arresterade
            NumberArrested++;
            Console.WriteLine("Antal gripna tjuvar: " + NumberArrested);
        }
    }
}
