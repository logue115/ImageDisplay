using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Image_display
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();
        byte[] JPEG_SIGNATURE = {255, 216};

        private byte[] ReadTwoBytes(string filepath)
        {
            try
            {
                using (FileStream fsSource = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {

                    // Read two bytes from source file into a byte array.
                    byte[] bytes = new byte[2];
                    int n = fsSource.Read(bytes, 0, 2);
                    return bytes;
                }
            }
            catch
            {

                return (byte[])null;
            }
        }

        private bool IsJPEG(string filePath)
        {
            byte[] b = ReadTwoBytes(filePath);
            return b.SequenceEqual(JPEG_SIGNATURE);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            string Chosen_File = "";
            Chosen_File = ofd.FileName;
            ofd.Title = "insert an Image";
            ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ofd.FileName = "";
            ofd.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|All Files|*.*";
            if (ofd.ShowDialog()!=DialogResult.Cancel)
            {
                Chosen_File = ofd.FileName;
                /*byte[] b = ReadTwoBytes(Chosen_File);
                string toDisplay = string.Join(Environment.NewLine, b);
                MessageBox.Show(toDisplay);
                */
                if (IsJPEG(Chosen_File) == false)
                {
                    string msg = "This is not a JPEG File!";
                    MessageBox.Show(msg);
                    ofd.Dispose();
                }

                else
                {
                    Image img = Image.FromFile(Chosen_File);

                    this.Size = new Size(img.Width + 300, img.Height + 100);
                    button1.Location = new Point(img.Width + 100, 22);
                    pictureBox1.Size = new Size(img.Width, img.Height);
                    pictureBox1.Image = Image.FromFile(Chosen_File);
                }
            }

        }
    }
}
