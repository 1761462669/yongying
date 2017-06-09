using DALFactory;
using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Business
{
    /// <summary>
    /// 获取定时服务调用配置
    /// </summary>
    public class _GetCallTarget
    {
        public void GetCall(out List<GetCallTarget> timeConfig)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlReaderSettings xrs = new XmlReaderSettings();
            xrs.IgnoreComments = true;//忽略注释
            XmlReader xmlReader = XmlReader.Create("..//..//TimeConfig.xml", xrs);
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            xmlDocument.Load(xmlReader);
            XmlNode xmlNode = xmlDocument.DocumentElement;
            XmlNodeList xmlNodeList = xmlNode.SelectNodes("data");
            timeConfig = new List<GetCallTarget>();
            DataTable dt = GetDataTableByXml(xmlNodeList);
            Tools tools = new Tools();
            ArrayList al = new ArrayList();
            al = tools.change("GetCallTarget", dt);
            foreach (GetCallTarget item in al)
            {
                timeConfig.Add(item);
            }
        }
        /// <summary>
        /// 通过xml节点获取DataTable
        /// </summary>
        /// <param name="xmlNodeList"></param>
        /// <returns></returns>
        public DataTable GetDataTableByXml(XmlNodeList xmlNodeList)
        {
            String columnName1 = "mark";
            String columnName2 = "time";
            DataTable dt = new DataTable();
            DataColumn[] columns = { 
                                  new DataColumn(columnName1,typeof(string)),
                                  new DataColumn(columnName2,typeof(DateTime))
                                                 };
            dt.Columns.AddRange(columns);
            foreach (XmlNode xn in xmlNodeList)
            {
                DataRow dr = dt.NewRow();
                String mark = "";
                String time = "";

                foreach (XmlNode xnode in xn.ChildNodes)
                {
                    switch (xnode.Name)
                    {
                        case "mark":
                            mark = xnode.InnerXml.Trim();
                            break;
                        case "time":
                            time = xnode.InnerXml.Trim();
                            break;
                        default:
                            break;
                    }
                }
                dr[columnName1] = mark;
                dr[columnName2] = time;
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
