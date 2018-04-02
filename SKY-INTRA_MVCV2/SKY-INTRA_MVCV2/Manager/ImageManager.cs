using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace SKY_INTRA_MVCV2.Manager
{
    public class ImageManager
    {
        /**
        * this method will get all the PDF in a dir, and render them as image trimmed, and place in a another dir
        **/
        public void SavePDFAsImageTrim(string dirPath, string imgPath)
        {
            try
            {
  
                List<string> allFiles = new List<string>();
                DirectoryInfo d = new DirectoryInfo(dirPath);
                FileInfo[] Files = d.GetFiles("*.pdf");
                foreach (FileInfo fi in Files)
                {

                    using (var document = PdfiumViewer.PdfDocument.Load(fi.FullName))
                    {
                        //var image = document.Render(0, 300, 300, true);
                        //image.Save(@"C:\inetpub\wwwroot\SkylightInfoScreen\PresentationFiles\Kantine\Gæster2\PDF billeder\" + Path.GetFileNameWithoutExtension(fi.FullName) + ".png", ImageFormat.Png);
                        for (int index = 0; index < document.PageCount; index++)
                        {

                            var image = document.Render(index, 300, 300, true);
                            var trimImage = ImageTrim(new Bitmap(image));
                            trimImage.Save(imgPath + Path.GetFileNameWithoutExtension(fi.FullName) + ".png", ImageFormat.Png);
                        }
                    }
                }
                
            }

            catch (Exception ex)
            {
                Console.WriteLine("Der skete en fejl: " + ex.InnerException);
            }
        }


        /**
         * This method will remove all the white around in an image
         * */
        public Bitmap ImageTrim(Bitmap img)
        {
            //get image data
            BitmapData bd = img.LockBits(new Rectangle(Point.Empty, img.Size),
            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int[] rgbValues = new int[img.Height * img.Width];
            Marshal.Copy(bd.Scan0, rgbValues, 0, rgbValues.Length);
            img.UnlockBits(bd);


            #region determine bounds
            int left = bd.Width;
            int top = bd.Height;
            int right = 0;
            int bottom = 0;

            //determine top
            for (int i = 0; i < rgbValues.Length; i++)
            {
                int color = rgbValues[i] & 0xffffff;
                if (color != 0xffffff)
                {
                    int r = i / bd.Width;
                    int c = i % bd.Width;

                    if (left > c)
                    {
                        left = c;
                    }
                    if (right < c)
                    {
                        right = c;
                    }
                    bottom = r;
                    top = r;
                    break;
                }
            }

            //determine bottom
            for (int i = rgbValues.Length - 1; i >= 0; i--)
            {
                int color = rgbValues[i] & 0xffffff;
                if (color != 0xffffff)
                {
                    int r = i / bd.Width;
                    int c = i % bd.Width;

                    if (left > c)
                    {
                        left = c;
                    }
                    if (right < c)
                    {
                        right = c;
                    }
                    bottom = r;
                    break;
                }
            }

            if (bottom > top)
            {
                for (int r = top + 1; r < bottom; r++)
                {
                    //determine left
                    for (int c = 0; c < left; c++)
                    {
                        int color = rgbValues[r * bd.Width + c] & 0xffffff;
                        if (color != 0xffffff)
                        {
                            if (left > c)
                            {
                                left = c;
                                break;
                            }
                        }
                    }

                    //determine right
                    for (int c = bd.Width - 1; c > right; c--)
                    {
                        int color = rgbValues[r * bd.Width + c] & 0xffffff;
                        if (color != 0xffffff)
                        {
                            if (right < c)
                            {
                                right = c;
                                break;
                            }
                        }
                    }
                }
            }

            int width = right - left + 1;
            int height = bottom - top + 1;
            #endregion

            //copy image data
            int[] imgData = new int[width * height];
            for (int r = top; r <= bottom; r++)
            {
                Array.Copy(rgbValues, r * bd.Width + left, imgData, (r - top) * width, width);
            }

            //create new image
            Bitmap newImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData nbd
                = newImage.LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(imgData, 0, nbd.Scan0, imgData.Length);
            newImage.UnlockBits(nbd);

            return newImage;
        }
    }
}