using News.Common;
using News.IApplication.Action.Dtos;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.IApplication.Action
{
    [ServiceBundle(NewsConstants.RouteTemplet)]
    public interface IAcationAppService : IServiceKey
    {
        [Service(DisableNetwork = true)]
        Task<string> InitActions(ICollection<InitActionActionInput> actions);
    }
}
