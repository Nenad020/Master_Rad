using System;
using System.Windows.Forms;

internal static class Program
{
   [STAThread]
   private static void Main(string[] args)
   {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(defaultValue: false);
      Application.Run(new MainForm());
   }
}