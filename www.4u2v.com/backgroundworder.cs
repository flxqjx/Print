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
    public partial class backgroundworder : Form
    {
        BackgroundWorker worker = null;
        public backgroundworder()
        {
            InitializeComponent();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                if (e.Argument != null && e.Argument is FilePair)
                {
                    FilePair obj = (FilePair)e.Argument;
                    string orgFile = obj.OrgFile;
                    string newFile = obj.NewFile;


                    FileStream readFileStream = new FileStream(orgFile, FileMode.Open, FileAccess.Read);
                    FileStream writeFileStream = new FileStream(newFile, FileMode.Create, FileAccess.Write);


                    long totalByte = readFileStream.Length;


                    int buffLength = 1024;
                    byte[] buff = new byte[buffLength];


                    long writtenByte = 0;
                    int everytimeReadByteLength = readFileStream.Read(buff, 0, buffLength);


                    while (everytimeReadByteLength > 0)
                    {

                        writeFileStream.Write(buff, 0, everytimeReadByteLength);
                        everytimeReadByteLength = readFileStream.Read(buff, 0, buffLength);

                        writtenByte += everytimeReadByteLength;
                        int percent = (int)(writtenByte * 100 / totalByte);
                        worker.ReportProgress(percent);
                    }
                    writeFileStream.Close();
                    readFileStream.Close();
                    worker.ReportProgress(100);


                }
            }
            catch (Exception ex)
            {
                worker.ReportProgress(100,ex);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                lblMsg.Text = (e.UserState as Exception).Message;
               
            }
            else
            {
                   
                progressBar1.Value = e.ProgressPercentage;
            
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblMsg.Text = "文件复制完成";
            System.Diagnostics.Process.Start("http://www.4u2v.com");
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
                     if (DialogResult.Cancel == MessageBox.Show("新文件已经存在，是不继续复制，新文件即被覆盖", "文件已存在", MessageBoxButtons.OKCancel))
                     {
                         copy = false ;
                     }
                 }
                 if (copy)
                 {

                     FilePair fileObj = new FilePair(orgFile, newFile);
                     lblMsg.Text = "文件开始复制";
                     worker.RunWorkerAsync(fileObj);
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

    
        public class FilePair
        {
            private string _orgFile;
            private string _newFile;
            public FilePair(string orgfile,string newfile)
            {
            _orgFile=orgfile;
            _newFile=newfile;
            }
            public string OrgFile
            {
             get{return _orgFile;}
             set{_orgFile=value;}
            }
            public string NewFile
            {
             get{return _newFile;}
             set{_newFile=value;}
            }
        }
}
