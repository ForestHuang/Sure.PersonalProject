namespace Sure.PersonalProject.Web.Controllers
{
    /*---------------------------------------------------------------------
     [author]:senlin.huang   
     [time]:2017-9-21
     [explain]: GeneratedFileController 生成对应的Model、DAL、BLL
     -----------------------------------------------------------------------*/
    using Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Collections;
    using Newtonsoft.Json;
    using Utilities.Enum;
    using System.IO;
    using System.Text;
    using Contract;
    using System.ServiceModel;
    using System.Web.Script.Serialization;

    public class GeneratedFileController : Controller
    {
        #region Private 属性

        /// <summary>
        /// log4net 配置文件路径
        /// </summary>
        private static string log4netPath = string.Empty;

        /// <summary>
        /// 创建 DBHelper 对象
        /// </summary>
        private static DBGeneral dbHelper = new DBGeneral();

        /// <summary>
        /// 创建 WCF 服务对象
        /// </summary>
        private static ChannelFactory<ISURE_GENERATED> ISURE_GENERATEDChannelFactory = new ChannelFactory<ISURE_GENERATED>("BasicHttpBinding_SURE_GENERATED");

        /// <summary>
        /// WCF 接口对象
        /// </summary>
        private static ISURE_GENERATED proxy = null;


        #endregion
       
        //主页
        public ActionResult GeneratedFile()
        {
            log4netPath = Server.MapPath("~/App_Data/log4net/log4net.config");
            if (ISURE_GENERATEDChannelFactory.State != CommunicationState.Opened)
                ISURE_GENERATEDChannelFactory.Open();
            proxy = ISURE_GENERATEDChannelFactory.CreateChannel();
            return View();
        }

        #region 登录到数据库

        //登录到数据库
        public JsonResult LoginDB(string userAccount, string userPwd, string localIP)
        {
            try
            {
                string strConnction = string.Concat(new string[] { "SERVER=", localIP, ";DATABASE=MASTER;UID=", userAccount, ";PWD=", userPwd });
                string sqlStr = "SELECT NAME FROM SYSDATABASES ORDER BY NAME";
                string message = (string)null;
                ArrayList dataByConAndSql = dbHelper.GetDataByConAndSql(strConnction, sqlStr, out message);
                if (dataByConAndSql.Count <= 0) { return Json(new { message = "Fail", content = "没有对应的数据库 ！ " }); }
                else
                {
                    //暂时存入缓存
                    CacheGeneral.SetCache("key_userAccount", userAccount);
                    CacheGeneral.SetCache("key_userPwd", userPwd);
                    CacheGeneral.SetCache("key_localIP", localIP);
                    return Json(new { message = "Success", content = JsonConvert.SerializeObject(dataByConAndSql) });
                }
            }
            catch (Exception ex)
            {
                //报错记录错误
                Log4net.log4netCreate(typeof(GeneratedFileController), log4netPath)
                    .Error("LoginDB(string userAccount, string userPwd, string localIP) ----> " + ex.Message);
                return Json(new { message = "Fail", content = "登录失败 ！ " });
            }
        }

        #endregion

        #region 根据数据库DB查询表Table

        //根据数据库联动表名
        public JsonResult LoginTable(string localDB)
        {
            try
            {
                string strConnction = string.Concat(new string[] {
                "SERVER=", CacheGeneral.GetCache("key_localIP").ToString(),
                ";DATABASE=MASTER;UID=",  CacheGeneral.GetCache("key_userAccount").ToString(),
                ";PWD=",  CacheGeneral.GetCache("key_userPwd").ToString()});
                string sqlStr = @"SELECT TABLE_NAME FROM " + localDB + ".INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME";
                string message = (string)null;
                ArrayList dataByConAndSql = dbHelper.GetDataByConAndSql(strConnction, sqlStr, out message);
                return Json(new { message = "Success", content = JsonConvert.SerializeObject(dataByConAndSql) });
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(GeneratedFileController), log4netPath)
                    .Error("LoginTable(string localDB) ---- > " + ex.Message);
                return Json(new { message = "Fail", content = "表查询错误," + ex.Message });
            }
        }

        #endregion

        #region 读取模板

        //读取模板
        public JsonResult ReadTemplate(int marking)
        {
            string TemplateType = string.Empty;
            switch (marking)
            {
                case (int)TemplateEnum.BLL: TemplateType = "BLLTemplate.txt"; break;
                case (int)TemplateEnum.Model: TemplateType = "ModelTemplate.txt"; break;
                case (int)TemplateEnum.DAL: TemplateType = "DALTemplate.txt"; break;
            }
            string TemplatePath = Server.MapPath("~/App_Data/Template/" + TemplateType);
            //文件路径计入缓存
            CacheGeneral.SetCache("TemplatePath", TemplatePath);
            //读取模板
            string strTxt = ReadTxt(TemplatePath);
            if (strTxt.Contains("读取模板错误"))
                return Json(new { message = "Fail", content = "读取模板错误 ! " });
            else
                return Json(new { message = "Success", content = strTxt });
        }

        #endregion

        #region 模板保存

        //保存修改模板
        public JsonResult SaveTemplate(string content)
        {
            bool result = WriterTxt(content);
            if (!result)
                return Json(new { message = "Fail", content = "保存错误 ! " });
            else
                return Json(new { message = "Success", content = "保存成功 ! " });
        }

        #endregion

        #region 生成类文件

        public JsonResult GeneratedTemplate(int marking, string loacalDataBase, string loacaDataTable, string strNamespace, string notesContent)
        {
            string TemplateType = string.Empty; //模板类型
            string ModelString = string.Empty;  //Model内容生成
            string FilePath = string.Empty;     //文件保存路径                                  
            Dictionary<string, string> dir = new Dictionary<string, string>(); //存储参数

            try
            {
                //模板读取
                switch (marking) { case (int)TemplateEnum.BLL: TemplateType = "BLLTemplate.txt"; break; case (int)TemplateEnum.Model: TemplateType = "ModelTemplate.txt"; break; case (int)TemplateEnum.DAL: TemplateType = "DALTemplate.txt"; break; }
                string TemplatePath = Server.MapPath("~/App_Data/Template/" + TemplateType); //模板路径
                string TemplateString = ReadTxt(TemplatePath);

                //加入替换参数
                dir.Add("$Data.time", DateTime.Now.ToString("yyyy-MM-dd"));
                dir.Add("$Data.explain", notesContent);
                dir.Add("$Data.className", loacaDataTable);

                //内容生成
                switch (marking)
                {
                    case (int)TemplateEnum.BLL:
                        #region BLL
                        dir.Add("$Data.namespace", string.IsNullOrEmpty(strNamespace) ? "Sure.PersonalProject.BLL" : strNamespace);
                        ModelString = SplitStr(TemplateString, dir);
                        FilePath = @"\App_Data\Generating\BLL\" + loacaDataTable + ".cs";
                        #endregion
                        break;
                    case (int)TemplateEnum.Model:
                        #region Model
                        //获取 Model 内容
                        ModelString = CreateModelFile(loacalDataBase, loacaDataTable);
                        dir.Add("$Data.namespace", string.IsNullOrEmpty(strNamespace) ? "Sure.PersonalProject.Entity" : strNamespace);
                        dir.Add("$Data.content", ModelString.Substring(2));
                        ModelString = SplitStr(TemplateString, dir);
                        FilePath = @"\App_Data\Generating\Model\" + loacaDataTable + ".cs";
                        #endregion
                        break;
                    case (int)TemplateEnum.DAL:
                        #region DAL
                        dir.Add("$Data.namespace", string.IsNullOrEmpty(strNamespace) ? "Sure.PersonalProject.BLL" : strNamespace);
                        ModelString = SplitStr(TemplateString, dir);
                        FilePath = @"\App_Data\Generating\DAL\" + loacaDataTable + ".cs";
                        #endregion
                        break;
                }

                //存储到表
                Entity.SURE_GENERATED model = new Entity.SURE_GENERATED()
                {
                    SURE_GENERATED_NAME = loacaDataTable,
                    SURE_GENERATED_DATE = DateTime.Now,
                    SURE_GENERATED_OPERATOR = "Admin",
                    SURE_GENERATED_PATH = FilePath,
                    SURE_GENERATED_TYPE = Enum.GetName(typeof(TemplateEnum), marking)
                };
                proxy.INSERT_SURE_GENERATED(model);

                //文件保存
                CreateFile(FilePath, ModelString);

                return Json(new { message = "Success", content = ModelString });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Fail", content = "生成错误 ! " + ex.Message });
            }

        }

        #endregion

        #region 获取全部数据信息

        //获取全部数据信息
        public string Get_SURE_FILEINFO()
        {
            List<Entity.SURE_GENERATED> model = proxy.GET_SURE_GENERATED();
            Sure_SURE_GENERATEDResponse responseData = new Sure_SURE_GENERATEDResponse() { aaData = model };
            return JsonConvert.SerializeObject(responseData);
        }

        #endregion

        #region 下载已生成的文件

        //下载
        public ActionResult DownGeneratingFile(string path)
        {
            string filePath = Server.MapPath(path);
            return File(filePath, "application/octet-stream", Path.GetFileName(filePath));
        }

        #endregion

        #region 预览

        //读取Txt文件
        public JsonResult ReadGeneratedFile(string path)
        {
            return Json(new { message = "Success", content = ReadTxt(Server.MapPath(path)) });
        }

        #endregion

        #region  Private 方法

        /// <summary>
        /// 读取 TxT 文件
        /// </summary>
        /// <param name="pathTxt">要读取文件路径</param>
        /// <returns>string</returns>
        private string ReadTxt(string pathTxt)
        {
            StreamReader fileStream = null;
            try
            {
                string content = string.Empty;
                if (string.IsNullOrEmpty(pathTxt))
                    return string.Empty;
                fileStream = new StreamReader(pathTxt, Encoding.UTF8);
                return fileStream.ReadToEnd();
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(GeneratedFileController), log4netPath)
                    .Error("ReadTxt(string pathTxt) ----> " + ex.Message);
                return "读取模板错误 ！ ";
            }
            finally
            {
                fileStream.Close();
                fileStream.Dispose();
            }
        }

        /// <summary>
        /// 写入 Txt 文件
        /// </summary>
        /// <param name="path">写入文件路径,已写入缓存</param>
        /// <param name="content">内容</param>
        /// <returns>bool 是否成功</returns>
        private bool WriterTxt(string content, string path = "")
        {
            path = CacheGeneral.GetCache("TemplatePath").ToString();
            if (string.IsNullOrEmpty(path))
                return false;
            StreamWriter stream = null;
            try
            {
                stream = new StreamWriter(path, false, Encoding.UTF8);
                stream.Write(content);
                return true;
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(GeneratedFileController), log4netPath).Error(" WriterTxt(string path, string content) ----> " + ex.Message);
                return false;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
        }

        /// <summary>
        /// 生成 Model 内容
        /// </summary>
        /// <param name="loacalDataBase">数据库</param>
        /// <param name="loacaDataTable">表名</param>
        /// <returns>生成后的内容</returns>
        private string CreateModelFile(string loacalDataBase, string loacaDataTable)
        {
            try
            {
                //查询列名
                ArrayList dbColnameAndType = GetCloumnByTable(loacalDataBase, loacaDataTable);
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string str in dbColnameAndType)
                {
                    //private
                    var arr = str.Split('|'); //切割内容
                    stringBuilder.Append("\t\tprivate ");
                    stringBuilder.Append(arr[1].ToString()); //类型
                    stringBuilder.Append(" ");
                    stringBuilder.Append(arr[0].ToString());  //字段
                    stringBuilder.AppendLine(";");
                    //说明
                    if (!string.IsNullOrEmpty(arr[2].ToString()))
                    {
                        stringBuilder.Append("\t\t/// <summary>");
                        stringBuilder.Append("\n\t\t/// " + arr[2].ToString());
                        stringBuilder.Append("\n\t\t/// </summary>");
                    }
                    stringBuilder.Append("\n\t\t[DisplayName(\"" + arr[2].ToString() + "\")]");
                    //主键
                    if (Convert.ToBoolean(arr[3]))
                    {
                        stringBuilder.Append("\n\t\t[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                    }
                    //public
                    stringBuilder.Append("\n\t\tpublic ");
                    stringBuilder.Append(arr[1].ToString()); //类型
                    stringBuilder.Append(" ");
                    stringBuilder.Append(arr[0].ToString().Substring(0, 1).ToUpper() + arr[0].ToString().Substring(1)); //字段
                    stringBuilder.Append("{");
                    stringBuilder.Append("\n\t\t\tget { return ");
                    stringBuilder.Append(arr[0].ToString());
                    stringBuilder.Append("; }");
                    stringBuilder.Append("\n\t\t\tset { ");
                    stringBuilder.Append(arr[0].ToString());
                    stringBuilder.Append(" = value; }");
                    stringBuilder.Append("\n\t\t}");
                    stringBuilder.AppendLine();
                }
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(GeneratedFileController), log4netPath)
                    .Error("CreateModelFile(string loacalDataBase, string loacaDataTable, string strNamespace) ---- > " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 查询相对于的表的列名
        /// </summary>
        /// <param name="loacalDataBase">数据库</param>
        /// <param name="loacaDataTable">表名</param>
        /// <returns>返回所有的列名</returns>
        private ArrayList GetCloumnByTable(string loacalDataBase, string loacaDataTable)
        {
            try
            {
                string strConnction = string.Concat(new string[] {
                "SERVER=", CacheGeneral.GetCache("key_localIP").ToString(),
                ";DATABASE=",loacalDataBase,
                ";UID=",  CacheGeneral.GetCache("key_userAccount").ToString(),
                ";PWD=",  CacheGeneral.GetCache("key_userPwd").ToString(),
            });
                string strSql = string.Concat(new string[] {
                " SELECT COLUMN_NAME,DATA_TYPE,T.VALUE,T.IS_IDENTITY",
                " FROM ",loacalDataBase,".INFORMATION_SCHEMA.COLUMNS ",
                " INNER JOIN (SELECT B.NAME,C.VALUE,B.IS_IDENTITY FROM SYS.TABLES A ",
                " INNER JOIN SYS.COLUMNS B ON A.OBJECT_ID = B.OBJECT_ID ",
                " INNER JOIN SYS.EXTENDED_PROPERTIES C ON C.MAJOR_ID = B.OBJECT_ID AND C.MINOR_ID = B.COLUMN_ID) T ON COLUMN_NAME=T.NAME ",
                " WHERE TABLE_NAME = '",loacaDataTable,"'"
            });
                ArrayList fieldByConAndSql = dbHelper.GetFieldByConAndSql(strConnction, strSql);
                return fieldByConAndSql;
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(GeneratedFileController), log4netPath)
                    .Error("GetCloumnByTable(string loacalDataBase, string loacaDataTable) ---- > " + ex.Message);
                throw;
            }

        }

        /// <summary>
        /// 创建生成文件
        /// </summary>
        /// <param name="fileName">路径</param>
        /// <param name="content">内容</param>
        private void CreateFile(string fileName, string content)
        {
            try
            {
                string filePath = Server.MapPath(fileName);
                FileStream fileStream = System.IO.File.Create(filePath); //创建文件
                fileStream.Close();
                StreamWriter sw = new StreamWriter(filePath);  //创建写入流
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(GeneratedFileController), log4netPath)
                    .Error("CreateFile(string fileName, string content) ---- >" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 根据键值对替换值
        /// </summary>
        /// <param name="str">需替换的字符串</param>
        /// <param name="dri">键值对集合</param>
        /// <returns>string 替换之后的字符串</returns>
        private string SplitStr(string str, Dictionary<string, string> dictionary)
        {
            try
            {
                string resultStr = string.Empty;
                if (dictionary.Count <= 0)
                    return string.Empty;
                foreach (var dirItem in dictionary)
                {
                    str = str.Replace(dirItem.Key, dirItem.Value);
                    resultStr = str;
                }
                return resultStr;
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(GeneratedFileController), log4netPath)
                    .Error("SplitStr(string str, Dictionary<string, string> dictionary) ----> " + ex.Message);
                return "替换错误 ！ ";
            }
        }

        #endregion
    }

    /// <summary>
    /// 生成类输出 DTO
    /// </summary>
    public class Sure_SURE_GENERATEDResponse
    {
        private List<Entity.SURE_GENERATED> _aaData;
        public List<Entity.SURE_GENERATED> aaData
        {
            get { return _aaData; }
            set { _aaData = value; }
        }

    }
}