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
    public interface I_ATT
    {
        GH_DocumentObject OWNER
        {
            get;
        }
        bool ATT
        {
           get;
           set;
        }
        bool MENU
        {
            get;
            set;
        }
        bool TAG
        {
            get;
            set;
        }
    }
}
