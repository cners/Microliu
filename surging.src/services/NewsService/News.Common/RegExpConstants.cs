﻿using System;
using System.Collections.Generic;
using System.Text;

namespace News.Common
{
    public static class RegExpConstants
    {
        public const string UserName = "^[a-zA-Z0-9_-]{4,16}$";

        public const string Phone = "^[1](([3][0-9])|([4][5-9])|([5][0-3,5-9])|([6][5,6])|([7][0-8])|([8][0-9])|([9][1,8,9]))[0-9]{8}$";

        public const string Password = "^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,20}$";

        public const string WordbookCode = "^[a-zA-Z0-9_-]{4,50}$";
    }
}
