using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LexiconHangman
{
    public static class WordCollection
    {
        public static string[] WordArray => arrayOfAnimalWords;

        static readonly string[] arrayOfAnimalWords = File.ReadAllText(Environment.CurrentDirectory + "/animals.csv").Split(',');

        public static string GetStringFromWordArrayByIndex(int index)
        {
            return arrayOfAnimalWords[index];
        }
       
    }

}
