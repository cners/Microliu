using Microliu.Utils.NPOI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace Microliu.Core.UtilsTest
{
    public class XLSTest
    {
        [Fact]
        public void SaveDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("工号");
            dt.Columns.Add("部门");
            dt.Columns.Add("姓名");
            dt.Columns.Add("电话");
            dt.Columns.Add("工资");
            dt.Columns.Add("入职时间");

            DataRow row = dt.NewRow();
            row["工号"] = "D01";
            row["部门"] = "开发部";
            row["姓名"] = "刘壮";
            row["电话"] = "6723";
            row["工资"] = 8000;
            row["入职时间"] = DateTime.Parse("2018-03-15");

            dt.Rows.Add(row);


            var savePath = "E:\\test.xlsx";
            var sheetName = "员工信息";
            XLS.Save(dt, savePath,sheetName);
        }
    }
}
