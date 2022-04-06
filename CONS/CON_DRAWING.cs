
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Grasshopper.Kernel;
namespace UI.CONS
{
	public class CON_DRAWING
	{
        public static SolidBrush BlackBrush;
        public static SolidBrush RedBrush;
		public static SolidBrush TBlackBrush;
		public static SolidBrush WhiteBrush;
		public static SolidBrush TWhiteBrush;
		public static LinearGradientBrush BlackGradBrush;
		public static Pen OutPen;
		public static Pen InPen;
		public static Pen WirePen;
		public static Pen FEWirePen;
		public static Pen WireEndPen;
		public static Pen SelectedPen;
		public static Font AtFont;
        public static Font MessageFont;
        public static Font MENU_FONT;
		public static Font TitleFont;
		public static SolidBrush AtBrush;
		public static Bitmap dummybmp;
		public static Graphics graphics;
        internal static Image selected_com_icon(Image image, float opacity)
        {
            float[][] nArray = { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, opacity, 0 }, new float[] { 0, 0, 0, 0, 1 } };
            System.Drawing.Imaging.ColorMatrix matrix = new System.Drawing.Imaging.ColorMatrix(nArray);
            System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();
            attributes.SetColorMatrix(matrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
            Bitmap resultImage = new Bitmap(image.Width, image.Height);
            Graphics g = Graphics.FromImage(resultImage);
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            return resultImage;
        }
        internal static double VSCALE(Rhino.Geometry.Point3d p)
        {
            double num;
            Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.ActiveViewport.GetWorldToScreenScale(p, out num);
            return num;
        }
        static CON_DRAWING()
		{
			BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            RedBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(127, 255, 0, 0));
			TBlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(120, 0, 0, 0));
			WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
			TWhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(120, 255, 255, 255));
			BlackGradBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.PointF(0f, 0f), new System.Drawing.PointF(0f, 20f), System.Drawing.Color.Black, System.Drawing.Color.FromArgb(0, 0, 0, 0));
			OutPen = new System.Drawing.Pen(System.Drawing.Color.Black, 4f);
			InPen = new System.Drawing.Pen(System.Drawing.Color.White, 2f);
			WirePen = new System.Drawing.Pen(System.Drawing.Color.White, 1f);
			FEWirePen = new System.Drawing.Pen(System.Drawing.Color.Black, 1.5f);
			WireEndPen = new System.Drawing.Pen(System.Drawing.Color.Black, 1.5f);
			SelectedPen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(100, 255, 255, 255), 2f);
#if rh6
            AtFont = GH_FontServer.StandardAdjusted;
#else
            AtFont = GH_FontServer.Standard;
#endif
            MessageFont = GH_FontServer.Script;
            MENU_FONT = GH_FontServer.Small;
#if rh6
           TitleFont = GH_FontServer.LargeAdjusted;
#else
            TitleFont = GH_FontServer.Large;
#endif

            AtBrush = new SolidBrush(System.Drawing.Color.Black);
			dummybmp = new System.Drawing.Bitmap(128, 128, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			graphics = System.Drawing.Graphics.FromImage(CON_DRAWING.dummybmp);
			FEWirePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			FEWirePen.DashPattern = new float[]
			{
				2f,
				1f
			};
			SelectedPen.Color = System.Drawing.Color.Black;
			SelectedPen.Width = 1f;
			SelectedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			SelectedPen.DashPattern = new float[]
			{
				1f,
				1f
			};
		}

	}
}
