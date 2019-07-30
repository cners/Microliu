using News.Common;
using News.IApplication.Dtos;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.Domain.PagedAndSorted;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.IApplication
{
    [ServiceBundle(NewsConstants.RouteTemplet)]
    public interface INewsAppService : IServiceKey
    {
        Task<string> Create(CreateNewsInput input);

        Task<IPagedResult<GetNewsOutput>> Query(QueryNewsInput input);

    }
}
