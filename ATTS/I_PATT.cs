using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel;

namespace UI.ATTS
{
    internal interface I_PATT:I_ATT
    {
        bool GUM
        {
            get;
            set;
        }
        bool SELECTED
        {
            get;
            set;
        }
        void SHOW_GUM();
        void HIDE_GUM();
    }
}
