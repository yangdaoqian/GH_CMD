using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using UI.ATTS;
using Grasshopper.Kernel.Attributes;
namespace UI.SETTINGS
{
   internal class UI_ATT:UI_SETTING_BOOL
    {
        internal override string NAME => "PANDA_ATT";
        internal UI_ATT():base()
        {
        }
    }
}
