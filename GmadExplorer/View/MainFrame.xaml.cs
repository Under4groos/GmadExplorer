using GmadExplorer.Extensions;
using GmadExplorer.View.Components.TreeViewItem;
using SharpGMad;
using System;
using System.Collections.Generic;
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
            App.Addon = RealtimeAddon.Load(addon, !FileExtensions.CanWrite(addon));
            var list_files = App.Addon.OpenAddon.GetDictionaryFiles();

            foreach (var item in list_files)
            {
                var list_path = item.Key.Split(@"\");

                string path_ = list_path[0];
                if (ExtControls.ListVirtualTreeViewItem.ContainsKey(path_))
                {
                    continue;
                }
                VirtualTreeViewItem virtualTreeViewItem = new VirtualTreeViewItem()
                {
                    Header = path_
                };
                _list_files.AppendItem(path_, virtualTreeViewItem);
                var lll_ = VirtualTreeViewItem.CreateChild_Rec(virtualTreeViewItem, list_path);
                foreach (var asd in item.Value)
                {
                    lll_.AppendItem(VirtualTreeViewItem.CreateChild(asd));

                }
            }
        }
    }
}
