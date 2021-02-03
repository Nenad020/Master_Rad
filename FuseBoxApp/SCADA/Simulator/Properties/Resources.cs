namespace Properties
{
   using System.CodeDom.Compiler;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using System.Globalization;
   using System.Resources;
   using System.Runtime.CompilerServices;

   [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
   [DebuggerNonUserCode]
   [CompilerGenerated]
   internal class Resources
   {
      private static ResourceManager resourceMan;

      private static CultureInfo resourceCulture;

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static ResourceManager ResourceManager
      {
         get
         {
            if (resourceMan == null)
            {
               ResourceManager resourceManager = resourceMan = new ResourceManager(
                                                    "Properties.Resources",
                                                    typeof(Resources).Assembly);
            }

            return resourceMan;
         }
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static CultureInfo Culture
      {
         get
         {
            return resourceCulture;
         }

         set
         {
            resourceCulture = value;
         }
      }

      internal static Bitmap button2_Image
      {
         get
         {
            object @object = ResourceManager.GetObject("button2.Image", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Bitmap configure_2
      {
         get
         {
            object @object = ResourceManager.GetObject("configure-2", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Bitmap pictureBox1_Image
      {
         get
         {
            object @object = ResourceManager.GetObject("pictureBox1.Image", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Icon PLCLoggerCompact
      {
         get
         {
            object @object = ResourceManager.GetObject("PLCLoggerCompact", resourceCulture);
            return (Icon)@object;
         }
      }

      internal static Bitmap PLCLoggerCompactBitmap
      {
         get
         {
            object @object = ResourceManager.GetObject("PLCLoggerCompactBitmap", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Bitmap red
      {
         get
         {
            object @object = ResourceManager.GetObject("red", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal Resources()
      {
      }
   }
}