using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new HumaneSocietyDataContext();
            //PointOfEntry.Run();
            var animal = db.Animals.Select(a => a).Where(b => b.Name == "Laika").FirstOrDefault();
            Query.GetShots(animal);
            Query.UpdateShot("Parvo", animal);
           
        }
    }
}
