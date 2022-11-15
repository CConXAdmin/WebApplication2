using ClosedXML.Excel;
using WebApplication2.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class ExcelController : WebApplication2.Controllers.BaseController<ExcelController>
    {
        public IActionResult Index(bool nolayout = false)
        {
            if (nolayout) ViewBag.NoLayout = true;
            return View();
        }

        public async Task<ActionResult> Test()
        {
            XLWorkbook wbook = new XLWorkbook();
            IXLWorksheet Sheet = wbook.Worksheets.Add("Sheet 1");
            for (int i = 0; i < 10; i++)
            {
                Sheet.Cell(1, (i + 1)).Value = $"Col{(i + 1)}";
                Sheet.Cell(1, (i + 1)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                Sheet.Cell(1, (i + 1)).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            }
            for (int i = 0; i < 5; i++)
            { 
                for (int j = 0; j < 10; j++)
                {
                    Sheet.Cell((i + 2), (j + 1)).Value = $"V{i+1}{j+1}";
                    Sheet.Cell((i + 2), (j + 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    if (j==9)
                    {
                        Sheet.Cell((i + 2), (j + 1)).Style.Fill.BackgroundColor = XLColor.Yellow;
                    }
                    if (i == 4)
                    {
                        Sheet.Cell((i + 2), (j + 1)).Style.Fill.BackgroundColor = XLColor.Green;
                    }
                }
            }
            Stream spreadsheetStream = new MemoryStream();
            wbook.SaveAs(spreadsheetStream);
            spreadsheetStream.Position = 0;
            return new FileStreamResult(spreadsheetStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = "Report1.xlsx" };
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestImport(IFormFile file)
        {
            if (file == null)
            {
                ViewBag.Error = "No File";
                return View("Import");
            }
            try
            {
                var fileextension = System.IO.Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = System.IO.Path.Combine(hostingEnvironment.WebRootPath, "temp", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                int rowno = 1;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                var list = new List<ImportModel>();
                foreach (var row in rows)
                {
                    if (rowno != 1)
                    {
                        var test = row.Cell(1).Value.ToString();
                        if (string.IsNullOrWhiteSpace(test) || string.IsNullOrEmpty(test))
                        {
                            break;
                        }
                        ImportModel item = new ImportModel();
                        item.Col1 = row.Cell(1).Value.ToString();
                        item.Col2 = row.Cell(2).Value.ToString();
                        item.Col3 = row.Cell(3).Value.ToString(); 
                        list.Add(item);
                    }
                    else
                    {
                        if (row.Cell(1).Value.ToString() != "Col1") { ViewBag.Error = "Invalid column "; break; } ;
                        if (row.Cell(2).Value.ToString() != "Col2") { ViewBag.Error = "Invalid column "; break; };
                        if (row.Cell(3).Value.ToString() != "Col3") { ViewBag.Error = "Invalid column "; break; };
                        rowno = 2;
                    }
                }

                ViewBag.Error += (rows.Count() - 1) + (ViewBag.Error==null?" rows imported": " rows detected but not imported");
                return View("Import", list);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Import");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestImport2(IFormFile file)
        {
            if (file == null)
            {
                ViewBag.Error = "No File";
                return View("Import2");
            }
            try
            {
                var fileextension = System.IO.Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = System.IO.Path.Combine(hostingEnvironment.WebRootPath, "temp", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                int rowno = 1;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                var list = new List<List<string>>();
                foreach (var row in rows)
                {
                    if (rowno != 0)
                    {
                        var test = row.Cell(1).Value.ToString();
                        if (string.IsNullOrWhiteSpace(test) || string.IsNullOrEmpty(test))
                        {
                            break;
                        }
                        var item = new List<string>();
                        for (var col = 1; col <= sheets.Columns().Count(); col++) {
                            item.Add("<h6 class='"+row.Cell(col).DataType.ToString()+" fg-black' style='background: rgb("+ row.Cell(col).Style.Fill.BackgroundColor.Color.R+","+ row.Cell(col).Style.Fill.BackgroundColor.Color.G+","+ row.Cell(col).Style.Fill.BackgroundColor.Color.B + ")'>" + row.Cell(col).Value.ToString()+ "</h6>" );
                        } 
                        list.Add(item);
                    }
                    else
                    {
                        rowno = 2;
                    }
                }

                ViewBag.Error += (rows.Count() - 1) +  " rows imported";
                return View("Import2", list);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Import2");
            }
        }

    }
}
