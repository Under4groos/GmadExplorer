using GmadExplorer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmadExplorer.View.Components.TreeViewItem
{
    public class VirtualTreeViewItem : base_VirtualTreeViewItem
    {
        public static VirtualTreeViewItem CreateChild(SharpGMad.ContentFile _ContentFile)
        {
            return new VirtualTreeViewItem()
            {
                ContentFile = _ContentFile
            };
        }

        public static VirtualTreeViewItem CreateChild_Rec(VirtualTreeViewItem VirtualTreeViewItem, string[] req)
        {
            VirtualTreeViewItem VirtualTreeViewItem_ = VirtualTreeViewItem;
            string path_ = "";
            for (int i = 1; i < req.Length; i++)
            {

                string p = req[i];
                path_ = System.IO.Path.Combine(path_, p);
                var v = CreateChild(p.ToString());

                VirtualTreeViewItem_.AppendItem(path_, v);

                VirtualTreeViewItem_ = v;
            }
            return VirtualTreeViewItem_;
        }

        public static VirtualTreeViewItem CreateChild(string Header)
        {
            return new VirtualTreeViewItem()
            {
                Header = Header
            };
        }

    }
}
