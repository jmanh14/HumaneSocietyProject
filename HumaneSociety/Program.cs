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
<<<<<<< HEAD
            var db = new HumaneSocietyDataContext();
            //PointOfEntry.Run();
            var animal = db.Animals.Select(a => a).Where(b => b.Name == "Laika").FirstOrDefault();
            Query.GetShots(animal);
            Query.UpdateShot("Parvo", animal);
           
        }
=======
            // PointOfEntry.Run();

            //Laika still out here!
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(8, "12");

            Query.UpdateAnimal(7, dict);

        }  
          
            
        
>>>>>>> 7862f7729ce0439276ad98b14bd6e31c15101cce
    }
}
