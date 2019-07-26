using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Utils
{
    public interface IApplication:IDisposable
    {
        /// <summary>
        /// autofac container
        /// </summary>
        IContainer Container { get; set; }

        IApplication DisposeAction(Action<IContainer> action);

        IApplication RunAction(Action<IContainer> action);

        IApplication BeforeRunAction(Action<IContainer> action);

        void Run();
    }
}
