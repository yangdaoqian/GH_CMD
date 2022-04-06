namespace UI.CONS
{
    using Grasshopper.GUI;
    using Microsoft.VisualBasic.CompilerServices;
    using Rhino.Geometry;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Grasshopper.Kernel.Types;
    using ALIEN_DLL.ATTS;
    [DesignerGenerated]
    public class UI_PSETTING : UserControl
    {
        private Label _icon;
        private CON_CHECK_ITEM _ui;
        private CON_CHECK_ITEM _att;
        private CON_CHECK_ITEM _menu;
        private CON_CHECK_ITEM _tag;
        private CON_CHECK_ITEM _gum;

        public UI_PSETTING()
        {
            base.Load += new EventHandler(this.LoftOptionsUI_Load);
            this.InitializeComponent();
    
            try
            {
                this._ui.CHECK = UI_SETTING.INS.UI;
                this._att.CHECK = UI_SETTING.INS.ATT;
                this._tag.CHECK = UI_SETTING.INS.TAG;
                this._menu.CHECK = UI_SETTING.INS.MENU;
                this._gum.CHECK = UI_SETTING.INS.GUM;
                //UI_MENU_ITEMS.ITEMS["UI"].Checked = UI_SETTING.SETTINGS.UI;
                //UI_MENU_ITEMS.ITEMS["ATT"].Checked = UI_SETTING.SETTINGS.ATT;
                //UI_MENU_ITEMS.ITEMS["MENU"].Checked = UI_SETTING.SETTINGS.MENU;
                //UI_MENU_ITEMS.ITEMS["TAG"].Checked = UI_SETTING.SETTINGS.TAG;
                //UI_MENU_ITEMS.ITEMS["GUM"].Checked = UI_SETTING.SETTINGS.TAG;
                //UI_MENU_ITEMS.ITEMS["ATT"].Checked = UI_SETTING.SETTINGS.UI;
                //UI_MENU_ITEMS.ITEMS["MENU"].Checked = UI_SETTING.SETTINGS.UI;
                //UI_MENU_ITEMS.ITEMS["TAG"].Checked = UI_SETTING.SETTINGS.UI;
                //UI_MENU_ITEMS.ITEMS["GUM"].Checked = UI_SETTING.SETTINGS.UI;

                _ui.VALUE_CHANGED += ui_CheckedChanged;
                _att.VALUE_CHANGED += ui_CheckedChanged;
                _menu.VALUE_CHANGED += ui_CheckedChanged;
                _tag.VALUE_CHANGED += ui_CheckedChanged;
                _gum.VALUE_CHANGED += ui_CheckedChanged;
            }
            catch
            {

            }
        }

        private void ui_CheckedChanged(object o, CON_CHECK_ITEM.CHECK_ARGS _ARGS)
        {
            switch (_ARGS.KEY)
            {
                case "Panda UI":
                    UI_SETTING.INS.UI = _ARGS.CHECK;
                    _att.ENABLE = UI_SETTING.INS.UI;
                    _menu.ENABLE = UI_SETTING.INS.UI;
                    _tag.ENABLE = UI_SETTING.INS.UI;
                    _gum.ENABLE = UI_SETTING.INS.UI;
             
                    //((CON_CHECK_ITEM)o).CHECK = _ARGS.CHECK;
                    break;
                case "Panda ATT":
                    UI_SETTING.INS.ATT = _ARGS.CHECK;
                    //((CON_CHECK_ITEM)o).CHECK = _ARGS.CHECK;
                    break;
                case "Panda MENU":
                    UI_SETTING.INS.MENU = _ARGS.CHECK;
                    //((CON_CHECK_ITEM)o).CHECK = _ARGS.CHECK;
                    break;
                case "Panda TAG":
                    UI_SETTING.INS.TAG = _ARGS.CHECK;
                   // ((CON_CHECK_ITEM)o).CHECK = _ARGS.CHECK;
                    break;
                case "Panda GUM":
                    UI_SETTING.INS.GUM = _ARGS.CHECK;
                   // ((CON_CHECK_ITEM)o).CHECK = _ARGS.CHECK;
                    break;
                case "Panda SAVE":
                    UI_SETTING.INS.SAVE = _ARGS.CHECK;
                    //((CON_CHECK_ITEM)o).CHECK = _ARGS.CHECK;
                    break;
                case "Panda UPDATE":
                    UI_SETTING.INS.SAVE = _ARGS.CHECK;
                    //((CON_CHECK_ITEM)o).CHECK = _ARGS.CHECK;
                    break;
            }
        }

        private void chkAlign_GotFocus(object sender, EventArgs e)
        {
            //this._ui = new CON_CHECK_ITEM("Panda UI", new GH_Boolean(CONTROL_SETTING.UI));
            //this._att = new CON_CHECK_ITEM("Panda ATT", new GH_Boolean(CONTROL_SETTING.ATT));
            //this._menu = new CON_CHECK_ITEM("Panda MENU", new GH_Boolean(CONTROL_SETTING.MENU));
            //this._tag = new CON_CHECK_ITEM("Panda TAG", new GH_Boolean(CONTROL_SETTING.TAG));
            //this._gum = new CON_CHECK_ITEM("Panda GUM", new GH_Boolean(CONTROL_SETTING.GUM));
            //this._snap = new CON_CHECK_ITEM("Panda SNAP", new GH_Boolean(CONTROL_SETTING.SNAP));
            //this.Controls.Add(this._ui);
            //this.Controls.Add(this._att);
            //this.Controls.Add(this._menu);
            //this.Controls.Add(this._tag);
            //this.Controls.Add(this._gum);
            //this.Controls.Add(this._snap);
            base.InvokeGotFocus(this, e);
        }

   
        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this._icon = new System.Windows.Forms.Label();
            this._ui = new CON_CHECK_ITEM("Panda UI");
            this._att = new CON_CHECK_ITEM("Panda ATT");
            this._menu = new CON_CHECK_ITEM("Panda MENU");
            this._tag = new CON_CHECK_ITEM("Panda TAG");
            this._gum = new CON_CHECK_ITEM("Panda GUM");

            this.SuspendLayout();
            // 
            // _icon
            // 
            this._icon.BackColor = System.Drawing.Color.Transparent;
            this._icon.Font = new System.Drawing.Font("SimSun", 15F);
            //this._icon.Image = global::PANDA.ICON.SETTING;
            this._icon.Location = new System.Drawing.Point(12, 12);
            this._icon.Name = "_icon";
            this._icon.Size = new System.Drawing.Size(24, 24);
            this._icon.TabIndex = 17;
            this._icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _ui
            // 
            this._ui.BackColor = System.Drawing.Color.Transparent;
            this._ui.Location = new System.Drawing.Point(42, 12);
            this._ui.Name = "_ui";
            this._ui.Size = new System.Drawing.Size(168, 24);
            this._ui.TabIndex = 18;
            // 
            // _att
            // 
            this._att.BackColor = System.Drawing.Color.Transparent;
            this._att.Location = new System.Drawing.Point(42, 42);
            this._att.Name = "_att";
            this._att.Size = new System.Drawing.Size(168, 24);
            this._att.TabIndex = 19;
            // 
            // _menu
            // 
            this._menu.BackColor = System.Drawing.Color.Transparent;
            this._menu.Location = new System.Drawing.Point(42, 132);
            this._menu.Name = "_menu";
            this._menu.Size = new System.Drawing.Size(168, 24);
            this._menu.TabIndex = 20;
            // 
            // _tag
            // 
            this._tag.BackColor = System.Drawing.Color.Transparent;
            this._tag.Location = new System.Drawing.Point(42, 102);
            this._tag.Name = "_tag";
            this._tag.Size = new System.Drawing.Size(168, 24);
            this._tag.TabIndex = 21;
            // 
            // _gum
            // 
            this._gum.BackColor = System.Drawing.Color.Transparent;
            this._gum.Location = new System.Drawing.Point(42, 72);
            this._gum.Name = "_gum";
            this._gum.Size = new System.Drawing.Size(168, 24);
            this._gum.TabIndex = 22;
            // 
            // _snap
            // 

            // 
            // _save
            // 
      
            // 
            // _update
            // 
      
            // 
            // CONTROL_PSETTING
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._icon);
            this.Controls.Add(this._ui);
            this.Controls.Add(this._att);
            this.Controls.Add(this._menu);
            this.Controls.Add(this._tag);
            this.Controls.Add(this._gum);

            this.Name = "CONTROL_PSETTING";
            this.Size = new System.Drawing.Size(278, 255);
            this.ResumeLayout(false);

        }

        private void LoftOptionsUI_Load(object sender, EventArgs e)
        {
            GH_WindowsControlUtil.FixTextRenderingDefault(base.Controls);
        }

        private void _itemd_Click(object sender, MouseEventArgs e)
        {
           // CONTROL_SETTING.Instance.ResetSettings();
        }
        private void _itemu_Click(object sender, MouseEventArgs e)
        {
        }
    }
}

