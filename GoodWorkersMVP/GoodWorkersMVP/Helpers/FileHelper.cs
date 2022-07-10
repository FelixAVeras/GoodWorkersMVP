using System.IO;

namespace GoodWorkersMVP.Helpers
{
    public class FileHelper
    {
        public static byte[] ReadFull(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
