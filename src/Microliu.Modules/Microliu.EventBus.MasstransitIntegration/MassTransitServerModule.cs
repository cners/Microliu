﻿using Autofac;
using MassTransit;
using Microliu.Logger;
using Microliu.Utils;
using Microliu.Utils.Module;
using Microsoft.Extensions.Configuration;

namespace Microliu.EventBus.MasstransitIntegration
{
    public class MassTransitServerModule : ServerModuleBase
    {
        readonly MassTransitOptions _options;
        IContainer _serverContainer;

        public MassTransitServerModule(IConfigurationRoot appSettings)
            : base(appSettings)
        {
            _options = MicroliuAppSettings.GetSection(typeof(MassTransitServerModule).Name).Get<MassTransitOptions>();
        }

        public override void DoServiceRegister(ContainerBuilder serviceContainerBuilder)
        {
            if (_options != null)
            {
                serviceContainerBuilder.Register(x => _options).SingleInstance();
                serviceContainerBuilder.Register(context =>
                {
                    var bc = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var host = cfg.Host(_options.HostAddress, h =>
                        {
                            h.Username(_options.Username);
                            h.Password(_options.Password);
                        });

                        cfg.ReceiveEndpoint(_options.QueueName, ec =>
                        {
                            ec.LoadFrom(context);
                        });
                    });
                    return bc;
                }).SingleInstance().As<IBusControl>().As<IBus>();
            }
            base.DoServiceRegister(serviceContainerBuilder);
        }

        public override void DoInit(IContainer container)
        {
            _serverContainer = container;
            base.DoInit(container);
        }

        public override void DoServiceInit(IContainer container)
        {
            if (_options != null)
            {
                if (_serverContainer != null)
                {
                    var logger = container.Resolve<ILogger>();
                    logger.Info($"[config]use Masstransit for EventBus, options.HostAddress {_options.HostAddress.ToString()}, options.SendEndPointUrl {_options.SendEndPointUri.ToString()}");
                }

                var bus = container.Resolve<IBusControl>();
                bus.StartAsync();

                if (_serverContainer != null)
                {
                    IApplication host = _serverContainer.Resolve<IApplication>();
                    host.DisposeAction(c =>
                    {
                        bus.Stop();
                    });
                }
            }
            base.DoServiceInit(container);
        }
    }
}
