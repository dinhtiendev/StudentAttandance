using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using StudentAttandanceLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Services
{
    public class ExcelService : IExcelService
    {
        public XSSFWorkbook OpenWorkbookTemplate(string fileName)
        {
            string templatePath = @$"{Directory.GetCurrentDirectory()}/Templates/{fileName}";
            FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
            XSSFWorkbook workbook = new XSSFWorkbook(fileStream);
            return workbook;
        }

        public XSSFSheet GetSheetFromWorkbook(int sheetIndex, XSSFWorkbook workbook)
        {
            XSSFSheet sheet = (XSSFSheet)workbook.GetSheetAt(sheetIndex);
            return sheet;
        }

        public bool CheckHeaderFile(XSSFSheet sheetTemplate, XSSFSheet sheetUpload, int index)
        {
            try
            {
                //Get header row from template
                IRow uploadRow = sheetUpload!.GetRow(index);
                IRow templateRow = sheetTemplate!.GetRow(index);

                if (uploadRow == null)
                {
                    return false;
                }
                //Get list cells from sheet
                var cellsFileTemplate = templateRow.Cells.Select(x => x.StringCellValue).ToList();
                var cellsFileUpload = uploadRow.Cells.Select(x => x.StringCellValue).ToList();

                var check = cellsFileUpload.SequenceEqual(cellsFileTemplate);
                if (!check)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
