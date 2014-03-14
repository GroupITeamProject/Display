using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//Brian. This class is used to check if a string has invaild chars in it EXAMPLE such as @#$%^&.
//Call it using "new ValidString().isValid(YOURSTRINGGOESHERE)

namespace Display
{
    public class ValidString
    {
        public ValidString()
        {

        }

        public bool isValid(string S)
        {
            String newstring = S;

            char[] chararray;

            bool pass = true;

            chararray = newstring.ToCharArray();

            for (int i = 0; i < newstring.Length; i++)
            {

                int check = ((int)chararray[i]);

                if (check >= 65 && check <= 90 || check >= 97 && check <= 122)
                {
                    //then it is a vaild letter
                }
                else
                {
                    pass = false;
                }


            }

            return pass;
        }
    }
}

