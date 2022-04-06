using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
namespace UI.CONS
{
    internal class CONTROL_ALIEN_INFO : Form
    {
        Image animatedImage;
        internal CONTROL_ALIEN_INFO(Image gif)
        {
            animatedImage = gif;
        }
        bool currentlyAnimating = false;
        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }
        private void OnFrameChanged(object o, EventArgs e)
        {
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            AnimateImage();
            ImageAnimator.UpdateFrames();
            e.Graphics.DrawImage(this.animatedImage, new Point(0, 0));
            e.Graphics.DrawString("ImageAnimator+OnPaint", Font, SystemBrushes.ControlText, new PointF(0, 0));
        }
        protected override void OnLoad(EventArgs e)
        {
            new Label()
            {
                AutoSize = false,
                Image = animatedImage,
                Parent = this,
                Location = new Point(0, 0),
                Size = animatedImage.Size,
                Text = ""
            };
            Text = "ImageAnimator";
            this.Height = animatedImage.Height + 60;
            this.Width = animatedImage.Width + 20;
            base.OnLoad(e);
        }
    }
}

