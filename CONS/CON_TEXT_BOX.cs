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
    [DesignerGenerated]
    public class CON_TEXT_BOX : UserControl
    {
        internal bool m_check;
        private Label label1;
        private TextBox textBox1;
        private bool m_enable;
        [field: CompilerGenerated]
        internal event VALUE_CHANGED_EVENT_HANDLER VALUE_CHANGED;
        public CON_TEXT_BOX(string NAME)
        {         
            base.Load += new EventHandler(this.load);
            this.InitializeComponent();
            this.label1.Text= NAME;                 
        }

   
        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "NAME";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(75, 2);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 26);
            this.textBox1.TabIndex = 1;
           // this.textBox1.TextChanged
            // 
            // CON_TEXT_BOX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "CON_TEXT_BOX";
            this.Size = new System.Drawing.Size(202, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void OnValueChanged()
        {
            try
            {
                //this.m_check = checkBox1.Checked;
                //if (this.VALUE_CHANGED != null)
                //{
                //    VALUE_CHANGED.Invoke(this, new CHECK_ARGS(this.checkBox1.Text, this.checkBox1.Checked));
                //}
                this.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void load(object sender, EventArgs e)
        {
            GH_WindowsControlUtil.FixTextRenderingDefault(base.Controls);
        }
        internal bool CHECK
        {
            get
            {
                return this.m_check;
            }
            set
            {
                this.m_check = value;
                //this.checkBox1.Checked = this.m_check;
                this.m_check = value;
                this.Refresh();
            }
        }
        internal bool ENABLE
        {
            get
            {
                return this.m_enable;
            }
            set
            {
                this.m_enable = value;
               // this.checkBox1.Enabled = this.m_enable;
                this.Refresh();
            }
        }
        internal class CHECK_ARGS : EventArgs
        {
            private bool m_check;
            private string m_key;
            internal CHECK_ARGS(string key, bool ckeck)
            {
                this.m_check = ckeck;
                this.m_key = key;
            }
            internal string KEY
            {
                get
                {
                    return this.m_key;
                }
            }
            internal bool CHECK
            {
                get
                {
                    return this.m_check;
                }
            }
        }
        internal delegate void VALUE_CHANGED_EVENT_HANDLER(object sender, CON_CHECK_ITEM.CHECK_ARGS e);


    }
}

