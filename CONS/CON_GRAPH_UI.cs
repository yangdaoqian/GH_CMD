namespace UI.CONS
{
    using Grasshopper;
    using Grasshopper.GUI.Base;
    using Grasshopper.GUI.Canvas;
    using Grasshopper.Kernel.Graphs;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Grasshopper.GUI;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Types;
    using Grasshopper.Kernel.Special;


    [DesignerGenerated]
    public class CON_GRAPH_UI : UserControl,ICON_MENU
    {
        [AccessedThroughProperty("pnlGraph"), CompilerGenerated]
        private GH_DoubleBufferedPanel _pnlGraph;
        protected GH_GraphMapper m_graph;
        private IGH_Param m_param;
        public CON_GRAPH_UI()
        {
            base.Load += new EventHandler(this.GH_GraphEditor_Load);
           // base.KeyDown += new KeyEventHandler(this.GH_GraphEditor_KeyDown);
            this.InitializeComponent();
            this._pnlGraph.SizeChanged += new EventHandler(this.pnlGraph_SizeChanged);
            this._pnlGraph.Paint += new PaintEventHandler(this.pnlGraph_Paint);
            this._pnlGraph.MouseDown += new MouseEventHandler(this.pnlGraph_MouseDown);
            this._pnlGraph.MouseMove += new MouseEventHandler(this.pnlGraph_MouseMove);
            this._pnlGraph.MouseUp += new MouseEventHandler(this.pnlGraph_MouseUp);
           // this.MouseLeave += f_d;
        }
        private void f_d(object o, EventArgs e)
        {
            this.WRITE();
            //this.Close();
            this.Dispose();
        }


        //private void GH_GraphEditor_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Escape)
        //    {
        //        base.DialogResult = DialogResult.Cancel;
        //    }
        //}

        private void GH_GraphEditor_Load(object sender, EventArgs e)
        {
            GH_WindowsControlUtil.FixTextRenderingDefault(base.Controls);
            this.pnlGraph_SizeChanged(this._pnlGraph, e);
        }

        private void GraphChanged(GH_GraphContainer sender, bool bIntermediate)
        {
            this._pnlGraph.Refresh();
            this.m_param?.ExpireSolution(true);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this._pnlGraph = new Grasshopper.GUI.GH_DoubleBufferedPanel();
            this.SuspendLayout();
            // 
            // _pnlGraph
            // 
            this._pnlGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pnlGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlGraph.Location = new System.Drawing.Point(3, 3);
            this._pnlGraph.Name = "_pnlGraph";
            this._pnlGraph.Size = new System.Drawing.Size(240, 240);
            this._pnlGraph.TabIndex = 1;
            // 
            // CONTROL_GRAPH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pnlGraph);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.MinimumSize = new System.Drawing.Size(246, 246);
            this.Name = "CONTROL_GRAPH";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(246, 246);
            this.ResumeLayout(false);

        }

        private void pnlGraph_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.m_graph != null)
            {
                GH_Viewport vp = new GH_Viewport(new Point(0, 0), 1f);
                GH_CanvasMouseEvent event2 = new GH_CanvasMouseEvent(vp, e);
                this.m_graph.Container.RespondToMouseDown(null, event2);
            }
        }

        private void pnlGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_graph != null)
            {
                GH_Viewport vp = new GH_Viewport(new Point(0, 0), 1f);
                GH_CanvasMouseEvent event2 = new GH_CanvasMouseEvent(vp, e);
                this.m_graph.Container.RespondToMouseMove(null, event2);
            }
        }

        private void pnlGraph_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.m_graph != null)
            {
                GH_Viewport vp = new GH_Viewport(new Point(0, 0), 1f);
                GH_CanvasMouseEvent event2 = new GH_CanvasMouseEvent(vp, e);
                this.m_graph.Container.RespondToMouseUp(null, event2);
            }
        }

        private void pnlGraph_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this._pnlGraph.BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.TextRenderingHint = GH_TextRenderingConstants.GH_CrispText;
            if (this.m_graph != null)
            {
#if rh6
                this.m_graph.Container.DisplayScale = Global_Proc.UiAdjust((float) 1f);
#endif
                this.m_graph.Container.Render(e.Graphics, true, null);
            }
        }

        private void pnlGraph_SizeChanged(object sender, EventArgs e)
        {
            if (this.m_graph != null)
            {
                Rectangle clientRectangle = this._pnlGraph.ClientRectangle;
                clientRectangle.Inflate(-5, -5);
                this.m_graph.Container.Region = clientRectangle;
            }
        }

        public bool SETUP(IGH_Param param)
        {

            this.m_graph = param as GH_GraphMapper;
            if (this.m_graph != null)
            {
                //if (this.m_graph.Container.Graph != null)
                //{
                    this.m_graph.Container.Region = new Rectangle(0, 0, 100, 100);
                    this.m_graph.Container.LockGrips = false;
                    this.m_graph.Container.GraphChanged += new GH_GraphContainer.GraphChangedEventHandler(this.GraphChanged);
                this.m_param = param;
                return true;
                //}
            }
            //PARAM_GRAPH_MAPPER pp= param as PARAM_GRAPH_MAPPER;
            //if (pp != null)
            //{
            //    this.m_graph = pp.graph;
            //    if (this.m_graph != null)
            //    {
            //        //if (this.m_graph.Container.Graph != null)
            //        //{
            //            this.m_graph.Container.Region = new Rectangle(0, 0, 100, 100);
            //            this.m_graph.Container.LockGrips = false;
            //            this.m_graph.Container.GraphChanged += new GH_GraphContainer.GraphChangedEventHandler(this.GraphChanged);
            //            this.m_param = param;
            //            return true;
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

