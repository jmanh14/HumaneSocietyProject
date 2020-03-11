﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {        
        static HumaneSocietyDataContext db;

        static Query()
        {
            db = new HumaneSocietyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();       

            return allStates;
        }
            
        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            // find corresponding Client from Db
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }
            
            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if(updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;
            
            // submit changes
            db.SubmitChanges();
        }
        
        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName != null;
        }


        //// TODO Items: ////
        
        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            switch (crudOperation)
            {
                case ("update"):                   
                   var data = db.Employees.SingleOrDefault(a => a.EmployeeId == employee.EmployeeId);
                      if (data != null)
                      {
                          data.FirstName = employee.FirstName;
                          data.LastName = employee.LastName;
                          data.EmployeeNumber = employee.EmployeeNumber;
                          data.Email = employee.Email;
                      }
                      db.SubmitChanges();                 
                    break;
                case ("read"):
                    var employeeData = db.Employees.Select(a => a).Where(b => b.EmployeeId == employee.EmployeeId);
                    break;
                case ("delete"):
                    var employeeToDelete = db.Employees.Where(a => a.EmployeeId == employee.EmployeeId).FirstOrDefault();
                    db.Employees.DeleteOnSubmit(employeeToDelete);
                    db.SubmitChanges();
                    break;
                case ("create"):
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    break;

            }
        }

        // TODO: Animal CRUD Operations
        internal static void AddAnimal(Animal animal)
        {
            //Add An Animal to the Database;
            var data = db.Animals.Where(e => e.AnimalId == animal.AnimalId && e.Name == animal.Name).FirstOrDefault();
            //Check if the animal is already in the database;
            if (data == null)
            {
                db.Animals.InsertOnSubmit(animal);
                db.SubmitChanges();
            }
            else 
            {
                throw new OperationCanceledException();
                //Animal ID && Name Fed in was alreay in DATABASE;
            }
            //add if not;
            //Throw Exception if it is.
        }

        internal static Animal GetAnimalByID(int id)
        {
            var animalToReturnFromDB = db.Animals.Where(e => e.AnimalId == id).FirstOrDefault();
            return animalToReturnFromDB;
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {

            foreach (KeyValuePair<int, string> keyValuePair in updates) 
            {
                var animalObject = db.Animals.Where(e => e.AnimalId == animalId).FirstOrDefault();//Get An Animal Object.
                //Commands come from line 187: User Employee 
                //Paramaters for cases come from: Line 193 in user Interface. 
                //this is where we update for every animal.
                switch (keyValuePair.Key)
                {
                    //Update: Param, New_Value(Dictionary Value)
                    case 1://Category
                        var categoryID = GetCategoryId(keyValuePair.Value); // Get the ID Of the animal.
                        animalObject.CategoryId = categoryID;
                        break;
                    case 2://Name, New_Name, 
                        var name = keyValuePair.Value;
                        animalObject.Name = name;
                        break;
                    case 3://Age
                        var newAge = Convert.ToInt32(keyValuePair.Value);
                        animalObject.Age = newAge;
                        break;
                    case 4://Demeanor
                        var newDemeanor = keyValuePair.Value;
                        animalObject.Demeanor = newDemeanor;
                        break;
                    case 5://Kid Friendly, True/False.ToString(),
                        var KidFriendlyStatus = keyValuePair.Value;
                        animalObject.KidFriendly = Convert.ToBoolean(KidFriendlyStatus);
                        break;
                    case 6://Pet Friendly, T/F.ToString()
                        var PetFriendlyStatus = keyValuePair.Value;
                        animalObject.PetFriendly = Convert.ToBoolean(PetFriendlyStatus);
                        break;
                    case 7://Weight DictionaryVal= String
                        var newWeight = Convert.ToInt32(keyValuePair.Value);
                        animalObject.Weight = newWeight;
                        break;
                }
                db.SubmitChanges();
                //TODO: FIGURE OUT WHAT TO RUN WITH THE DICTIONARY VALUES.
            }
        }

        internal static void RemoveAnimal(Animal animal)
        {
            //check if animal is in DB
            var animalToRemove = db.Animals.Where(e => e.AnimalId == animal.AnimalId && e.Name == animal.Name).FirstOrDefault();

            //if not null remove
            if (animalToRemove != null) 
            {
                db.Animals.DeleteOnSubmit(animalToRemove);
                db.SubmitChanges();
            }
            else { throw new ArgumentNullException(); }
        }
        
        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {
            throw new NotImplementedException();
        }
         
        // TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {
            //Get the animals Category; 
            int categoryIDfromDB = 0;
            var data = db.Categories.Where(e => e.Name == categoryName);
            if (data != null)
            {
                categoryIDfromDB = data.Select(e => e.CategoryId).FirstOrDefault();
            }
            else 
            {
                throw new ArgumentNullException();
            }
            return categoryIDfromDB;
        }
        
        internal static Room GetRoom(int animalId)
        {
            throw new NotImplementedException();
        }
        
        internal static int GetDietPlanId(string dietPlanName)
        {
            throw new NotImplementedException();
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            throw new NotImplementedException();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            throw new NotImplementedException();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}