using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.Doamin
{
    public class NewsDomainService :ManagerBase, INewsDomainService
    {
        private readonly IDapperRepository<NewsInfo, string> _newsRepository;


        public NewsDomainService(IDapperRepository<NewsInfo, string> dapperRepository)
        {
            _newsRepository = dapperRepository;
        }
        public async Task CreateNews(NewsInfo newsInfo)
        {
            await _newsRepository.InsertAsync(newsInfo);
        }

        public async Task<NewsInfo> GetNews(string Id)
        {
            return await _newsRepository.GetAsync(Id);
        }
    }
}
