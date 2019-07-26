﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.ApiGateway.SwaggerIntegration
{
    public class TypeHelper
    {
        public static string GetArrayType(string type)
        {
            if (type.IndexOf("System.Collections") >= 0 || type.IndexOf("[]") >= 0)
            {
                if (type.IndexOf("System.Collections") >= 0)
                {
                    type = type.Substring(type.IndexOf('[') + 1).TrimEnd(']');
                }
                else if (type.IndexOf("[]") >= 0)
                {
                    type = type.Replace("[]", "");
                }
            }
            return type;
        }

        public static bool CheckIsArray(string type)
        {
            return type.IndexOf("System.Collections") >= 0 || type.IndexOf("[]") >= 0;
        }

        public static bool CheckIsObject(string type)
        {
            var systemType = GetSystemTypeDic();
            foreach (var st in systemType)
            {
                if (type == st.Value)
                    return false;
            }
            return false;
        }

        private static Dictionary<string, string> GetSystemTypeDic()
        {
            Dictionary<string, string> systemTypeDic = new Dictionary<string, string>();
            systemTypeDic.Add("System.String", "string");
            systemTypeDic.Add("System.Int16", "integer");
            systemTypeDic.Add("System.UInt16", "integer");
            systemTypeDic.Add("System.Int32", "integer");
            systemTypeDic.Add("System.UInt32", "integer");
            systemTypeDic.Add("System.Int64", "integer");
            systemTypeDic.Add("System.UInt64", "integer");
            systemTypeDic.Add("System.Double", "number");
            systemTypeDic.Add("System.Decimal", "number");
            systemTypeDic.Add("System.Byte", "string");
            systemTypeDic.Add("System.Boolean", "boolean");
            systemTypeDic.Add("System.DateTime", "datetime");
            return systemTypeDic;
        }

        public static string ReplaceTypeToJsType(string str)
        {
            if (str == null) return null;
            foreach (var p in GetSystemTypeDic())
            {
                str = str.Replace(p.Key, p.Value);
            }
            return str;
        }
    }
}
