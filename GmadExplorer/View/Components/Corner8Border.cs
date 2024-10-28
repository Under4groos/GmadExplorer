using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GmadExplorer.View.Components
{
    public class Corner8Border : Border
    {
        public Corner8Border()
        {
            this.CornerRadius = new System.Windows.CornerRadius(8);
            this.BorderThickness = new System.Windows.Thickness(1);
        }
    }
}
