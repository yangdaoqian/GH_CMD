using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Grasshopper.Kernel;
namespace UI.SETTINGS
{
    internal abstract class UI_SETTING_ITEM: IUI_SETTING_ITEM
    {
        internal delegate void ValueChangedEventHandler(object value);
        protected object m_value;
        protected ToolStripMenuItem m_menu;
        internal virtual string NAME
        {
            get;
        }
        public object VALUE
        {
            get
            {
                return m_value;
            }
            set
            {
                if (m_value != value)
                {
                    m_value = value;
                    ADD_TO_SETTING(UI_SETTING.INS);
                    VALUE_CHANGED(m_value);
                }
            }
        }
        internal virtual ToolStripMenuItem MENU
        {
            get
            {
                if(m_menu==null)
                {
                    m_menu = new ToolStripMenuItem(NAME);
                    m_menu.Click += click_changed;
                }
                return m_menu;
            }
        }
        internal ValueChangedEventHandler VALUE_CHANGED;
        internal UI_SETTING_ITEM()
        {
            init();
        }
        public Type TYPE => m_value==null?null: m_value.GetType();
        protected virtual void init()
        {
            VALUE_CHANGED += value_changed;
            ADD_TO_SETTING(UI_SETTING.INS);
        }
        protected virtual void value_changed(object value)
        {
        }
        protected virtual void click_changed(object sender, EventArgs e)
        {

        }
        public virtual void ADD_TO_MENU(ToolStripMenuItem menu)
        {
            menu.DropDownItems.Add(MENU);
        }
        public virtual void ADD_TO_SETTING(UI_SETTING S)
        {
        }
        protected void change(Action<IGH_ActiveObject, bool> ac, bool va, List<IGH_DocumentObject> list)
        {
            try
            {
                foreach (IGH_ActiveObject obj2 in list)
                {
                    ac(obj2, va);
                }
            }
            catch
            {

            }
        }
    }
}