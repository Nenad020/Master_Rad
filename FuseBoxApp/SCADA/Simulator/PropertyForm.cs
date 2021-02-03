using System;
using System.ComponentModel;
using System.Windows.Forms;

public class PropertyForm : Form
{
   public delegate void settingsChangedEvent();

   private Settings settings = new Settings();

   private Settings settingsFromMainForm = new Settings();

   private IContainer components = null;

   private PropertyGrid propertyGrid1;

   private Button btnDischard;

   private Button btnAccept;

   public event settingsChangedEvent SettingsChangedEvent;

   public PropertyForm(Settings settings)
   {
      this.settingsFromMainForm.Port = settings.Port;
      this.settingsFromMainForm.ModbusTypeSelection = settings.ModbusTypeSelection;
      this.settings = settings;
      this.InitializeComponent();
      this.propertyGrid1.SelectedObject = settings;
   }

   private void btnDischard_Click(object sender, EventArgs e)
   {
      this.settings.Port = this.settingsFromMainForm.Port;
      this.settings.ModbusTypeSelection = this.settingsFromMainForm.ModbusTypeSelection;
      if (this.SettingsChangedEvent != null)
      {
         this.SettingsChangedEvent();
      }

      this.Close();
   }

   private void btnAccept_Click(object sender, EventArgs e)
   {
      if (this.SettingsChangedEvent != null)
      {
         this.SettingsChangedEvent();
      }

      this.Close();
   }

   private void propertyGrid1_Click(object sender, EventArgs e)
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
         new ComponentResourceManager(typeof(PropertyForm));
      this.propertyGrid1 = new PropertyGrid();
      this.btnDischard = new Button();
      this.btnAccept = new Button();
      this.SuspendLayout();
      this.propertyGrid1.Location = new System.Drawing.Point(3, 1);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(280, 206);
      this.propertyGrid1.TabIndex = 0;
      this.propertyGrid1.Click += new EventHandler(this.propertyGrid1_Click);
      this.btnDischard.Image = (System.Drawing.Image)resources.GetObject("btnDischard.Image");
      this.btnDischard.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnDischard.Location = new System.Drawing.Point(3, 228);
      this.btnDischard.Name = "btnDischard";
      this.btnDischard.Size = new System.Drawing.Size(75, 56);
      this.btnDischard.TabIndex = 37;
      this.btnDischard.Text = "Dischard";
      this.btnDischard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.btnDischard.UseVisualStyleBackColor = true;
      this.btnDischard.Click += new EventHandler(this.btnDischard_Click);
      this.btnAccept.Image = (System.Drawing.Image)resources.GetObject("btnAccept.Image");
      this.btnAccept.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnAccept.Location = new System.Drawing.Point(206, 228);
      this.btnAccept.Name = "btnAccept";
      this.btnAccept.Size = new System.Drawing.Size(75, 56);
      this.btnAccept.TabIndex = 38;
      this.btnAccept.Text = "Accept";
      this.btnAccept.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.btnAccept.UseVisualStyleBackColor = true;
      this.btnAccept.Click += new EventHandler(this.btnAccept_Click);
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 286);
      this.ControlBox = false;
      this.Controls.Add(this.btnAccept);
      this.Controls.Add(this.btnDischard);
      this.Controls.Add(this.propertyGrid1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "PropertyForm";
      this.Text = "Properties";
      this.ResumeLayout(false);
   }
}