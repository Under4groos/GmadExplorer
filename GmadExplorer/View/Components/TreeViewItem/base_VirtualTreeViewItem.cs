using GmadExplorer.Extensions;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using UI.SyntaxBox;

namespace GmadExplorer.View.Components.TreeViewItem
{
    public class base_VirtualTreeViewItem : System.Windows.Controls.TreeViewItem
    {
        
        public string FullPath
        {
            get;set;
        }

       


        private SharpGMad.ContentFile _contentFile;
        public SharpGMad.ContentFile ContentFile
        {
            get
            {
                return _contentFile;
            }
            set
            {
                _contentFile = value;
                if (_contentFile != null)
                {
                    this.Header = value.GetName;
                }
            }
        }

        private Image _Icon = null;
        public Image Icon
        {
            get
            {
                return _Icon;
            }
        }
        private Grid _Grid = null;
        public Grid MainGrid
        {
            get
            {
                return _Grid;
            }
        }

        public base_VirtualTreeViewItem()
        {
            this.Foreground = Brushes.White;
            this.Loaded += Base_VirtualTreeViewItem_LayoutUpdated;
            this.IsExpanded = false;

        }






        private void Base_VirtualTreeViewItem_LayoutUpdated(object sender, EventArgs e)
        {
            _Icon = this.GetChildOfType<Image>();
            _Grid = this.GetChildOfType<Grid>();

            if (IsFile())
            {

                _Grid?.AddContextMenuItem("Show in folder", "");
                if (this.ContentFile.GetExtension == ".lua")
                {
                    this.SetIcon(App.Icons.GetValueStr("File.lua"));
                    return;
                }
                this.SetIcon(App.Icons.GetValueStr("File"));
            }
            else
            {
                _Grid?.AddContextMenuItem("Copy path", "copypath", (Header, Tag) =>
                {
                    Debug.WriteLine($"{Header} {Tag}");
                }).AddContextMenuItem("Export to folder", "export", (Header, Tag) =>
                {
                    Debug.WriteLine($"{Header} {Tag}");
                }).AddContextMenuItem("123", "1export", (Header, Tag) =>
                {
                    Debug.WriteLine($"{Header} {Tag}");
                });


                this.SetIcon(App.Icons.GetValueStr("Folder"));

            }

        }

        #region Virtual

        public virtual bool SetIcon(string path)
        {
            if (this.Icon == null)
                return false;
           
            return this.Icon.SetImage(path);
        }

        public virtual bool IsFile()
        {
            return ContentFile != null;
        }
        #endregion


        #region t
        public override string ToString()
        {
            if (IsFile())
            {
                return $"{this.ContentFile}";
            }
            return $"{this.Header}";
        }

        #endregion
    }
}
