using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace UI.SETTINGS
{
    internal abstract class UI_SETTING_GROUP: UI_SETTING_ITEM
    {
        protected List<IUI_SETTING_ITEM> m_groups;
        protected virtual List<IUI_SETTING_ITEM> GROUPS => null;
        internal UI_SETTING_GROUP():base()
        {
        }
        public override void ADD_TO_MENU(ToolStripMenuItem menu)
        {
            menu.DropDownItems.Add(MENU);
            foreach (IUI_SETTING_ITEM i in GROUPS)
            {
                i.ADD_TO_MENU(MENU);
            }
        }
        public override void ADD_TO_SETTING(UI_SETTING S)
        {
            base.ADD_TO_SETTING(S);
            foreach (IUI_SETTING_ITEM i in GROUPS)
            {
                i.ADD_TO_MENU(MENU);
            }
        }

    }
}