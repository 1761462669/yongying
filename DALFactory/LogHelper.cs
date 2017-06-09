using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QcAnalysis
{
    public class LogHelper
    {
        /// <summary>
        /// 日志文件名（日期部分）
        /// </summary>
        public static string logDate = string.Format("{0}_{1}_{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="errormsg"></param>
        public static void WriteLog(string path,string context)
        {
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(string.Format("发生时间:{0},记录内容:{1}.", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), context));
            sw.Close();
        }
        /// <summary>
        /// 保存内容到文件
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="msg">文件内容</param>
        public static void SaveMsgToFile(string SavePath, string FileName, string FileMsg)
        {
            //获取要保存的路径
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
            //如果文件夹不存在则创建
            string FullPath = SavePath + '\\' + FileName;
            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
            using (StreamWriter StrWriter = File.AppendText(FullPath))
            {
                StrWriter.Write(FileMsg);
                StrWriter.Close();
            }
        }
    }
}
