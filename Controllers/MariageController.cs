using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCoder;
using TIS.Core.Entities.Mariage;



namespace TIS.Controllers
{
    public class MariageController : Controller
    {
        public List<TableMariage>? tables;

            public  MariageController()
        {
            LoadTablesFromJson();
        }

        public void LoadTablesFromJson()
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/tables.json");
            var json = System.IO.File.ReadAllText(jsonFilePath);
            tables = JsonConvert.DeserializeObject<List<TableMariage>>(json);
        }

        public IActionResult Index(int? id)
        {
            ViewBag.TableId = id; 
            return View(tables);
        }

        public IActionResult GenerateQr(int? id)
        {
            var table = tables.FirstOrDefault(t => t.Id == id);
            if (table == null) return NotFound();

            using (var qrGenerator = new QRCodeGenerator())
            {
                var url = Url.Action("Index", "Mariage", new { id = table.Id }, Request.Scheme);
                using (var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
                {
                    using (var qrCode = new PngByteQRCode(qrCodeData))
                    {
                        var qrImage = qrCode.GetGraphic(20);
                        return File(qrImage, "image/png");
                    }
                }
            }
        }
    }
}
