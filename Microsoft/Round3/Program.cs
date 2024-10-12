using System;
using System.Collections.Generic;

namespace Round3
{
    public class Solution {
        public static void Main(string[] args) {
            var contacts = new List<Contact>();
            contacts.Add(new Contact() { ID = 1, Name="Jonathan", Emails = new List<string>() {"one", "two"}} );
            contacts.Add(new Contact() { ID = 2, Name="Antenehe", Emails = new List<string>() {"tree", "two", "five"}} );
            contacts.Add(new Contact() { ID = 3, Name="Nikita", Emails = new List<string>() {"four", "five"}} );
            // contact id 1 //3
            var contactlist = UniqueContacts(contacts);
            foreach(Contact c in contactlist)
                Console.WriteLine($"{c.Name}");

        }
        public static List<Contact> UniqueContacts(List<Contact> contacts) {
            var result = new List<Contact>();
            //                     email
            var storage = new HashSet<string>();
            foreach(Contact c in contacts){
                bool canAdd = true;
                foreach(string email in c.Emails) {
                    if(storage.Contains(email)){
                        canAdd = false;
                        break;
                    }
                }
                if(canAdd) {
                    foreach(string email in c.Emails) { 
                        storage.Add(email);
                    }
                    result.Add(c);
                }
            }
            return result;
        }
    }

    public class Contact {
        public int ID {get;set;}
        public string Name {get;set;}
        public List<string> Emails {get;set;}
    }

}

