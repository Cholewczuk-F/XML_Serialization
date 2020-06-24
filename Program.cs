using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random generator = new Random();
            List<Osoba> database = new List<Osoba>();
            for (int i = 0; i < 100; i++)
            {
                database.Add(new Osoba(i, generator.Next(0, 100)));
            }
            
            serializeUsers(database);
            database.Clear();
            Console.WriteLine("Database state after serialization: " + database.Count());

            database = deserializeUsers();
            Console.WriteLine("Database state after deserialization: " + database.Count());

            database.Sort((a, b) => a.wiek.CompareTo(b.wiek));
            saveAsText(database);
        }

        static void serializeUsers(List<Osoba> database)
        {
            StreamWriter writer = new StreamWriter("database.xml");
            using (writer)
            {
                new XmlSerializer(typeof(List<Osoba>)).Serialize(writer, database);
            }
        }

        static List<Osoba> deserializeUsers()
        {
            List<Osoba> database = new List<Osoba>();
            StreamReader reader = new StreamReader("database.xml");
            using(reader)
            {
                database = (List<Osoba>)new XmlSerializer(typeof(List<Osoba>)).Deserialize(reader);
            }
            
            return database;
        }

        static void saveAsText(List<Osoba> database)
        {
            StreamWriter writer = new StreamWriter("Osoby.txt");
            using(writer)
            {
                foreach(Osoba osoba in database)
                {
                    String line = osoba.id + ", " + osoba.imie + ", " + osoba.nazwisko + ", " + osoba.wiek;
                    writer.WriteLine(line);
                }
            }
            Console.WriteLine("Written file Osoby.txt");
        }
    }

    public class Osoba
    {
        public int id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public int wiek { get; set; }

        public Osoba(int Id, int Wiek)
        {
            id = Id;
            wiek = Wiek;
            imie = "Senor";
            nazwisko = "Papayagos";
        }

        public Osoba() { }
    }

    
}
