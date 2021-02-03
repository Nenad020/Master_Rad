using System.ComponentModel;

public class Settings
{
   public enum ModbusType
   {
      ModbusTCP,

      ModbusUDP,

      ModbusRTU
   }

   private int port = 502;

   private ModbusType modbusType;

   private string comPort;

   private byte slaveAddress;

   [Description("Listenig Port for Modbus-TCP or Modbus-UDP Server")]
   [Category("ModbusProperties")]
   public int Port
   {
      get
      {
         return this.port;
      }

      set
      {
         this.port = value;
      }
   }

   [Description("Activate Modbus UDP; Disable Modbus TCP")]
   [Category("ModbusProperties")]
   public ModbusType ModbusTypeSelection
   {
      get
      {
         return this.modbusType;
      }

      set
      {
         this.modbusType = value;
      }
   }

   [Description("ComPort Used for Modbus RTU connection ")]
   [Category("Modbus RTU Properties")]
   public string ComPort
   {
      get
      {
         return this.comPort;
      }

      set
      {
         this.comPort = value;
      }
   }

   [Description("UnitIdentifier (Slave address) for Modbus RTU connection")]
   [Category("Modbus RTU Properties")]
   public byte SlaveAddress
   {
      get
      {
         return this.slaveAddress;
      }

      set
      {
         this.slaveAddress = value;
      }
   }
}