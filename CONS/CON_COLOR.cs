namespace UI.CONS
{
    using Grasshopper.GUI;
    using Grasshopper;
    using Grasshopper.Kernel;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using Grasshopper.Kernel.Types;
    using System.Collections.Generic;
    using System.Linq;
    using Grasshopper.Kernel.Parameters;
  
    [DesignerGenerated]
    public class CON_COLOR :UserControl,ICON_MENU
    {
      private List<GH_Colour> m_colors;
      public CON_COLOR()
        {     
            this.InitializeComponent();
            base.Paint += new PaintEventHandler(this.GH_ToolTipForm_Paint);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            this.MouseLeave += f_d;
        }
        private void f_d(object o, EventArgs e)
        {
            this.WRITE();
           // this.Close();
            this.Dispose();
        }
        private string  get_str(GH_Colour colour)
        {
            return string.Format("{0}",colour);
        }
#if rh6
              private int width => GH_FontServer.MeasureString("255,255,255", GH_FontServer.StandardAdjusted).Width;

        private int height => GH_FontServer.MeasureString("255,255,255", GH_FontServer.StandardAdjusted).Height+6;
#else
        private int width => GH_FontServer.MeasureString("255,255,255", GH_FontServer.Standard).Width;

        private int height => GH_FontServer.MeasureString("255,255,255", GH_FontServer.Standard).Height + 6;
#endif


        private void GH_ToolTipForm_Paint(object sender, PaintEventArgs e)
        {
                e.Graphics.TextRenderingHint = GH_TextRenderingConstants.GH_CrispText;  
                this.PaintText(e.Graphics);        
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GH_ToolTipForme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(383, 243);
          //  this.ControlBox = false;
            this.DoubleBuffered = true;
           // this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
          //  this.MaximizeBox = false;
          //  this.MinimizeBox = false;
            this.Name = "GH_ToolTipForme";
          //  this.ShowIcon = false;
           // this.ShowInTaskbar = false;
          //  this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
           // this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Grasshopper Custom Tooltip";
            //this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.ResumeLayout(false);

        }
        private void PaintText(Graphics g)
        {
            int num = m_colors.Count;
            for(int i=0;i<num;i++)
            {
                Rectangle r = new Rectangle(3,3+(height+3)*i,width,height);
                Rectangle tr = new Rectangle(3, 3 + (height + 3)* i, width, height);
                tr.Inflate(-3,-3);
                Color c = m_colors[i].Value;
                SolidBrush b = new SolidBrush(c);
                g.DrawRectangle(new Pen(c),r);
#if rh6
                g.DrawString(m_colors[i].ToString(),GH_FontServer.StandardAdjusted,b,tr.X,tr.Y);
#else
                g.DrawString(m_colors[i].ToString(), GH_FontServer.Standard, b, tr.X, tr.Y);
#endif
                b.Dispose();
            }
        }
        //protected override bool ShowWithoutActivation =>true;
        public bool SETUP(IGH_Param param)
        {
            try
            {
                this.m_colors = (param as Param_Colour).VolatileData.AllData(false).ToList().ConvertAll(i => (GH_Colour)i);
            }
            catch
            {
            }
            try
            {
                //this.m_colors = (param as PARAM_COLOR).VolatileData.AllData(false).ToList().ConvertAll(i => (GH_Colour)i);
            }
            
            catch
            {
            }
            if (this.m_colors == null)
                return false;
            this.Width = width + 6;
            this.Height = m_colors.Count * (height + 3) + 3;
            return true;
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

