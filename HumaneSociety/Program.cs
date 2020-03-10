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
            //PointOfEntry.Run();


            //Laika still out here!
            Animal humainlyTestedAnimal = new Animal();
            humainlyTestedAnimal.Gender = "Male";
            humainlyTestedAnimal.Age = 10;
            humainlyTestedAnimal.Name = "Laika";

            Query.AddAnimal(humainlyTestedAnimal);
        }
    }
}
