using News.Doamin;
using News.IApplication;
using News.IApplication.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.Domain.PagedAndSorted;
using Surging.Core.Domain.PagedAndSorted.Extensions;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.Application
{
    [ModuleName("news")]
    public class NewsAppService : ProxyServiceBase, INewsAppService
    {
        private readonly INewsDomainService _newsDomainService;
        private readonly IDapperRepository<NewsInfo, string> _newsRepository;

        public NewsAppService(INewsDomainService newsDomainService,
            IDapperRepository<NewsInfo, string> newsRepository)
        {
            _newsDomainService = newsDomainService;
            _newsRepository = newsRepository;
        }

        public async Task<string> Create(CreateNewsInput input)
        {
            //input.CheckDataAnnotations().CheckValidResult();
            var existNews = await _newsRepository.FirstOrDefaultAsync(p => p.Title == input.Title);
            if (existNews == null)
            {
                throw new Exception($"新闻标题 '{input.Title}' 已存在");
            }

            var newsInfo = input.MapTo<NewsInfo>();
            await _newsDomainService.CreateNews(newsInfo);
            return "新增新闻成功";
        }

        public async Task<IPagedResult<GetNewsOutput>> Query(QueryNewsInput query)
        {
            var queryResult = await _newsRepository.GetPageAsync(p => p.Title.Contains(query.SearchTitle)
                        || p.Id.Equals(query.SearchKey), query.PageIndex, query.PageCount);
            return queryResult.Item1.MapTo<IEnumerable<GetNewsOutput>>().GetPagedResult(queryResult.Item2);
        }

        public async Task<string> Query()
        {
            return "ok";
        }
    }
}
