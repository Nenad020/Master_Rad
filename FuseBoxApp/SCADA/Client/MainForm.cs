using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

using EasyModbus;

using Properties;

public class MainForm : Form
{
   private delegate void UpdateReceiveDataCallback();

   private ModbusClient modbusClient;

   private string receiveData = null;

   private string sendData = null;

   private bool listBoxPrepareCoils = false;

   private bool listBoxPrepareRegisters = false;

   private IContainer components = null;

   private TextBox txtIpAddressInput;

   private Label txtIpAddress;

   private Label txtPort;

   private TextBox txtPortInput;

   private Button btnReadCoils;

   private Button btnReadDiscreteInputs;

   private Button btnReadHoldingRegisters;

   private Button btnReadInputRegisters;

   private TextBox txtStartingAddressInput;

   private Label txtStartingAddress;

   private Label txtNumberOfValues;

   private TextBox txtNumberOfValuesInput;

   private ListBox lsbAnswerFromServer;

   private PictureBox pictureBox1;

   private LinkLabel linkLabel1;

   private ComboBox cbbSelctionModbus;

   private Label txtCOMPort;

   private ComboBox cbbSelectComPort;

   private Label txtSlaveAddress;

   private TextBox txtSlaveAddressInput;

   private TextBox textBox1;

   private Button btnPrepareCoils;

   private Button button1;

   private Button btnWriteMultipleRegisters;

   private Button btnWriteMultipleCoils;

   private Button btnWriteSingleRegister;

   private Button btnWriteSingleCoil;

   private TextBox txtCoilValue;

   private TextBox txtRegisterValue;

   private Button btnClear;

   private Button button2;

   private Label lblReadOperations;

   private Button button3;

   private Button button4;

   private Label label1;

   private Label label4;

   private TextBox txtStartingAddressOutput;

   private ListBox lsbWriteToServer;

   private Label lblParity;

   private Label lblStopbits;

   private ComboBox cbbParity;

   private ComboBox cbbStopbits;

   private TextBox txtBaudrate;

   private Label lblBaudrate;

   private TextBox txtConnectedStatus;

   public MainForm()
   {
      this.InitializeComponent();
      this.modbusClient = new ModbusClient();
      this.modbusClient.ReceiveDataChanged += this.UpdateReceiveData;
      this.modbusClient.SendDataChanged += this.UpdateSendData;
      this.modbusClient.ConnectedChanged += this.UpdateConnectedChanged;
   }

   private void UpdateReceiveData(object sender)
   {
      this.receiveData = "Rx: " + BitConverter.ToString(this.modbusClient.receiveData).Replace("-", " ") + Environment.NewLine;
      Thread thread = new Thread(this.updateReceiveTextBox);
      thread.Start();
   }

   private void updateReceiveTextBox()
   {
      if (this.textBox1.InvokeRequired)
      {
         UpdateReceiveDataCallback method = this.updateReceiveTextBox;
         this.Invoke(method, new object[0]);
      }
      else
      {
         this.textBox1.AppendText(this.receiveData);
      }
   }

   private void UpdateSendData(object sender)
   {
      this.sendData = "Tx: " + BitConverter.ToString(this.modbusClient.sendData).Replace("-", " ") + Environment.NewLine;
      Thread thread = new Thread(this.updateSendTextBox);
      thread.Start();
   }

   private void updateSendTextBox()
   {
      if (this.textBox1.InvokeRequired)
      {
         UpdateReceiveDataCallback method = this.updateSendTextBox;
         this.Invoke(method, new object[0]);
      }
      else
      {
         this.textBox1.AppendText(this.sendData);
      }
   }

   private void BtnConnectClick(object sender, EventArgs e)
   {
      this.modbusClient.IPAddress = this.txtIpAddressInput.Text;
      this.modbusClient.Port = int.Parse(this.txtPortInput.Text);
      this.modbusClient.Connect();
   }

   private void BtnReadCoilsClick(object sender, EventArgs e)
   {
      checked
      {
         try
         {
            if (!this.modbusClient.Connected)
            {
               this.button3_Click(null, null);
            }

            bool[] array = this.modbusClient.ReadCoils(
               int.Parse(this.txtStartingAddressInput.Text) - 1,
               int.Parse(this.txtNumberOfValuesInput.Text));
            this.lsbAnswerFromServer.Items.Clear();
            for (int i = 0; i < array.Length; i++)
            {
               this.lsbAnswerFromServer.Items.Add(array[i]);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(
               ex.Message,
               "Exception Reading values from Server",
               MessageBoxButtons.OK,
               MessageBoxIcon.Hand);
         }
      }
   }

   private void btnReadDiscreteInputs_Click(object sender, EventArgs e)
   {
      checked
      {
         try
         {
            if (!this.modbusClient.Connected)
            {
               this.button3_Click(null, null);
            }

            bool[] array = this.modbusClient.ReadDiscreteInputs(
               int.Parse(this.txtStartingAddressInput.Text) - 1,
               int.Parse(this.txtNumberOfValuesInput.Text));
            this.lsbAnswerFromServer.Items.Clear();
            for (int i = 0; i < array.Length; i++)
            {
               this.lsbAnswerFromServer.Items.Add(array[i]);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(
               ex.Message,
               "Exception Reading values from Server",
               MessageBoxButtons.OK,
               MessageBoxIcon.Hand);
         }
      }
   }

   private void btnReadHoldingRegisters_Click(object sender, EventArgs e)
   {
      checked
      {
         try
         {
            if (!this.modbusClient.Connected)
            {
               this.button3_Click(null, null);
            }

            int[] array = this.modbusClient.ReadHoldingRegisters(
               int.Parse(this.txtStartingAddressInput.Text) - 1,
               int.Parse(this.txtNumberOfValuesInput.Text));
            this.lsbAnswerFromServer.Items.Clear();
            for (int i = 0; i < array.Length; i++)
            {
               this.lsbAnswerFromServer.Items.Add(array[i]);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(
               ex.Message,
               "Exception Reading values from Server",
               MessageBoxButtons.OK,
               MessageBoxIcon.Hand);
         }
      }
   }

   private void btnReadInputRegisters_Click(object sender, EventArgs e)
   {
      checked
      {
         try
         {
            if (!this.modbusClient.Connected)
            {
               this.button3_Click(null, null);
            }

            int[] array = this.modbusClient.ReadInputRegisters(
               int.Parse(this.txtStartingAddressInput.Text) - 1,
               int.Parse(this.txtNumberOfValuesInput.Text));
            this.lsbAnswerFromServer.Items.Clear();
            for (int i = 0; i < array.Length; i++)
            {
               this.lsbAnswerFromServer.Items.Add(array[i]);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(
               ex.Message,
               "Exception Reading values from Server",
               MessageBoxButtons.OK,
               MessageBoxIcon.Hand);
         }
      }
   }

   private void pictureBox1_Click(object sender, EventArgs e)
   {
      Process.Start("http://www.EasyModbusTCP.net");
   }

   private void cbbSelctionModbus_SelectedIndexChanged(object sender, EventArgs e)
   {
      if (this.modbusClient.Connected)
      {
         this.modbusClient.Disconnect();
      }

      if (this.cbbSelctionModbus.SelectedIndex == 0)
      {
         this.txtIpAddress.Visible = true;
         this.txtIpAddressInput.Visible = true;
         this.txtPort.Visible = true;
         this.txtPortInput.Visible = true;
         this.txtCOMPort.Visible = false;
         this.cbbSelectComPort.Visible = false;
         this.txtSlaveAddress.Visible = false;
         this.txtSlaveAddressInput.Visible = false;
         this.lblBaudrate.Visible = false;
         this.lblParity.Visible = false;
         this.lblStopbits.Visible = false;
         this.txtBaudrate.Visible = false;
         this.cbbParity.Visible = false;
         this.cbbStopbits.Visible = false;
      }

      if (this.cbbSelctionModbus.SelectedIndex == 1)
      {
         this.cbbSelectComPort.SelectedIndex = 0;
         this.cbbParity.SelectedIndex = 0;
         this.cbbStopbits.SelectedIndex = 0;
         if (this.cbbSelectComPort.SelectedText == string.Empty)
         {
            this.cbbSelectComPort.SelectedItem.ToString();
         }

         this.txtIpAddress.Visible = false;
         this.txtIpAddressInput.Visible = false;
         this.txtPort.Visible = false;
         this.txtPortInput.Visible = false;
         this.txtCOMPort.Visible = true;
         this.cbbSelectComPort.Visible = true;
         this.txtSlaveAddress.Visible = true;
         this.txtSlaveAddressInput.Visible = true;
         this.lblBaudrate.Visible = true;
         this.lblParity.Visible = true;
         this.lblStopbits.Visible = true;
         this.txtBaudrate.Visible = true;
         this.cbbParity.Visible = true;
         this.cbbStopbits.Visible = true;
      }
   }

   private void cbbSelectComPort_SelectedIndexChanged(object sender, EventArgs e)
   {
      if (this.modbusClient.Connected)
      {
         this.modbusClient.Disconnect();
      }

      this.modbusClient.SerialPort = this.cbbSelectComPort.SelectedItem.ToString();
      this.modbusClient.UnitIdentifier = byte.Parse(this.txtSlaveAddressInput.Text);
   }

   private void TxtSlaveAddressInputTextChanged(object sender, EventArgs e)
   {
      try
      {
         this.modbusClient.UnitIdentifier = byte.Parse(this.txtSlaveAddressInput.Text);
      }
      catch (FormatException)
      {
      }
   }

   private void btnPrepareCoils_Click(object sender, EventArgs e)
   {
      if (!this.listBoxPrepareCoils)
      {
         this.lsbAnswerFromServer.Items.Clear();
      }

      this.listBoxPrepareCoils = true;
      this.listBoxPrepareRegisters = false;
      this.lsbWriteToServer.Items.Add(this.txtCoilValue.Text);
   }

   private void button1_Click(object sender, EventArgs e)
   {
      if (!this.listBoxPrepareRegisters)
      {
         this.lsbAnswerFromServer.Items.Clear();
      }

      this.listBoxPrepareRegisters = true;
      this.listBoxPrepareCoils = false;
      this.lsbWriteToServer.Items.Add(int.Parse(this.txtRegisterValue.Text));
   }

   private void btnWriteSingleCoil_Click(object sender, EventArgs e)
   {
      try
      {
         if (!this.modbusClient.Connected)
         {
            this.button3_Click(null, null);
         }

         bool flag = false;
         flag = bool.Parse(this.lsbWriteToServer.Items[0].ToString());
         this.modbusClient.WriteSingleCoil(checked(int.Parse(this.txtStartingAddressOutput.Text) - 1), flag);
      }
      catch (Exception ex)
      {
         MessageBox.Show(
            ex.Message,
            "Exception writing values to Server",
            MessageBoxButtons.OK,
            MessageBoxIcon.Hand);
      }
   }

   private void btnWriteSingleRegister_Click(object sender, EventArgs e)
   {
      try
      {
         if (!this.modbusClient.Connected)
         {
            this.button3_Click(null, null);
         }

         int num = 0;
         num = int.Parse(this.lsbWriteToServer.Items[0].ToString());
         this.modbusClient.WriteSingleRegister(checked(int.Parse(this.txtStartingAddressOutput.Text) - 1), num);
      }
      catch (Exception ex)
      {
         MessageBox.Show(
            ex.Message,
            "Exception writing values to Server",
            MessageBoxButtons.OK,
            MessageBoxIcon.Hand);
      }
   }

   private void btnWriteMultipleCoils_Click(object sender, EventArgs e)
   {
      checked
      {
         try
         {
            if (!this.modbusClient.Connected)
            {
               this.button3_Click(null, null);
            }

            bool[] array = new bool[this.lsbWriteToServer.Items.Count];
            for (int i = 0; i < this.lsbWriteToServer.Items.Count; i++)
            {
               array[i] = bool.Parse(this.lsbWriteToServer.Items[i].ToString());
            }

            this.modbusClient.WriteMultipleCoils(int.Parse(this.txtStartingAddressOutput.Text) - 1, array);
         }
         catch (Exception ex)
         {
            MessageBox.Show(
               ex.Message,
               "Exception writing values to Server",
               MessageBoxButtons.OK,
               MessageBoxIcon.Hand);
         }
      }
   }

   private void btnWriteMultipleRegisters_Click(object sender, EventArgs e)
   {
      checked
      {
         try
         {
            if (!this.modbusClient.Connected)
            {
               this.button3_Click(null, null);
            }

            int[] array = new int[this.lsbWriteToServer.Items.Count];
            for (int i = 0; i < this.lsbWriteToServer.Items.Count; i++)
            {
               array[i] = int.Parse(this.lsbWriteToServer.Items[i].ToString());
            }

            this.modbusClient.WriteMultipleRegisters(int.Parse(this.txtStartingAddressOutput.Text) - 1, array);
         }
         catch (Exception ex)
         {
            MessageBox.Show(
               ex.Message,
               "Exception writing values to Server",
               MessageBoxButtons.OK,
               MessageBoxIcon.Hand);
         }
      }
   }

   private void lsbAnswerFromServer_DoubleClick(object sender, EventArgs e)
   {
      int selectedIndex = this.lsbAnswerFromServer.SelectedIndex;
   }

   private void txtCoilValue_DoubleClick(object sender, EventArgs e)
   {
      if (this.txtCoilValue.Text.Equals("FALSE"))
      {
         this.txtCoilValue.Text = "TRUE";
      }
      else
      {
         this.txtCoilValue.Text = "FALSE";
      }
   }

   private void btnClear_Click(object sender, EventArgs e)
   {
      this.lsbWriteToServer.Items.Clear();
   }

   private void button2_Click(object sender, EventArgs e)
   {
      int selectedIndex = this.lsbWriteToServer.SelectedIndex;
      if (selectedIndex >= 0)
      {
         this.lsbWriteToServer.Items.RemoveAt(selectedIndex);
      }
   }

   private void MainForm_Load(object sender, EventArgs e)
   {
   }

   private void textBox1_TextChanged(object sender, EventArgs e)
   {
   }

   private void txtRegisterValue_TextChanged(object sender, EventArgs e)
   {
   }

   private void button3_Click(object sender, EventArgs e)
   {
      try
      {
         if (this.modbusClient.Connected)
         {
            this.modbusClient.Disconnect();
         }

         if (this.cbbSelctionModbus.SelectedIndex == 0)
         {
            this.modbusClient.IPAddress = this.txtIpAddressInput.Text;
            this.modbusClient.Port = int.Parse(this.txtPortInput.Text);
            this.modbusClient.SerialPort = null;
            this.modbusClient.Connect();
         }

         if (this.cbbSelctionModbus.SelectedIndex == 1)
         {
            this.modbusClient.SerialPort = this.cbbSelectComPort.SelectedItem.ToString();
            this.modbusClient.UnitIdentifier = byte.Parse(this.txtSlaveAddressInput.Text);
            this.modbusClient.Baudrate = int.Parse(this.txtBaudrate.Text);
            if (this.cbbParity.SelectedIndex == 0)
            {
               this.modbusClient.Parity = Parity.Even;
            }

            if (this.cbbParity.SelectedIndex == 1)
            {
               this.modbusClient.Parity = Parity.Odd;
            }

            if (this.cbbParity.SelectedIndex == 2)
            {
               this.modbusClient.Parity = Parity.None;
            }

            if (this.cbbStopbits.SelectedIndex == 0)
            {
               this.modbusClient.StopBits = StopBits.One;
            }

            if (this.cbbStopbits.SelectedIndex == 1)
            {
               this.modbusClient.StopBits = StopBits.OnePointFive;
            }

            if (this.cbbStopbits.SelectedIndex == 2)
            {
               this.modbusClient.StopBits = StopBits.Two;
            }

            this.modbusClient.Connect();
         }
      }
      catch (Exception ex)
      {
         MessageBox.Show(ex.Message, "Unable to connect to Server", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
   }

   private void UpdateConnectedChanged(object sender)
   {
      if (this.modbusClient.Connected)
      {
         this.txtConnectedStatus.Text = "Connected to Server";
         this.txtConnectedStatus.BackColor = Color.Green;
      }
      else
      {
         this.txtConnectedStatus.Text = "Not Connected to Server";
         this.txtConnectedStatus.BackColor = Color.Red;
      }
   }

   private void button4_Click(object sender, EventArgs e)
   {
      this.modbusClient.Disconnect();
   }

   private void txtBaudrate_TextChanged(object sender, EventArgs e)
   {
      if (this.modbusClient.Connected)
      {
         this.modbusClient.Disconnect();
      }

      this.modbusClient.Baudrate = int.Parse(this.txtBaudrate.Text);
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
         new ComponentResourceManager(typeof(MainForm));
      this.txtIpAddressInput = new TextBox();
      this.txtIpAddress = new Label();
      this.txtPort = new Label();
      this.txtPortInput = new TextBox();
      this.btnReadCoils = new Button();
      this.btnReadDiscreteInputs = new Button();
      this.btnReadHoldingRegisters = new Button();
      this.btnReadInputRegisters = new Button();
      this.txtStartingAddressInput = new TextBox();
      this.txtStartingAddress = new Label();
      this.txtNumberOfValues = new Label();
      this.txtNumberOfValuesInput = new TextBox();
      this.lsbAnswerFromServer = new ListBox();
      this.linkLabel1 = new LinkLabel();
      this.cbbSelctionModbus = new ComboBox();
      this.txtCOMPort = new Label();
      this.cbbSelectComPort = new ComboBox();
      this.txtSlaveAddress = new Label();
      this.txtSlaveAddressInput = new TextBox();
      this.textBox1 = new TextBox();
      this.btnWriteMultipleRegisters = new Button();
      this.btnWriteMultipleCoils = new Button();
      this.btnWriteSingleRegister = new Button();
      this.btnWriteSingleCoil = new Button();
      this.txtCoilValue = new TextBox();
      this.txtRegisterValue = new TextBox();
      this.button2 = new Button();
      this.btnClear = new Button();
      this.button1 = new Button();
      this.btnPrepareCoils = new Button();
      this.pictureBox1 = new PictureBox();
      this.lblReadOperations = new Label();
      this.button3 = new Button();
      this.button4 = new Button();
      this.label1 = new Label();
      this.label4 = new Label();
      this.txtStartingAddressOutput = new TextBox();
      this.lsbWriteToServer = new ListBox();
      this.lblParity = new Label();
      this.lblStopbits = new Label();
      this.cbbParity = new ComboBox();
      this.cbbStopbits = new ComboBox();
      this.txtBaudrate = new TextBox();
      this.lblBaudrate = new Label();
      this.txtConnectedStatus = new TextBox();
      ((ISupportInitialize)this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.txtIpAddressInput.Location = new Point(34, 55);
      this.txtIpAddressInput.Name = "txtIpAddressInput";
      this.txtIpAddressInput.Size = new Size(118, 20);
      this.txtIpAddressInput.TabIndex = 0;
      this.txtIpAddressInput.Text = "127.0.0.1";
      this.txtIpAddress.Location = new Point(34, 35);
      this.txtIpAddress.Name = "txtIpAddress";
      this.txtIpAddress.Size = new Size(100, 14);
      this.txtIpAddress.TabIndex = 1;
      this.txtIpAddress.Text = "Server IP-Address";
      this.txtPort.Location = new Point(158, 35);
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new Size(73, 17);
      this.txtPort.TabIndex = 3;
      this.txtPort.Text = "Server Port";
      this.txtPortInput.Location = new Point(158, 55);
      this.txtPortInput.Name = "txtPortInput";
      this.txtPortInput.Size = new Size(56, 20);
      this.txtPortInput.TabIndex = 2;
      this.txtPortInput.Text = "502";
      this.btnReadCoils.Location = new Point(12, 176);
      this.btnReadCoils.Name = "btnReadCoils";
      this.btnReadCoils.Size = new Size(161, 23);
      this.btnReadCoils.TabIndex = 5;
      this.btnReadCoils.Text = "Read Coils - FC1";
      this.btnReadCoils.TextAlign = ContentAlignment.MiddleLeft;
      this.btnReadCoils.UseVisualStyleBackColor = true;
      this.btnReadCoils.Click += new EventHandler(this.BtnReadCoilsClick);
      this.btnReadDiscreteInputs.Location = new Point(12, 205);
      this.btnReadDiscreteInputs.Name = "btnReadDiscreteInputs";
      this.btnReadDiscreteInputs.Size = new Size(161, 23);
      this.btnReadDiscreteInputs.TabIndex = 6;
      this.btnReadDiscreteInputs.Text = "Read Discrete Inputs - FC2";
      this.btnReadDiscreteInputs.TextAlign = ContentAlignment.MiddleLeft;
      this.btnReadDiscreteInputs.UseVisualStyleBackColor = true;
      this.btnReadDiscreteInputs.Click += new EventHandler(this.btnReadDiscreteInputs_Click);
      this.btnReadHoldingRegisters.Location = new Point(12, 234);
      this.btnReadHoldingRegisters.Name = "btnReadHoldingRegisters";
      this.btnReadHoldingRegisters.Size = new Size(161, 23);
      this.btnReadHoldingRegisters.TabIndex = 7;
      this.btnReadHoldingRegisters.Text = "Read Holding Registers - FC3";
      this.btnReadHoldingRegisters.TextAlign = ContentAlignment.MiddleLeft;
      this.btnReadHoldingRegisters.UseVisualStyleBackColor = true;
      this.btnReadHoldingRegisters.Click += new EventHandler(this.btnReadHoldingRegisters_Click);
      this.btnReadInputRegisters.Location = new Point(12, 263);
      this.btnReadInputRegisters.Name = "btnReadInputRegisters";
      this.btnReadInputRegisters.Size = new Size(161, 23);
      this.btnReadInputRegisters.TabIndex = 8;
      this.btnReadInputRegisters.Text = "Read Input Registers - FC4";
      this.btnReadInputRegisters.TextAlign = ContentAlignment.MiddleLeft;
      this.btnReadInputRegisters.UseVisualStyleBackColor = true;
      this.btnReadInputRegisters.Click += new EventHandler(this.btnReadInputRegisters_Click);
      this.txtStartingAddressInput.Location = new Point(198, 198);
      this.txtStartingAddressInput.Name = "txtStartingAddressInput";
      this.txtStartingAddressInput.Size = new Size(39, 20);
      this.txtStartingAddressInput.TabIndex = 9;
      this.txtStartingAddressInput.Text = "1";
      this.txtStartingAddress.Location = new Point(198, 178);
      this.txtStartingAddress.Name = "txtStartingAddress";
      this.txtStartingAddress.Size = new Size(89, 17);
      this.txtStartingAddress.TabIndex = 10;
      this.txtStartingAddress.Text = "Starting Address";
      this.txtNumberOfValues.Location = new Point(198, 233);
      this.txtNumberOfValues.Name = "txtNumberOfValues";
      this.txtNumberOfValues.Size = new Size(100, 17);
      this.txtNumberOfValues.TabIndex = 12;
      this.txtNumberOfValues.Text = "Number of Values";
      this.txtNumberOfValuesInput.Location = new Point(198, 253);
      this.txtNumberOfValuesInput.Name = "txtNumberOfValuesInput";
      this.txtNumberOfValuesInput.Size = new Size(39, 20);
      this.txtNumberOfValuesInput.TabIndex = 11;
      this.txtNumberOfValuesInput.Text = "1";
      this.lsbAnswerFromServer.FormattingEnabled = true;
      this.lsbAnswerFromServer.Location = new Point(310, 147);
      this.lsbAnswerFromServer.Name = "lsbAnswerFromServer";
      this.lsbAnswerFromServer.Size = new Size(188, 160);
      this.lsbAnswerFromServer.TabIndex = 13;
      this.lsbAnswerFromServer.DoubleClick += new EventHandler(this.lsbAnswerFromServer_DoubleClick);
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new Point(417, 13);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(165, 13);
      this.linkLabel1.TabIndex = 16;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "http://www.EasyModbusTCP.net";
      this.cbbSelctionModbus.FormattingEnabled = true;
      this.cbbSelctionModbus.Items.AddRange(new object[2] { "ModbusTCP (Ethernet)", "ModbusRTU (Serial)" });
      this.cbbSelctionModbus.Location = new Point(34, 6);
      this.cbbSelctionModbus.Name = "cbbSelctionModbus";
      this.cbbSelctionModbus.Size = new Size(180, 21);
      this.cbbSelctionModbus.TabIndex = 17;
      this.cbbSelctionModbus.Text = "ModbusTCP (Ethernet)";
      this.cbbSelctionModbus.SelectedIndexChanged += new EventHandler(this.cbbSelctionModbus_SelectedIndexChanged);
      this.txtCOMPort.Location = new Point(34, 35);
      this.txtCOMPort.Name = "txtCOMPort";
      this.txtCOMPort.Size = new Size(100, 14);
      this.txtCOMPort.TabIndex = 18;
      this.txtCOMPort.Text = "COM-Port";
      this.txtCOMPort.Visible = false;
      this.cbbSelectComPort.FormattingEnabled = true;
      this.cbbSelectComPort.Items.AddRange(
         new object[8] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8" });
      this.cbbSelectComPort.Location = new Point(34, 55);
      this.cbbSelectComPort.Name = "cbbSelectComPort";
      this.cbbSelectComPort.Size = new Size(121, 21);
      this.cbbSelectComPort.TabIndex = 19;
      this.cbbSelectComPort.Visible = false;
      this.cbbSelectComPort.SelectedIndexChanged += new EventHandler(this.cbbSelectComPort_SelectedIndexChanged);
      this.txtSlaveAddress.Location = new Point(158, 35);
      this.txtSlaveAddress.Name = "txtSlaveAddress";
      this.txtSlaveAddress.Size = new Size(56, 19);
      this.txtSlaveAddress.TabIndex = 20;
      this.txtSlaveAddress.Text = "Slave ID";
      this.txtSlaveAddress.Visible = false;
      this.txtSlaveAddressInput.Location = new Point(158, 55);
      this.txtSlaveAddressInput.Name = "txtSlaveAddressInput";
      this.txtSlaveAddressInput.Size = new Size(56, 20);
      this.txtSlaveAddressInput.TabIndex = 21;
      this.txtSlaveAddressInput.Text = "1";
      this.txtSlaveAddressInput.Visible = false;
      this.txtSlaveAddressInput.TextChanged += new EventHandler(this.TxtSlaveAddressInputTextChanged);
      this.textBox1.Location = new Point(12, 506);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.ScrollBars = ScrollBars.Vertical;
      this.textBox1.Size = new Size(641, 157);
      this.textBox1.TabIndex = 22;
      this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
      this.btnWriteMultipleRegisters.Location = new Point(12, 453);
      this.btnWriteMultipleRegisters.Name = "btnWriteMultipleRegisters";
      this.btnWriteMultipleRegisters.Size = new Size(161, 23);
      this.btnWriteMultipleRegisters.TabIndex = 30;
      this.btnWriteMultipleRegisters.Text = "Write Multiple Registers - FC16";
      this.btnWriteMultipleRegisters.TextAlign = ContentAlignment.MiddleLeft;
      this.btnWriteMultipleRegisters.UseVisualStyleBackColor = true;
      this.btnWriteMultipleRegisters.Click += new EventHandler(this.btnWriteMultipleRegisters_Click);
      this.btnWriteMultipleCoils.Location = new Point(12, 424);
      this.btnWriteMultipleCoils.Name = "btnWriteMultipleCoils";
      this.btnWriteMultipleCoils.Size = new Size(161, 23);
      this.btnWriteMultipleCoils.TabIndex = 29;
      this.btnWriteMultipleCoils.Text = "Write Multiple Coils - FC15";
      this.btnWriteMultipleCoils.TextAlign = ContentAlignment.MiddleLeft;
      this.btnWriteMultipleCoils.UseVisualStyleBackColor = true;
      this.btnWriteMultipleCoils.Click += new EventHandler(this.btnWriteMultipleCoils_Click);
      this.btnWriteSingleRegister.Location = new Point(12, 395);
      this.btnWriteSingleRegister.Name = "btnWriteSingleRegister";
      this.btnWriteSingleRegister.Size = new Size(161, 23);
      this.btnWriteSingleRegister.TabIndex = 28;
      this.btnWriteSingleRegister.Text = "Write Single Register - FC6";
      this.btnWriteSingleRegister.TextAlign = ContentAlignment.MiddleLeft;
      this.btnWriteSingleRegister.UseVisualStyleBackColor = true;
      this.btnWriteSingleRegister.Click += new EventHandler(this.btnWriteSingleRegister_Click);
      this.btnWriteSingleCoil.Location = new Point(12, 366);
      this.btnWriteSingleCoil.Name = "btnWriteSingleCoil";
      this.btnWriteSingleCoil.Size = new Size(161, 23);
      this.btnWriteSingleCoil.TabIndex = 27;
      this.btnWriteSingleCoil.Text = "Write Single Coil - FC5";
      this.btnWriteSingleCoil.TextAlign = ContentAlignment.MiddleLeft;
      this.btnWriteSingleCoil.UseVisualStyleBackColor = true;
      this.btnWriteSingleCoil.Click += new EventHandler(this.btnWriteSingleCoil_Click);
      this.txtCoilValue.BackColor = SystemColors.Info;
      this.txtCoilValue.Location = new Point(563, 413);
      this.txtCoilValue.Name = "txtCoilValue";
      this.txtCoilValue.ReadOnly = true;
      this.txtCoilValue.Size = new Size(81, 20);
      this.txtCoilValue.TabIndex = 31;
      this.txtCoilValue.Text = "FALSE";
      this.txtCoilValue.DoubleClick += new EventHandler(this.txtCoilValue_DoubleClick);
      this.txtRegisterValue.BackColor = SystemColors.Info;
      this.txtRegisterValue.Location = new Point(563, 461);
      this.txtRegisterValue.Name = "txtRegisterValue";
      this.txtRegisterValue.Size = new Size(81, 20);
      this.txtRegisterValue.TabIndex = 32;
      this.txtRegisterValue.Text = "0";
      this.txtRegisterValue.TextChanged += new EventHandler(this.txtRegisterValue_TextChanged);
      this.button2.Cursor = Cursors.Default;
      this.button2.Image = Resources.circle_minus;
      this.button2.ImageAlign = ContentAlignment.TopCenter;
      this.button2.Location = new Point(518, 340);
      this.button2.Name = "button2";
      this.button2.Size = new Size(64, 52);
      this.button2.TabIndex = 34;
      this.button2.Text = "clear entry";
      this.button2.TextAlign = ContentAlignment.BottomCenter;
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.btnClear.Cursor = Cursors.Default;
      this.btnClear.Image = Resources.circle_delete1;
      this.btnClear.ImageAlign = ContentAlignment.TopCenter;
      this.btnClear.Location = new Point(585, 340);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new Size(64, 52);
      this.btnClear.TabIndex = 33;
      this.btnClear.Text = "clear all";
      this.btnClear.TextAlign = ContentAlignment.BottomCenter;
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new EventHandler(this.btnClear_Click);
      this.button1.Image = Resources.arrow_left;
      this.button1.ImageAlign = ContentAlignment.MiddleLeft;
      this.button1.Location = new Point(507, 457);
      this.button1.Name = "button1";
      this.button1.Size = new Size(146, 43);
      this.button1.TabIndex = 26;
      this.button1.Text = "Prepare Registers";
      this.button1.TextAlign = ContentAlignment.BottomRight;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.btnPrepareCoils.Image = Resources.arrow_left;
      this.btnPrepareCoils.ImageAlign = ContentAlignment.MiddleLeft;
      this.btnPrepareCoils.Location = new Point(507, 408);
      this.btnPrepareCoils.Name = "btnPrepareCoils";
      this.btnPrepareCoils.Size = new Size(146, 43);
      this.btnPrepareCoils.TabIndex = 25;
      this.btnPrepareCoils.Text = "Prepare Coils";
      this.btnPrepareCoils.TextAlign = ContentAlignment.BottomRight;
      this.btnPrepareCoils.UseVisualStyleBackColor = true;
      this.btnPrepareCoils.Click += new EventHandler(this.btnPrepareCoils_Click);
      this.pictureBox1.Image = Resources.PLCLoggerCompact;
      this.pictureBox1.Location = new Point(588, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(65, 66);
      this.pictureBox1.TabIndex = 15;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
      this.lblReadOperations.AutoSize = true;
      this.lblReadOperations.Font = new Font(
         "Microsoft Sans Serif",
         12f,
         FontStyle.Bold,
         GraphicsUnit.Point,
         0);
      this.lblReadOperations.Location = new Point(19, 127);
      this.lblReadOperations.Name = "lblReadOperations";
      this.lblReadOperations.Size = new Size(206, 20);
      this.lblReadOperations.TabIndex = 37;
      this.lblReadOperations.Text = "Read values from Server";
      this.button3.BackColor = Color.Lime;
      this.button3.Font = new Font(
         "Microsoft Sans Serif",
         9.75f,
         FontStyle.Regular,
         GraphicsUnit.Point,
         0);
      this.button3.Location = new Point(310, 65);
      this.button3.Name = "button3";
      this.button3.Size = new Size(89, 47);
      this.button3.TabIndex = 38;
      this.button3.Text = "connect";
      this.button3.UseVisualStyleBackColor = false;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button4.BackColor = Color.Red;
      this.button4.Font = new Font(
         "Microsoft Sans Serif",
         9.75f,
         FontStyle.Regular,
         GraphicsUnit.Point,
         0);
      this.button4.Location = new Point(409, 65);
      this.button4.Name = "button4";
      this.button4.Size = new Size(89, 47);
      this.button4.TabIndex = 39;
      this.button4.Text = "disconnect";
      this.button4.UseVisualStyleBackColor = false;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.label1.AutoSize = true;
      this.label1.Font = new Font(
         "Microsoft Sans Serif",
         12f,
         FontStyle.Bold,
         GraphicsUnit.Point,
         0);
      this.label1.Location = new Point(19, 310);
      this.label1.Name = "label1";
      this.label1.Size = new Size(185, 20);
      this.label1.TabIndex = 40;
      this.label1.Text = "Write values to Server";
      this.label4.Location = new Point(198, 395);
      this.label4.Name = "label4";
      this.label4.Size = new Size(89, 17);
      this.label4.TabIndex = 42;
      this.label4.Text = "Starting Address";
      this.txtStartingAddressOutput.Location = new Point(198, 415);
      this.txtStartingAddressOutput.Name = "txtStartingAddressOutput";
      this.txtStartingAddressOutput.Size = new Size(39, 20);
      this.txtStartingAddressOutput.TabIndex = 41;
      this.txtStartingAddressOutput.Text = "1";
      this.lsbWriteToServer.FormattingEnabled = true;
      this.lsbWriteToServer.Location = new Point(310, 340);
      this.lsbWriteToServer.Name = "lsbWriteToServer";
      this.lsbWriteToServer.Size = new Size(188, 160);
      this.lsbWriteToServer.TabIndex = 43;
      this.lblParity.Location = new Point(96, 82);
      this.lblParity.Name = "lblParity";
      this.lblParity.Size = new Size(56, 19);
      this.lblParity.TabIndex = 46;
      this.lblParity.Text = "Parity";
      this.lblParity.Visible = false;
      this.lblStopbits.Location = new Point(158, 82);
      this.lblStopbits.Name = "lblStopbits";
      this.lblStopbits.Size = new Size(56, 19);
      this.lblStopbits.TabIndex = 48;
      this.lblStopbits.Text = "Stopbits";
      this.lblStopbits.Visible = false;
      this.cbbParity.FormattingEnabled = true;
      this.cbbParity.Items.AddRange(new object[3] { "Even", "Odd", "None" });
      this.cbbParity.Location = new Point(97, 101);
      this.cbbParity.Name = "cbbParity";
      this.cbbParity.Size = new Size(55, 21);
      this.cbbParity.TabIndex = 50;
      this.cbbParity.Visible = false;
      this.cbbStopbits.FormattingEnabled = true;
      this.cbbStopbits.Items.AddRange(new object[3] { "1", "1.5", "2" });
      this.cbbStopbits.Location = new Point(158, 101);
      this.cbbStopbits.Name = "cbbStopbits";
      this.cbbStopbits.Size = new Size(55, 21);
      this.cbbStopbits.TabIndex = 51;
      this.cbbStopbits.Visible = false;
      this.txtBaudrate.Location = new Point(35, 102);
      this.txtBaudrate.Name = "txtBaudrate";
      this.txtBaudrate.Size = new Size(56, 20);
      this.txtBaudrate.TabIndex = 53;
      this.txtBaudrate.Text = "9600";
      this.txtBaudrate.Visible = false;
      this.txtBaudrate.TextChanged += new EventHandler(this.txtBaudrate_TextChanged);
      this.lblBaudrate.Location = new Point(35, 82);
      this.lblBaudrate.Name = "lblBaudrate";
      this.lblBaudrate.Size = new Size(56, 19);
      this.lblBaudrate.TabIndex = 52;
      this.lblBaudrate.Text = "Baudrate";
      this.lblBaudrate.Visible = false;
      this.txtConnectedStatus.BackColor = Color.Red;
      this.txtConnectedStatus.Font = new Font("Microsoft Sans Serif", 16f);
      this.txtConnectedStatus.Location = new Point(3, 669);
      this.txtConnectedStatus.Name = "txtConnectedStatus";
      this.txtConnectedStatus.Size = new Size(665, 32);
      this.txtConnectedStatus.TabIndex = 54;
      this.txtConnectedStatus.Text = "Not connected to Server";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(669, 702);
      this.Controls.Add(this.txtConnectedStatus);
      this.Controls.Add(this.txtBaudrate);
      this.Controls.Add(this.lblBaudrate);
      this.Controls.Add(this.cbbStopbits);
      this.Controls.Add(this.cbbParity);
      this.Controls.Add(this.lblStopbits);
      this.Controls.Add(this.lblParity);
      this.Controls.Add(this.lsbWriteToServer);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtStartingAddressOutput);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.lblReadOperations);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.btnClear);
      this.Controls.Add(this.txtRegisterValue);
      this.Controls.Add(this.txtCoilValue);
      this.Controls.Add(this.btnWriteMultipleRegisters);
      this.Controls.Add(this.btnWriteMultipleCoils);
      this.Controls.Add(this.btnWriteSingleRegister);
      this.Controls.Add(this.btnWriteSingleCoil);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.btnPrepareCoils);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.txtSlaveAddressInput);
      this.Controls.Add(this.txtSlaveAddress);
      this.Controls.Add(this.cbbSelectComPort);
      this.Controls.Add(this.txtCOMPort);
      this.Controls.Add(this.cbbSelctionModbus);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.lsbAnswerFromServer);
      this.Controls.Add(this.txtNumberOfValues);
      this.Controls.Add(this.txtNumberOfValuesInput);
      this.Controls.Add(this.txtStartingAddress);
      this.Controls.Add(this.txtStartingAddressInput);
      this.Controls.Add(this.btnReadInputRegisters);
      this.Controls.Add(this.btnReadHoldingRegisters);
      this.Controls.Add(this.btnReadDiscreteInputs);
      this.Controls.Add(this.btnReadCoils);
      this.Controls.Add(this.txtPort);
      this.Controls.Add(this.txtPortInput);
      this.Controls.Add(this.txtIpAddress);
      this.Controls.Add(this.txtIpAddressInput);
      this.Icon = (Icon)resources.GetObject("$this.Icon");
      this.Name = "MainForm";
      this.Text = "EasyModbus Client";
      this.Load += new EventHandler(this.MainForm_Load);
      ((ISupportInitialize)this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
   }
}