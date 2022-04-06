namespace UI
{
    using Microsoft.VisualBasic.CompilerServices;
    using ALIEN_DLL;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;
    [DesignerGenerated]
    internal class CON_SLIDER_PANEL : UserControl
    {
        private Label label_add;
        private Label label_remove;
        private Label label_value;
        private int m_digital;
        private decimal m_value;

        [field: CompilerGenerated]
        internal event VALUE_CHANGED_EVENT_HANDLER VALUE_CHANGED;

        internal CON_SLIDER_PANEL()
        {
            this.InitializeComponent();
            this.label_add.MouseDown += new MouseEventHandler(this.add_down);
            this.label_remove.MouseDown += new MouseEventHandler(this.remove_down);
        }

        private void add_down(object sender, MouseEventArgs e)
        {
            decimal t = new decimal();
            if (e.Button == MouseButtons.Left)
            {
                t = decimal.One;
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    t = new decimal(10);
                }
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    t = new decimal(100);
                }
                if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && ((Control.ModifierKeys & Keys.Control) == Keys.Control))
                {
                    t = new decimal(0x3e8);
                }
            }
            this.OnValueChanged(t);
        }

        protected string FormatGripText(string mask, int decimals, double val)
        {
            string str = null;
            switch (decimals)
            {
                case 0:
                    str = string.Format("{0:0}", val);
                    break;

                case 1:
                    str = string.Format("{0:0.0}", val);
                    break;

                case 2:
                    str = string.Format("{0:0.00}", val);
                    break;

                case 3:
                    str = string.Format("{0:0.000}", val);
                    break;

                case 4:
                    str = string.Format("{0:0.0000}", val);
                    break;

                case 5:
                    str = string.Format("{0:0.00000}", val);
                    break;

                case 6:
                    str = string.Format("{0:0.000000}", val);
                    break;

                case 7:
                    str = string.Format("{0:0.0000000}", val);
                    break;

                case 8:
                    str = string.Format("{0:0.00000000}", val);
                    break;

                case 9:
                    str = string.Format("{0:0.000000000}", val);
                    break;

                case 10:
                    str = string.Format("{0:0.0000000000}", val);
                    break;

                case 11:
                    str = string.Format("{0:0.00000000000}", val);
                    break;

                case 12:
                    str = string.Format("{0:0.000000000000}", val);
                    break;

                case 13:
                    str = string.Format("{0:0.0000000000000}", val);
                    break;

                case 14:
                    str = string.Format("{0:0.00000000000000}", val);
                    break;

                case 15:
                    str = string.Format("{0:0.000000000000000}", val);
                    break;

                case 0x10:
                    str = string.Format("{0:0.0000000000000000}", val);
                    break;

                default:
                    str = string.Format("{0:0.0###############}", val);
                    break;
            }
            return string.Format(mask, str);
        }

        [DebuggerStepThrough]
        protected void InitializeComponent()
        {
            this.label_remove = new System.Windows.Forms.Label();
            this.label_add = new System.Windows.Forms.Label();
            this.label_value = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_remove
            // 
            this.label_remove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_remove.BackColor = System.Drawing.SystemColors.Control;
            this.label_remove.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_remove.Image = ICONS.remove;
            this.label_remove.Location = new System.Drawing.Point(0, 0);
            this.label_remove.Name = "label_remove";
            this.label_remove.Size = new System.Drawing.Size(35, 24);
            this.label_remove.TabIndex = 1;
            this.label_remove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_add
            // 
            this.label_add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_add.BackColor = System.Drawing.SystemColors.Control;
            this.label_add.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label_add.Image =ICONS.add;
            this.label_add.Location = new System.Drawing.Point(115, 0);
            this.label_add.Name = "label_add";
            this.label_add.Size = new System.Drawing.Size(35, 24);
            this.label_add.TabIndex = 2;
            this.label_add.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_value
            // 
            this.label_value.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_value.BackColor = System.Drawing.SystemColors.Control;
            this.label_value.Location = new System.Drawing.Point(35, 0);
            this.label_value.Name = "label_value";
            this.label_value.Size = new System.Drawing.Size(80, 24);
            this.label_value.TabIndex = 3;
            this.label_value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CON_SLIDER_PANEL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label_value);
            this.Controls.Add(this.label_add);
            this.Controls.Add(this.label_remove);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CON_SLIDER_PANEL";
            this.Size = new System.Drawing.Size(150, 24);
            this.ResumeLayout(false);

        }

        private void OnValueChanged(decimal t)
        {
            try
            {
                VALUE_CHANGED_EVENT_HANDLER expressionStack_48_0;
                this.m_value += t;
                this.label_value.Text = this.FormatGripText("{0}", this.m_digital, Convert.ToDouble(this.m_value));
                if (this.VALUE_CHANGED != null)
                {
                    VALUE_CHANGED.Invoke(this, new PANEL_EVENT_ARGS(this.m_value));
                }
            }
            catch
            {
            }
        }

        private void remove_down(object sender, MouseEventArgs e)
        {
            decimal t = new decimal();
            if (e.Button == MouseButtons.Left)
            {
                t = decimal.MinusOne;
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    t = new decimal(-10);
                }
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    t = new decimal(-100);
                }
                if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && ((Control.ModifierKeys & Keys.Control) == Keys.Control))
                {
                    t = new decimal(-1000);
                }
            }
            this.OnValueChanged(t);
        }

        private void value_down(object sender, MouseEventArgs e)
        {
            this.OnValueChanged(decimal.One);
        }

        internal int DIGITAL
        {
            get
            {
                return this.m_digital;
            }
            set
            {
                this.m_digital = value;
                this.label_value.Text = this.FormatGripText("{0}", this.m_digital, Convert.ToDouble(this.m_value));
            }
        }

        internal decimal VALUE
        {
            get
            {
                return this.m_value;
            }
            set
            {
                this.m_value = value;
                this.label_value.Text = this.FormatGripText("{0}", this.m_digital, Convert.ToDouble(this.m_value));
                this.Refresh();
            }
        }

        internal class PANEL_EVENT_ARGS : EventArgs
        {
            private decimal m_value;

            internal PANEL_EVENT_ARGS(decimal t)
            {
                this.m_value = t;
            }

            internal decimal VALUE
            {
                get
                {
                    return this.m_value;
                }
            }
        }

        internal delegate void VALUE_CHANGED_EVENT_HANDLER(object sender, CON_SLIDER_PANEL.PANEL_EVENT_ARGS e);
    }
}

