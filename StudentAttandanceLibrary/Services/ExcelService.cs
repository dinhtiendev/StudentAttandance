using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using StudentAttandanceLibrary.Services.Interfaces;
using System.Data;

namespace StudentAttandanceLibrary.Services
{
    public class ExcelService : IExcelService
    {
        public DataTable GetSheetFromTemplate(string fileName)
        {
            string pathFile = @$"{Directory.GetCurrentDirectory()}/Templates/{fileName}";
            var stream = File.OpenRead(pathFile);
            var reader = ExcelReaderFactory.CreateReader(stream);
            var result = GetDataSet(reader);
            var table = result.Tables;
            var sheet = table[0];
            return sheet;
        }

        public DataTable GetSheetFromUpload(IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                var reader = ExcelReaderFactory.CreateReader(memoryStream);
                var result = GetDataSet(reader);
                var table = result.Tables;
                var sheet = table[0];
                return sheet;
            }
            catch (Exception e)
            {
                memoryStream.Close();
            }
            return null;
        }

        public List<string> GetHeaderColumns(DataTable dataTable)
        {
            try
            {
                var columns = new List<string>();
                var row = dataTable.AsEnumerable().FirstOrDefault();
                //Check row of file excel is null
                if (row == null)
                {
                    return null;
                }
                var numberColumns = row.Table.Columns.Count;
                //Add each value of columns to list
                for (int i = 0; i < numberColumns; i++)
                {
                    columns.Add(Convert.ToString(dataTable!.Rows[0][i].ToString().Trim()));
                }
                return columns;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool CheckHeader(List<string> columnsTemplate, List<string> columnsUpload)
        {
            try
            {
                //Check columns file upload is null
                if (columnsUpload == null)
                {
                    return false;
                }

                //Check number of columns file upload and file template
                if (columnsTemplate.Count != columnsUpload.Count)
                {
                    return false;
                }

                //Check value of each columns of file upload and file template
                var checkHeader = columnsUpload.SequenceEqual(columnsTemplate);
                if (!checkHeader)
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

        public DataSet GetDataSet(IExcelDataReader reader)
        {
            var result = reader.AsDataSet();
            return result;
        }

    }
}
