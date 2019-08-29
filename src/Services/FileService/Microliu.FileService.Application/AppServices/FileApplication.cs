using Microliu.FileService.Data;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using Microliu.FileService.Domain;
using System.Threading;
using System.Threading.Tasks;
using FastDFS.Client;

namespace Microliu.FileService.Application
{
    public partial class FileApplication : IFileApplication
    {
        private readonly IServiceProvider _services;
        private IUnitOfWork _unitOfWork;

        public FileApplication(IServiceProvider services)
        {
            _services = services;

            _unitOfWork = services.GetService<IUnitOfWork>();
        }

        /// <summary>
        /// 上传一个文件（该方法不支持断点续传）
        /// </summary>
        /// <param name="uploadDto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<FileDownloadDto> Upload(FileUploadDto uploadDto, CancellationToken ct = default)
        {
            // 初始化 
            ConnectionManager.Initialize(uploadDto.UploadServers);

            // 获取存储服务器节点
            var storageNode = FastDFSClient.GetStorageNodeAsync("group1").Result;
            Console.WriteLine($"storage node : {storageNode.GroupName},{storageNode.EndPoint}");

            // 执行上传
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:uploading...");
            string filePath = FastDFSClient.UploadFileAsync(storageNode, uploadDto.Buffer, uploadDto.Suffix).Result;
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:Upload Success !");

            Console.WriteLine($"Visit : {filePath}");

            // 上传完成，返回结果
            var downloadViewModel = new FileDownloadDto
            {
                DownloadServer = uploadDto.DownloadServer,
                FilePath = filePath,
                Suffix = uploadDto.Suffix,
                FileSize = uploadDto.FileSize,
                Download = uploadDto.DownloadServer + filePath,
                TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                FileUnits = "bytes",
                Guid = Guid.NewGuid().ToString("N"),
                Expir = "0"
            };

            return downloadViewModel;
        }


        /// <summary>
        /// 单个文件上传（支持断点续传）
        /// </summary>
        /// <param name="uploadViewModel"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<FileDownloadDto> UploadAppenderFileAsync(FileUploadDto uploadDto,
         CancellationToken ct = default)
        {
            // 初始化 
            ConnectionManager.Initialize(uploadDto.UploadServers);

            // 获取存储服务器节点
            var storageNode = FastDFSClient.GetStorageNodeAsync("group1").Result;
            Console.WriteLine($"storage node : {storageNode.GroupName},{storageNode.EndPoint}");

            // 执行上传
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:UploadAppendFile First...");
            string filePath = FastDFSClient.UploadAppenderFileAsync(storageNode, uploadDto.Buffer, uploadDto.Suffix).Result;
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:UploadAppendFile First Success !");

            Console.WriteLine($"Visit Url: {filePath}");

            // 上传完成，返回结果
            var downloadViewModel = new FileDownloadDto
            {
                DownloadServer = uploadDto.DownloadServer,
                FilePath = filePath,
                Suffix = uploadDto.Suffix,
                FileSize = uploadDto.FileSize,
                Download = uploadDto.DownloadServer + filePath,
                TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                FileUnits = "bytes",
                Guid = Guid.NewGuid().ToString("N"),
                Expir = "0"
            };

            return downloadViewModel;
        }

        /// <summary>
        /// 单个文件上传（断点续传的追加文件）
        /// </summary>
        /// <param name="uploadViewModel"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> AppendFileAsync(FileUploadAppendDto uploadViewModel,
         CancellationToken ct = default)
        {
            // 初始化 
            //ConnectionManager.Initialize(uploadViewModel.UploadServers);

            // 获取存储服务器节点
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync("group1");
            Console.WriteLine($"append storage node : {storageNode.GroupName},{storageNode.EndPoint}");
            FDFSFileInfo fileInfo = await FastDFSClient.GetFileInfoAsync(storageNode, uploadViewModel.FileName);
            if (fileInfo == null)
            {
                Console.WriteLine($"GetFileInfoAsync Fail, path:{uploadViewModel.FileName}");
                return false;
            }
            //Console.WriteLine("FileName:{0}", uploadViewModel.FileName);
            //Console.WriteLine("FileSize:{0}", fileInfo.FileSize);
            //Console.WriteLine("CreateTime:{0}", fileInfo.CreateTime);
            //Console.WriteLine("Crc32:{0}", fileInfo.Crc32);


            // 执行上传
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:UploadAppending...");
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:GropName:{uploadViewModel.GroupName}");//group1
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:FileName:{uploadViewModel.FileName}");//M00/00/00/wKjmhFx2NL6EXFwjAAAAAA79E7k517.zip
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:Buffer:{uploadViewModel.Buffer.LongLength}");//432644

            await FastDFSClient.AppendFileAsync(uploadViewModel.GroupName, uploadViewModel.FileName, uploadViewModel.Buffer);

            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:UploadAppendFile Success !");

            return true;
        }

    }
}
