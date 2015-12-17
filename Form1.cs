using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;


namespace PicLottery {
	public partial class Form1 : Form {
		// Import API for delay
		[DllImport("kernel32.dll")]
		private static extern uint GetTickCount();

		string fTitle = "图片抽奖器 V1.0";

		//delay
		private void Delay(uint ms) {
			uint start = GetTickCount();
			while (GetTickCount() - start < ms) {
				Application.DoEvents();
				Console.WriteLine((GetTickCount() - start).ToString());
			}
		}

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e) {
			this.Text = fTitle;
			return;
		}

		private void button2_Click(object sender, EventArgs e) {
			MessageBox.Show("将需要抽奖的图片放入 pics 目录下，\n单击抽奖即可随机抽取，\n支持 JPG PNG BMP 格式图片，\n文件名会显示在图片下方。\n\nCopyright © 2015 Sykie Chen\nhttp://www.devchen.com", fTitle);
			return;
		}

		private void button1_Click(object sender, EventArgs e) {
			button1.Enabled = false;
			//randomize
			Random rd = new Random();


			DirectoryInfo TheFolder = new DirectoryInfo(".\\pics");
			string[] ext = { "*.jpg", "*.png", "*.bmp" };
			List<FileInfo> AllFiles = new List<FileInfo>();

			foreach (string ex in ext) {
				FileInfo[] aFiles = TheFolder.GetFiles(ex);
				for (int ii = 0; ii < aFiles.Length; ii++) {
					AllFiles.Add(aFiles[ii]);
				}
			}
			//MessageBox.Show(AllFiles.Count.ToString());
			for (int i = 0; i < 40; i++) {
				int rnd = rd.Next(0, AllFiles.Count);
				pictureBox1.Image = Image.FromFile(AllFiles[rnd].FullName);
				label1.Text = AllFiles[rnd].Name.Substring(0, AllFiles[rnd].Name.Length - 4);
				if (i < 30) Delay(100);
				else Delay((uint)(100 + i * 10));
			}
			button1.Enabled = true;
			return;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			System.Environment.Exit(0);
		}
	}
}
