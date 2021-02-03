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

      internal static Bitmap arrow_1
      {
         get
         {
            object @object = ResourceManager.GetObject("arrow_1", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Bitmap arrow_2
      {
         get
         {
            object @object = ResourceManager.GetObject("arrow_2", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Bitmap arrow_left
      {
         get
         {
            object @object = ResourceManager.GetObject("arrow_left", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Bitmap circle_delete
      {
         get
         {
            object @object = ResourceManager.GetObject("circle_delete", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Bitmap circle_delete1
      {
         get
         {
            object @object = ResourceManager.GetObject("circle_delete1", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Bitmap circle_minus
      {
         get
         {
            object @object = ResourceManager.GetObject("circle_minus", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal static Bitmap PLCLoggerCompact
      {
         get
         {
            object @object = ResourceManager.GetObject("PLCLoggerCompact", resourceCulture);
            return (Bitmap)@object;
         }
      }

      internal Resources()
      {
      }
   }
}