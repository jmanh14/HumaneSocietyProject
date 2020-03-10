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
            // PointOfEntry.Run();
            Employee employee = new Employee();
            employee.EmployeeId = 10;
            //employee.FirstName = "J";
            //Query.RunEmployeeQueries(employee, "create");
            Query.RunEmployeeQueries(employee, "delete");
            
            
            //Laika still out here!
            Animal humainlyTestedAnimal = Query.GetAnimalByID(7)



        }  
          
            
        
    }
}
