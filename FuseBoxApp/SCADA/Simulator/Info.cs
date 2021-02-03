using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

using Properties;

public class Info : Form
{
   private IContainer components = null;

   private PictureBox pictureBox1;

   private Label lblVersion;

   private LinkLabel linkLabel1;

   private Label label1;

   private LinkLabel linkLabel2;

   public Info()
   {
      this.InitializeComponent();
      Assembly.GetExecutingAssembly().GetName().Version.ToString();
      this.lblVersion.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
   }

   private void label1_Click(object sender, EventArgs e)
   {
   }

   protected override void Dispose(bool disposing)
   {
      if (disposing && this.components != null)
      {
         this.components.Dispose();
      }

      base.Dispose(disposing);
   }

   private void InitializeComponent()
   {
      ComponentResourceManager resources =
         new ComponentResourceManager(typeof(Info));
      this.pictureBox1 = new PictureBox();
      this.lblVersion = new Label();
      this.linkLabel1 = new LinkLabel();
      this.label1 = new Label();
      this.linkLabel2 = new LinkLabel();
      ((ISupportInitialize)this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.pictureBox1.Image = Resources.PLCLoggerCompactBitmap;
      this.pictureBox1.Location = new System.Drawing.Point(12, 16);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(69, 67);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
      this.pictureBox1.TabIndex = 6;
      this.pictureBox1.TabStop = false;
      this.lblVersion.AutoSize = true;
      this.lblVersion.Location = new System.Drawing.Point(87, 39);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(60, 13);
      this.lblVersion.TabIndex = 14;
      this.lblVersion.Text = "Version 2.9";
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(86, 12);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(165, 13);
      this.linkLabel1.TabIndex = 13;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "http://www.EasyModbusTCP.net";
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(87, 66);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(158, 13);
      this.label1.TabIndex = 15;
      this.label1.Text = "(c) Rossmann-Engineering 2018";
      this.label1.Click += new EventHandler(this.label1_Click);
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.Location = new System.Drawing.Point(87, 82);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(189, 13);
      this.linkLabel2.TabIndex = 16;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "http://www.Rossmann-Engineering.de";
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(287, 103);
      this.Controls.Add(this.linkLabel2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblVersion);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.pictureBox1);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Info";
      this.ShowIcon = false;
      this.Text = "Info";
      ((ISupportInitialize)this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
   }
}