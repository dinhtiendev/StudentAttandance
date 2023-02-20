using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using StudentAttandanceLibrary.Services.Interfaces;
using System.Data;

namespace StudentAttandanceLibrary.Services
{
    public class ExcelService : IExcelService
    {
        public DataTable GetSheetFromTemplate(string fileName, int sheetIndex = 0)
        {
            string pathFile = @$"{Directory.GetCurrentDirectory()}/Templates/{fileName}";
            var stream = File.OpenRead(pathFile);
            var reader = ExcelReaderFactory.CreateReader(stream);
            var result = CreateDataSet(reader);
            var table = result.Tables;
            var sheet = table[sheetIndex];
            return sheet;
        }

        public DataTable GetSheetFromFile(IFormFile uploadFile, int sheetIndex = 0)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                uploadFile.CopyToAsync(memoryStream).ConfigureAwait(false);
                memoryStream.Position = 0;
                var reader = ExcelReaderFactory.CreateReader(memoryStream);
                //Get DataSet
                var result = CreateDataSet(reader);
                var table = result.Tables;
                var sheet = table[sheetIndex];
                return sheet;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                memoryStream.Close();
            }
            return null;
        }

        public DataTable GetSheetFromStream(MemoryStream memoryStream, int sheetIndex = 0)
        {
            try
            {
                var reader = ExcelReaderFactory.CreateReader(memoryStream);
                //Get DataSet
                var result = CreateDataSet(reader);
                var table = result.Tables;
                var sheet = table[sheetIndex];
                return sheet;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                memoryStream.Close();
            }
            return null;
        }

        public int SheetCount(IFormFile uploadFile)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                if (uploadFile == null)
                {
                    return 0;
                }
                uploadFile.CopyToAsync(memoryStream).ConfigureAwait(false);
                memoryStream.Position = 0;
                var reader = ExcelReaderFactory.CreateReader(memoryStream);
                //Get DataSet
                var result = CreateDataSet(reader);
                var numberOfSheet = result.Tables.Count;
                return numberOfSheet;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                memoryStream.Close();
            }
            return 0;
        }

        public List<string> GetColumnsInHeader(DataTable dataTable, int rowIndex = 0, int startColumn = 0)
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
                var numberColumn = row.Table.Columns.Count;
                for (int i = startColumn; i < numberColumn; i++)
                {
                    columns.Add(Convert.ToString(dataTable!.Rows[rowIndex][i].ToString().Trim()));
                }
                return columns;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool CheckHeaderFileExcel(List<string> columnsTemplate, List<string> columnsUpload, int rowIndex = 0, int startColumn = 0)
        {
            try
            {
                //Check header file upload
                if (columnsUpload == null)
                {
                    return false;
                }
                //Check size of list columns template vs list columns upload
                if (columnsTemplate.Count != columnsUpload.Count)
                {
                    return false;
                }
                var checkHeader = columnsUpload.SequenceEqual(columnsTemplate);
                if (!checkHeader)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public DataSet CreateDataSet(IExcelDataReader reader)
        {
            var result = reader.AsDataSet();
            return result;
        }

    }
}
