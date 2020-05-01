using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIN.Common
{
    public class FileUtil
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            string str = "";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    str += line;
                }
            }
            return str;
        }

        public static void WriteFile(string path, string context)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(fs);
                writer.WriteLine(context);
                writer.Flush();
                writer.Close();
            }
        }
    }
}
