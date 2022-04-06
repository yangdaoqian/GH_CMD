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
    using System.Collections.Generic;
    using System.Linq;
    using GH_IO;
    [DesignerGenerated]
    internal class CONTROL_COLLECTION_UI<T> : UserControl where T : class,GH_ISerializable
    {
        internal CONTROL_LIST_BOX<T> listbox;
        internal List<CON_LIST_ITEM<T>> ITEMS;

        internal CONTROL_COLLECTION_UI(CON_LIST_ITEM<T>[] p_items,SelectionMode m)
        {
            base.Load += new EventHandler(this.load);
            this.listbox = new CONTROL_LIST_BOX<T>(p_items,m);
            ITEMS = p_items.ToList();
            this.InitializeComponent();
            EventHandler handler = new EventHandler(this.listbox_checked_changed);
            EventHandler handler2 = new EventHandler(this.listbox_focus);
            this.listbox.SelectedIndexChanged += handler;
            this.listbox.GotFocus += handler2;
        }
        private void listbox_checked_changed(object sender, EventArgs e)
        {
            int num = this.listbox.Items.Count;
            for (int i = 0; i < num; i++)
            {
                ITEMS[i].SELECTED =new GH_Boolean( this.listbox.GetSelected(i));
            }
            
        }
        private void listbox_focus(object sender, EventArgs e)
        {
            base.InvokeGotFocus(this, e);
        }
        [DebuggerStepThrough]
        private void InitializeComponent()
        {
           
            this.SuspendLayout();
            // 
            // listbox
            // 
            this.listbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listbox.BackColor = System.Drawing.SystemColors.Window;
            this.listbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listbox.FormattingEnabled = true;
            this.listbox.Location = new System.Drawing.Point(5, 5);
            this.listbox.Name = "listbox";
            this.listbox.Size = new System.Drawing.Size(276, 290);
            this.listbox.TabIndex = 0;
           // this.listbox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listbox_DrawItem);
            // 
            // CONTROL_COLLECTION_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.listbox);
            this.Name = "CONTROL_COLLECTION_UI";
            this.Size = new System.Drawing.Size(286, 300);
            this.ResumeLayout(false);

        }
        private void load(object sender, EventArgs e)
        {
            GH_WindowsControlUtil.FixTextRenderingDefault(base.Controls);
        }
    }
}

