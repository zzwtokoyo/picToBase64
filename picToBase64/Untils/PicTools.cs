using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace picToBase64.Untils
{
    public class PicTools
    {
        private static PictureBox this_pictureBox;

        /// <summary>
        /// 显示url图片
        /// </summary>
        /// <param name="urlpath"></param>
        /// <returns>Image对象</returns>
        public static object urlToshowPic(string urlpath)
        {
            try
            {
                string path = string.Format(@urlpath);
                System.Net.WebRequest webreq = System.Net.WebRequest.Create(path);
                System.Net.WebResponse webres = webreq.GetResponse();
                using (System.IO.Stream stream = webres.GetResponseStream())
                {
                    this_pictureBox = new PictureBox();
                    this_pictureBox.Image = Image.FromStream(stream);
                }
                return this_pictureBox.Image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 图像转base64
        /// </summary>
        /// <param name="file">图片文件对象</param>
        /// <param name="drophead">包含头部文件</param>
        /// <returns></returns>
        public static string ConvertImageToBase64(Image file, bool drophead)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, file.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                string rtn = Convert.ToBase64String(imageBytes);
                return rtn;
            }
        }

        /// <summary>
        /// 图像转base64-非文件流
        /// </summary>
        /// <param name="file">图像文件对象</param>
        /// <param name="formatType">图像文件对象类型</param>
        /// <returns></returns>
        public static string ConvertImageToBase64(Image file, ImageFormat formatType)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, formatType);
                byte[] imageBytes = memoryStream.ToArray();
                string rtn = Convert.ToBase64String(imageBytes);
                return rtn;
            }
        }

        /// <summary>
        /// base64转成image
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static Image ConvertBase64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                return Image.FromStream(ms, true);
            }
        }

        /// <summary>
        /// 获取Image图片格式
        /// </summary>
        /// <param name="_img"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(Image _img, out string format)
        {
            if (_img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                format = ".jpg";
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            }
            if (_img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
            {
                format = ".gif";
                return System.Drawing.Imaging.ImageFormat.Gif;
            }
            if (_img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
            {
                format = ".png";
                return System.Drawing.Imaging.ImageFormat.Png;
            }
            if (_img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {
                format = ".bmp";
                return System.Drawing.Imaging.ImageFormat.Bmp;
            }
            format = string.Empty;
            return null;
        }

        /// <summary>
        /// 某个事件触发图层属性颜色变化
        /// </summary>
        /// <param name="e"></param>
        /// <param name="bitmap"></param>
        /// <param name="colorAlpha"></param>
        public static void ChangePictureAlpha(PaintEventArgs e, Bitmap bitmap, float colorAlpha)
        {
            // Create the Bitmap object and load it with the texture image.
            // Bitmap bitmap = new Bitmap("..//..//test.jpg");C:/Documents and Settings/Administrator/桌面/images
            // Initialize the color matrix.
            // Note the value 0.8 in row 4, column 4.
            float[][] matrixItems ={
            new float[] {1, 0, 0, 0, 0},
            new float[] {0, 1, 0,0 , 0},
            new float[] {0, 0, 1, 0, 0},
            new float[] {0, 0, 0, colorAlpha, 0},
            new float[] {0, 0, 0, 0, 1}};
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            // Create an ImageAttributes object and set its color matrix.  设置色调整矩阵
            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            // Now draw the semitransparent bitmap image.
            int iWidth = bitmap.Width;
            int iHeight = bitmap.Height;
            e.Graphics.DrawImage(
                bitmap,
                new Rectangle(0, 0, iWidth, iHeight),  // destination rectangle  图片位置
                0.0f,                          // source rectangle x
                0.0f,                          // source rectangle y
                iWidth,                        // source rectangle width
                iHeight,                       // source rectangle height
                GraphicsUnit.Pixel,
                imageAtt);
        }
    }
}