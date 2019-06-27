using System;
using System.IO;
using OfficeOpenXml;

namespace L2_Task1_Excel
{
    public class PersonsWorkbook : IDisposable
    {
        private readonly ExcelPackage _package;
        private readonly ExcelWorkbook _workbook;
        private readonly ExcelWorksheet _worksheet;

        private bool _disposed;

        public PersonsWorkbook(Person[] persons)
        {
            _package = new ExcelPackage();
            _workbook = _package.Workbook;
            _worksheet = _package.Workbook.Worksheets.Add("Persons");

            _worksheet.Cells[1, 1].Value = "ID";
            _worksheet.Cells[1, 2].Value = "Family";
            _worksheet.Cells[1, 3].Value = "Name";
            _worksheet.Cells[1, 4].Value = "Phone";
            _worksheet.Cells[1, 5].Value = "Age";

            for (var i = 0; i < persons.Length; ++i)
            {
                var row = i + 2;

                _worksheet.Cells[row, 1].Value = i;
                _worksheet.Cells[row, 2].Value = persons[i].Family;
                _worksheet.Cells[row, 3].Value = persons[i].Name;
                _worksheet.Cells[row, 4].Value = persons[i].Phone;
                _worksheet.Cells[row, 5].Value = persons[i].Age;
            }
        }

        ~PersonsWorkbook()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _package.Dispose();
            }

            _disposed = true;
        }

        public ExcelPackage GetPackage()
        {
            return _package;
        }

        public ExcelWorkbook GetWorkbook()
        {
            return _workbook;
        }

        public ExcelWorksheet GetWorksheet()
        {
            return _worksheet;
        }

        public void SaveToFile(string fileName)
        {
            try
            {
                var file = new FileInfo(fileName);
                _package.SaveAs(file);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.Message);
            }
        }
    }
}
