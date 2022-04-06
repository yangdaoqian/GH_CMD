using System;
using System.Windows.Forms;

namespace UI.SETTINGS
{
    internal interface IUI_SETTING_ITEM
    {
        //Type TYPE
        //{
        //    get;
        //}
        //object VALUE
        //{
        //    get;
        //    set;
        //}
        void ADD_TO_MENU(ToolStripMenuItem menu);
        void ADD_TO_SETTING(UI_SETTING _SETTING);
    }
}