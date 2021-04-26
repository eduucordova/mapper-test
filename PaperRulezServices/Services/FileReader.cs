using PaperRulez.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PaperRulez.Services
{
    public class FileReader : IFileHelper
    {
        private readonly string _path;

        public FileReader(string path)
        {
            _path = path;
        }

        public string[] ReadAllLines()
        {
            return File.ReadAllLines(_path);
        }

        public string ReadFirstLine()
        {
            return File.ReadLines(_path).First();
        }

        // Using stream to read file because we don't the size of it
        // the array size of 4096 was just for testing, we can later define in an external config file
        public IEnumerable<string> ReadBlock()
        {
            using (Stream stream = ReadStream())
            using (StreamReader sr = new StreamReader(stream))
            {
                //Ignores first line of the file
                sr.ReadLine();

                char[] buffer = new char[4096];

                for (int len = sr.Read(buffer, 0, 4096); len != 0; len = sr.Read(buffer, 0, 4096))
                {
                    yield return new String(buffer);
                    Array.Clear(buffer, 0, len);
                }
            }
        }

        public string FileName()
        {
            return Path.GetFileName(_path);
        }

        public void Remove()
        {
            if (File.Exists(_path))
                File.Delete(_path);
        }

        private Stream ReadStream()
        {
            return new FileStream(_path, FileMode.Open);
        }
    }
}