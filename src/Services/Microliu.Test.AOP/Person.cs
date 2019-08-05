using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Test.AOP
{
    public class Person
    {
        [CustomInterceptor]
        public virtual void Say(string msg)//需要pulic 类，虚方法
        {
            Console.WriteLine("service calling..." + msg);
        }
    }
}
