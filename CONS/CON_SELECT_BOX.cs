namespace UI.CONS
{
    using Grasshopper.Kernel.Types;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [DesignerGenerated]
    internal class CON_SELECT_BOX<T> : UserControl
    {
        private Dictionary<T, GH_Boolean> m_items;
        private List<CON_SELECT_ITEM<T>> m_sitems;
        private bool m_sm;

        internal event VALUE_CHANGED_EVENT_HANDLER VALUE_CHANGED;

        internal CON_SELECT_BOX(Dictionary<T, GH_Boolean> items, bool sm)
        {
            this.m_sm = sm;
            int count = items.Count;
            this.m_items = new Dictionary<T, GH_Boolean>();
            for (int i = 0; i < count; i++)
            {
                KeyValuePair<T, GH_Boolean> pair = items.ElementAt<KeyValuePair<T, GH_Boolean>>(i);
                this.m_items.Add(pair.Key,pair.Value);
            }
            this.m_sitems = new List<CON_SELECT_ITEM<T>>();
            for (int i = 0; i < count; i++)
            {
                KeyValuePair<T, GH_Boolean> pair = this.m_items.ElementAt<KeyValuePair<T, GH_Boolean>>(i);
                CON_SELECT_ITEM<T> item = new CON_SELECT_ITEM<T> {
                    VALUE = pair.Key,
                    CHECK = pair.Value
                };
                item.VALUE_CHANGED += new CON_SELECT_ITEM<T>.VALUE_CHANGED_EVENT_HANDLER(this.value_down);
                this.m_sitems.Add(item);
            }
            //this.set_items(false);
            this.InitializeComponent();
        }

        [DebuggerStepThrough]
        protected void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = SystemColors.Window;
            base.BorderStyle = BorderStyle.FixedSingle;
            this.Font = new Font("Microsoft YaHei", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.ForeColor = SystemColors.Control;
            int y = 0;
            int width = 0;
            foreach (CON_SELECT_ITEM<T> con_select_item in this.m_sitems)
            {
                con_select_item.Location = new Point(0, y);
                base.Controls.Add(con_select_item);
                y += con_select_item.Height + 3;
                if (width == 0)
                {
                    width = con_select_item.Width;
                }
            }
            y -= 3;
            base.Margin = new Padding(3, 4, 3, 4);
            base.Name = "CON_SELECT_BOX";
            base.Size = new Size(width, y);
            base.BorderStyle = BorderStyle.None;
            base.ResumeLayout(false);
        }

        private void OnValueChanged()
        {
            try
            {
                if (this.VALUE_CHANGED != null)
                {
                    VALUE_CHANGED.Invoke(this,new BOX_EVENT_ARGS(this.m_items));
                }
            }
            catch
            {
            }
        }

        private void reset_items()
        {
            int count = this.m_items.Count;
            bool flag = !this.m_sm;
            for (int i = 0; i < count; i++)
            {
                KeyValuePair<T, GH_Boolean> pair = this.m_items.ElementAt<KeyValuePair<T, GH_Boolean>>(i);
                if (pair.Value.Value)
                {
                    //if (reset)
                    //{
                        this.m_items[pair.Key] = new GH_Boolean(false);
                        this.m_sitems[i].CHECK = new GH_Boolean(false);
                    //}
                    //else if (flag)
                    //{
                    //    this.m_sitems[i].CHECK = new GH_Boolean(true);
                    //    flag = false;
                    //}
                }
            }
        }

        private void value_down(object sender, CON_SELECT_ITEM<T>.ITEM_EVENT_ARGS e)
        {
            if (this.m_sm)
            {
                this.m_items[e.VALUE] = e.CKECK;
            }
            else
            {
                this.reset_items();
                this.m_items[e.VALUE] = e.CKECK;
                //((CON_SELECT_ITEM<T>)sender).CHECK = new GH_Boolean(true);
            }
        }

        internal Dictionary<T, GH_Boolean> ITEMS
        {
            get
            {
                return this.m_items;
            }
            set
            {
                this.m_items = value;
            }
        }

        internal class BOX_EVENT_ARGS : EventArgs
        {
            private Dictionary<T, GH_Boolean> m_items;

            internal BOX_EVENT_ARGS(Dictionary<T, GH_Boolean> items)
            {
                this.m_items = items;
            }

            internal Dictionary<T, GH_Boolean> ITEMS
            {
                get
                {
                    return this.m_items;
                }
            }
        }

        internal delegate void VALUE_CHANGED_EVENT_HANDLER(object sender, BOX_EVENT_ARGS e);
    }
}

