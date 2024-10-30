using GmadExplorer.Extensions;
using GmadExplorer.View.Components.TreeViewItem;
using SharpGMad;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GmadExplorer.View
{
    /// <summary>
    /// Логика взаимодействия для MainFrame.xaml
    /// </summary>
    public partial class MainFrame : UserControl
    {

        public MainFrame()
        {
            InitializeComponent();






            this.Loaded += MainFrame_Loaded;



        }

        class ExplorerAddon()
        {
            public string Path;
            public VirtualTreeViewItem Tree;
            List<ExplorerAddon> Items;
        }

        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            string addon = @"D:\Steam\steamapps\workshop\content\4000\104619813\1041619813.gma";
#else
            string addon = @"104619813.gma";
#endif

            LoadAddon(addon);
        }
        void LoadAddon(string addon)
        {
            this.Clear();
          
            App.Addon = RealtimeAddon.Load(addon, !FileExtensions.CanWrite(addon));

            Addon OpenAddon = App.Addon.OpenAddon;

            string AddonName = System.IO.Path.GetFileName(addon).Replace(System.IO.Path.GetExtension(addon), "") + $"_{OpenAddon.Title}";
            RenderControls(OpenAddon, AddonName, System.IO.Path.GetFileName(addon));


        }



        void RenderControls(Addon OpenAddon, string AddonName, string Name)
        {

            List<ContentFile> listfiles = OpenAddon.Files;

            VirtualTreeViewItem __base = VirtualTreeViewItem.Create_FolderChild(Name);
            this._list_files.AppendItem(__base);
            int C = 0;
            foreach (ContentFile item in listfiles)
            {
                string path_ = item.Path;
                string[] spl_path = path_.Split("/");
                VirtualTreeViewItem rep_panel = __base;
                string ___p = "";
                foreach (string pp in spl_path)
                {
                    string lastpath = ___p;
                    ___p = System.IO.Path.Combine(___p, pp);
                    if (ExtControls.VirtualTreeIsValid(___p))
                        continue;
                    if (ExtControls.VirtualTreeIsValid(lastpath))
                    {
                        rep_panel = ExtControls.GetVirtualTree(lastpath);
                    }
                    var panel = string.IsNullOrEmpty(System.IO.Path.GetExtension(pp)) ?
                        VirtualTreeViewItem.Create_FolderChild(pp) : VirtualTreeViewItem.Create_FileChild(item, (VirtualTreeViewItem panel) =>
                        {
                            string path_export_ = ContentFile.GenerateExternalPathLocal(AddonName, panel.ContentFile.Path);
                            if (Directory.Exists(path_export_))
                            {
                                File.WriteAllBytes(path_export_, panel.ContentFile.Content);
                            }
                            textBox.Text = path_export_ + "\n" + System.Text.Encoding.UTF8.GetString(panel.ContentFile.Content);
                        });
                    rep_panel.AppendItem(panel);
                    ExtControls.VirtualTreeAddNewPath(panel, ___p);

                }
            }
        }

        public void Clear()
        {
            _list_files.Items.Clear();
            textBox.Text = "";
            if (App.Addon != null)
                App.Addon.Close();
        }

        private void DropFile(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string file = ((string[])(e.Data.GetData(DataFormats.FileDrop))).First();
                if (System.IO.Path.GetExtension(file) == ".gma")
                {
                    LoadAddon(file);
                }

            }
        }
    }
}
