using Microliu.FileService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.FileService.Application
{
    public interface IFileApplication
    {

        /// <summary>
        /// 上传一个文件（该方法不支持断点续传）
        /// </summary>
        /// <param name="uploadDto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<FileDownloadDto> Upload(FileUploadDto uploadDto, CancellationToken ct = default);


        /// <summary>
        /// 单个文件上传（支持断点续传）
        /// </summary>
        /// <param name="uploadViewModel"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<FileDownloadDto> UploadAppenderFileAsync(FileUploadDto uploadDto, CancellationToken ct = default);

        /// <summary>
        /// 单个文件上传（断点续传的追加文件）
        /// </summary>
        /// <param name="uploadViewModel"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> AppendFileAsync(FileUploadAppendDto uploadViewModel, CancellationToken ct = default);
    }
}
