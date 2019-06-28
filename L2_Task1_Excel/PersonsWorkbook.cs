using System;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace L2_Task1_Excel
{
    public class PersonsWorkbook : IDisposable
    {
        private readonly ExcelPackage _package;
        private readonly ExcelWorkbook _workbook;
        private readonly ExcelWorksheet _worksheet;

        private readonly int _leftColumn;
        private readonly int _rightColumn;
        private readonly int _headerRow;
        private readonly int _topBodyRow;
        private readonly int _bottomBodyRow;

        private bool _disposed;

        public PersonsWorkbook(Person[] persons) : this(persons, 1, 1) { }

        public PersonsWorkbook(Person[] persons, int leftCell, int topCell)
        {
            _leftColumn = leftCell;
            _rightColumn = _leftColumn + 4;
            _headerRow = topCell;
            _topBodyRow = topCell + 1;
            _bottomBodyRow = topCell + persons.Length;

            _package = new ExcelPackage();
            _workbook = _package.Workbook;
            _worksheet = _package.Workbook.Worksheets.Add("Persons");

            _worksheet.Cells[_headerRow, leftCell].Value = "ID";
            _worksheet.Cells[_headerRow, leftCell + 1].Value = "Family";
            _worksheet.Cells[_headerRow, leftCell + 2].Value = "Name";
            _worksheet.Cells[_headerRow, leftCell + 3].Value = "Phone";
            _worksheet.Cells[_headerRow, leftCell + 4].Value = "Age";

            for (var i = 0; i < persons.Length; ++i)
            {
                var row = _topBodyRow + i;

                _worksheet.Cells[row, leftCell].Value = i;
                _worksheet.Cells[row, leftCell + 1].Value = persons[i].Family;
                _worksheet.Cells[row, leftCell + 2].Value = persons[i].Name;
                _worksheet.Cells[row, leftCell + 3].Value = persons[i].Phone;
                _worksheet.Cells[row, leftCell + 4].Value = persons[i].Age;
            }

            _worksheet.Cells.AutoFitColumns(0);
        }

        public void SetProperties(string name, string title, string author, string company, string comments)
        {
            _worksheet.Name = name;

            _worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\"" + title;
            _worksheet.HeaderFooter.OddFooter.RightAlignedText =
                string.Format("Страница {0} из {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
            _worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;

            _worksheet.Workbook.Properties.Title = title;
            _worksheet.Workbook.Properties.Author = author;
            _worksheet.Workbook.Properties.Comments = comments;
            _worksheet.Workbook.Properties.Company = company;
        }

        public void SetDefaultStyle(Color color)
        {
            SetBodyStyle(Color.White, Color.LightGray, color);
            SetHeaderStyle(color, Color.White);
        }

        public void SetHeaderStyle(Color backgroundColor, Color color)
        {
            using (var range = _worksheet.Cells[_headerRow, _leftColumn, _headerRow, _rightColumn])
            {
                range.Style.Font.Bold = true;
                range.Style.Font.Color.SetColor(color);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(backgroundColor);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            using (var range = _worksheet.Cells[_headerRow, _leftColumn, _headerRow, _rightColumn - 1])
            {
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Color.SetColor(color);
            }
        }

        public void SetBodyStyle(Color backgroundColor1, Color backgroundColor2, Color color)
        {
            using (var range = _worksheet.Cells[_topBodyRow, _leftColumn, _bottomBodyRow, _rightColumn])
            {
                range.Style.Font.Bold = false;
                range.Style.Font.Color.SetColor(color);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(backgroundColor1);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Color.SetColor(color);
                range.Style.Border.Right.Color.SetColor(color);
            }

            for (var i = _topBodyRow; i <= _bottomBodyRow; ++i)
            {
                if ((i - _topBodyRow) % 2 != 0)
                {
                    using (var range = _worksheet.Cells[i, _leftColumn, i, _rightColumn])
                    {
                        range.Style.Fill.BackgroundColor.SetColor(backgroundColor2);
                    }
                }
            }

            using (var range = _worksheet.Cells[_topBodyRow, _leftColumn, _bottomBodyRow, _leftColumn])
            {
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Color.SetColor(color);
            }

            using (var range = _worksheet.Cells[_topBodyRow, _leftColumn + 1, _bottomBodyRow, _rightColumn - 1])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            }
        }

        public void SetTableBorder(ExcelBorderStyle borderStyle, Color color)
        {
            using (var range = _worksheet.Cells[_headerRow, _leftColumn, _headerRow, _rightColumn])
            {
                range.Style.Border.Top.Style = borderStyle;
                range.Style.Border.Top.Color.SetColor(color);
            }

            using (var range = _worksheet.Cells[_bottomBodyRow, _leftColumn, _bottomBodyRow, _rightColumn])
            {
                range.Style.Border.Bottom.Style = borderStyle;
                range.Style.Border.Bottom.Color.SetColor(color);
            }

            using (var range = _worksheet.Cells[_headerRow, _leftColumn, _bottomBodyRow, _leftColumn])
            {
                range.Style.Border.Left.Style = borderStyle;
                range.Style.Border.Left.Color.SetColor(color);
            }

            using (var range = _worksheet.Cells[_headerRow, _rightColumn, _bottomBodyRow, _rightColumn])
            {
                range.Style.Border.Right.Style = borderStyle;
                range.Style.Border.Right.Color.SetColor(color);
            }
        }

        public void SetPageLayoutView(bool pageLayoutView)
        {
            _worksheet.View.PageLayoutView = pageLayoutView;
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
