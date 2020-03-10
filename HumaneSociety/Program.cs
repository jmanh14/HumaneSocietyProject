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
            // PointOfEntry.Run();
            Employee employee = new Employee();
            employee.EmployeeId = 10;
            //employee.FirstName = "J";
            //Query.RunEmployeeQueries(employee, "create");
            Query.RunEmployeeQueries(employee, "delete");
        }  
=======
            //PointOfEntry.Run();


            //Laika still out here!
            Animal humainlyTestedAnimal = new Animal();
            humainlyTestedAnimal.Gender = "Male";
            humainlyTestedAnimal.Age = 10;
            humainlyTestedAnimal.Name = "Laika";
            humainlyTestedAnimal.AnimalId = 13;

            Query.RemoveAnimal(humainlyTestedAnimal);
        }
>>>>>>> 7162cf9f71e5d0452e734462fd26f8a9f3af0324
    }
}
