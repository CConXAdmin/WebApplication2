using ClosedXML.Excel;
using WebApplication2.Models; 
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;
using Humanizer;

namespace WebApplication2.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class ZipController : WebApplication2.Controllers.BaseController<ZipController>
    {
        public IActionResult Index(bool nolayout=false)
        {
            if (nolayout) ViewBag.NoLayout = true;
            return View();
        }
        public void Test()
        {
            Response.ContentType = "application/octet-stream";
            Response.Headers.Add("Content-Disposition", "attachment; filename=\"Bots.zip\"");

            var contentPath = hostingEnvironment.WebRootPath + "\\img\\";
            var files = Directory.GetFiles(contentPath);
            var i =0;
            using (ZipArchive archive = new ZipArchive(new PositionWrapperStream(Response.Body), ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    i++;
                    var first = System.IO.Path.GetFileName(file)[0];
                    var dir = $"Dir_{first}/";
                    //if(!archive.Entries.Any(e => e.FullName == dir))
                    //    archive.CreateEntry(dir);
                    var entry = archive.CreateEntry(dir + System.IO.Path.GetFileName(file));
 

                    using (var entryStream = entry.Open())
                    using (var fileStream = System.IO.File.OpenRead(file))
                    {
                        
                        fileStream.CopyTo(entryStream);
                    }
                }
            }
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
                    using (var zip = new ZipArchive(fs, ZipArchiveMode.Read))
                    {
                        var list = new List<ImportModel>();
                        var i = 0;
                        foreach (var row in zip.Entries)
                        {
                            i++;
                            list.Add(new ImportModel { Col1 = i.ToString(), Col2 = row.Name==""?"Directory": row.Name, Col3 = row.FullName + "("+ row.CompressedLength.Bytes() + ")"});
                        }

                        ViewBag.Error = zip.Entries.Count()  + " rows imported";
                        return View("Import", list);
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Import");
            }
        }
        //public async Task<IActionResult> PrintAllMarkups()
        //{
        //    var drawingmodel = db.Drawings.Include(x => x.Area);
        //    var drawinglist = new List<string>();
        //    foreach (var drawing in drawingmodel)
        //    {
        //        var area = drawing.Area.Description.Replace("/", "_");
        //        var drwg = drawing.Description.Replace("/", "_");
        //        area = System.IO.Path.GetInvalidPathChars()
        //            .Aggregate(area, (current, c) => current.Replace(c, '_'));
        //        drwg = System.IO.Path.GetInvalidFileNameChars()
        //            .Aggregate(drwg, (current, c) => current.Replace(c, '_'));


        //        var list = new List<byte[]>();
        //        list.Add(await AddAllMarkups(drawing.Id));
        //        byte[] byte1 = Combine(list);
        //        var basefolder = this.hostingEnvironment.WebRootPath + "\\uploads\\exportdrawings\\" + area + "\\";
        //        if (!System.IO.Directory.Exists(basefolder))
        //        {
        //            System.IO.Directory.CreateDirectory(basefolder);
        //        }
        //        await System.IO.File.WriteAllBytesAsync(basefolder + drwg + ".pdf", byte1);
        //        drawinglist.Add(basefolder + ".pdf");
        //    }

        //    var contentPath = this.hostingEnvironment.WebRootPath + "\\uploads\\exportdrawings\\";
        //    string zipPath = _hostingEnvironment.ContentRootPath + $"\\wwwroot\\uploads\\Markups - {DateTime.Now.Ticks}.zip";
        //    //string extractPath = _hostingEnvironment.ContentRootPath + $"\\wwwroot\\uploads\\extract - {DateTime.Now.Ticks}";
        //    if (System.IO.File.Exists(zipPath))
        //    {
        //        System.IO.File.Delete(zipPath);
        //    }
        //    //if (System.IO.Directory.Exists(extractPath))
        //    //{
        //    //    System.IO.Directory.Delete(extractPath, true);
        //    //}
        //    ZipFile.CreateFromDirectory(contentPath, zipPath);

        //    //ZipFile.ExtractToDirectory(zipPath, extractPath);
        //    return File(System.IO.File.ReadAllBytes(zipPath), "application/zip", $"Markups.zip");

        //}

        public class PositionWrapperStream : Stream
        {
            private readonly Stream wrapped;

            private long pos = 0;

            public PositionWrapperStream(Stream wrapped)
            {
                this.wrapped = wrapped;
            }

            public override bool CanSeek { get { return false; } }

            public override bool CanWrite { get { return true; } }

            public override long Position
            {
                get { return pos; }
                set { throw new NotSupportedException(); }
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                pos += count;
                wrapped.Write(buffer, offset, count);
            }

            public override void Flush()
            {
                wrapped.Flush();
            }

            protected override void Dispose(bool disposing)
            {
                wrapped.Dispose();
                base.Dispose(disposing);
            }

            // all the other required methods can throw NotSupportedException

            public override bool CanRead => throw new NotImplementedException();

            public override long Length => throw new NotImplementedException();

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }
        }
    }
}
