using Grasshopper.GUI;
using System;
using System.Drawing;
using Grasshopper.GUI.Base;
using System.Windows.Forms;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel;
using System.Linq;
using Grasshopper.Kernel.Types;
using ALIEN_DLL.GEOS;
using GH_IO;
using Grasshopper.Kernel.Data;

namespace UI.CONS
{
	internal class CONTROL_C : UserControl, ICON_MENU
	{
		private TableLayoutPanel tblType;
		private TableLayoutPanel tblValue;
        private IGH_Param m_p;
        private Button button1;
        private Button button2;
        private ComboBox _cmbType;
        decimal t = 1;
        public CONTROL_C()
		{
            base.Load += new System.EventHandler(this.GH_DoubleSliderPopup_Load);
            this.InitializeComponent();
            //this.s_slider.ValueChanged += new Grasshopper.GUI.GH_Slider.ValueChangedEventHandler(this.s_slider_ValueChanged);
        }
        private void GH_DoubleSliderPopup_Load(object sender, System.EventArgs e)
		{
			GH_WindowsControlUtil.FixTextRenderingDefault(base.Controls);
		}
		private void InitializeComponent()
		{
            this.tblType = new System.Windows.Forms.TableLayoutPanel();
            this.tblValue = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this._cmbType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tblType
            // 
            this.tblType.Location = new System.Drawing.Point(0, 0);
            this.tblType.Name = "tblType";
            this.tblType.Size = new System.Drawing.Size(200, 100);
            this.tblType.TabIndex = 0;
            // 
            // tblValue
            // 
            this.tblValue.Location = new System.Drawing.Point(0, 0);
            this.tblValue.Name = "tblValue";
            this.tblValue.Size = new System.Drawing.Size(200, 100);
            this.tblValue.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 41);
            this.button1.TabIndex = 24;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(225, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 41);
            this.button2.TabIndex = 25;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // _cmbType
            // 
            this._cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cmbType.BackColor = System.Drawing.Color.White;
            this._cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbType.FormattingEnabled = true;
            this._cmbType.IntegralHeight = false;
            this._cmbType.Items.AddRange(new object[] {
            "Developable",
            "Loose",
            "Normal",
            "Straight",
            "Tight",
            "Uniform"});
            this._cmbType.Location = new System.Drawing.Point(75, 1);
            this._cmbType.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this._cmbType.Name = "_cmbType";
            this._cmbType.Size = new System.Drawing.Size(150, 39);
            this._cmbType.Sorted = true;
            this._cmbType.TabIndex = 26;
            // 
            // CONTROL_C
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._cmbType);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(200, 25);
            this.Name = "CONTROL_C";
            this.Size = new System.Drawing.Size(300, 41);
            this.ResumeLayout(false);

        }
        public bool SETUP(IGH_Param p)
        {
            //m_p = p;
            //if (m_p is Param_Number)
            //{
            //    try
            //    {
            //        s_slider.Value = Convert.ToDecimal(((GH_Number)(m_p.VolatileData.AllData(false).ElementAt(0))).Value);
            //    }
            //    catch
            //    {
            //    }
            //    this.s_slider.Type = GH_SliderAccuracy.Float;
            //    s_slider.InternalSlider.DecimalPlaces = 3;
            //    goto L;
            //}
            //if (m_p is Param_Integer)
            //{
            //    try
            //    {
            //        s_slider.Value = Convert.ToDecimal(((GH_Integer)(m_p.VolatileData.AllData(false).ElementAt(0))).Value);
            //    }
            //    catch
            //    {
            //    }
            //    this.s_slider.Type = GH_SliderAccuracy.Integer;
            //    s_slider.InternalSlider.DecimalPlaces = 0;
            //    goto L;
            //}
            //return false;
            //L:
            //READ();
            //this.fix_slider(s_slider, 1);
            return true;
        }
        //private void fix_slider(GH_Slider slider,decimal t)
        //{
        //    decimal l = new decimal(1);
        //    if (slider.Maximum < slider.Value + 2)
        //    {
        //        slider.Maximum += t;
        //        slider.Minimum = slider.Maximum- 10 *t;
        //    }             
        //    if (slider.Minimum + 2> slider.Value)
        //    {
        //        slider.Minimum -= t;
        //       slider.Maximum = slider.Minimum + 10 * t;
        //    }                       
        //    this.Refresh();
        //}
        //private void s_slider_ValueChanged(object sender, GH_SliderEventArgs e)
        //{
        //    if (ModifierKeys ==Keys.Up)
        //        t+=10;
        //    if (ModifierKeys == Keys.Down)
        //        t-=10;
        //    if (ModifierKeys == Keys.Shift)
        //        t = 10;
        //    if (ModifierKeys == Keys.Control)
        //        t = 100;
        //    t = Math.Max(1,t);
        //    fix_slider(s_slider,t);
        //    if (m_p is Param_Number)
        //    {
        //        ((Param_Number)m_p).PersistentData.ClearData();
        //        ((Param_Number)m_p).PersistentData.Append(new GH_Number((float)this.s_slider.Value));
        //        m_p.OnObjectChanged(GH_ObjectEventType.PersistentData);
        //        m_p.ExpireSolution(true);
        //    }
        //    if (m_p is Param_Integer)
        //    {
        //        ((Param_Integer)m_p).PersistentData.ClearData();
        //        ((Param_Integer)m_p).PersistentData.Append(new GH_Integer((int)this.s_slider.Value));
        //        m_p.OnObjectChanged(GH_ObjectEventType.PersistentData);
        //        m_p.ExpireSolution(true);
        //    }
        //}
        public bool WRITE()
        {
            try
            {
                //object[] values = new object[] {s_slider.Maximum, s_slider.Minimum,t };
                //string str = PANDA_OBJECT.VALUE.Serialize(values);
                //PANDA_OBJECT.VALUE.DOC_VALUE("SetValue", m_p, "Values", str);
                //PANDA_OBJECT.VALUE.DOC_VALUE("SetValue", m_p, "Write", true);
                return true;
            }
            catch
            {
                return false;
            } 
        }
        public bool READ()
        {
            try
            {
                //if (PANDA_OBJECT.VALUE.DOC_VALUE("GetValue", m_p, "Write", false))
                //{
                //    string str = PANDA_OBJECT.VALUE.DOC_VALUE("GetValue", m_p, "Values", "Panda");
                //    object[] values = PANDA_OBJECT.VALUE.Deserialize<object[]>(str);
                //    s_slider.Maximum = (decimal)(values[0]);
                //    s_slider.Minimum = (decimal)(values[1]);
                //    t= (decimal)(values[2]);
                //    return true;
                //}
                return false;
            }
            catch
            {
                return false;
            }
        }
        public Control CONTROL => this;
    }
}
