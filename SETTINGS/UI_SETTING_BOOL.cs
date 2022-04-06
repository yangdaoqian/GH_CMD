using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Grasshopper.Kernel;
namespace UI.SETTINGS
{
    internal abstract class UI_SETTING_BOOL:UI_SETTING_ITEM
    {
        private bool m_b=>(bool)m_value;
        internal UI_SETTING_BOOL():base()
        {
        }  
        protected override void set_value(object value)
        {
            UI_SETTING.INS.SETTINGS.SetValue(NAME,m_b);
        }
        protected override void get_value()
        {
            VALUE = UI_SETTING.INS.SETTINGS.GetValue(NAME, m_b);
        }
        protected override void value_changed(object value)
        {
            MENU.Checked = m_b;
        }
        protected override void click_changed(object sender, EventArgs e)
        {
            VALUE = MENU.Checked;
        }
    }
}