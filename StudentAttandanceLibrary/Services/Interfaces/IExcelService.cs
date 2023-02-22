using Microsoft.AspNetCore.Http;
using System.Data;

namespace StudentAttandanceLibrary.Services.Interfaces
{
    public interface IExcelService
    {
        public DataTable GetSheetFromTemplate(string fileName);
        public DataTable GetSheetFromUpload(IFormFile file);
        public DataTable GetSheetFromStream(MemoryStream memoryStream);
        public List<string> GetHeaderColumns(DataTable dataTable);
        public bool CheckHeader(List<string> columnsTemplate, List<string> columnsUpload);
    }
}
