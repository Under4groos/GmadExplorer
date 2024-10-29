using GmadExplorer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GmadExplorer.View.Components.TreeViewItem
{
    public class VirtualTreeViewItem : base_VirtualTreeViewItem
    {

        public static bool IsValidPath(string FullPath)
        {
            return App.VirtualTreeViewItems.ContainsKey(FullPath);
        }


        public static VirtualTreeViewItem Create_FileChild(
            SharpGMad.ContentFile _ContentFile , 
            Action<VirtualTreeViewItem> EventDoubleClick = null
            )
        {
            var vtvi = new VirtualTreeViewItem()
            {
                ContentFile = _ContentFile
            };
            if(EventDoubleClick != null)
                vtvi.PreviewMouseLeftButtonDown += (o, e) =>
                {
                    if(e.ClickCount > 1)
                    {
                        EventDoubleClick?.Invoke(vtvi);
                    }
                };
            return vtvi;
        }

        public static VirtualTreeViewItem Create_FolderChild(string Header)
        {
            return new VirtualTreeViewItem()
            {
                Header = Header,

                
            };
        }

    }
}
