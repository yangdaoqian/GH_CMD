namespace UI
{
    using Grasshopper;
    using Grasshopper.GUI;
    using Grasshopper.GUI.Canvas;
    using Grasshopper.Kernel;
    using ALIEN_DLL;
    using ALIEN_DLL.GEOS;
    using Rhino;
    using Rhino.Display;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.IO;
    using GH_IO.Serialization;
    using System.Reflection;
    using Grasshopper.Kernel.Special;
    using ALIEN_DLL.ATTS;
    internal class UI_MENU_ITEMS
    {
        private static UI_MENU_ITEMS m_items;
        internal static UI_MENU_ITEMS ITEMS
        {
            get
            {
                if (m_items == null)
                {
                   
                    m_items = new UI_MENU_ITEMS();
                }
                return m_items;
            }
        }
        //private void dd(string s,bool b)
        //{
   
        //}
        //internal Dictionary<string,ToolStripMenuItem> menu_items = new Dictionary<string,ToolStripMenuItem>();
        //internal ToolStripMenuItem this[string index]
        //{
        //    get
        //    {
        //        try
        //        {
        //            return menu_items[index];
        //        }
        //        catch
        //        {
        //            return new ToolStripMenuItem();
        //        }
        //    }
        //}
        internal UI_MENU_ITEMS()
        {
            // UI_SETTING.SETTINGS.VALUE_CHANGED += dd;
#if release
            P_SECURITY.CHECK();        
#endif
            if (Grasshopper.Instances.ActiveCanvas == null)
            {
                Instances.CanvasCreated -= new Instances.CanvasCreatedEventHandler(this.register_menu_items);
                Instances.CanvasCreated += new Instances.CanvasCreatedEventHandler(this.register_menu_items);
            }
            else
                register_menu_items(Instances.ActiveCanvas);
        }

        private void create_tidy_menu(GH_DocumentEditor editor, GH_Canvas canvas)
        {
            if (editor != null)
            {
                Panel panel = null;
                foreach (Control control in editor.Controls)
                {
                    panel = control as Panel;
                    if (panel != null)
                    {
                        break;
                    }
                }
                if (panel != null)
                {
                    GH_Toolstrip toolstrip = null;
                    foreach (Control control in panel.Controls)
                    {
                        toolstrip = control as GH_Toolstrip;
                        if (toolstrip != null)
                        {
                            break;
                        }
                    }
                    if (toolstrip != null)
                    {

                        ToolStripButton item = new System.Windows.Forms.ToolStripButton();
                        item.Click += ui_click;
                        item.Checked =UI_SETTING.INS.UI;
                        item.CheckState = System.Windows.Forms.CheckState.Checked;
                        item.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                        item.Image = ICONS.P_UI;
                        toolstrip.Items.Add(item);
                    }

                }

                GH_MenuStrip strip = null;
                foreach (Control control in editor.Controls)
                {
                    strip = control as GH_MenuStrip;
                    if (strip != null)
                    {
                        break;
                    }
                }
                if (strip != null)
                {
                    ToolStripMenuItem item = null;
                    ToolStripMenuItem itemU = null;
                    foreach (ToolStripItem t in strip.Items)
                    {
                        if (t.Text == "Panda"&&t.ForeColor==Color.Red)
                        {
                            item = t as ToolStripMenuItem;
                            foreach (ToolStripItem ti in item.DropDownItems)
                            {
                                if (ti.Text == "UI")
                                {
                                    itemU = ti as ToolStripMenuItem;
                                }
                            }
                        }
                    }
                    if (item == null)
                    {
                        item = new ToolStripMenuItem
                        {
                            Text = "Panda",
                            ForeColor = Color.Red
                        };
                        strip.Items.Add(item);
                    }
                    if (itemU == null)
                    {
                        itemU = new ToolStripMenuItem
                        {
                            Text = "UI"
                        };
                        item.DropDownItems.Add(itemU);
                        set_menu_items(itemU);
                    }

                    //List<string> ltext = new List<string>();
                    //foreach(ToolStripMenuItem ri in itemU.DropDownItems)
                    //{
                    //    ltext.Add(ri.Text);
                    //}
                    //foreach (KeyValuePair<string, ToolStripMenuItem> i in this.menu_items)
                    //{
                    //    if (!ltext.Contains(i.Value.Text))
                    //        itemU.DropDownItems.Add(i.Value);
                    //}
                    //item.ForeColor = Color.Red;
                    //if (!item.DropDownItems.Contains(itemU))
                    //    item.DropDownItems.Add(itemU);
                    //if (!strip.Items.Contains(item))
                    //    strip.Items.Add(item);
                   
                }

                //GH_DocumentEditor.AggregateShortcutMenuItems += new GH_DocumentEditor.AggregateShortcutMenuItemsEventHandler(this.aggregate_menu_items);
            }
        }
        private void register_menu_items(GH_Canvas canvas)
        {
            GH_DocumentEditor documentEditor = Instances.DocumentEditor;
            if (documentEditor != null)
            {
                this.create_tidy_menu(documentEditor, canvas);
            }
        }
        private void set_menu_items(ToolStripMenuItem menu)
        {
            ToolStripMenuItem item_ui = new ToolStripMenuItem("Panda UI", null, new EventHandler(ui_click)) {
                ShortcutKeys = Keys.Alt | Keys.C,
                ToolTipText = "Apply Panda UI to all the objects",
                Image=ICONS.P_UI,
                Checked = UI_SETTING.INS.UI              
        };
            
            menu.DropDownItems.Add(item_ui);
            ToolStripMenuItem item_att = new ToolStripMenuItem("Panda ATT", null, new EventHandler(att_click))
            {
                ShortcutKeys = Keys.Alt | Keys.C,
                ToolTipText = "Apply Panda ATT to all the objects",
                Image = ICONS.P_ATT,
                Checked = UI_SETTING.INS.ATT
            };
            menu.DropDownItems.Add(item_att);
            ToolStripMenuItem item_menu = new ToolStripMenuItem("Panda MENU", null, new EventHandler(menu_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_MENU,
                Checked = UI_SETTING.INS.MENU

            };
            menu.DropDownItems.Add(item_menu);
            ToolStripMenuItem item_tag = new ToolStripMenuItem("Panda TAG", null, new EventHandler(tag_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_TAG,
                Checked = UI_SETTING.INS.TAG
            };
            menu.DropDownItems.Add(item_tag);
            ToolStripMenuItem item_gum = new ToolStripMenuItem("Panda GUM", null, new EventHandler(gum_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_GUM,
                Checked = UI_SETTING.INS.GUM
            };
            menu.DropDownItems.Add(item_gum);
            ToolStripMenuItem item_save = new ToolStripMenuItem("Panda SAVE", null,new EventHandler(save_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_SAVE,
                Checked = UI_SETTING.INS.SAVE
            };
            menu.DropDownItems.Add(item_save);
            //ToolStripMenuItem item8 = new ToolStripMenuItem("Panda THREAD", null, new EventHandler(thread_click))
            //{
            //    ShortcutKeys = Keys.Alt | Keys.S,
            //    ToolTipText = "",
            //    Image = ICONS.P_THREAD
            //};
            //menu.DropDownItems.Add(item8);
            ToolStripMenuItem item_mode = new ToolStripMenuItem("Panda MODE")
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                //Image = ICONS.P_THREAD
            };
            ToolStripMenuItem item_dark_mode = new ToolStripMenuItem("Dark", null, new EventHandler(dark_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_SAVE,
                Checked = UI_SETTING.INS.DARK_MODE == UI_SETTING.MODE.DARK
        };
            item_mode.DropDownItems.Add(item_dark_mode);
            ToolStripMenuItem item_light_mode = new ToolStripMenuItem("Light", null, new EventHandler(light_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_SAVE,
                Checked = UI_SETTING.INS.DARK_MODE == UI_SETTING.MODE.LIGHT
            };
            item_mode.DropDownItems.Add(item_light_mode);
            ToolStripMenuItem item_trans_mode = new ToolStripMenuItem("ransparent", null, new EventHandler(trans_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_SAVE,
                Checked = UI_SETTING.INS.DARK_MODE == UI_SETTING.MODE.TRANSPARENT
            };
            item_mode.DropDownItems.Add(item_trans_mode);

            menu.DropDownItems.Add(item_mode);

            ToolStripMenuItem item_snap = new ToolStripMenuItem("Panda SNAP")
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                //Image = ICONS.P_THREAD
            };
            ToolStripMenuItem item_snap_c = new ToolStripMenuItem("Center", null, new EventHandler(dark_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_SAVE,
                Checked = UI_SETTING.INS.SC
            };
            item_mode.DropDownItems.Add(item_snap_c);
            ToolStripMenuItem item_snap_e = new ToolStripMenuItem("Edge", null, new EventHandler(light_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_SAVE,
                Checked = UI_SETTING.INS.SE
            };
            item_mode.DropDownItems.Add(item_snap_e);
            ToolStripMenuItem item_snap_w= new ToolStripMenuItem("Wire", null, new EventHandler(trans_click))
            {
                ShortcutKeys = Keys.Alt | Keys.S,
                ToolTipText = "",
                Image = ICONS.P_SAVE,
                Checked = UI_SETTING.INS.SW
            };
            item_mode.DropDownItems.Add(item_snap_w);

            menu.DropDownItems.Add(item_snap);

            UI_SETTING.INS.VALUE_CHANGED += (o,s) =>
            {
                bool b=(bool)o;
                switch (s.PropertyName)
                {
                    case "UI":
                        item_ui.Checked = b;
                        item_att.Enabled = b;
                        item_tag.Enabled = b;
                        item_menu.Enabled = b;
                        item_gum.Enabled = b;
                        item_save.Enabled = b;
                        item_mode.Enabled = b;
                        break;
                    case "ATT":
                       item_att.Checked = b;
                        break;
                    case "TAG":
                        item_tag.Checked = b;
                        break;
                    case "MENU":
                        item_menu.Checked = b;
                        break;
                    case "GUM":
                        item_gum.Checked = b;
                        break;
                    case "SAVE":
                        item_save.Checked = b;
                        break;
                    case "DARK_MODE":
                        item_dark_mode.Checked =true;
                        item_light_mode.Checked = false;
                        item_trans_mode.Checked =false;
                        break;
                    case "LIGHT_MODE":
                        item_dark_mode.Checked = false;
                        item_light_mode.Checked =true;
                        item_trans_mode.Checked = false;
                        break;
                    case "TRANS_MODE":
                        item_dark_mode.Checked = false;
                        item_light_mode.Checked =false;
                        item_trans_mode.Checked = true;
                        break;
                    case "SNAP":
                        item_snap.Checked = b;
                        item_snap_c.Enabled = b;
                        item_snap_e.Enabled = b;
                        item_snap_w.Enabled = b;
                        break;
                    case "SNAPC":
                        item_snap_c.Checked = true;

                        break;
                    case "SNAPE":
                        item_snap_e.Checked = true;

                        break;
                    case "SNAPW":
                        item_snap_w.Checked = true;

                        break;

                }
            };
        }
     
        private void ui_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.UI = !item.Checked;
            }
            else
            {
                ToolStripButton itemp = sender as ToolStripButton;
                if (itemp != null)
                {
                    UI_SETTING.INS.UI = !UI_SETTING.INS.UI;
                    itemp.Checked = UI_SETTING.INS.UI;
                }
            }
        }
        private void att_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.ATT = !item.Checked;
            }
        }
        private void menu_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.MENU = !item.Checked;
            }
        }
        private void tag_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.TAG = !item.Checked;
            }
        }
        private void gum_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.GUM = !item.Checked;
            }
        }
        private void save_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.SAVE = !item.Checked;
            }
        }
        private void thread_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.THREAD = !item.Checked;
            }
        }
        private void dark_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.DARK_MODE = UI_SETTING.MODE.DARK;
            }
        }
        private void light_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.DARK_MODE = UI_SETTING.MODE.LIGHT;
            }
        }
        private void trans_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.DARK_MODE = UI_SETTING.MODE.TRANSPARENT;
            }
        }
        private void snap_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.SNAP = !item.Checked;
            }
        }
        private void snapc_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.SNAP = !item.Checked;
            }
        }
        private void snape_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.SNAP = !item.Checked;
            }
        }
        private void snapw_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                UI_SETTING.INS.SNAP = !item.Checked;
            }
        }
        private Action<object, EventArgs> bool_click(string name)
        {
            return (sender,e) =>
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item != null)
                {
                    UI_SETTING.INS.SNAP = !item.Checked;
                }
            };
        }
    }
}

