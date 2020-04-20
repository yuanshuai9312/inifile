using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public partial class Form1 : Form
    {
        string iniFilePath = "";

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            string directory = System.AppDomain.CurrentDomain.BaseDirectory;
            this.iniFilePath = Path.Combine(directory, "config.ini");
            readIniFile();
        }

        /// <summary>
        /// 读取ini文件信息
        /// </summary>
        private void readIniFile()
        {
            if (File.Exists(this.iniFilePath))
            {
                this.webAddresTxt.Text = IniFileHelper.GetValue("Web信息", "Web地址", this.iniFilePath);
                this.webLoginNameTxt.Text = IniFileHelper.GetValue("Web信息", "用户名", this.iniFilePath);
                this.webPwdTxt.Text = IniFileHelper.GetValue("Web信息", "密码", this.iniFilePath);
                this.ftpAddressTxt.Text = IniFileHelper.GetValue("FTPInfo", "address", this.iniFilePath);
                this.ftpLoginNameTxt.Text = IniFileHelper.GetValue("FTPInfo", "loginName", this.iniFilePath);
                this.ftpPwdTxt.Text = IniFileHelper.GetValue("FTPInfo", "pwd", this.iniFilePath);
                MessageBox.Show("读取文档成功");
            }
            else
            {
                MessageBox.Show("文件加载失败，请确认是否存在此文件：" + this.iniFilePath);
            }
        }

        /// <summary>
        /// 储存ini文件信息
        /// </summary>
        private void saveIniFile()
        {
            if (!File.Exists(this.iniFilePath))
            {
                using (File.Create(this.iniFilePath)) { };
            }

            IniFileHelper.SetValue("Web信息", "Web地址", this.webAddresTxt.Text ,this.iniFilePath);
            IniFileHelper.SetValue("Web信息", "用户名", this.webLoginNameTxt.Text, this.iniFilePath);
            IniFileHelper.SetValue("Web信息", "密码", this.webPwdTxt.Text, this.iniFilePath);
            IniFileHelper.SetValue("FTPInfo", "address", this.ftpAddressTxt.Text, this.iniFilePath);
            IniFileHelper.SetValue("FTPInfo", "loginName", this.ftpLoginNameTxt.Text, this.iniFilePath);
            IniFileHelper.SetValue("FTPInfo", "pwd", this.ftpPwdTxt.Text, this.iniFilePath);
            MessageBox.Show("存储成功");
        }

        // 【读取ini】
        private void readBtn_Click(object sender, EventArgs e)
        {
            readIniFile();
        }

        // 【保存ini】
        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveIniFile();
        }

        // 【移除section】
        private void removeSectionBtn_Click(object sender, EventArgs e)
        {
            bool rs = IniFileHelper.RemoveSection("Web信息", this.iniFilePath);
            if (rs)
            {
                MessageBox.Show("移除section成功");
            }
            else
            {
                MessageBox.Show("移除section失败");
            }
        }

        // 【移除key】
        private void removeKeyBtn_Click(object sender, EventArgs e)
        {
            bool rs = IniFileHelper.Removekey("Web信息","密码", this.iniFilePath);
            if (rs)
            {
                MessageBox.Show("移除key成功");
            }
            else
            {
                MessageBox.Show("移除key失败");
            }
        }

        // 【获取所有section】
        private void getAllSectionBtn_Click(object sender, EventArgs e)
        {
            List<string> rs = IniFileHelper.GetSectionNames(this.iniFilePath);
            MessageBox.Show(string.Join(",",rs));
        }

        // 【获取所有key】
        private void getAllKeyBtn_Click(object sender, EventArgs e)
        {
            List<string> rs = IniFileHelper.GetKeys("Web信息", this.iniFilePath);
            MessageBox.Show(string.Join(",", rs));
        }
    }
}
