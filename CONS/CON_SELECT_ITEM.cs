namespace UI.CONS
{
    using Grasshopper.Kernel.Types;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [DesignerGenerated]
    internal class CON_SELECT_ITEM<T> : UserControl
    {
        private Label label_value;
        private GH_Boolean m_check;
        private T m_value;

        [field: CompilerGenerated]
        internal event VALUE_CHANGED_EVENT_HANDLER VALUE_CHANGED;

        internal CON_SELECT_ITEM()
        {
            this.InitializeComponent();
            this.label_value.MouseDown += new MouseEventHandler(this.value_down);
        }

        [DebuggerStepThrough]
        protected void InitializeComponent()
        {
            this.label_value = new Label();
            base.SuspendLayout();
            this.label_value.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label_value.BackColor = Color.Transparent;
            this.label_value.ForeColor = SystemColors.ControlText;
            this.label_value.Location = new Point(0, 0);
            this.label_value.Name = "label_value";
            this.label_value.Size = new Size(240, 0x18);
            this.label_value.TabIndex = 3;
            this.label_value.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = SystemColors.Window;
            base.BorderStyle = BorderStyle.FixedSingle;
            base.Controls.Add(this.label_value);
            this.Font = new Font("Microsoft YaHei", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.ForeColor = SystemColors.Control;
            base.Margin = new Padding(3, 4, 3, 4);
            base.Name = "CON_SELECT_ITEM";
            base.Size = new Size(240, 0x18);
            base.ResumeLayout(false);
        }

        private void OnValueChanged()
        {
            try
            {
                this.m_check = new GH_Boolean(!this.m_check.Value);
                if (this.m_check.Value)
                {
                    this.label_value.ForeColor = Color.Red;
                }
                else
                {
                    this.label_value.ForeColor = Color.Black;
                }
                if (this.VALUE_CHANGED != null)
                {

                    VALUE_CHANGED.Invoke(this, new ITEM_EVENT_ARGS(this.m_value, this.m_check));
                }
            }
            catch
            {
            }
        }

        private void value_down(object sender, MouseEventArgs e)
        {
            this.OnValueChanged();
        }

        internal GH_Boolean CHECK
        {
            get
            {
                return this.m_check;
            }
            set
            {
                this.m_check = value;
                if (this.m_check.Value)
                {
                    this.label_value.ForeColor = Color.Red;
                }
                else
                {
                    this.label_value.ForeColor = Color.Black;
                }
                this.Refresh();
            }
        }

        internal T VALUE
        {
            get
            {
                return this.m_value;
            }
            set
            {
                this.m_value = value;
                this.label_value.Text = this.m_value.ToString();
                this.Refresh();
            }
        }

        internal class ITEM_EVENT_ARGS : EventArgs
        {
            private GH_Boolean m_check;
            private T m_value;

            internal ITEM_EVENT_ARGS(T str, GH_Boolean ckeck)
            {
                this.m_check = ckeck;
                this.m_value = str;
            }

            internal GH_Boolean CKECK
            {
                get
                {
                    return this.m_check;
                }
            }

            internal T VALUE
            {
                get
                {
                    return this.m_value;
                }
            }
        }

        internal delegate void VALUE_CHANGED_EVENT_HANDLER(object sender, CON_SELECT_ITEM<T>.ITEM_EVENT_ARGS e);
    }
}

