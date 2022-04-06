namespace UI.CONS
{
    using Grasshopper.GUI.Gradient;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    using Grasshopper.GUI;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Types;
    using Grasshopper.Kernel.Special;
 

    [DesignerGenerated]
    public class CON_GRADIENT_UI : UserControl, ICON_MENU
    {
        private IContainer components;
        private GH_DoubleBufferedPanel _pnlGradient;
        protected GH_Gradient m_gradient;
        private IGH_Param m_param;
        public CON_GRADIENT_UI()
        {
            base.Load += new EventHandler(this.GH_GradientEditor_Load);
            this.m_gradient = new GH_Gradient();
            this.InitializeComponent();
            this._pnlGradient.MouseDown += this.pnlGradient_MouseDown;
            this._pnlGradient.MouseMove += this.pnlGradient_MouseMove;
            this._pnlGradient.MouseUp += this.pnlGradient_MouseUp;
            this._pnlGradient.Paint += this.pnlGradient_Paint;
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void GH_GradientEditor_Load(object sender, EventArgs e)
        {
            GH_WindowsControlUtil.FixTextRenderingDefault(base.Controls);
           
        }

        private void Gradient_Changed(object sender, GH_GradientChangedEventArgs e)
        {
            this._pnlGradient.Refresh();
            this.m_param.ExpireSolution(true);
        }

        private void Gradient_SelectionChanged(object sender, GH_GradientChangedEventArgs e)
        {
            this._pnlGradient.Refresh();
            this.m_param.ExpireSolution(true);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this._pnlGradient = new Grasshopper.GUI.GH_DoubleBufferedPanel();
            this.SuspendLayout();
            // 
            // _pnlGradient
            // 
            this._pnlGradient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pnlGradient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlGradient.Location = new System.Drawing.Point(0, 0);
            this._pnlGradient.Margin = new System.Windows.Forms.Padding(0);
            this._pnlGradient.Name = "_pnlGradient";
            this._pnlGradient.Size = new System.Drawing.Size(480, 90);
            this._pnlGradient.TabIndex = 0;
            // 
            // CONTROL_GRADIENT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pnlGradient);
            this.MinimumSize = new System.Drawing.Size(480, 90);
            this.Name = "CONTROL_GRADIENT";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(480, 90);
            this.ResumeLayout(false);

        }

        private void pnlGradient_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.m_gradient != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.m_gradient.MouseDown(this._pnlGradient.ClientRectangle, (PointF) e.Location);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    int num = this.m_gradient.NearestGrip(this._pnlGradient.ClientRectangle, (PointF) e.Location, 10.0);
                    if (num >= 0)
                    {
                        this.m_gradient.DisplayGripColourPicker(this.m_gradient[num]);
                    }
                }
            }
        }

        private void pnlGradient_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_gradient != null)
            {
                this.m_gradient.MouseMove(this._pnlGradient.ClientRectangle, (PointF) e.Location);
            }
        }

        private void pnlGradient_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.m_gradient != null)
            {
#if rh6
                this.m_gradient.MouseUp(this._pnlGradient.ClientRectangle, (PointF) e.Location, false);
#else
                this.m_gradient.MouseUp(this._pnlGradient.ClientRectangle, (PointF)e.Location);
#endif
            }
        }

        private void pnlGradient_Paint(object sender, PaintEventArgs e)
        {
            if (this.m_gradient == null)
            {
                e.Graphics.Clear(this._pnlGradient.BackColor);
            }
            else
            {
                this.m_gradient.Render_Background(e.Graphics, this._pnlGradient.ClientRectangle);
                this.m_gradient.Render_Gradient(e.Graphics, this._pnlGradient.ClientRectangle);
                this.m_gradient.Render_Grips(e.Graphics, this._pnlGradient.ClientRectangle);
            }
        }

        private void propGrip_PropertyValueChanged(object sender, EventArgs e)
        {
            this._pnlGradient.Refresh();
        }

        private void propGrip_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            this._pnlGradient.Refresh();
        }
        public bool SETUP(IGH_Param param)
        {

            //this.m_graph = param as GH_GraphMapper;
            //if (this.m_graph != null)
            //{
            //    //if (this.m_graph.Container.Graph != null)
            //    //{
            //    this.m_graph.Container.Region = new Rectangle(0, 0, 100, 100);
            //    this.m_graph.Container.LockGrips = false;
            //    this.m_graph.Container.GraphChanged += new GH_GraphContainer.GraphChangedEventHandler(this.GraphChanged);
            //    this.m_param = param;
            //    return true;
            //    //}
            //}
            //PARAM_GRADIENT_CONTROL pp = param as PARAM_GRADIENT_CONTROL;
            //if (pp != null)
            //{
            //    this.m_gradient = pp.graph.Gradient;
            //    if (this.m_gradient != null)
            //    {
            //        this.m_gradient.GradientChanged += new GH_Gradient.GradientChangedEventHandler(this.Gradient_Changed);
            //        this.m_gradient.SelectionChanged += new GH_Gradient.SelectionChangedEventHandler(this.Gradient_SelectionChanged);
            //        this.m_param = param;
            //        return true;
            //    }
            //}
            return false;
        }
        public bool WRITE()
        {
            return true;
        }
        public bool READ()
        {
            return true;
        }
        public Control CONTROL => this;


    }
}

