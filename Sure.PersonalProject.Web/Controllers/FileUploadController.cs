namespace Sure.PersonalProject.Web.Controllers
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: FileUploadController 上传下载
    -----------------------------------------------------------------------*/

    using Sure.PersonalProject.Utilities;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    public class FileUploadController : Controller
    {
        #region 属性

        /// <summary>
        /// 文件上传的路径
        /// </summary>
        private static string UploadPath = string.Empty;

        /// <summary>
        /// log4net 配置文件路径
        /// </summary>
        private static string log4netPath = string.Empty;

        #endregion

        //主页
        public ActionResult FileUpload()
        {
            //文件上传目录
            UploadPath = Server.MapPath("~/App_Data/UploadFile");
            log4netPath = Server.MapPath("~/App_Data/log4net/log4net.config");
            return View();
        }

        //保存文件
        public ActionResult SveFile()
        {
            try
            {
                var data = Request.Files["file"];//表单中取得分块文件
                string fileName = Request["name"]; //文件名
                int chunk = Convert.ToInt32(Request["chunk"]); //分片大小
                string TempFolder = Path.Combine(UploadPath, Request["guid"]);//临时保存分块的目录
                if (!System.IO.Directory.Exists(TempFolder))
                    System.IO.Directory.CreateDirectory(TempFolder);
                string filePath = Path.Combine(TempFolder, chunk.ToString());//分块文件名为索引名
                if (data != null)
                {
                    data.SaveAs(filePath);
                }
            }
            catch (Exception) { throw; }
            return Json(new { errMessage = "上传成功", Message = string.Empty });
        }

        //合并文件
        public ActionResult FileMerge()
        {
            var TempFolder = string.Empty;
            FileStream fileStream = null;
            try
            {
                TempFolder = Path.Combine(UploadPath, Request["guid"]);//临时文件夹
                var filesName = System.IO.Directory.GetFiles(TempFolder);//获得下面的所有文件
                var filesNamePath = Path.Combine(UploadPath, Request["fileName"]);//最终的文件名
                fileStream = new FileStream(filesNamePath, FileMode.Create);
                foreach (var part in filesName.OrderBy(x => x.Length).ThenBy(x => x))//排一下序，保证从0-N Write
                {
                    var bytes = System.IO.File.ReadAllBytes(part);
                    fileStream.Write(bytes, 0, bytes.Length);
                    //删除分块
                    System.IO.File.Delete(part);
                }
                return Json(new { message = "Success", content = string.Empty });
            }
            catch (Exception ex)
            {
                Log4net.log4netCreate(typeof(FileUploadController), log4netPath)
                       .Error("FileMerge() ----> " + ex.Message);
                return Json(new { message = "Fail", content = ex.Message });
            }
            finally
            {
                fileStream.Close();
                fileStream.Dispose();
                //删除文件夹
                System.IO.Directory.Delete(TempFolder);
            }
        }
    }
}