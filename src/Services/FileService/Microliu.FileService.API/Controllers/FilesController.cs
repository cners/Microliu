using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microliu.FileService.API.Configurations;
using Microliu.FileService.Application;
using Microliu.FileService.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Microliu.FileService.API.Controllers
{
    /// <summary>
    /// 文件控制器
    /// </summary>
    //[Route("api/[controller]")]
    //[ApiVersion("1")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileApplication _fileApplication;
        private readonly IOptions<AppSettings> _settings;

        public FilesController(IFileApplication fileApplication,
            IOptions<AppSettings> settings)
        {
            _fileApplication = fileApplication;
            _settings = settings;
        }

        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        //[RequestSizeLimit(300_000_000)]
        //[RequestFormSizeLimitAttribute(100_000_000)]
        [HttpPost(nameof(Upload))]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null)
                return BadRequest();

            var upload = new FileUploadDto();
            long fileSize = file.Length;

            // 文件上传大小限制（非服务器的文件大小限制）
            long kb = 1024;
            long M = kb * 1024;
            long G = M * 1024;
            if (fileSize <= 0)
                return BadRequest("该文件已损坏，不支持上传");
            else if (fileSize > 100 * M)
                return BadRequest("超出文件最大限制");

            upload.Buffer = new byte[fileSize];
            var stream = file.OpenReadStream();
            await stream.ReadAsync(upload.Buffer, 0, upload.Buffer.Length);

            upload.Suffix = Path.GetExtension(file.FileName).TrimStart('.');
            upload.Suffix = (upload.Suffix.Length == 0) ? "unknown" : upload.Suffix;
            upload.FileSize = fileSize;
            upload.UploadServers = _settings.Value.FastDFS.GetUploadServers();
            upload.DownloadServer = _settings.Value.FastDFS.DownloadServer ?? "http://192.168.230.144/group1/";

            var uploadResult = await _fileApplication.Upload(upload);
            return Ok(uploadResult);
        }

        //[HttpPost, Route(nameof(UploadAppend)]
        //public async Task<IActionResult> UploadAppend(IFormFile file)
        //{
        //    try
        //    {
        //        if (file == null)
        //            return BadRequest();

        //        var upload = new FileUploadDto();
        //        long fileSize = file.Length;

        //        // 文件上传大小限制（非服务器的文件大小限制）
        //        long kb = 1024;
        //        long M = kb * 1024;
        //        long G = M * 1024;
        //        if (fileSize <= 0)
        //            return BadRequest("该文件已损坏，不支持上传");
        //        else if (fileSize > 1 * G)
        //            return BadRequest("超出文件最大限制");

        //        upload.Buffer = new byte[fileSize];
        //        var stream = file.OpenReadStream();
        //        await stream.ReadAsync(upload.Buffer, 0, upload.Buffer.Length);

        //        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:Buffer Size:{upload.Buffer.LongLength}");
        //        upload.Suffix = Path.GetExtension(Request.Form["name"]).TrimStart('.');
        //        upload.Suffix = (upload.Suffix.Length == 0) ? "unknown" : upload.Suffix;

        //        string md5File = Request.Form["md5"];
        //        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:file md5:{md5File}");

        //        upload.FileSize = fileSize;
        //        upload.UploadServers = _settings.Value.FastDFS.GetUploadServers();
        //        upload.DownloadServer = _settings.Value.FastDFS.DownloadServer ?? "http://192.168.230.144/group1/";


        //        FileDownloadDto result = null;
        //        if (Request.Form["chunk"] == "0")//第一个使用 upload_appender_file()上传
        //        {
        //            result = await _fileApplication.UploadAppenderFileAsync(upload);
        //            //_cacheService.Set<DfsFileDownloadViewModel>(md5File, result, 60 * 5);//5分钟内，单chunk需要上传完成
        //        }
        //        else
        //        {//非第1个文件chunk，使用append_file()追加
        //            //if (_cacheService.HasKey(md5File))
        //            {
        //                result = _cacheService.Get<DfsFileDownloadViewModel>(md5File);
        //                _cacheService.Set<DfsFileDownloadViewModel>(md5File, result, 60 * 5);

        //                var appendViewModel = new DfsFileUploadAppendViewModel();
        //                appendViewModel.UploadServers = upload.UploadServers;
        //                appendViewModel.GroupName = "group1";
        //                appendViewModel.FileName = result.FilePath;
        //                appendViewModel.Buffer = upload.Buffer;
        //                appendViewModel.Suffix = upload.Suffix;
        //                appendViewModel.FileSize = upload.FileSize;
        //                appendViewModel.DownloadServer = upload.DownloadServer;
        //                bool appendRetsult = await _dfsSupervisor.AppendFileAsync(appendViewModel, ct);
        //                if (appendRetsult)
        //                {
        //                    return Ok("ok");
        //                }
        //                else
        //                {
        //                    return BadRequest("fail");
        //                }
        //            }
        //        }

        //        if (_cacheService.HasKey(md5File))
        //        {
        //            result = _cacheService.Get<DfsFileDownloadViewModel>(md5File);
        //            return new ObjectResult(result);
        //        }
        //        else
        //        {
        //            return Ok("chunk upload fail");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message + "" + ex.StackTrace);
        //        return BadRequest($"{ex.Message}");
        //    }
        //}


        /// <summary>
        /// 断点续传，测试专用，看到横杠说明没问题。
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet, Route(nameof(UploadAppendTest))]
        [MapToApiVersion("1")]
        public async Task<IActionResult> UploadAppendTest()
        {
            var testBytes = Encoding.UTF8.GetBytes("我是前半部分数据\r\n");
            var upload = new FileUploadDto();
            upload.Buffer = testBytes.Take(6).ToArray();

            upload.Suffix = "txt";
            upload.FileSize = testBytes.Take(6).ToArray().Length;
            upload.UploadServers = _settings.Value.FastDFS.GetUploadServers();
            upload.DownloadServer = _settings.Value.FastDFS.DownloadServer ?? "http://192.168.230.144/group1/";


            var filename = await _fileApplication.UploadAppenderFileAsync(upload);


            var appendViewModel = new FileUploadAppendDto();
            appendViewModel.UploadServers = upload.UploadServers;
            appendViewModel.GroupName = "group1";
            appendViewModel.FileName = filename.FilePath;
            appendViewModel.Buffer = Encoding.UTF8.GetBytes("我是后半部分数据\r\n----------\r\n如果看到这里，说明断点续传功能没问题").ToArray();
            appendViewModel.Suffix = upload.Suffix;
            appendViewModel.FileSize = upload.FileSize;
            appendViewModel.DownloadServer = upload.DownloadServer;
            _fileApplication.AppendFileAsync(appendViewModel).Wait();

            Console.WriteLine("UploadAppendFile Success" + filename);
            return Content(filename.FilePath);
        }


        [HttpPost(nameof(Upload))]
        [MapToApiVersion("2")]
        public async Task<IActionResult> UploadFile()
        {
            return Ok();
        }

        [HttpPost("UploadFile")]
        //[MapToApiVersion("2")]
        [ApiVersion("2",Deprecated =false)]
        public async Task<IActionResult> UploadFile2()
        {
            return Ok();
        }
    }
}