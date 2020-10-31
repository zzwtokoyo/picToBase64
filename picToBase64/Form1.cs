using picToBase64.Untils;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace picToBase64
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButton3.Checked = true;
        }

        private void getfile_Porp(file file)
        {
            //图片大小
            label9.Text = file.filezise;
            //尺寸
            label10.Text = string.Format("{0}x{1}", file.filelength, file.filehegth);
            //类型
            label11.Text = file.filetype;
            //时间
            label12.Text = file.filecreatetime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                PublicInfo.image = this.pictureBox1.Image = (Image)PicTools.urlToshowPic(textBox1.Text);
                //查看图片属性
            }
            else
            {
                MessageBox.Show("请输入网址");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            DialogResult result = open.ShowDialog();
            if (result == DialogResult.OK)
            {
                PublicInfo.image = this.pictureBox1.Image = Image.FromFile(open.FileName);
                label4.Text = open.FileName;
                string type = string.Empty;
                PicTools.GetImageFormat(this.pictureBox1.Image, out type);
                label11.Text = type;
            }
            //查看图片属性
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox1.Text = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Text = string.Empty;
                label13.Text = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                string copy = this.textBox2.Text;
                Clipboard.SetDataObject(copy);
                label13.Text = "已复制到剪贴板";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Text = string.Empty;
            }
            if (this.pictureBox1.Image != null)
            {
                if (this.pictureBox1.Image.RawFormat != null)
                {
                    Task.Run(delegate
                    {
                        textBox2.Text = PicTools.ConvertImageToBase64(this.pictureBox1.Image, true);
                    }
                     );
                }
                else
                {
                    Task.Run(delegate
                    {
                        if (!string.IsNullOrEmpty(comboBox1.Text))
                        {
                            switch (comboBox1.Text)
                            {
                                case "jpg":
                                    textBox2.Text = PicTools.ConvertImageToBase64(this.pictureBox1.Image, ImageFormat.Jpeg);
                                    return;
                                case "bmp":
                                    textBox2.Text = PicTools.ConvertImageToBase64(this.pictureBox1.Image, ImageFormat.Bmp);
                                    return;
                                case "gif":
                                    textBox2.Text = PicTools.ConvertImageToBase64(this.pictureBox1.Image, ImageFormat.Gif);
                                    return;
                                case "png":
                                    textBox2.Text = PicTools.ConvertImageToBase64(this.pictureBox1.Image, ImageFormat.Png);
                                    return;
                                case "ico":
                                    textBox2.Text = PicTools.ConvertImageToBase64(this.pictureBox1.Image, ImageFormat.Icon);
                                    return;
                                case "tiff":
                                    textBox2.Text = PicTools.ConvertImageToBase64(this.pictureBox1.Image, ImageFormat.Tiff);
                                    return;
                            }
                            label11.Text = comboBox1.Text;
                        }
                        else
                            MessageBox.Show("请选择正确的图像格式");
                        return;
                    }
                     );
                }
            }
            else
            {
                pictureBox1.Image = null;
                MessageBox.Show("图片对象Error，请重新选择一张图片");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string textTemp = string.Empty;
            string base64str = string.Empty;
            string type = string.Empty;
            try
            {
                if (radioButton3.Checked == true)
                {
                    textTemp = textBox2.Text;
                    if (textTemp.Contains(","))
                    {
                        //存在类型判断
                        type = label14.Text = textTemp.Split(',')[0].Split('/')[1].Substring(0, 3);
                        base64str = textTemp.Split(',')[1];
                    }
                    else
                    {
                        MessageBox.Show("请选择图片类型");
                        return;
                    }
                }
                else
                {
                    textTemp = textBox2.Text;
                    if (string.IsNullOrEmpty(comboBox1.Text))
                    {
                        MessageBox.Show("请选择图像类型");
                        return;
                    }
                    else
                    {
                        type =label11.Text = comboBox1.Text;
                        base64str = textTemp;
                    }
                }

                if (!string.IsNullOrEmpty(base64str))
                {
                    PublicInfo.image = PicTools.ConvertBase64ToImage(base64str);
                    this.pictureBox1.Image = PublicInfo.image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("图像转换异常");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioButton3.Checked = false;
            radioButton3.Refresh();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Text = string.Empty;
        }
    }
}