using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Display
{
    class CreateList
    {
        // I want to read in a text file and 
        // put it into a list

        public List<string> WordList = new List<string>();

        public CreateList(string FileLocation) : this()
        {

            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(FileLocation);
            while ((line = file.ReadLine()) != null)
            {

                //This will Split the string into an array
                String[] temp = line.Split(',');

                foreach (String s in temp)
                {
                    Console.WriteLine(s);
                }

                WordList.Add(line);
            }

            file.Close();


        }

        public CreateList()
        {
            //Nothing Happens
        }

    }
}