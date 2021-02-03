using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using EasyModbus;

using Properties;

public class MainForm : Form
{
   private delegate void coilsChangedCallback(int coil, int numberOfCoil);

   private delegate void registersChangedCallback(int register, int numberOfRegisters);

   private delegate void numberOfConnectionsCallback();

   private delegate void logDataChangedCallback();

   private Settings settings = new Settings();

   private ModbusServer easyModbusTCPServer;

   private ushort startingAddressDiscreteInputs = 1;

   private ushort startingAddressCoils = 1;

   private ushort startingAddressHoldingRegisters = 1;

   private ushort startingAddressInputRegisters = 1;

   private bool showProtocolInformations = true;

   private bool preventInvokeDiscreteInputs = false;

   private bool preventInvokeCoils = false;

   private bool preventInvokeInputRegisters = false;

   private bool preventInvokeHoldingRegisters = false;

   private bool registersChanegesLocked;

   private bool LockNumberOfConnectionsChanged = false;

   private bool locked;

   private int xLastLocation;

   private int yLastLocation;

   private IContainer components = null;

   private TabControl tabControl1;

   private TabPage tabPage1;

   private DataGridView dataGridView1;

   private TabPage tabPage2;

   private TabPage tabPage3;

   private TabPage tabPage4;

   private BindingSource form1BindingSource;

   private BindingSource easyModbusTCPServerBindingSource;

   private NumericUpDown numericUpDown1;

   private Label label1;

   private DataGridView dataGridView2;

   private DataGridView dataGridView3;

   private DataGridView dataGridView4;

   private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

   private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

   private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

   private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

   private Label label2;

   private Label label3;

   private DataGridViewTextBoxColumn Column1;

   private DataGridViewTextBoxColumn Value;

   private PictureBox pictureBox1;

   private LinkLabel linkLabel1;

   private VScrollBar vScrollBar1;

   private VScrollBar vScrollBar2;

   private VScrollBar vScrollBar3;

   private VScrollBar vScrollBar4;

   private Label label4;

   private ListBox listBox1;

   private Label label5;

   private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

   private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

   private TextBox textBox1;

   private CheckBox checkBox1;

   private Label lblVersion;

   private CheckBox checkBox2;

   private CheckBox checkBox3;

   private CheckBox checkBox4;

   private CheckBox checkBox5;

   private CheckBox checkBox6;

   private CheckBox checkBox7;

   private CheckBox checkBox8;

   private CheckBox checkBox9;

   private Button btnProperties;

   private CheckBox checkBox10;

   private Panel panel1;

   private ToolTip toolTip1;

   private PerformanceCounter performanceCounter1;

   private MenuStrip menuStrip1;

   private ToolStripMenuItem setupToolStripMenuItem;

   private ToolStripMenuItem infoToolStripMenuItem;

   public MainForm()
   {
      this.InitializeComponent();
      Assembly.GetExecutingAssembly().GetName().Version.ToString();
      this.lblVersion.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "."
                        + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
      this.easyModbusTCPServer = new ModbusServer();
      this.easyModbusTCPServer.Listen();
      this.easyModbusTCPServer.CoilsChanged += this.CoilsChanged;
      this.easyModbusTCPServer.HoldingRegistersChanged += this.HoldingRegistersChanged;
      this.easyModbusTCPServer.NumberOfConnectedClientsChanged += this.NumberOfConnectionsChanged;
      this.easyModbusTCPServer.LogDataChanged += this.LogDataChanged;
      this.tabControl1.SelectTab(1);
   }

   private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
   {
      checked
      {
         if (this.tabControl1.SelectedIndex == 0)
         {
            this.numericUpDown1.Value = this.startingAddressDiscreteInputs;
            this.dataGridView1.Rows.Clear();
            for (int i = this.startingAddressDiscreteInputs; i < 20 + unchecked((int)this.startingAddressDiscreteInputs); i++)
            {
               this.dataGridView1.Rows.Add(i, this.easyModbusTCPServer.discreteInputs[i]);
               if (this.easyModbusTCPServer.discreteInputs[i])
               {
                  this.dataGridView1[1, i - unchecked((int)this.startingAddressDiscreteInputs)].Style.BackColor = Color.Green;
               }
               else
               {
                  this.dataGridView1[1, i - unchecked((int)this.startingAddressDiscreteInputs)].Style.BackColor = Color.Red;
               }
            }
         }

         if (this.tabControl1.SelectedIndex == 1)
         {
            this.numericUpDown1.Value = this.startingAddressCoils;
            this.dataGridView2.Rows.Clear();
            for (int j = this.startingAddressCoils; j < 20 + unchecked((int)this.startingAddressCoils); j++)
            {
               this.dataGridView2.Rows.Add(j, this.easyModbusTCPServer.coils[j]);
               if (this.easyModbusTCPServer.coils[j])
               {
                  this.dataGridView2[1, j - unchecked((int)this.startingAddressCoils)].Style.BackColor = Color.Green;
               }
               else
               {
                  this.dataGridView2[1, j - unchecked((int)this.startingAddressCoils)].Style.BackColor = Color.Red;
               }
            }
         }

         if (this.tabControl1.SelectedIndex == 2)
         {
            this.numericUpDown1.Value = this.startingAddressInputRegisters;
            this.dataGridView3.Rows.Clear();
            for (int k = this.startingAddressInputRegisters; k < 20 + unchecked((int)this.startingAddressInputRegisters); k++)
            {
               this.dataGridView3.Rows.Add(k, this.easyModbusTCPServer.inputRegisters[k]);
            }
         }

         if (this.tabControl1.SelectedIndex == 3)
         {
            this.numericUpDown1.Value = this.startingAddressHoldingRegisters;
            this.dataGridView4.Rows.Clear();
            for (int l = this.startingAddressHoldingRegisters;
                 l < 20 + unchecked((int)this.startingAddressHoldingRegisters);
                 l++)
            {
               this.dataGridView4.Rows.Add(l, this.easyModbusTCPServer.holdingRegisters[l]);
            }
         }
      }
   }

   private void Form1_Load(object sender, EventArgs e)
   {
      this.tabControl1_SelectedIndexChanged(null, null);
   }

   private void numericUpDown1_ValueChanged(object sender, EventArgs e)
   {
      if (this.tabControl1.SelectedIndex == 0)
      {
         this.startingAddressDiscreteInputs = (ushort)this.numericUpDown1.Value;
      }

      if (this.tabControl1.SelectedIndex == 1)
      {
         this.startingAddressCoils = (ushort)this.numericUpDown1.Value;
      }

      if (this.tabControl1.SelectedIndex == 2)
      {
         this.startingAddressInputRegisters = (ushort)this.numericUpDown1.Value;
      }

      if (this.tabControl1.SelectedIndex == 3)
      {
         this.startingAddressHoldingRegisters = (ushort)this.numericUpDown1.Value;
      }

      this.tabControl1_SelectedIndexChanged(null, null);
   }

   private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
   {
      int rowIndex = this.dataGridView1.SelectedCells[0].RowIndex;
      checked
      {
         if (!this.easyModbusTCPServer.discreteInputs[rowIndex + unchecked((int)this.startingAddressDiscreteInputs)])
         {
            this.easyModbusTCPServer.discreteInputs[rowIndex + unchecked((int)this.startingAddressDiscreteInputs)] = true;
         }
         else
         {
            this.easyModbusTCPServer.discreteInputs[rowIndex + unchecked((int)this.startingAddressDiscreteInputs)] = false;
         }

         this.tabControl1_SelectedIndexChanged(null, null);
      }
   }

   private void CoilsChanged(int coil, int numberOfCoil)
   {
      if (!this.preventInvokeCoils)
      {
         if (this.tabControl1.InvokeRequired)
         {
            coilsChangedCallback method = this.CoilsChanged;
            this.Invoke(method, coil, numberOfCoil);
         }
         else if (this.tabControl1.SelectedIndex == 1)
         {
            this.tabControl1_SelectedIndexChanged(null, null);
         }
      }
   }

   private void HoldingRegistersChanged(int register, int numberOfRegisters)
   {
      if (!this.preventInvokeHoldingRegisters)
      {
         try
         {
            if (this.tabControl1.InvokeRequired)
            {
               if (!this.registersChanegesLocked)
               {
                  lock (this)
                  {
                     this.registersChanegesLocked = true;
                     registersChangedCallback method = this.HoldingRegistersChanged;
                     this.Invoke(method, register, numberOfRegisters);
                  }
               }
            }
            else if (this.tabControl1.SelectedIndex == 3)
            {
               this.tabControl1_SelectedIndexChanged(null, null);
            }
         }
         catch (Exception)
         {
         }

         this.registersChanegesLocked = false;
      }
   }

   private void NumberOfConnectionsChanged()
   {
      if (this.label3.InvokeRequired & !this.LockNumberOfConnectionsChanged)
      {
         lock (this)
         {
            this.LockNumberOfConnectionsChanged = true;
            numberOfConnectionsCallback method = this.NumberOfConnectionsChanged;
            try
            {
               this.Invoke(method);
            }
            catch (Exception)
            {
            }
            finally
            {
               this.LockNumberOfConnectionsChanged = false;
            }
         }
      }
      else
      {
         try
         {
            this.label3.Text = this.easyModbusTCPServer.NumberOfConnections.ToString();
         }
         catch (Exception)
         {
         }
      }
   }

   private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
   {
      int rowIndex = this.dataGridView2.SelectedCells[0].RowIndex;
      checked
      {
         if (!this.easyModbusTCPServer.coils[rowIndex + unchecked((int)this.startingAddressCoils)])
         {
            this.easyModbusTCPServer.coils[rowIndex + unchecked((int)this.startingAddressCoils)] = true;
         }
         else
         {
            this.easyModbusTCPServer.coils[rowIndex + unchecked((int)this.startingAddressCoils)] = false;
         }

         this.tabControl1_SelectedIndexChanged(null, null);
      }
   }

   private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
   {
      checked
      {
         if (this.dataGridView3.SelectedCells.Count > 0)
         {
            int rowIndex = this.dataGridView3.SelectedCells[0].RowIndex;
            try
            {
               this.easyModbusTCPServer.inputRegisters[rowIndex + unchecked((int)this.startingAddressInputRegisters)] =
                  short.Parse(this.dataGridView3.SelectedCells[0].Value.ToString());
            }
            catch (Exception)
            {
            }

            this.tabControl1_SelectedIndexChanged(null, null);
         }
      }
   }

   private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
   {
      checked
      {
         if (this.dataGridView4.SelectedCells.Count > 0)
         {
            int rowIndex = this.dataGridView4.SelectedCells[0].RowIndex;
            try
            {
               this.easyModbusTCPServer.holdingRegisters[rowIndex + unchecked((int)this.startingAddressHoldingRegisters)] =
                  short.Parse(this.dataGridView4.SelectedCells[0].Value.ToString());
            }
            catch (Exception)
            {
            }

            this.tabControl1_SelectedIndexChanged(null, null);
         }
      }
   }

   private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
   {
      Process.Start("http://www.EasyModbusTCP.net");
   }

   private void vScrollBar1_ValueChanged(object sender, EventArgs e)
   {
      this.startingAddressDiscreteInputs = checked((ushort)this.vScrollBar1.Value);
      this.tabControl1_SelectedIndexChanged(null, null);
   }

   private void vScrollBar2_ValueChanged(object sender, EventArgs e)
   {
      this.startingAddressCoils = checked((ushort)this.vScrollBar2.Value);
      this.tabControl1_SelectedIndexChanged(null, null);
   }

   private void vScrollBar3_ValueChanged(object sender, EventArgs e)
   {
      this.startingAddressInputRegisters = checked((ushort)this.vScrollBar3.Value);
      this.tabControl1_SelectedIndexChanged(null, null);
   }

   private void vScrollBar4_ValueChanged(object sender, EventArgs e)
   {
      this.startingAddressHoldingRegisters = checked((ushort)this.vScrollBar4.Value);
      this.tabControl1_SelectedIndexChanged(null, null);
   }

   private void LogDataChanged()
   {
      if (!this.showProtocolInformations)
      {
         return;
      }

      checked
      {
         if (this.listBox1.InvokeRequired)
         {
            if (!this.locked)
            {
               lock (this)
               {
                  this.locked = true;
                  try
                  {
                     logDataChangedCallback method = this.LogDataChanged;
                     this.Invoke(method);
                  }
                  catch (Exception)
                  {
                  }
               }
            }
         }
         else
         {
            try
            {
               this.listBox1.Items.Clear();
               for (int i = 0;
                    i < this.easyModbusTCPServer.ModbusLogData.Length && this.easyModbusTCPServer.ModbusLogData[i] != null;
                    i++)
               {
                  if (this.easyModbusTCPServer.ModbusLogData[i].request)
                  {
                     string text = this.easyModbusTCPServer.ModbusLogData[i].timeStamp.ToString("H:mm:ss.ff")
                                   + " Request from Client - Functioncode: " + this.easyModbusTCPServer.ModbusLogData[i]
                                      .functionCode.ToString();
                     if (this.easyModbusTCPServer.ModbusLogData[i].functionCode <= 4)
                     {
                        text = text + " ; Starting Address: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].startingAdress.ToString() + " Quantity: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].quantity.ToString();
                     }

                     if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 5)
                     {
                        text = text + " ; Output Address: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].startingAdress.ToString()
                                    + " Output Value: ";
                        if (this.easyModbusTCPServer.ModbusLogData[i].receiveCoilValues[0] == 0)
                        {
                           text += "False";
                        }

                        if (this.easyModbusTCPServer.ModbusLogData[i].receiveCoilValues[0] == 65280)
                        {
                           text += "True";
                        }
                     }

                     if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 6)
                     {
                        text = text + " ; Starting Address: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].startingAdress.ToString()
                                    + " Register Value: " + this.easyModbusTCPServer.ModbusLogData[i]
                                       .receiveRegisterValues[0].ToString();
                     }

                     if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 15)
                     {
                        text = text + " ; Starting Address: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].startingAdress.ToString() + " Quantity: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].quantity.ToString() + " Byte Count: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].byteCount.ToString()
                                    + " Values Received: ";
                        for (int j = 0; j < this.easyModbusTCPServer.ModbusLogData[i].quantity; j++)
                        {
                           int num = unchecked(j % 16);
                           if ((i == unchecked((int)this.easyModbusTCPServer.ModbusLogData[i].quantity) - 1)
                               & (unchecked((int)this.easyModbusTCPServer.ModbusLogData[i].quantity % 2) != 0))
                           {
                              num = ((num >= 8) ? (num - 8) : (num + 8));
                           }

                           int num2 = 1;
                           num2 <<= num;
                           text = (((this.easyModbusTCPServer.ModbusLogData[i].receiveCoilValues[unchecked(j / 16)]
                                     & (ushort)num2) != 0)
                                      ? (text + " True")
                                      : (text + " False"));
                        }
                     }

                     if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 16)
                     {
                        text = text + " ; Starting Address: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].startingAdress.ToString() + " Quantity: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].quantity.ToString() + " Byte Count: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].byteCount.ToString()
                                    + " Values Received: ";
                        for (int k = 0; k < this.easyModbusTCPServer.ModbusLogData[i].quantity; k++)
                        {
                           text = text + " " + this.easyModbusTCPServer.ModbusLogData[i].receiveRegisterValues[k];
                        }
                     }

                     if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 23)
                     {
                        text = text + " ; Starting Address Read: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].startingAddressRead.ToString()
                                    + " ; Quantity Read: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].quantityRead.ToString()
                                    + " ; Starting Address Write: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].startingAddressWrite.ToString()
                                    + " ; Quantity Write: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].quantityWrite.ToString()
                                    + " ; Byte Count: " + this.easyModbusTCPServer.ModbusLogData[i].byteCount.ToString()
                                    + " ; Values Received: ";
                        for (int l = 0; l < this.easyModbusTCPServer.ModbusLogData[i].quantityWrite; l++)
                        {
                           text = text + " " + this.easyModbusTCPServer.ModbusLogData[i].receiveRegisterValues[l];
                        }
                     }

                     this.listBox1.Items.Add(text);
                  }

                  if (this.easyModbusTCPServer.ModbusLogData[i].response)
                  {
                     if (this.easyModbusTCPServer.ModbusLogData[i].exceptionCode > 0)
                     {
                        string text = this.easyModbusTCPServer.ModbusLogData[i].timeStamp.ToString("H:mm:ss.ff");
                        text = text + " Response To Client - Error code: " + Convert.ToString(
                                  this.easyModbusTCPServer.ModbusLogData[i].errorCode,
                                  16);
                        text = text + " Exception Code: "
                                    + this.easyModbusTCPServer.ModbusLogData[i].exceptionCode.ToString();
                        this.listBox1.Items.Add(text);
                     }
                     else
                     {
                        string text = this.easyModbusTCPServer.ModbusLogData[i].timeStamp.ToString("H:mm:ss.ff")
                                      + " Response To Client - Functioncode: " + this.easyModbusTCPServer.ModbusLogData[i]
                                         .functionCode.ToString();
                        if (this.easyModbusTCPServer.ModbusLogData[i].functionCode <= 4)
                        {
                           text = text + " ; Bytecount: " + this.easyModbusTCPServer.ModbusLogData[i].byteCount.ToString()
                                  + " ; Send values: ";
                        }

                        if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 5)
                        {
                           text = text + " ; Starting Address: "
                                       + this.easyModbusTCPServer.ModbusLogData[i].startingAdress.ToString()
                                       + " ; Output Value: ";
                           if (this.easyModbusTCPServer.ModbusLogData[i].receiveCoilValues[0] == 0)
                           {
                              text += "False";
                           }

                           if (this.easyModbusTCPServer.ModbusLogData[i].receiveCoilValues[0] == 65280)
                           {
                              text += "True";
                           }
                        }

                        if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 6)
                        {
                           text = text + " ; Starting Address: "
                                       + this.easyModbusTCPServer.ModbusLogData[i].startingAdress.ToString()
                                       + " ; Register Value: " + this.easyModbusTCPServer.ModbusLogData[i]
                                          .receiveRegisterValues[0].ToString();
                        }

                        if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 15)
                        {
                           text = text + " ; Starting Address: "
                                       + this.easyModbusTCPServer.ModbusLogData[i].startingAdress.ToString()
                                       + " ; Quantity: " + this.easyModbusTCPServer.ModbusLogData[i].quantity.ToString();
                        }

                        if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 16)
                        {
                           text = text + " ; Starting Address: "
                                       + this.easyModbusTCPServer.ModbusLogData[i].startingAdress.ToString()
                                       + " ; Quantity: " + this.easyModbusTCPServer.ModbusLogData[i].quantity.ToString();
                        }

                        if (this.easyModbusTCPServer.ModbusLogData[i].functionCode == 23)
                        {
                           text = text + " ; ByteCount: " + this.easyModbusTCPServer.ModbusLogData[i].byteCount.ToString()
                                  + " ; Send Register Values: ";
                        }

                        if (this.easyModbusTCPServer.ModbusLogData[i].sendCoilValues != null)
                        {
                           for (int m = 0; m < this.easyModbusTCPServer.ModbusLogData[i].sendCoilValues.Length; m++)
                           {
                              text = text + this.easyModbusTCPServer.ModbusLogData[i].sendCoilValues[m].ToString() + " ";
                           }
                        }

                        if (this.easyModbusTCPServer.ModbusLogData[i].sendRegisterValues != null)
                        {
                           for (int n = 0; n < this.easyModbusTCPServer.ModbusLogData[i].sendRegisterValues.Length; n++)
                           {
                              text = text + this.easyModbusTCPServer.ModbusLogData[i].sendRegisterValues[n].ToString()
                                          + " ";
                           }
                        }

                        this.listBox1.Items.Add(text);
                     }
                  }
               }
            }
            catch (Exception)
            {
            }

            this.locked = false;
         }
      }
   }

   private void checkBox1_CheckedChanged(object sender, EventArgs e)
   {
      if (this.checkBox1.Checked)
      {
         this.showProtocolInformations = true;
      }
      else
      {
         this.showProtocolInformations = false;
      }
   }

   private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
   {
      this.easyModbusTCPServer.StopListening();
      Environment.Exit(0);
   }

   private void checkBox2_CheckedChanged(object sender, EventArgs e)
   {
      this.easyModbusTCPServer.FunctionCode1Disabled = !this.checkBox2.Checked;
   }

   private void checkBox3_CheckedChanged(object sender, EventArgs e)
   {
      CheckBox checkBox = (CheckBox)sender;
      this.easyModbusTCPServer.FunctionCode2Disabled = !this.checkBox3.Checked;
   }

   private void checkBox4_CheckedChanged(object sender, EventArgs e)
   {
      CheckBox checkBox = (CheckBox)sender;
      this.easyModbusTCPServer.FunctionCode3Disabled = !this.checkBox4.Checked;
   }

   private void checkBox5_CheckedChanged(object sender, EventArgs e)
   {
      CheckBox checkBox = (CheckBox)sender;
      this.easyModbusTCPServer.FunctionCode4Disabled = !this.checkBox5.Checked;
   }

   private void checkBox6_CheckedChanged(object sender, EventArgs e)
   {
      CheckBox checkBox = (CheckBox)sender;
      this.easyModbusTCPServer.FunctionCode5Disabled = !this.checkBox6.Checked;
   }

   private void checkBox7_CheckedChanged(object sender, EventArgs e)
   {
      CheckBox checkBox = (CheckBox)sender;
      this.easyModbusTCPServer.FunctionCode6Disabled = !this.checkBox7.Checked;
   }

   private void checkBox9_CheckedChanged(object sender, EventArgs e)
   {
      this.easyModbusTCPServer.FunctionCode15Disabled = !this.checkBox9.Checked;
   }

   private void checkBox8_CheckedChanged(object sender, EventArgs e)
   {
      this.easyModbusTCPServer.FunctionCode16Disabled = !this.checkBox8.Checked;
   }

   private void dataGridView2_MouseEnter(object sender, EventArgs e)
   {
      this.preventInvokeCoils = true;
   }

   private void dataGridView2_MouseLeave(object sender, EventArgs e)
   {
      this.preventInvokeCoils = false;
   }

   private void tabControl1_MouseEnter(object sender, EventArgs e)
   {
   }

   private void tabControl1_MouseLeave(object sender, EventArgs e)
   {
   }

   private void dataGridView4_MouseEnter(object sender, EventArgs e)
   {
      this.preventInvokeHoldingRegisters = true;
   }

   private void dataGridView4_MouseLeave(object sender, EventArgs e)
   {
      this.preventInvokeHoldingRegisters = false;
   }

   private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
   {
   }

   private void btnProperties_Click(object sender, EventArgs e)
   {
      this.settings.ComPort = this.easyModbusTCPServer.SerialPort;
      this.settings.SlaveAddress = this.easyModbusTCPServer.UnitIdentifier;
      PropertyForm propertyForm = new PropertyForm(this.settings);
      propertyForm.SettingsChangedEvent += this.SettingsChanged;
      propertyForm.Show();
   }

   private void SettingsChanged()
   {
      this.easyModbusTCPServer.StopListening();
      this.easyModbusTCPServer.Port = this.settings.Port;
      this.easyModbusTCPServer.SerialPort = this.settings.ComPort;
      this.easyModbusTCPServer.UnitIdentifier = this.settings.SlaveAddress;
      if (this.settings.ModbusTypeSelection == Settings.ModbusType.ModbusUDP)
      {
         this.easyModbusTCPServer.UDPFlag = true;
         this.easyModbusTCPServer.SerialFlag = false;
         this.label4.Text = "...Modbus-UDP Server Listening (Port " + this.settings.Port + ")...";
      }
      else if (this.settings.ModbusTypeSelection == Settings.ModbusType.ModbusTCP)
      {
         this.easyModbusTCPServer.UDPFlag = false;
         this.easyModbusTCPServer.SerialFlag = false;
         this.label4.Text = "...Modbus-TCP Server Listening (Port " + this.settings.Port + ")...";
      }
      else if (this.settings.ModbusTypeSelection == Settings.ModbusType.ModbusRTU)
      {
         this.easyModbusTCPServer.UDPFlag = false;
         this.easyModbusTCPServer.SerialFlag = true;
         this.label4.Text = "...Modbus-RTU Client Listening (Com-Port: " + this.settings.ComPort + ")...";
      }

      this.easyModbusTCPServer.PortChanged = true;
      this.easyModbusTCPServer.Listen();
   }

   private void EasyModbusTCPServerBindingSourceCurrentChanged(object sender, EventArgs e)
   {
   }

   private void pictureBox1_Click(object sender, EventArgs e)
   {
      Process.Start("http://www.EasyModbusTCP.net");
   }

   private void checkBox10_CheckedChanged(object sender, EventArgs e)
   {
      this.easyModbusTCPServer.FunctionCode23Disabled = !this.checkBox10.Checked;
   }

   private void panel1_MouseLeave(object sender, EventArgs e)
   {
      if (this.checkBox1.Checked)
      {
         this.showProtocolInformations = true;
      }
      else
      {
         this.showProtocolInformations = false;
      }
   }

   private void panel1_MouseEnter(object sender, EventArgs e)
   {
      this.showProtocolInformations = false;
   }

   private void panel1_MouseLeave_1(object sender, EventArgs e)
   {
      if (this.checkBox1.Checked)
      {
         this.showProtocolInformations = true;
      }
      else
      {
         this.showProtocolInformations = false;
      }
   }

   private void MainForm_MouseMove(object sender, MouseEventArgs e)
   {
      if (this.checkBox1.Checked)
      {
         this.showProtocolInformations = true;
      }
      else
      {
         this.showProtocolInformations = false;
      }
   }

   private void listBox1_MouseMove(object sender, MouseEventArgs e)
   {
      if (!checked((Math.Abs(e.Location.X - this.xLastLocation) < 50) & (Math.Abs(e.Location.Y - this.yLastLocation) < 50)))
      {
         this.xLastLocation = e.Location.X;
         this.yLastLocation = e.Location.Y;
         this.showProtocolInformations = false;
         string caption = "";
         int num = this.listBox1.IndexFromPoint(e.Location);
         if (num >= 0 && num < this.listBox1.Items.Count)
         {
            caption = this.listBox1.Items[num].ToString();
         }

         this.toolTip1.SetToolTip(this.listBox1, caption);
      }
   }

   private void infoToolStripMenuItem_Click(object sender, EventArgs e)
   {
      Info info = new Info();
      info.Show();
   }

   private void setupToolStripMenuItem_Click(object sender, EventArgs e)
   {
      this.settings.ComPort = this.easyModbusTCPServer.SerialPort;
      this.settings.SlaveAddress = this.easyModbusTCPServer.UnitIdentifier;
      PropertyForm propertyForm = new PropertyForm(this.settings);
      propertyForm.SettingsChangedEvent += this.SettingsChanged;
      propertyForm.Show();
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
      this.components = new Container();
      DataGridViewCellStyle dataGridViewCellStyle =
         new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle2 =
         new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle3 =
         new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle4 =
         new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle5 =
         new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle6 =
         new DataGridViewCellStyle();
      ComponentResourceManager resources =
         new ComponentResourceManager(typeof(MainForm));
      this.tabControl1 = new TabControl();
      this.tabPage1 = new TabPage();
      this.vScrollBar1 = new VScrollBar();
      this.dataGridView1 = new DataGridView();
      this.Column1 = new DataGridViewTextBoxColumn();
      this.Value = new DataGridViewTextBoxColumn();
      this.tabPage2 = new TabPage();
      this.vScrollBar2 = new VScrollBar();
      this.dataGridView2 = new DataGridView();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.tabPage3 = new TabPage();
      this.vScrollBar3 = new VScrollBar();
      this.dataGridView3 = new DataGridView();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.tabPage4 = new TabPage();
      this.vScrollBar4 = new VScrollBar();
      this.dataGridView4 = new DataGridView();
      this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      this.numericUpDown1 = new NumericUpDown();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.linkLabel1 = new LinkLabel();
      this.label4 = new Label();
      this.listBox1 = new ListBox();
      this.label5 = new Label();
      this.textBox1 = new TextBox();
      this.checkBox1 = new CheckBox();
      this.lblVersion = new Label();
      this.checkBox2 = new CheckBox();
      this.checkBox3 = new CheckBox();
      this.checkBox4 = new CheckBox();
      this.checkBox5 = new CheckBox();
      this.checkBox6 = new CheckBox();
      this.checkBox7 = new CheckBox();
      this.checkBox8 = new CheckBox();
      this.checkBox9 = new CheckBox();
      this.btnProperties = new Button();
      this.pictureBox1 = new PictureBox();
      this.form1BindingSource = new BindingSource(this.components);
      this.easyModbusTCPServerBindingSource = new BindingSource(this.components);
      this.checkBox10 = new CheckBox();
      this.panel1 = new Panel();
      this.performanceCounter1 = new PerformanceCounter();
      this.toolTip1 = new ToolTip(this.components);
      this.menuStrip1 = new MenuStrip();
      this.setupToolStripMenuItem = new ToolStripMenuItem();
      this.infoToolStripMenuItem = new ToolStripMenuItem();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((ISupportInitialize)this.dataGridView1).BeginInit();
      this.tabPage2.SuspendLayout();
      ((ISupportInitialize)this.dataGridView2).BeginInit();
      this.tabPage3.SuspendLayout();
      ((ISupportInitialize)this.dataGridView3).BeginInit();
      this.tabPage4.SuspendLayout();
      ((ISupportInitialize)this.dataGridView4).BeginInit();
      ((ISupportInitialize)this.numericUpDown1).BeginInit();
      ((ISupportInitialize)this.pictureBox1).BeginInit();
      ((ISupportInitialize)this.form1BindingSource).BeginInit();
      ((ISupportInitialize)this.easyModbusTCPServerBindingSource).BeginInit();
      this.panel1.SuspendLayout();
      ((ISupportInitialize)this.performanceCounter1).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Location = new Point(588, 63);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(307, 502);
      this.tabControl1.TabIndex = 0;
      this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
      this.tabControl1.MouseEnter += new EventHandler(this.tabControl1_MouseEnter);
      this.tabControl1.MouseLeave += new EventHandler(this.tabControl1_MouseLeave);
      this.tabPage1.Controls.Add(this.vScrollBar1);
      this.tabPage1.Controls.Add(this.dataGridView1);
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(299, 476);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Discrete Inputs";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.vScrollBar1.Cursor = Cursors.Default;
      this.vScrollBar1.LargeChange = 20;
      this.vScrollBar1.Location = new Point(264, 2);
      this.vScrollBar1.Maximum = 65534;
      this.vScrollBar1.Minimum = 1;
      this.vScrollBar1.Name = "vScrollBar1";
      this.vScrollBar1.Padding = new Padding(1);
      this.vScrollBar1.Size = new Size(21, 473);
      this.vScrollBar1.TabIndex = 1;
      this.vScrollBar1.Value = 1;
      this.vScrollBar1.ValueChanged += new EventHandler(this.vScrollBar1_ValueChanged);
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode =
         DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(this.Column1, this.Value);
      dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle.BackColor = SystemColors.Window;
      dataGridViewCellStyle.Font = new Font(
         "Microsoft Sans Serif",
         8.25f,
         FontStyle.Regular,
         GraphicsUnit.Point,
         0);
      dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle.SelectionBackColor = SystemColors.Window;
      dataGridViewCellStyle.SelectionForeColor = SystemColors.ControlText;
      dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;
      this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle;
      this.dataGridView1.Location = new Point(53, 2);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.RowHeadersVisible = false;
      this.dataGridView1.ShowEditingIcon = false;
      this.dataGridView1.Size = new Size(204, 473);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.CellDoubleClick +=
         new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
      this.Column1.HeaderText = "Address";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      dataGridViewCellStyle2.Font = new Font(
         "Microsoft Sans Serif",
         8.25f,
         FontStyle.Bold,
         GraphicsUnit.Point,
         0);
      this.Value.DefaultCellStyle = dataGridViewCellStyle2;
      this.Value.HeaderText = "Value";
      this.Value.Name = "Value";
      this.Value.ReadOnly = true;
      this.tabPage2.Controls.Add(this.vScrollBar2);
      this.tabPage2.Controls.Add(this.dataGridView2);
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(299, 476);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Coils";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.vScrollBar2.Cursor = Cursors.Default;
      this.vScrollBar2.LargeChange = 20;
      this.vScrollBar2.Location = new Point(264, 2);
      this.vScrollBar2.Maximum = 65534;
      this.vScrollBar2.Minimum = 1;
      this.vScrollBar2.Name = "vScrollBar2";
      this.vScrollBar2.Padding = new Padding(1);
      this.vScrollBar2.Size = new Size(21, 473);
      this.vScrollBar2.TabIndex = 2;
      this.vScrollBar2.Value = 1;
      this.vScrollBar2.ValueChanged += new EventHandler(this.vScrollBar2_ValueChanged);
      this.dataGridView2.AllowUserToAddRows = false;
      this.dataGridView2.AllowUserToDeleteRows = false;
      this.dataGridView2.ColumnHeadersHeightSizeMode =
         DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView2.Columns.AddRange(this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2);
      dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = SystemColors.Window;
      dataGridViewCellStyle3.Font = new Font(
         "Microsoft Sans Serif",
         8.25f,
         FontStyle.Regular,
         GraphicsUnit.Point,
         0);
      dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = SystemColors.Window;
      dataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText;
      dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
      this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle3;
      this.dataGridView2.Location = new Point(53, 2);
      this.dataGridView2.Name = "dataGridView2";
      this.dataGridView2.ReadOnly = true;
      this.dataGridView2.RowHeadersVisible = false;
      this.dataGridView2.ShowEditingIcon = false;
      this.dataGridView2.Size = new Size(204, 473);
      this.dataGridView2.TabIndex = 1;
      this.dataGridView2.CellDoubleClick +=
         new DataGridViewCellEventHandler(this.dataGridView2_CellDoubleClick);
      this.dataGridView2.MouseEnter += new EventHandler(this.dataGridView2_MouseEnter);
      this.dataGridView2.MouseLeave += new EventHandler(this.dataGridView2_MouseLeave);
      this.dataGridViewTextBoxColumn1.HeaderText = "Address";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      dataGridViewCellStyle4.Font = new Font(
         "Microsoft Sans Serif",
         8.25f,
         FontStyle.Bold,
         GraphicsUnit.Point,
         0);
      this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
      this.dataGridViewTextBoxColumn2.HeaderText = "Value";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.tabPage3.Controls.Add(this.vScrollBar3);
      this.tabPage3.Controls.Add(this.dataGridView3);
      this.tabPage3.Location = new Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new Padding(3);
      this.tabPage3.Size = new Size(299, 476);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Input Registers";
      this.tabPage3.UseVisualStyleBackColor = true;
      this.vScrollBar3.Cursor = Cursors.Default;
      this.vScrollBar3.LargeChange = 20;
      this.vScrollBar3.Location = new Point(264, 2);
      this.vScrollBar3.Maximum = 65534;
      this.vScrollBar3.Minimum = 1;
      this.vScrollBar3.Name = "vScrollBar3";
      this.vScrollBar3.Padding = new Padding(1);
      this.vScrollBar3.Size = new Size(21, 473);
      this.vScrollBar3.TabIndex = 2;
      this.vScrollBar3.Value = 1;
      this.vScrollBar3.ValueChanged += new EventHandler(this.vScrollBar3_ValueChanged);
      this.dataGridView3.AllowUserToAddRows = false;
      this.dataGridView3.AllowUserToDeleteRows = false;
      this.dataGridView3.ColumnHeadersHeightSizeMode =
         DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView3.Columns.AddRange(this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4);
      dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = SystemColors.Window;
      dataGridViewCellStyle5.Font = new Font(
         "Microsoft Sans Serif",
         8.25f,
         FontStyle.Regular,
         GraphicsUnit.Point,
         0);
      dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = SystemColors.Window;
      dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlText;
      dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
      this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle5;
      this.dataGridView3.Location = new Point(53, 2);
      this.dataGridView3.Name = "dataGridView3";
      this.dataGridView3.RowHeadersVisible = false;
      this.dataGridView3.ShowEditingIcon = false;
      this.dataGridView3.Size = new Size(204, 473);
      this.dataGridView3.TabIndex = 1;
      this.dataGridView3.CellValueChanged +=
         new DataGridViewCellEventHandler(this.dataGridView3_CellValueChanged);
      this.dataGridViewTextBoxColumn3.HeaderText = "Address";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn4.HeaderText = "Value";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.tabPage4.Controls.Add(this.vScrollBar4);
      this.tabPage4.Controls.Add(this.dataGridView4);
      this.tabPage4.Location = new Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new Padding(3);
      this.tabPage4.Size = new Size(299, 476);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Holding Registers";
      this.tabPage4.UseVisualStyleBackColor = true;
      this.vScrollBar4.Cursor = Cursors.Default;
      this.vScrollBar4.LargeChange = 20;
      this.vScrollBar4.Location = new Point(264, 2);
      this.vScrollBar4.Maximum = 65534;
      this.vScrollBar4.Minimum = 1;
      this.vScrollBar4.Name = "vScrollBar4";
      this.vScrollBar4.Padding = new Padding(1);
      this.vScrollBar4.Size = new Size(21, 473);
      this.vScrollBar4.TabIndex = 2;
      this.vScrollBar4.Value = 1;
      this.vScrollBar4.ValueChanged += new EventHandler(this.vScrollBar4_ValueChanged);
      this.dataGridView4.AllowUserToAddRows = false;
      this.dataGridView4.AllowUserToDeleteRows = false;
      this.dataGridView4.ColumnHeadersHeightSizeMode =
         DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView4.Columns.AddRange(this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6);
      dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = SystemColors.Window;
      dataGridViewCellStyle6.Font = new Font(
         "Microsoft Sans Serif",
         8.25f,
         FontStyle.Regular,
         GraphicsUnit.Point,
         0);
      dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle6.SelectionBackColor = SystemColors.Window;
      dataGridViewCellStyle6.SelectionForeColor = SystemColors.ControlText;
      dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
      this.dataGridView4.DefaultCellStyle = dataGridViewCellStyle6;
      this.dataGridView4.Location = new Point(53, 2);
      this.dataGridView4.Name = "dataGridView4";
      this.dataGridView4.RowHeadersVisible = false;
      this.dataGridView4.ShowEditingIcon = false;
      this.dataGridView4.Size = new Size(204, 473);
      this.dataGridView4.TabIndex = 1;
      this.dataGridView4.CellValueChanged +=
         new DataGridViewCellEventHandler(this.dataGridView4_CellValueChanged);
      this.dataGridView4.MouseEnter += new EventHandler(this.dataGridView4_MouseEnter);
      this.dataGridView4.MouseLeave += new EventHandler(this.dataGridView4_MouseLeave);
      this.dataGridViewTextBoxColumn5.HeaderText = "Address";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      this.dataGridViewTextBoxColumn6.HeaderText = "Value";
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.numericUpDown1.Location = new Point(762, 37);
      this.numericUpDown1.Maximum = new decimal(new int[4] { 65515, 0, 0, 0 });
      this.numericUpDown1.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
      this.numericUpDown1.Name = "numericUpDown1";
      this.numericUpDown1.Size = new Size(83, 20);
      this.numericUpDown1.TabIndex = 1;
      this.numericUpDown1.Value = new decimal(new int[4] { 1, 0, 0, 0 });
      this.numericUpDown1.ValueChanged += new EventHandler(this.numericUpDown1_ValueChanged);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(669, 41);
      this.label1.Name = "label1";
      this.label1.Size = new Size(87, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Move to Address";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(12, 102);
      this.label2.Name = "label2";
      this.label2.Size = new Size(143, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Number of connected clients";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(152, 102);
      this.label3.Name = "label3";
      this.label3.Size = new Size(13, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "0";
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new Point(87, 54);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(165, 13);
      this.linkLabel1.TabIndex = 6;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "http://www.EasyModbusTCP.net";
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.label4.AutoSize = true;
      this.label4.Font = new Font(
         "Microsoft Sans Serif",
         12f,
         FontStyle.Bold,
         GraphicsUnit.Point,
         0);
      this.label4.ForeColor = Color.FromArgb(0, 192, 0);
      this.label4.Location = new Point(235, 29);
      this.label4.Name = "label4";
      this.label4.Size = new Size(361, 20);
      this.label4.TabIndex = 7;
      this.label4.Text = "...Modbus-TCP Server Listening (Port 502)...";
      this.listBox1.FormattingEnabled = true;
      this.listBox1.HorizontalScrollbar = true;
      this.listBox1.Location = new Point(2, 3);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new Size(571, 160);
      this.listBox1.TabIndex = 8;
      this.listBox1.MouseMove += new MouseEventHandler(this.listBox1_MouseMove);
      this.label5.AutoSize = true;
      this.label5.Font = new Font(
         "Microsoft Sans Serif",
         8.25f,
         FontStyle.Bold,
         GraphicsUnit.Point,
         0);
      this.label5.Location = new Point(15, 133);
      this.label5.Name = "label5";
      this.label5.Size = new Size(121, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Protocol Information";
      this.textBox1.Font = new Font(
         "Microsoft Sans Serif",
         9.75f,
         FontStyle.Bold,
         GraphicsUnit.Point,
         0);
      this.textBox1.Location = new Point(15, 335);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new Size(314, 187);
      this.textBox1.TabIndex = 10;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      this.checkBox1.AutoSize = true;
      this.checkBox1.Checked = true;
      this.checkBox1.CheckState = CheckState.Checked;
      this.checkBox1.Location = new Point(428, 129);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(158, 17);
      this.checkBox1.TabIndex = 11;
      this.checkBox1.Text = "Show Protocol Informations ";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
      this.lblVersion.AutoSize = true;
      this.lblVersion.Location = new Point(88, 71);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new Size(60, 13);
      this.lblVersion.TabIndex = 12;
      this.lblVersion.Text = "Version 2.6";
      this.checkBox2.AutoSize = true;
      this.checkBox2.Checked = true;
      this.checkBox2.CheckState = CheckState.Checked;
      this.checkBox2.Location = new Point(29, 371);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new Size(15, 14);
      this.checkBox2.TabIndex = 13;
      this.checkBox2.UseVisualStyleBackColor = true;
      this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
      this.checkBox3.AutoSize = true;
      this.checkBox3.Checked = true;
      this.checkBox3.CheckState = CheckState.Checked;
      this.checkBox3.Location = new Point(29, 387);
      this.checkBox3.Name = "checkBox3";
      this.checkBox3.Size = new Size(15, 14);
      this.checkBox3.TabIndex = 14;
      this.checkBox3.UseVisualStyleBackColor = true;
      this.checkBox3.CheckedChanged += new EventHandler(this.checkBox3_CheckedChanged);
      this.checkBox4.AutoSize = true;
      this.checkBox4.Checked = true;
      this.checkBox4.CheckState = CheckState.Checked;
      this.checkBox4.Location = new Point(29, 403);
      this.checkBox4.Name = "checkBox4";
      this.checkBox4.Size = new Size(15, 14);
      this.checkBox4.TabIndex = 15;
      this.checkBox4.UseVisualStyleBackColor = true;
      this.checkBox4.CheckedChanged += new EventHandler(this.checkBox4_CheckedChanged);
      this.checkBox5.AutoSize = true;
      this.checkBox5.Checked = true;
      this.checkBox5.CheckState = CheckState.Checked;
      this.checkBox5.Location = new Point(29, 419);
      this.checkBox5.Name = "checkBox5";
      this.checkBox5.Size = new Size(15, 14);
      this.checkBox5.TabIndex = 16;
      this.checkBox5.UseVisualStyleBackColor = true;
      this.checkBox5.CheckedChanged += new EventHandler(this.checkBox5_CheckedChanged);
      this.checkBox6.AutoSize = true;
      this.checkBox6.Checked = true;
      this.checkBox6.CheckState = CheckState.Checked;
      this.checkBox6.Location = new Point(29, 435);
      this.checkBox6.Name = "checkBox6";
      this.checkBox6.Size = new Size(15, 14);
      this.checkBox6.TabIndex = 17;
      this.checkBox6.UseVisualStyleBackColor = true;
      this.checkBox6.CheckedChanged += new EventHandler(this.checkBox6_CheckedChanged);
      this.checkBox7.AutoSize = true;
      this.checkBox7.Checked = true;
      this.checkBox7.CheckState = CheckState.Checked;
      this.checkBox7.Location = new Point(29, 451);
      this.checkBox7.Name = "checkBox7";
      this.checkBox7.Size = new Size(15, 14);
      this.checkBox7.TabIndex = 18;
      this.checkBox7.UseVisualStyleBackColor = true;
      this.checkBox7.CheckedChanged += new EventHandler(this.checkBox7_CheckedChanged);
      this.checkBox8.AutoSize = true;
      this.checkBox8.Checked = true;
      this.checkBox8.CheckState = CheckState.Checked;
      this.checkBox8.Location = new Point(29, 483);
      this.checkBox8.Name = "checkBox8";
      this.checkBox8.Size = new Size(15, 14);
      this.checkBox8.TabIndex = 20;
      this.checkBox8.UseVisualStyleBackColor = true;
      this.checkBox8.CheckedChanged += new EventHandler(this.checkBox8_CheckedChanged);
      this.checkBox9.AutoSize = true;
      this.checkBox9.Checked = true;
      this.checkBox9.CheckState = CheckState.Checked;
      this.checkBox9.Location = new Point(29, 467);
      this.checkBox9.Name = "checkBox9";
      this.checkBox9.Size = new Size(15, 14);
      this.checkBox9.TabIndex = 19;
      this.checkBox9.UseVisualStyleBackColor = true;
      this.checkBox9.CheckedChanged += new EventHandler(this.checkBox9_CheckedChanged);
      this.btnProperties.BackgroundImage = Resources.configure_2;
      this.btnProperties.BackgroundImageLayout = ImageLayout.Stretch;
      this.btnProperties.Location = new Point(532, 63);
      this.btnProperties.Name = "btnProperties";
      this.btnProperties.Size = new Size(54, 52);
      this.btnProperties.TabIndex = 21;
      this.btnProperties.UseVisualStyleBackColor = true;
      this.btnProperties.Click += new EventHandler(this.btnProperties_Click);
      this.pictureBox1.Image = Resources.PLCLoggerCompactBitmap;
      this.pictureBox1.Location = new Point(12, 32);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(69, 67);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
      this.pictureBox1.TabIndex = 5;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
      this.easyModbusTCPServerBindingSource.CurrentChanged +=
         new EventHandler(this.EasyModbusTCPServerBindingSourceCurrentChanged);
      this.checkBox10.AutoSize = true;
      this.checkBox10.Checked = true;
      this.checkBox10.CheckState = CheckState.Checked;
      this.checkBox10.Location = new Point(29, 499);
      this.checkBox10.Name = "checkBox10";
      this.checkBox10.Size = new Size(15, 14);
      this.checkBox10.TabIndex = 22;
      this.checkBox10.UseVisualStyleBackColor = true;
      this.checkBox10.CheckedChanged += new EventHandler(this.checkBox10_CheckedChanged);
      this.panel1.Controls.Add(this.listBox1);
      this.panel1.Location = new Point(13, 149);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(573, 180);
      this.panel1.TabIndex = 23;
      this.panel1.MouseEnter += new EventHandler(this.panel1_MouseEnter);
      this.panel1.MouseLeave += new EventHandler(this.panel1_MouseLeave_1);
      this.menuStrip1.Items.AddRange(
         new ToolStripItem[2] { this.setupToolStripMenuItem, this.infoToolStripMenuItem });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(897, 24);
      this.menuStrip1.TabIndex = 24;
      this.menuStrip1.Text = "menuStrip1";
      this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
      this.setupToolStripMenuItem.Size = new Size(49, 20);
      this.setupToolStripMenuItem.Text = "Setup";
      this.setupToolStripMenuItem.Click += new EventHandler(this.setupToolStripMenuItem_Click);
      this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
      this.infoToolStripMenuItem.Size = new Size(40, 20);
      this.infoToolStripMenuItem.Text = "Info";
      this.infoToolStripMenuItem.Click += new EventHandler(this.infoToolStripMenuItem_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(897, 570);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.checkBox10);
      this.Controls.Add(this.btnProperties);
      this.Controls.Add(this.checkBox8);
      this.Controls.Add(this.checkBox9);
      this.Controls.Add(this.checkBox7);
      this.Controls.Add(this.checkBox6);
      this.Controls.Add(this.checkBox5);
      this.Controls.Add(this.checkBox4);
      this.Controls.Add(this.checkBox3);
      this.Controls.Add(this.checkBox2);
      this.Controls.Add(this.lblVersion);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.numericUpDown1);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.menuStrip1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Icon = (Icon)resources.GetObject("$this.Icon");
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.Text = "EasyModbusTCP Server Simulator";
      this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
      this.FormClosed += new FormClosedEventHandler(this.MainForm_FormClosed);
      this.Load += new EventHandler(this.Form1_Load);
      this.MouseMove += new MouseEventHandler(this.MainForm_MouseMove);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      ((ISupportInitialize)this.dataGridView1).EndInit();
      this.tabPage2.ResumeLayout(false);
      ((ISupportInitialize)this.dataGridView2).EndInit();
      this.tabPage3.ResumeLayout(false);
      ((ISupportInitialize)this.dataGridView3).EndInit();
      this.tabPage4.ResumeLayout(false);
      ((ISupportInitialize)this.dataGridView4).EndInit();
      ((ISupportInitialize)this.numericUpDown1).EndInit();
      ((ISupportInitialize)this.pictureBox1).EndInit();
      ((ISupportInitialize)this.form1BindingSource).EndInit();
      ((ISupportInitialize)this.easyModbusTCPServerBindingSource).EndInit();
      this.panel1.ResumeLayout(false);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
   }
}