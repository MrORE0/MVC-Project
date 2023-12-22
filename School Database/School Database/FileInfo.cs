using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Database
{
   public class FileInfo
    {
        //potentila fixes
        public FileInfo()
        {

        }
        public FileInfo(string fileLocation)
        {
            FileLocation = fileLocation;
        }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        public byte[] FileBinaryData { get; set; }
        public string UploadDate { get; set; }
        public string PostedBy { get; set; }
        public string FileLocation { get; set; }

        //potential fix
        public object Length { get; internal set; }
    }
}
