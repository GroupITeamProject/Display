using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
/*                      _______________________________________________
                       |Code by Enda Sexton for team I(Team infusion)  |
  |---------------------------------------------------------------------------------------------------|
  |                 Intention of code is to open a text file, go through it                           |
  |            line by line and add new words to a dictionary. The new word is the                    |
  | key in the dictionary and then the value counts the number of times the word appears in the file. |
  |---------------------------------------------------------------------------------------------------|
 
*/

namespace Display
{
    class FileAnalyser
    {

        String fileLocation;

        List<String> LS;

        Dictionary<string, int> D;

        //Declaration of the dictionary.

        public FileAnalyser()
        {
            //empty constructor 
        }

        public FileAnalyser(String FL)
        {
            fileLocation = FL;
        }

        public Dictionary<string, int> getDictionary()
        {
            var dict = new Dictionary<string, int>();

            Console.WriteLine(File.Exists(fileLocation) ? "File exists." : "File does not exist.");
            using (StreamReader sr = new StreamReader(fileLocation))
            {
                string line;

                /*
                While loop to read in string by string from the text file once
                the ReadLine function doesn't return null
                */
                while ((line = sr.ReadLine()) != null)
                {
                    /*
                    Declaration of the string array "words" to hold individual words
                    from the split up line string. The line string is split up on the
                    space ascii number by using the .Split(' '); function. 
                    */

                    string[] words = line.Split(' ');

                    /*
                     foreach loop goes through each word in the array of words
                    */
                    foreach (var word in words)
                        /*
                         if/else statement to check if the current word appears in the dictionary already
                         and if it does it increments the value for said word, if not(or if else :) ) 
                         then it adds the word to the dictionary
                         */

                        //Checks if the "string" in word ONLY contains letters!!
                        if (new ValidString().isValid(word))
                        {

                            if (dict.ContainsKey(word))
                            {
                                dict[word]++;
                            }
                            else
                            {
                                dict.Add(word, 1);
                            }
                        }

                    }

                sr.Close();

                /*
                 Creates variable items, from the dictionary called dict
                 orders them based on highest value to key pair first.
                 */
                var items = from pair in dict
                            orderby pair.Value descending
                            select pair;

                //Because the items above are an ordered list, i 
                //add them too a new empty dictionary
                D = new Dictionary<String,int>();

                foreach (var entrys in items)
                {
                    D.Add(entrys.Key, entrys.Value);  
                }

                /*
                 Uses StreamWriter to write all the pairs of keys and values to a text file.
                 */
                using (StreamWriter writer = new StreamWriter("fileOutput.txt"))
                    foreach (var entry in items)

                        writer.WriteLine("{0},{1}", entry.Key, entry.Value);
            }

            return D;
        }
    }
}
