using Microsoft.AspNetCore.Http;
using System.Data;

namespace StudentAttandanceLibrary.Services.Interfaces
{
    public interface IExcelService
    {
        public DataTable GetSheetFromTemplate(string fileName, int sheetIndex = 0);
        public DataTable GetSheetFromFile(IFormFile uploadFile, int sheetIndex = 0);
        public DataTable GetSheetFromStream(MemoryStream memoryStream, int sheetIndex = 0);
        public List<string> GetColumnsInHeader(DataTable dataTable, int rowIndex = 0, int startColumn = 0);
        public bool CheckHeaderFileExcel(List<string> columnsTemplate, List<string> columnsUpload, int rowIndex = 0, int startColumn = 0);
    }
}
