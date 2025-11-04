using System;
using System.IO;

namespace Vim.Format
{
    public class VIM
    {
        public VIM()
        {
            
        }

        /// <summary>
        /// Opens the VIM file defined in the given seekable Stream.
        /// </summary>
        public static VIM Open(Stream stream)
        {
            if (!stream.CanSeek)
                throw new InvalidOperationException("Could not open VIM file. Stream must be seekable.");

            var vim = new VIM();



            return vim;
        }

        /// <summary>
        /// Opens the VIM file defined at the given file path.
        /// </summary>
        public static VIM Open(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("VIM file not found", filePath);

            using (var fileStream = File.OpenRead(filePath))
            {
                return Open(fileStream);
            }
        }


    }
    
}

