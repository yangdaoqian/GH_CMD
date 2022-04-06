namespace UI.CONS
{
    using Grasshopper.GUI;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Types;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [DesignerGenerated]
    internal class CON_SLIDER_LABEL : GH_TextInputBaseControl
    {
        private int m_digital;
        private GH_Interval m_value;

        internal event VALUE_CHANGED_EVENT_HANDLER VALUE_CHANGED;

        internal CON_SLIDER_LABEL()
        {
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

        protected override void Input_Assign(string text)
        {
            int length = 0;
            int num2 = 0;
            try
            {
                char[] separator = new char[] { ' ' };
                char[] chArray2 = new char[] { '.' };
                length = text.Split(separator)[0].Split(chArray2)[1].Length;
            }
            catch
            {
            }
            try
            {
                char[] chArray3 = new char[] { ' ' };
                char[] chArray4 = new char[] { '.' };
                num2 = text.Split(chArray3)[2].Split(chArray4)[1].Length;
            }
            catch
            {
            }
            this.m_digital = Math.Max(length, num2);
            GH_Interval target = new GH_Interval();
            if (GH_Convert.ToGHInterval(text, GH_Conversion.Secondary, ref target))
            {
                this.m_value = target;
                this.on_value_changed();
                base.Invalidate();
            }
        }

        protected override bool Input_Parse(string text)
        {
            GH_Interval target = new GH_Interval();
            return GH_Convert.ToGHInterval(text, GH_Conversion.Secondary, ref target);
        }

        protected override string Input_Supply()
        {
            double min = this.m_value.Value.Min;
            double max = this.m_value.Value.Max;
            return ((this.m_value == null) ? "0 To 1" : (this.FormatGripText("{0}", this.m_digital, min) + " To " + this.FormatGripText("{0}", this.m_digital, max)));
        }

        private void on_value_changed()
        {
            if (this.VALUE_CHANGED != null)
            {
                VALUE_CHANGED.Invoke(this, new LABEL_EVENT_ARGS(this.m_value, this.m_digital));
            }
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
            }
        }

        internal GH_Interval VALUE
        {
            get
            {
                return this.m_value;
            }
            set
            {
                this.m_value = value;
            }
        }

        internal class LABEL_EVENT_ARGS : EventArgs
        {
            private int m_digital;
            private GH_Interval m_value;

            internal LABEL_EVENT_ARGS(GH_Interval t, int digital)
            {
                this.m_value = t;
                this.m_digital = digital;
            }

            internal int DIGITAL
            {
                get
                {
                    return this.m_digital;
                }
            }

            internal GH_Interval VALUE
            {
                get
                {
                    return this.m_value;
                }
            }
        }

        internal delegate void VALUE_CHANGED_EVENT_HANDLER(object sender, CON_SLIDER_LABEL.LABEL_EVENT_ARGS e);

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CON_SLIDER_LABEL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "CON_SLIDER_LABEL";
            this.Size = new System.Drawing.Size(344, 86);
            this.ResumeLayout(false);

        }
    }
}

