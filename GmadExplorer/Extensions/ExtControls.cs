using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GmadExplorer.Delegates.EventContextMenu;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using GmadExplorer.View.Components.TreeViewItem;
using System.IO;

namespace GmadExplorer.Extensions
{
    public static class ExtControls
    {
        public static T GetChildOfType<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        public static MenuItem CreateMenuItem(string Header = "<null>", string Command = "<null>")
        {
            return new MenuItem() { Header = Header, Tag = Command };
        }

        public static ContextMenu AddContextMenuItem(this FrameworkElement frameworkElement,
            string Header = "<null>", string Command = "<null>",
             EventHandlerContextMenu eventHandlerContextMenu = null)
        {

            var cm_item = CreateMenuItem(Header, Command);
            cm_item.PreviewMouseLeftButtonDown += (o, e) =>
            {
                var it = o as MenuItem;
                eventHandlerContextMenu?.Invoke(it.Header.ToString(), it.Tag.ToString());
            };

            var cm = frameworkElement.ContextMenu;
            cm.Items.Add(cm_item);
            return cm;
        }
        public static ContextMenu AddContextMenuItem(this ContextMenu ContextMenu,
            string Header = "<null>", string Command = "<null>",
            EventHandlerContextMenu eventHandlerContextMenu = null
            )
        {

            var cm_item = CreateMenuItem(Header, Command);
            cm_item.PreviewMouseLeftButtonDown += (o, e) =>
            {
                var it = o as MenuItem;
                eventHandlerContextMenu?.Invoke(it.Header.ToString(), it.Tag.ToString());
            };
            ContextMenu.Items.Add(cm_item);
            return ContextMenu;
        }





        public static void AppendItem(this System.Windows.Controls.TreeView treeView, object item)
        => treeView.Items.Add(item);
        public static void AppendItem(this System.Windows.Controls.TreeView treeView, string pathall, object item)
        {
            if (!ListVirtualTreeViewItem.ContainsKey(pathall))
            {
                treeView.AppendItem(item);
                ListVirtualTreeViewItem.Add(pathall, item as VirtualTreeViewItem);
            }
            else
            {
                ListVirtualTreeViewItem[pathall].AppendItem(item);
            }

        }

        public static void AppendItem(this VirtualTreeViewItem treeView, object item)
        => treeView.Items.Add(item);



        public static Dictionary<string, VirtualTreeViewItem> ListVirtualTreeViewItem = new Dictionary<string, VirtualTreeViewItem>();
        public static void AppendItem(this VirtualTreeViewItem treeView, string pathall, object item)
        {
            if (!ListVirtualTreeViewItem.ContainsKey(pathall))
            {
                treeView.AppendItem(item);
            }
            else
            {
                ListVirtualTreeViewItem[pathall].AppendItem(item);
            }

        }



        public static bool SetImage(this Image image, string path)
        {
            if (!File.Exists(path))
                return false;
            try
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.UriSource = new Uri(path);
                bi.EndInit();
                bi.Freeze();

                image.Source = bi;
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

    }
}
