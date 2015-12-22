using System;
using System.IO;

namespace InformationMachineAPI.PCL.Http.Client
{
    /// <summary>
    /// An DTO class to capture information for file uploads
    /// </summary>
    public class FileStreamInfo
    {
        public Stream FileStream { get; set; }
        public String FileName { get; set; }

        public FileStreamInfo(Stream stream, String fileName = null)
        {
            FileStream = stream;
            FileName = fileName;
        }
    }
}
