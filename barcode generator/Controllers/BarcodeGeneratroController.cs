using barcode_generator.NewFolder1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing.QrCode;

namespace barcode_generator.Controllers
{
    public class BarcodeGeneratroController : Controller
    {
        public System.Web.Mvc.FileResult GetTest(string s)
        {
            var img = Helper.ImageFromText(s, 30);
            var bytes = Helper.ConvertToByteArray(img);
            return File(bytes, "image/png");
        }

        public System.Web.Mvc.FileResult GetBarcode(string code, int codeType = (int)ZXing.BarcodeFormat.CODE_128)
        {
            var options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 300,
                Height = 100
            };

            var qr = new ZXing.BarcodeWriter();
            qr.Options = options;
            qr.Format = (ZXing.BarcodeFormat)codeType;
            var img = new Bitmap(qr.Write(code));

            var bytes = Helper.ConvertToByteArray(img);
            return File(bytes, "image/png");

        }

        public System.Web.Mvc.FileResult GetBarcode2(string code, string name, string batchNumber, string lotNumber, 
            bool includeLotBatchInCode = false, int width = 300, int height = 100,
            int fontSize = 20, int codeType = (int)ZXing.BarcodeFormat.CODE_128)
        {
            var options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height
            };

            var qr = new ZXing.BarcodeWriter();
            qr.Options = options;
            qr.Format = (ZXing.BarcodeFormat)codeType;

            if (includeLotBatchInCode)
                code += batchNumber + lotNumber;

            var img = new Bitmap(qr.Write(code));
            var imgName = new Bitmap(Helper.ImageFromText(name, fontSize));
            var imgBatch = new Bitmap(Helper.ImageFromText($"Batch No: {batchNumber}", fontSize));
            var imgLot = new Bitmap(Helper.ImageFromText($"Lot No: {lotNumber}", fontSize));

            var c1 = Helper.MergeTwoImages(img, imgName);
            var c2 = Helper.MergeTwoImages(imgBatch, imgLot);
            var finalImg = Helper.MergeTwoImages(c1, c2);

            var bytes = Helper.ConvertToByteArray(finalImg);
            return File(bytes, "image/png");
        }
    }
}