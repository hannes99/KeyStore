using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyPairStore
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyPairManager km = new KeyPairManager(@"C:\\Users\\Hannes Laimer\\source\\repos\\KeyPairStore\\KeyPairStore\\store.txt");
            var input = Console.ReadLine();
            while(input != "exit")
            {
                var val = "";
                var key = "";
                switch (input)
                {
                    case "get":
                        Console.Write("Enter key: ");
                        val = km.Get(Console.ReadLine());
                        Console.WriteLine("\nValue = " + val);
                        break;

                    case "put":
                        Console.Write("Enter key: ");
                        key = Console.ReadLine();
                        Console.Write("\nEnter value:");
                        val = Console.ReadLine();
                        km.Put(key, val);
                        break;

                    case "del":
                        Console.Write("Enter key: ");
                        key = Console.ReadLine();
                        km.Del(key);
                        break;

                    case "find":
                        Console.Write("Enter value to search for: ");
                        val = Console.ReadLine();
                        var count = 0;
                        foreach (String k in km.Find(val))
                        {
                            Console.WriteLine("   " + k + " -> " + km.Get(k));
                            count++;
                        }
                        Console.WriteLine(count + " Elements found!");
                        break;
                }
                Console.WriteLine("Enter command\n");
                input = Console.ReadLine();
            }
            km.Put("plapladdpla", "abc");
            Console.ReadLine();
        }
    }

    class KeyPairManager
    {
        private String path;
        private Dictionary<String, String> loaded_keypairs = new Dictionary<String, String>();
        public KeyPairManager(String path)
        {
            this.path = path;
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach(String l in lines)
            {
                var splitted = l.Split(':');
                var key = splitted[0];
                var val = splitted[1];
                if(!loaded_keypairs.Keys.Contains(key))
                    loaded_keypairs.Add(key, val);
            } 
        }

        public void Put(String key, String val)
        {
            if (loaded_keypairs.Keys.Contains(key))
                loaded_keypairs[key] = val;
            else
                loaded_keypairs.Add(key, val);
            this.Save();
        }

        public String Get(String key)
        {
            return loaded_keypairs[key];
        }

        public List<String> Find(String with)
        {
            var ret = new List<String>();
            foreach(String key in loaded_keypairs.Keys)
            {
                if (loaded_keypairs[key].Contains(with)){
                    ret.Add(key);
                }
            }
            return ret;
        }

        public void Del(String key)
        {
            loaded_keypairs.Remove(key);
            Save();
        }

        public void Save()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(path);
            foreach(String key in loaded_keypairs.Keys)
            {
                file.WriteLine(key + ":" + loaded_keypairs[key]);
            }
            file.Close();
        }
    }
}
