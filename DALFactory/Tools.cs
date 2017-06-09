using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DALFactory
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// 保存xml到本地磁盘
        /// </summary>
        /// <param name="XmlText"></param>
        /// <param name="strxmlpath"></param>
        /// <param name="destname"></param>
        /// <param name="strMessageName"></param>
        /// <param name="strOther"></param>
        public static void SaveDownloadLog(string XmlText, string strxmlpath, string destname)
        {
            XmlText = XmlText.Replace("UTF-8", "gbk");
            XmlText = XmlText.Replace("utf-8", "gbk");
            byte[] log = System.Text.Encoding.Default.GetBytes(XmlText);
            string strSaveSendLogAddress = "";
            strSaveSendLogAddress = string.Format("{0}_{1}_{2}.xml", strxmlpath + DateTime.Now.ToString("yyyy-MM-dd") + '\\', destname,  DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());

            FileStream stream = null;

            string sendaddress = strxmlpath + DateTime.Now.ToString("yyyy-MM-dd") + '\\';

            if (!Directory.Exists(sendaddress) && !string.IsNullOrEmpty(sendaddress))
                Directory.CreateDirectory(sendaddress);
            try
            {
                stream = File.Open(strSaveSendLogAddress, System.IO.FileMode.OpenOrCreate);
                stream.Seek(0, System.IO.SeekOrigin.End);
                stream.Write(log, 0, log.Length);
            }
            catch
            {
                if (!Directory.Exists(@"D:\Log\Exception\"))
                    Directory.CreateDirectory(@"D:\Log\Exception\");
                strSaveSendLogAddress = "D:" + string.Format(@"\Log\Exception\{0}{1}.xml", DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Now.Millisecond.ToString());

                stream = File.Open(strSaveSendLogAddress, System.IO.FileMode.OpenOrCreate);
                stream.Seek(0, System.IO.SeekOrigin.End);
                stream.Write(log, 0, log.Length);
            }
            finally
            {
                stream.Close();
            }
        }
        /// <summary>
        /// 将datatable转换为对象列表返回
        /// </summary>
        /// <param name="strEntity">实体名</param>
        /// <param name="dt">数据集合</param>
        /// <returns>对象列表（对象数组）</returns>
        public ArrayList change(string strEntity, DataTable dt)
        {
            ArrayList al = new ArrayList();//创建列表
            //获取类型
            //Type tp = Type.GetType(string.Format("SMESItg.Entity.{0}, SMESItg.Entity", strEntity), false, true);
            Type tp = Type.GetType(string.Format("Entity.{0}, Entity", strEntity), false, true);
            foreach (DataRow dr in dt.Rows)
            {//遍历数据行
                object obj = Activator.CreateInstance(tp);//实例化对象
                PropertyInfo[] ps = tp.GetProperties();//获取对象属性
                for (int i = 0; i < dt.Columns.Count; i++)
                {//遍历结果集
                    foreach (PropertyInfo pi in ps)
                    {//遍历属性
                        if (pi.Name.ToUpper() == dt.Columns[i].ColumnName.ToUpper())
                        {//当结果集的指定行的指定列与属性相同时，对属性赋值
                            pi.SetValue(obj, dr[i].ToString(), null);
                        }
                    }
                }
                //对象赋值完成，添加到列表中
                al.Add(obj);
            }
            //返回对象列表
            return al;
        }
        ///// <summary>
        ///// 读取xml里边的参数配置
        ///// </summary>
        ///// <param name="para"></param>
        ///// <returns></returns>    
        //public static string readXMLParameter(string para)
        //{
        //    //获取相对路径的XML
        //    XmlDocument xmldocument = new XmlDocument();
        //    string path = System.AppDomain.CurrentDomain.BaseDirectory;
        //    string filePath = string.Format(@"{0}ConstInfo.xml", path);
        //    xmldocument.Load(filePath);
        //    string result = xmldocument.SelectSingleNode(@"configuration/" + para).InnerText;
        //    return result;
        //}
//        /// <summary>
//        /// 将MessagePackage对象序列化为xml文本
//        /// </summary>
//        /// <param name="messagepackage">MessagePackage对象</param>
//        /// <returns>xml文本</returns>
//        public static string SerializeObjectToXmlText(MessagePackage<T> messagepackage)
//        {
//            string strXml = "";

//            XmlSerializer xmlser = new XmlSerializer(typeof(MessagePackage<T>));
//            StringWriter writer = new StringWriter();
//            try
//            {
//                xmlser.Serialize(writer, messagepackage);
//                writer.GetStringBuilder().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\" encoding=\"gbk\"?>");//由于序列化后的文本为utf-16，需要替换为utf-8
//                writer.GetStringBuilder().Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");    //同时替换掉不相关的属性
//                writer.GetStringBuilder().Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");

//                strXml = writer.ToString();
//                writer.Close();
//            }
//            catch (Exception ex)
//            {
//                writer.Close();
//                throw ex;
//            }
//            return strXml;
//        }
//        /// <summary>
//        /// 将xml文本反序列化为MessagePackage对象
//        /// </summary>
//        /// <param name="XmlText">xml文本</param>
//        /// <returns>MessagePackage对象</returns>
//        public static MessagePackage<T> DeserializeXmlTextToObject(string XmlText)
//        {
//            MessagePackage<T> messagepackage;

//            XmlSerializer xmlser = new XmlSerializer(typeof(MessagePackage<T>));
//            TextReader tr = new StringReader(XmlText);

//            messagepackage = (MessagePackage<T>)xmlser.Deserialize(tr);

//            return messagepackage;
//        }
        /// <summary>
        /// 行转化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T DataRowToObject<T>(DataRow dr) where T : class
        {
            T obj = Activator.CreateInstance<T>();
            string columnName = "";
            foreach (DataColumn dc in dr.Table.Columns)
            {
                columnName = dc.ColumnName;
                try
                {
                    System.Reflection.PropertyInfo pinfo = obj.GetType().GetProperty(columnName.ToUpper());

                    if (pinfo == null)
                    {
                        pinfo = obj.GetType().GetProperty(columnName.ToLower());
                    }
                    //尝试不改变大小写 找实体类
                    if (pinfo == null)
                    {
                        pinfo = obj.GetType().GetProperty(columnName);
                    }
                    if (pinfo != null)
                    {
                        switch (pinfo.PropertyType.Name.ToLower())
                        {
                            case "string":
                                pinfo.SetValue(obj, dr[columnName].ToString(), null);
                                break;
                            case "double":
                                if (dr[columnName].ToString().Trim() == "")
                                    pinfo.SetValue(obj, 0, null);
                                else
                                    pinfo.SetValue(obj, double.Parse(dr[columnName].ToString()), null);
                                break;
                            case "decimal":
                                if (dr[columnName].ToString() == "")
                                    pinfo.SetValue(obj, 0m, null);
                                else
                                    pinfo.SetValue(obj, decimal.Parse(dr[columnName].ToString()), null);
                                break;
                            case "nullable`1":
                                if (dr[columnName].ToString() == "")
                                    pinfo.SetValue(obj, null, null);
                                else
                                    pinfo.SetValue(obj, dr[columnName], null);
                                break;
                            case "int32":
                                if (dr[columnName].ToString() == "")
                                    pinfo.SetValue(obj, 0, null);
                                else
                                    pinfo.SetValue(obj, int.Parse(dr[columnName].ToString()), null);
                                break;
                            default:
                                pinfo.SetValue(obj, dr[columnName], null);
                                break;
                        }
                    }
                }
                catch
                { }

                columnName = null;
            }

            return obj;
        }
        /// <summary>
        /// DataTable转换为实体类型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : class
        {
            List<T> list = new List<T>();
            if (dt == null || dt.Rows.Count == 0)
                return list;

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(DataRowToObject<T>(dr));
            }
            return list;
        }

  }
}
