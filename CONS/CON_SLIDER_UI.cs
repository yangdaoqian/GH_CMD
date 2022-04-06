namespace UI.CONS
{
    using Grasshopper.GUI;
    using Grasshopper.GUI.Base;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Parameters;
    using Grasshopper.Kernel.Types;
    using ALIEN_DLL.GEOS;
    using Rhino.Geometry;
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using UI.GEOS;
    internal class CON_SLIDER_UI : UserControl, ICON_MENU
    {
        private CON_SLIDER_LABEL domain_label;
        private IGH_Param m_p;
        private GH_TooltipComponent m_tooltipcomponent0;
        private GH_TooltipComponent m_tooltipcomponent1;
        private GH_TooltipComponent m_tooltipcomponent2;
        private GH_TooltipComponent m_tooltipcomponent3;
        private CON_SLIDER_PANEL max_panel;
        private CON_SLIDER_PANEL min_panel;
        private GH_Slider s_slider;
        private decimal t = decimal.One;

        public CON_SLIDER_UI()
        {
            base.Load += new EventHandler(this.GH_DoubleSliderPopup_Load);
            this.InitializeComponent();
            this.s_slider.ValueChanged += new GH_Slider.ValueChangedEventHandler(this.s_slider_ValueChanged);
            this.s_slider.Load += new EventHandler(this.s_slider_Load);
            this.s_slider.MouseClick += new MouseEventHandler(this.s_slider_click);
            this.domain_label.VALUE_CHANGED += new CON_SLIDER_LABEL.VALUE_CHANGED_EVENT_HANDLER(this.value_changed);
            this.min_panel.VALUE_CHANGED += new CON_SLIDER_PANEL.VALUE_CHANGED_EVENT_HANDLER(this.min_dwon);
            this.max_panel.VALUE_CHANGED += new CON_SLIDER_PANEL.VALUE_CHANGED_EVENT_HANDLER(this.max_dwon);
            this.m_tooltipcomponent0.PopulateTooltip += new GH_TooltipComponent.PopulateTooltipEventHandler(this._populate_tooltip);
            this.m_tooltipcomponent0.Delay = 200;
            this.m_tooltipcomponent0.Target = this.s_slider;
            this.m_tooltipcomponent1.PopulateTooltip += new GH_TooltipComponent.PopulateTooltipEventHandler(this._populate_tooltip);
            this.m_tooltipcomponent1.Delay = 200;
            this.m_tooltipcomponent1.Target = this.domain_label;
            this.m_tooltipcomponent2.PopulateTooltip += new GH_TooltipComponent.PopulateTooltipEventHandler(this._populate_tooltip);
            this.m_tooltipcomponent2.Delay = 200;
            this.m_tooltipcomponent2.Target = this.min_panel;
            this.m_tooltipcomponent3.PopulateTooltip += new GH_TooltipComponent.PopulateTooltipEventHandler(this._populate_tooltip);
            this.m_tooltipcomponent3.Delay = 200;
            this.m_tooltipcomponent3.Target = this.max_panel;
        }

        private void _populate_tooltip(object sender, GH_TooltipDisplayEventArgs e)
        {
            GH_TooltipComponent component = (GH_TooltipComponent) sender;
            if (component != null)
            {
                if (component.Target == this.s_slider)
                {
                    e.Title = "Slider";
                    e.Description = "Double click to set slider's value";
                    e.Text = this.s_slider.Value.ToString();
                }
                if (component.Target == this.domain_label)
                {
                    e.Title = "Domain";
                    e.Description = "Double click to set slider's domain";
                    e.Text = this.domain_label.VALUE.ToString();
                }
                if (component.Target == this.min_panel)
                {
                    e.Title = "Min";
                    e.Description = "Slider's min value";
                    e.Text = this.s_slider.Minimum.ToString();
                }
                if (component.Target == this.max_panel)
                {
                    e.Title = "Max";
                    e.Description = "Slider's max value";
                    e.Text = this.s_slider.Maximum.ToString();
                }
            }
        }

        private void GH_DoubleSliderPopup_Load(object sender, EventArgs e)
        {
            GH_WindowsControlUtil.FixTextRenderingDefault(base.Controls);
        }

        private void InitializeComponent()
        {
            this.s_slider = new Grasshopper.GUI.GH_Slider();
            this.m_tooltipcomponent0 = new Grasshopper.GUI.GH_TooltipComponent();
            this.m_tooltipcomponent1 = new Grasshopper.GUI.GH_TooltipComponent();
            this.m_tooltipcomponent2 = new Grasshopper.GUI.GH_TooltipComponent();
            this.m_tooltipcomponent3 = new Grasshopper.GUI.GH_TooltipComponent();
            this.max_panel = new CON_SLIDER_PANEL();
            this.min_panel = new CON_SLIDER_PANEL();
            this.domain_label = new CON_SLIDER_LABEL();
            this.SuspendLayout();
            // 
            // s_slider
            // 
            this.s_slider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.s_slider.BackColor = System.Drawing.Color.White;
            this.s_slider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.s_slider.ControlEdgeColour = System.Drawing.Color.Black;
            this.s_slider.ControlShadowColour = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.s_slider.DecimalPlaces = 0;
            this.s_slider.DrawControlBorder = false;
            this.s_slider.DrawControlShadows = false;
            this.s_slider.ForeColor = System.Drawing.Color.Black;
            this.s_slider.FormatMask = "{0}";
            this.s_slider.GripBottomColour = System.Drawing.Color.White;
            this.s_slider.GripDisplay = Grasshopper.GUI.Base.GH_SliderGripDisplay.Numeric;
            this.s_slider.GripEdgeColour = System.Drawing.SystemColors.ControlDarkDark;
            this.s_slider.GripTopColour = System.Drawing.Color.White;
            this.s_slider.Location = new System.Drawing.Point(3, 30);
            this.s_slider.Margin = new System.Windows.Forms.Padding(0);
            this.s_slider.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.s_slider.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.s_slider.Name = "s_slider";
            this.s_slider.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.s_slider.RailBrightColour = System.Drawing.SystemColors.ControlLightLight;
            this.s_slider.RailDarkColour = System.Drawing.SystemColors.ControlDark;
            this.s_slider.RailDisplay = Grasshopper.GUI.Base.GH_SliderRailDisplay.Simple;
            this.s_slider.RailEmptyColour = System.Drawing.SystemColors.Control;
            this.s_slider.RailFullColour = System.Drawing.SystemColors.Highlight;
            this.s_slider.ShadowSize = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.s_slider.ShowTextInputOnDoubleClick = true;
            this.s_slider.ShowTextInputOnKeyDown = false;
            this.s_slider.Size = new System.Drawing.Size(456, 30);
            this.s_slider.TabIndex = 22;
            this.s_slider.TextColour = System.Drawing.SystemColors.ActiveCaptionText;
            this.s_slider.TickCount = 20;
            this.s_slider.TickDisplay = Grasshopper.GUI.Base.GH_SliderTickDisplay.Simple;
            this.s_slider.TickFrequency = 10;
            this.s_slider.Type = Grasshopper.GUI.Base.GH_SliderAccuracy.Integer;
            this.s_slider.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // domain_label
            // 
            this.domain_label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.domain_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.domain_label.Location = new System.Drawing.Point(156, 3);
            this.domain_label.Margin = new System.Windows.Forms.Padding(6);
            this.domain_label.Name = "domain_label";
            this.domain_label.ShowTextInputOnDoubleClick = true;
            this.domain_label.ShowTextInputOnKeyDown = true;
            this.domain_label.Size = new System.Drawing.Size(150, 24);
            this.domain_label.TabIndex = 23;
            // 
            // min_panel
            // 
            this.min_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.min_panel.AutoSize = true;
            this.min_panel.BackColor = System.Drawing.SystemColors.Window;
            this.min_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.min_panel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.min_panel.Location = new System.Drawing.Point(3, 3);
            this.min_panel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.min_panel.Name = "min_panel";
            this.min_panel.Size = new System.Drawing.Size(150, 24);
            this.min_panel.TabIndex = 24;
            // 
            // max_panel
            // 
            this.max_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.max_panel.AutoSize = true;
            this.max_panel.BackColor = System.Drawing.SystemColors.Window;
            this.max_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.max_panel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.max_panel.Location = new System.Drawing.Point(309, 3);
            this.max_panel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.max_panel.Name = "max_panel";
            this.max_panel.Size = new System.Drawing.Size(150, 24);
            this.max_panel.TabIndex = 25;
            // 
            // m_tooltipcomponent0
            // 
            this.m_tooltipcomponent0.Tag = null;
            this.m_tooltipcomponent0.Target = null;
            // 
            // m_tooltipcomponent1
            // 
            this.m_tooltipcomponent1.Tag = null;
            this.m_tooltipcomponent1.Target = null;
            // 
            // m_tooltipcomponent2
            // 
            this.m_tooltipcomponent2.Tag = null;
            this.m_tooltipcomponent2.Target = null;
            // 
            // m_tooltipcomponent3
            // 
            this.m_tooltipcomponent3.Tag = null;
            this.m_tooltipcomponent3.Target = null;
            // 
            // CONTROL_SLIDER
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.s_slider);
            this.Controls.Add(this.domain_label);
            this.Controls.Add(this.min_panel);
            this.Controls.Add(this.max_panel);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(91, 39);
            this.Name = "CONTROL_SLIDER";
            this.Size = new System.Drawing.Size(462, 63);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void m_move(object sender, MouseEventArgs e)
        {
            if (this.s_slider.Bounds.Contains(e.Location))
            {
                this.m_tooltipcomponent0.Target = this.s_slider;
            }
            if (this.min_panel.Bounds.Contains(e.Location))
            {
                this.m_tooltipcomponent0.Target = this.min_panel;
            }
            if (this.max_panel.Bounds.Contains(e.Location))
            {
                this.m_tooltipcomponent0.Target = this.max_panel;
            }
            if (this.domain_label.Bounds.Contains(e.Location))
            {
                this.m_tooltipcomponent0.Target = this.domain_label;
            }
        }

        private void max_dClick(object sender, MouseEventArgs e)
        {
        }

        private void max_dwon(object sender, CON_SLIDER_PANEL.PANEL_EVENT_ARGS e)
        {
            this.s_slider.Maximum = e.VALUE;
            if (this.min_panel.VALUE > (this.s_slider.Maximum - decimal.One))
            {
                this.min_panel.VALUE = this.s_slider.Maximum - decimal.One;
            }
            this.domain_label.VALUE = new GH_Interval(new Interval((double) this.s_slider.Minimum, (double) this.s_slider.Maximum));
            this.WRITE();
        }

        private void min_dwon(object sender, CON_SLIDER_PANEL.PANEL_EVENT_ARGS e)
        {
            this.s_slider.Minimum = e.VALUE;
            if (this.max_panel.VALUE < (this.s_slider.Minimum + decimal.One))
            {
                this.max_panel.VALUE = this.s_slider.Minimum + decimal.One;
            }
            this.domain_label.VALUE = new GH_Interval(new Interval((double) this.s_slider.Minimum, (double) this.s_slider.Maximum));
            this.WRITE();
        }

        public bool READ()
        {
            try
            {
                if (P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", this.m_p as GH_DocumentObject, "Write", false))
                {
                    object[] objArray = P_OBJECT<object[], object>.Deserialize(P_OBJECT<string, object>.DOC_GETVALUE("GetValue", this.m_p as GH_DocumentObject, "Values", "Panda"));
                    this.s_slider.Maximum = (decimal) objArray[0];
                    this.s_slider.Minimum = (decimal) objArray[1];
                    this.t = (decimal) objArray[2];
                    this.s_slider.InternalSlider.DecimalPlaces = (int) objArray[3];
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void s_slider_click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
            }
        }

        private void s_slider_Load(object sender, EventArgs e)
        {
        }

        private void s_slider_ValueChanged(object sender, GH_SliderEventArgs e)
        {
            if (this.m_p is Param_Number)
            {

                ((Param_Number) this.m_p).PersistentData.ClearData();
                ((Param_Number) this.m_p).PersistentData.Append(new GH_Number((double) ((float) this.s_slider.Value)));
                this.m_p.OnObjectChanged(GH_ObjectEventType.PersistentData);
                this.m_p.ExpireSolution(true);
            }
            if (this.m_p is Param_Integer)
            {
                ((Param_Integer) this.m_p).PersistentData.ClearData();
                ((Param_Integer) this.m_p).PersistentData.Append(new GH_Integer((int) this.s_slider.Value));
                this.m_p.OnObjectChanged(GH_ObjectEventType.PersistentData);
                this.m_p.ExpireSolution(true);
            }
            this.WRITE();
        }

        public bool SETUP(IGH_Param p)
        {
            this.m_p = p;
            if (this.m_p is Param_Number)
            {
                try
                {
                    this.s_slider.Type = GH_SliderAccuracy.Float;
                    this.s_slider.InternalSlider.Type = GH_SliderAccuracy.Float;
                    this.s_slider.InternalSlider.DecimalPlaces = 3;
                    this.min_panel.DIGITAL = 3;
                    this.max_panel.DIGITAL = 3;
                    try
                    {
                        this.READ();
                    }
                    catch
                    {
                    }
                    try
                    {
                        decimal num = Convert.ToDecimal(((GH_Number) this.m_p.VolatileData.AllData(false).ElementAt<IGH_Goo>(0)).Value);
                        if (this.s_slider.Maximum < num)
                        {
                            this.s_slider.Maximum = num + 5M;
                        }
                        if (this.s_slider.Minimum > num)
                        {
                            this.s_slider.Minimum = num - 5M;
                        }
                        this.s_slider.Value = num;
                    }
                    catch
                    {
                    }
                    this.min_panel.DIGITAL = this.s_slider.InternalSlider.DecimalPlaces;
                    this.max_panel.DIGITAL = this.s_slider.InternalSlider.DecimalPlaces;
                    this.domain_label.DIGITAL = this.s_slider.InternalSlider.DecimalPlaces;
                    this.min_panel.VALUE = this.s_slider.Minimum;
                    this.max_panel.VALUE = this.s_slider.Maximum;
                    this.domain_label.VALUE = new GH_Interval(new Interval((double) this.s_slider.Minimum, (double) this.s_slider.Maximum));
                    return true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("uislider setup" + exception.Message);
                }
                return true;
            }
            if (this.m_p is Param_Integer)
            {
                try
                {
                    try
                    {
                        this.READ();
                    }
                    catch
                    {
                    }
                    try
                    {
                        decimal num = Convert.ToDecimal(((GH_Integer)this.m_p.VolatileData.AllData(false).ElementAt<IGH_Goo>(0)).Value);
                        if (this.s_slider.Maximum < num)
                        {
                            this.s_slider.Maximum = num + 5M;
                        }
                        if (this.s_slider.Minimum > num)
                        {
                            this.s_slider.Minimum = num - 5M;
                        }
                        this.s_slider.Value = num;
                    }
                    catch
                    {
                    }
                    this.s_slider.Type = GH_SliderAccuracy.Integer;
                    this.domain_label.VALUE = new GH_Interval(new Interval((double) this.s_slider.Minimum, (double) this.s_slider.Maximum));
                    this.min_panel.VALUE = this.s_slider.Minimum;
                    this.max_panel.VALUE = this.s_slider.Maximum;
                    return true;
                }
                catch
                {
                }
                return true;
            }
            return false;
        }

        private void value_changed(object sender, CON_SLIDER_LABEL.LABEL_EVENT_ARGS e)
        {
            GH_Interval vALUE = e.VALUE;
            int dIGITAL = e.DIGITAL;
            decimal min = (decimal) vALUE.Value.Min;
            decimal max = (decimal) vALUE.Value.Max;
            this.s_slider.Maximum = max;
            this.s_slider.Minimum = min;
            this.min_panel.DIGITAL = dIGITAL;
            this.max_panel.DIGITAL = dIGITAL;
            this.min_panel.VALUE = min;
            this.max_panel.VALUE = max;
            this.s_slider.InternalSlider.DecimalPlaces = dIGITAL;
            this.WRITE();
        }

        public bool WRITE()
        {
            try
            {
                object[] data = new object[] { this.s_slider.Maximum, this.s_slider.Minimum, this.t, this.s_slider.InternalSlider.DecimalPlaces };
                string v = P_OBJECT<object[], object>.Serialize(data);
                P_OBJECT<string, object>.DOC_SETVALUE("SetValue", this.m_p as GH_DocumentObject, "Values", v);
                P_OBJECT<bool, object>.DOC_SETVALUE("SetValue", this.m_p as GH_DocumentObject, "Write", true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Control CONTROL
        {
            get
            {
                return this;
            }
        }
    }
}

