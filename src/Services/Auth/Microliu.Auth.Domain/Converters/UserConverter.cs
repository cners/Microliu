using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.Domain.Converters
{
   public class UserConverter
    {

        public static User ToUser(CreateUserModel input)
        {
            if (input == null)
                throw new NullReferenceException();

            var u = new User();
            u.Id = Guid.NewGuid().ToString("N");
            u.CreateTime = DateTimeOffset.Now;
            u.NickName = input.NickName;
            u.Password = input.Password;
            u.UserName = input.UserName;
            return u;
        }
    }
}
