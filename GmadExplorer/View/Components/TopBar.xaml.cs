using GmadExplorer.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace GmadExplorer.View.Components
{
    /// <summary>
    /// Логика взаимодействия для TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl
    {
        public MainWindow MainWindow
        {
            get
            {
                return App.Current.MainWindow as MainWindow;
            }
        }
        public UIElementCollection Children
        {
            get
            {

                return _stack.Children;
            }
        }

        public double ButtonsWidth
        {
            get
            {
                return _stack.ActualWidth;
            }
        }

        public TopBar()
        {
            InitializeComponent();

            for (int i = 0; i < Children.Count; i++)
            {
                AddEvent(i);
            }

            this.MouseLeftButtonDown += TopBar_DragWindow;

        }

        private void TopBar_DragWindow(object sender, MouseButtonEventArgs e)
        {

            if (e.GetPosition(MainWindow).X < (this.ActualWidth - ButtonsWidth))
                MainWindow.DragMove();
        }
        private void AddEvent(int id) => (_stack.Children[id] as Border).PreviewMouseLeftButtonDown += (o, e) =>
        {
            switch (id)
            {
                case 0:
                    MainWindow.WindowState = WindowState.Minimized;
                    break;
                case 1:

                    switch (MainWindow.WindowState)
                    {
                        case WindowState.Normal:
                            MainWindow.SetMarginMainWIndow(8, WindowState.Maximized);

                            break;
                        case WindowState.Minimized:
                            MainWindow.WindowState = WindowState.Normal;

                            break;
                        case WindowState.Maximized:
                            MainWindow.SetMarginMainWIndow(0, WindowState.Normal);

                            break;
                    }
                    break;
                case 2:
                    Process.GetCurrentProcess().Kill();
                    break;

                default:
                    break;
            }
        };
    }
}
