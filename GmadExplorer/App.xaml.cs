using GmadExplorer.View.Components.TreeViewItem;
using SharpGMad;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace GmadExplorer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string PathIcon = "Icon0";
        public static RealtimeAddon Addon;
        public static Dictionary<string, string> Icons = new Dictionary<string, string>();
        public static Dictionary<string, VirtualTreeViewItem> VirtualTreeViewItems = new Dictionary<string, VirtualTreeViewItem>();
        public App()
        {

            if (!Directory.Exists(PathIcon))
            {
                Directory.CreateDirectory(PathIcon);
            }


            string file_save_paht_ = "";
            foreach ((string, byte[]) file_b in new List<(string, byte[])>()
            {
                ("File.png" , Resource.ResourceIcons.file),
                ("File.lua.png" , Resource.ResourceIcons.lua),
                ("Folder.png" , Resource.ResourceIcons.folder)


            })
            {
                file_save_paht_ = Path.Combine(PathIcon, file_b.Item1);
                if (!File.Exists(file_save_paht_))
                {
                    File.WriteAllBytes(file_save_paht_, file_b.Item2);
                    Icons.Add(Path.GetFileName(file_b.Item1), file_save_paht_);
                }
                else
                {
                    Icons.Add(file_b.Item1, file_save_paht_);
                }

            }




        }


        
    }

}
