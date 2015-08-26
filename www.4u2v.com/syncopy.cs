using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace www_4u2v_com
{
    public partial class syncopy : Form
    {
   
        public syncopy()
        {
            InitializeComponent();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;

        }






        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            string orgFile = textBox1.Text.Trim();
            string newFile= textBox2.Text.Trim();
            if(System.IO.File.Exists(orgFile))
            {

                bool copy = true;
                 if(System.IO.File.Exists(newFile))
                 {
                     if (DialogResult.OK == MessageBox.Show("新文件已经存在，是不继续复制，新文件即被覆盖", "文件已存在", MessageBoxButtons.OKCancel))
                     {
                         
                         System.IO.File.Delete(newFile);



                     }
                     else
                     {
                         copy = false ;
                     }
                 }
                 if (copy)
                 {

                     FilePair fileObj = new FilePair(orgFile, newFile);
                     lblMsg.Text = "文件开始复制";



                     FileStream readFileStream = new FileStream(orgFile, FileMode.Open, FileAccess.Read);
                     FileStream writeFileStream = new FileStream(newFile, FileMode.Create, FileAccess.Write);


                     long totalByte = (int)readFileStream.Length;


                     int buffLength = 1024;
                     byte[] buff = new byte[buffLength];


                     long writtenByte = 0;
                     int everytimeReadByteLength = readFileStream.Read(buff, 0, buffLength);


                     while (everytimeReadByteLength > 0)
                     {

                         writeFileStream.Write(buff, 0, everytimeReadByteLength);
                         everytimeReadByteLength = readFileStream.Read(buff, 0, buffLength);

                         writtenByte += everytimeReadByteLength;
                         int percent = (int)((writtenByte * 100) / totalByte);
              


                         lblMsg.Text = "文件复制已完成" + percent + "%";
                         progressBar1.Value = percent;

                         Application.DoEvents();
                     }

               
                     writeFileStream.Close();
                     readFileStream.Close();

                     lblMsg.Text = "文件复制已完成";
                     progressBar1.Value = 100;

                 }

            }
            else
            {
               lblMsg.Text="源文件不存在";
            }
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.4u2v.com");
        }

   

    }

    

}
