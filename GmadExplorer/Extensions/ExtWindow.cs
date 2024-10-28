using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace GmadExplorer.Extensions
{
    public static class ExtWindow
    {
        public static void SetMarginMainWIndow(this Window w, int m, WindowState windowState)
        {

            (w.Content as Border).Margin = new Thickness(m, m, m, m);
            w.WindowState = windowState;
        }
    }
}
