using System.Collections.Generic;

namespace PaperRulez.Interfaces
{
    public interface IFileHelper
    {
        string FileName();
        /// <summary>
        /// Return an array with all lines
        /// </summary>
        /// <returns></returns>
        string[] ReadAllLines();
        /// <summary>
        /// Returns the first line of the file
        /// </summary>
        /// <returns></returns>
        string ReadFirstLine();
        /// <summary>
        /// Returns a block of the file content
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> ReadBlock();
        /// <summary>
        /// Remove file
        /// </summary>
        void Remove();
    }
}