using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labWork3.Core;

namespace labWork3
{
    internal class Program
    {
        static ContactRepository? repository;
        static void Main(string[] args)
        {
            while (true)
            {
                string path;
                RepositoryType repositoryType;

                PrintRepositoryChoose();
                try
                {
                    var ch = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Input the source file path, or press enter to use default path.");
                    path = Console.ReadLine() ?? "";
                    
                    switch (ch)
                    {
                        case 1:
                            repositoryType = RepositoryType.JSON;
                            break;
                        case 2:
                            repositoryType = RepositoryType.XML;
                            break;
                        //case 3:
                            //AddContact();
                         //   break;
                       // case 4:
                      //      return;
                        default:
                            throw new FormatException();
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine("Incorrect input. Please, try again.");
                    continue;
                }

                repository = ContactRepository.CreateRepository(repositoryType, path);
                break;
            }
        
            while (true) 
            {
                PrintMenu();
                try
                {
                    var ch = Convert.ToInt32(Console.ReadLine());
                    switch (ch)
                    {
                        case 1:
                            PrintAllContacts();
                            break;
                        case 2:
                            FindContacts();
                            break;
                        case 3:
                            AddContact();
                            break;
                        case 4:
                            return;
                        default:
                            throw new FormatException();                         
                    }
                }
                catch 
                {
                    Console.WriteLine("Incorrect input. Please, try again.");
                    continue;
                }              
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("Enter the number of action and press [Enter]. Thn follow instructions.");
            Console.WriteLine("Menu:");
            Console.WriteLine("1.View all contacts");
            Console.WriteLine("2.Search");
            Console.WriteLine("3.New contact");
            Console.WriteLine("4.Exit");
        }

        private static void PrintRepositoryChoose()
        {
            Console.WriteLine("Enter the number of contact repository type and press [Enter]");
            Console.WriteLine("1.JSON");
            Console.WriteLine("2.XML");
            Console.WriteLine("3.DataBase");
            Console.WriteLine("4.Exit");
        }

        private static void PrintAllContacts() 
        {
            var contacts = repository.GetAllContacts();
            PrintContactsList(contacts);
        }

        private static void AddContact()
        {
            var firstname = NotEmptyInput("Enter firstname: ");
            var lastname = NotEmptyInput("Enter lastname: ");
            var phoneNumber = NotEmptyInput("Enter phone number: ");
            var email = NotEmptyInput("Enter e-mail: ");
            Contact contact = new Contact(firstname,lastname,phoneNumber,email);
            repository.AddContact(contact);
            Console.WriteLine("Contact added.");
        }

        private static string NotEmptyInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                var s = Console.ReadLine();
                if (s.Length == 0) 
                {
                    Console.WriteLine("The field should not be empty.");
                    continue;
                }
                    
                return s;
            }
            
        }

        private static void PrintContactsList(List<Contact> contacts)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("Contacts not found.");
                return;
            }
            foreach (Contact c in contacts)
            {
                Console.WriteLine(c);
            }
        }

        private static void FindContacts()
        {
            while (true)
            {
                Console.WriteLine("Select a search type:\n" +
                    "1.By firstname:\n" +
                    "2.By lastname\n" +
                    "3.By firstname and lastname\n" +
                    "4.By phone number\n" +
                    "5.By Email.\n" +
                    "6.Search through whole fields");
                try
                {
                    var ch = Convert.ToInt32(Console.ReadLine());
                    switch (ch)
                    {
                        case 1:
                            FindByFirstname();
                            return;
                        case 2:
                            FindByLastname();
                            return;
                        case 3:
                            FindByFirstAndLastname();
                            return;
                        case 4:
                            FindByPhoneNumber();
                            return;
                        case 5:
                            FindByEmail();
                            return;
                        case 6:
                            FindByWholeFields();
                            return;
                        default:
                            throw new FormatException();
                    }
                }
                catch
                {
                    Console.WriteLine("Incorrect input. Try again.");
                }
            }
        }

        private static void FindByFirstname() 
        {
            var firstname = NotEmptyInput("Enter firstname or its port: ");
            var resultSet =  repository.FindByFirstname(firstname);
            PrintContactsList(resultSet);
        }
        private static void FindByLastname()
        {
            var lastname = NotEmptyInput("Enter lastname or its port: ");
            var resultSet = repository.FindByLastname(lastname);
            PrintContactsList(resultSet);
        }
        private static void FindByFirstAndLastname()
        {
            var firstname = NotEmptyInput("Enter firstname or its port: ");
            var lastname = NotEmptyInput("Enter lastname or its port");
            var resultSet = repository.FindByFullname(firstname, lastname);
            PrintContactsList(resultSet);
        }

        private static void FindByPhoneNumber() 
        {
            var phoneNumber = NotEmptyInput("Enter phone number or its part: ");
            var resultSet = repository.FindByPhoneNumber(phoneNumber);
            PrintContactsList(resultSet);
        }
        private static void FindByEmail()
        {
            var email = NotEmptyInput("Enter email or its part: ");
            var resultSet = repository.FindByEmail(email);
            PrintContactsList(resultSet);
        }

        private static void FindByWholeFields()
        {
            var any = NotEmptyInput("Enter a part of any field: ");
            var resultSet = repository.FindByAnyField(any);
            PrintContactsList(resultSet);
        }

    }
}
