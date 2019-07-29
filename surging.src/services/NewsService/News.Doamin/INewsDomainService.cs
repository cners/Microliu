using Surging.Core.CPlatform.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.Doamin
{
    public interface INewsDomainService : ITransientDependency
    {
        Task CreateNews(NewsInfo newsInfo);

        Task<NewsInfo> GetNews(string Id);
    }
}
