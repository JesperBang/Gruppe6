using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Model
{
    class PSystem : Panel
    {

        public static PSystem createFromFile(String filePath)
        {
            System.IO.StreamReader file;
            try {
                file = new System.IO.StreamReader(filePath);
            }
            catch (System.IO.IOException)
            {
                return null;
            }

            String text = file.ReadToEnd();
            file.Close();
            String[] lines;
            if(text != null)
            {

                lines = text.Split(Environment.NewLine.ToArray()); //splits the lines in to seperate strings in the array.
                for(int l=6; l<lines.Length; l += 6)
                {

                }

            }

        }

    }
}
