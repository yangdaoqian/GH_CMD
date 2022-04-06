using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using System.Windows.Forms;

namespace UI.CONS
{
    internal interface ICON_MENU
    {
        Control CONTROL
        {
            get;
        }
        bool WRITE();
        bool READ();
        bool SETUP(IGH_Param param);

    }
}
