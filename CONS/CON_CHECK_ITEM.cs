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
    public class CON_CHECK_ITEM : UserControl
    {
        internal bool m_check;
        private CheckBox checkBox1;
        private bool m_enable;
        [field: CompilerGenerated]
       public  event VALUE_CHANGED_EVENT_HANDLER VALUE_CHANGED;
        public CON_CHECK_ITEM(string NAME)
        {         
            base.Load += new EventHandler(this.load);
            this.InitializeComponent();
            this.checkBox1.Text = NAME;                 
        }

   
        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(167, 24);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "NAME";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // CON_CHECK_ITEM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.checkBox1);
            this.Name = "CON_CHECK_ITEM";
            this.Size = new System.Drawing.Size(170, 26);
            this.ResumeLayout(false);

        }
        private void OnValueChanged()
        {
            try
            {
                this.m_check = checkBox1.Checked;
                if (this.VALUE_CHANGED != null)
                {
                    VALUE_CHANGED.Invoke(this, new CHECK_ARGS(this.checkBox1.Text, this.checkBox1.Checked));
                }
                this.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show("uicheckitem OnValueChanged" + ex.Message);
            }
        }
        private void load(object sender, EventArgs e)
        {
            GH_WindowsControlUtil.FixTextRenderingDefault(base.Controls);
        }
        public bool CHECK
        {
            get
            {
                return this.m_check;
            }
            set
            {
                this.m_check = value;
                this.checkBox1.Checked = this.m_check;
                this.m_check = value;
                this.Refresh();
            }
        }
       public  bool ENABLE
        {
            get
            {
                return this.m_enable;
            }
            set
            {
                this.m_enable = value;
                this.checkBox1.Enabled = this.m_enable;
                this.Refresh();
            }
        }
        public class CHECK_ARGS : EventArgs
        {
            private bool m_check;
            private string m_key;
            internal CHECK_ARGS(string key, bool ckeck)
            {
                this.m_check = ckeck;
                this.m_key = key;
            }
            public  string KEY
            {
                get
                {
                    return this.m_key;
                }
            }
            public  bool CHECK
            {
                get
                {
                    return this.m_check;
                }
            }
        }
        public  delegate void VALUE_CHANGED_EVENT_HANDLER(object sender, CON_CHECK_ITEM.CHECK_ARGS e);

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            OnValueChanged();
        }
    }
}

