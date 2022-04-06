using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grasshopper.Kernel.Types;
using GH_IO;

namespace UI.CONS
{ 
    internal class CONTROL_LIST_BOX<T> : ListBox where T : class,GH_ISerializable
    {    
        internal CONTROL_LIST_BOX(CON_LIST_ITEM<T>[] p_items,SelectionMode m):base()
        {
            this.ItemHeight = 20;
            this.Items.AddRange(p_items);
            int n = 0;
            foreach(CON_LIST_ITEM<T> i in p_items)
            {
                i.SELECTED = i.SELECTED == null ?new GH_Boolean(false): i.SELECTED;
                this.SetSelected(n, i.SELECTED.Value);
                n++;
            }
            this.SelectionMode = m;
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            CON_LIST_ITEM<T> p_item = (CON_LIST_ITEM<T>)this.Items[e.Index];
            try
            {

#if rh6
                e.Graphics.DrawString(p_item.NAME.Value, p_item.FONT == null ? CON_DRAWING.AtFont :p_item.FONT, new SolidBrush(p_item.COLOR == null ? e.ForeColor : p_item.COLOR.Value), e.Bounds);
#else
                e.Graphics.DrawString(p_item.NAME.Value, p_item.FONT == null ? CONTROL_DRAWING.AtFont : new Font(p_item.FONT.Value.FaceName, 8), new SolidBrush(p_item.COLOR == null ? e.ForeColor : p_item.COLOR.Value), e.Bounds);
#endif
            }
            catch(Exception EX)
            {
                MessageBox.Show("OnDrawItem error:" + EX.Message);
            }
        }
        //protected override void OnMeasureItem(MeasureItemEventArgs e)
        //{
        //    base.OnMeasureItem(e);
        //    CONTROL_LIST_ITEM<T> p_item = (CONTROL_LIST_ITEM<T>)this.Items[e.Index];
        //   SizeF sf= e.Graphics.MeasureString(p_item.NAME.Value, p_item.FONT == null ? CONTROL_DRAWING.AtFont: p_item.FONT.Value);
        //    e.ItemHeight = (int)(sf.Height)+24;
            
        //}
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
