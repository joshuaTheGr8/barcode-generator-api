using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace barcode_generator.NewFolder1
{
    public class Helper
    {
        public static byte[] ConvertToByteArray(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        public static Bitmap MergeTwoImages(Image firstImage, Image secondImage)
        {
            if (firstImage == null)
            {
                throw new ArgumentNullException("firstImage");
            }

            if (secondImage == null)
            {
                throw new ArgumentNullException("secondImage");
            }

            int outputImageWidth = firstImage.Width > secondImage.Width ? firstImage.Width : secondImage.Width;

            int outputImageHeight = firstImage.Height + secondImage.Height + 1;

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(outputImage))
            {
                var r1 = new Rectangle(new Point(), firstImage.Size);
                var r2 = new Rectangle(new Point(), firstImage.Size);

                graphics.DrawImage(firstImage, r1, r2 , GraphicsUnit.Pixel);

                var r3 = new Rectangle(new Point(0, firstImage.Height + 1), secondImage.Size);
                var r4 = new Rectangle(new Point(), secondImage.Size);

                //graphics.FillRectangle(new SolidBrush(Color.White), r2);

                graphics.DrawImage(secondImage, r3, r4, GraphicsUnit.Pixel);
            }

            return outputImage;
        }

        public static Bitmap ImageFromText(string sImageText, int fontSize)
        {
            Bitmap objBmpImage = new Bitmap(2, 2);

            int intWidth = 0;
            int intHeight = 0;

            // Create the Font object for the image text drawing.
            System.Drawing.Font objFont = new System.Drawing.Font("Times New Roman", fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));


            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color

            objGraphics.Clear(System.Drawing.Color.White);
            objGraphics.SmoothingMode = SmoothingMode.HighQuality;

            objGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit; //  <-- This is the correct value to use. ClearTypeGridFit is better yet!
            objGraphics.DrawString(sImageText, objFont, new SolidBrush(System.Drawing.Color.Black), 0, 0, StringFormat.GenericTypographic);

            objGraphics.Flush();

            return (objBmpImage);
        }
    }
  
}