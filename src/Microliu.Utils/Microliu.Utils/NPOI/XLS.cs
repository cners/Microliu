using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;

namespace Microliu.Utils.NPOI
{
    public class XLS
    {
        private string _file;

        private IWorkbook _workbook;

        public enum FileType
        {
            XLS = 1,
            XLSX = 2
        }

        public XLS(FileType fileType = FileType.XLSX)
        {
            if (fileType == FileType.XLSX)
                _workbook = new XSSFWorkbook();
            else
                _workbook = new HSSFWorkbook();
        }

        public XLS(string templateFile)
        {
            var fileExtension = Path.GetExtension(templateFile ?? "").ToLower();
            using (var fs = new FileStream(_file, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    if (fileExtension == ".xls")// 2003版本
                        _workbook = new HSSFWorkbook(fs);
                    else if (fileExtension == ".xlsx") // 2007版本及以上
                        _workbook = new XSSFWorkbook(fs);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"不支持的文件格式,{templateFile}");
                }
            }
        }

        /// <summary>
        /// 保存单表
        /// 默认无任何样式
        /// </summary>
        /// <param name="data">数据表</param>
        /// <param name="savePath">保存完整路径</param>
        /// <param name="sheetName">页名</param>
        public static void Save(DataTable data, string savePath, string sheetName = "Sheet1")
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (savePath == null)
            {
                throw new ArgumentNullException(nameof(savePath));
            }

            IWorkbook workbook;
            if (GetFileExtension(savePath) == FileType.XLSX)
            {
                workbook = new XSSFWorkbook();
            }
            else
            {
                workbook = new HSSFWorkbook();

            }

            ISheet sheet = string.IsNullOrEmpty(data.TableName) ? workbook.CreateSheet(sheetName) :
                workbook.CreateSheet(data.TableName);

            // 表头
            IRow rowHead = sheet.CreateRow(0);
            for (int i = 0; i < data.Columns.Count; i++)
            {
                ICell cell = rowHead.CreateCell(i);
                cell.SetCellValue(data.Columns[i].ColumnName);
            }

            // 数据
            for (int row_no = 0; row_no < data.Rows.Count; row_no++)
            {
                IRow row = sheet.CreateRow(row_no + 1);
                for (int col_no = 0; col_no < data.Columns.Count; col_no++)
                {
                    ICell cell = row.CreateCell(col_no);
                    cell.SetCellValue(data.Rows[row_no][col_no].ToString());
                }
            }

            // 保存
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                var buffer = ms.ToArray();

                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush();
                }
                ms.Flush();
            }
        }

        public static DataTable Read(string file)
        {
            IWorkbook workbook;
            DataTable dt = new DataTable();

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (GetFileExtension(file) == FileType.XLSX)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else
                {
                    workbook = new HSSFWorkbook(fs);
                }

                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);

                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }

        private static FileType GetFileExtension(string file)
        {
            string fileExtension = Path.GetExtension(file).ToLower();
            if (fileExtension == ".xlsx") return FileType.XLSX;
            else if (fileExtension == ".xls") return FileType.XLS;
            else throw new ArgumentException(nameof(file));
        }
    }
}
