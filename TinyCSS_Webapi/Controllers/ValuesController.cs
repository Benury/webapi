using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyCSS_Webapi.Models;
using TinyCSS_Webapi.Repository;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Qiniu.JSON;
using Qiniu.Util;
using Qiniu.Http;
using Qiniu.Common;
using Qiniu.IO;
using Qiniu.IO.Model;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Cors;
using System.DrawingCore;
using System.DrawingCore.Imaging;

namespace TinyCSS_Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
       // [Route("/")]
        [HttpGet]
        public List<tblelement> Get()
        {
            using (CoreDbContext _coreDbContext = new CoreDbContext())
            {
                return MElementRepository.Get();
            }
        }

        // GET api/values/5
        [HttpPost("{type}")]
        public ActionResult<string> Get(string type)
        {
            using (CoreDbContext _coreDbContext = new CoreDbContext())
            {
                return Ok(new ApiResultMutilObject<tblelement>()
                {
                    code = EnumError.SUCCESS,
                    message = "SUCCESS",
                    data = MElementRepository.GetElementByType(type)
                });
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromForm] ElementDTO el)
       //public IActionResult Post([FromForm] IFormCollection el)
        {
            tblelement element = new tblelement();
            try
            {
                // 上传图片到七牛云       
                string fileName = Guid.NewGuid().ToString() + ".png";
                dynamic type = (new Program()).GetType();
                string imgTempPath = Path.GetDirectoryName(type.Assembly.Location) + "\\" + fileName;

                //var photoBytes = Convert.FromBase64String(image);
                System.IO.File.WriteAllBytes(imgTempPath, el.file);

                //Bitmap imgFile = Base64StringToImage(el.file);
                //imgFile.Save(imgTempPath,ImageFormat.Png);
                //var stream = new FileStream(imgTempPath, FileMode.CreateNew);
                //el.Files[0].CopyTo(stream);
                //stream.Dispose();
                HttpResult res = UploadFile(imgTempPath, fileName);
                if (res.Code != 200)
                {
                    return Ok(new ApiResultMutilObject<tblelement>()
                    {
                        code = EnumError.NET_FRE_UPLOADQINIU_ERROR,
                        message = "NET_FRE_UPLOADQINIU_ERROR"
                    });
                }

                // 保存记录 

                element.mtitle = el.title;
                element.mdesc = el.desc;
                element.mhtml = el.html;
                element.mcss = el.css;
                element.mtype = el.type;
                element.userid = el.userid;
                element.mimg = fileName;

                MElementRepository.AddElement(element);
                System.IO.File.Delete(imgTempPath);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResultSingleObject<tblelement>()
                {
                    code = EnumError.NET_FRE_UNKNOWN_ERROR,
                    message = "NET_FRE_UNKNOWN_ERROR",
                });
            }

            return Ok(new ApiResultSingleObject<tblelement>()
            {
                code = EnumError.SUCCESS,
                message = "SUCCESS",
                data = element
            });
        }
        //base64编码的字符串转为图片
        public Bitmap Base64StringToImage(byte[] strbase64)
        {
            try
            {
                MemoryStream ms = new MemoryStream(strbase64);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(@"d:\test.jpg", ImageFormat.Jpeg);
                //bmp.Save(@"d:\"test.bmp", ImageFormat.Bmp);
                //bmp.Save(@"d:\"test.gif", ImageFormat.Gif);
                //bmp.Save(@"d:\"test.png", ImageFormat.Png);
                ms.Close();
                return bmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 简单上传-上传小文件
        /// </summary>
        public HttpResult UploadFile(string filePath,string fileName)
        {
            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            //Mac mac = new Mac(Settings.AccessKey, Settings.SecretKey);
            Mac mac = new Mac("-xIS6CAYwO2FU0XAb3_-ApbUWYoc0yeO5NDK-e7u", "JNAK9FjIjxk5lReGAsBzUahAz56xBpLqBKPFEaLv");
            string bucket = "tinycss";
            string saveKey = fileName;
            string localFile = filePath;
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            Qiniu.Common.Config.AutoZone("-xIS6CAYwO2FU0XAb3_-ApbUWYoc0yeO5NDK-e7u", "tinycss", false);
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            UploadManager um = new UploadManager();
            HttpResult result = um.UploadFile(localFile, saveKey, token);
            return result;
           // Console.WriteLine(result);
        }

        public void  UploadImgToQiniu(byte[] file,string fileName)
        {
            //AccessKey/SecretKey
            // -xIS6CAYwO2FU0XAb3_-ApbUWYoc0yeO5NDK-e7u
            // JNAK9FjIjxk5lReGAsBzUahAz56xBpLqBKPFEaLv
            Mac mac = new Mac("-xIS6CAYwO2FU0XAb3_-ApbUWYoc0yeO5NDK-e7u", "JNAK9FjIjxk5lReGAsBzUahAz56xBpLqBKPFEaLv");
            Qiniu.Common.Config.AutoZone("-xIS6CAYwO2FU0XAb3_-ApbUWYoc0yeO5NDK-e7u", "tinycss", false);
            string bucket = "tinycss";
            string saveKey = fileName;
            byte[] data = file;
            //byte[] data = System.Text.Encoding.UTF8.GetBytes("Hello World!");
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            FormUploader fu = new FormUploader();
            HttpResult result = fu.UploadData(data, saveKey, token);
            Console.WriteLine(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
