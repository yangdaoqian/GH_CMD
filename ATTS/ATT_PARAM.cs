namespace UI.ATTS
{
    using Grasshopper;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Attributes;
    using Grasshopper.Kernel.Types;
    using ALIEN_DLL.GEOS;
    using Rhino;
    using Rhino.Display;
    using Rhino.Geometry;
    using Rhino.Geometry.Intersect;
    using Rhino.Input.Custom;
    using System;
    using Rhino.UI;
    using Rhino.UI.Gumball;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using UI.GEOS;
    internal class ATT_PARAM : GH_FloatingParamAttributes,I_PATT
    {
        private List<GumballDisplayConduit> m_conduits;
        private List<GumballObject> m_gumballs;
        internal MouseHandler m_handler;
        private List<IGH_GeometricGoo> m_points;
        protected bool m_att;
        protected bool m_menu;
        protected bool m_tag;
        protected bool m_gum;
        public bool ATT
        {
            get => (bool)UI_SETTING.INS["ATT"] ? this.m_att : false;
            set
            {
                m_att = value;
            }
        }
        public bool MENU
        {
            get => (bool)UI_SETTING.INS["MENU"] ? this.m_menu : false;
            set
            {
                m_menu = value;
            }
        }
        public bool TAG
        {
            get => (bool)UI_SETTING.INS["TAG"] ? this.m_tag : false;
            set
            {
                m_tag = value;
            }
        }
        public bool GUM
        {
            get => (bool)UI_SETTING.INS["GUM"] ? this.m_gum : false;
            set
            {
                m_gum = value;
       
            }
        }
        public bool SELECTED
        {
            get => this.Selected;
            set
            {
                this.Selected = value;
            }
        }
        public GH_DocumentObject OWNER => this.Owner as GH_DocumentObject;
        public ATT_PARAM(IGH_Param param) : base(param)
        {          
            this.m_points = new List<IGH_GeometricGoo>();
            this.m_gumballs = new List<GumballObject>();
            this.m_conduits = new List<GumballDisplayConduit>();     
            if (System.Activator.CreateInstance(param.Type) is IGH_GeometricGoo)
            {
                this.m_gum= P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "GUM",true);    
            }
            this.m_att = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "ATT", true);
            this.m_menu = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "MENU", true);
            this.m_tag = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "TAG", true);
        }
        public void HIDE_GUM()
        {
            try
            {
                foreach (GumballDisplayConduit local1 in this.m_conduits)
                {
                    local1.Enabled = false;
                    local1.Dispose();
                }
                using (List<GumballObject>.Enumerator enumerator2 = this.m_gumballs.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        enumerator2.Current.Dispose();
                    }
                }
                this.m_gumballs.Clear();
                this.m_conduits.Clear();
                this.m_points.Clear();
                if (this.m_handler != null)
                {
                    this.m_handler.Enabled = false;
                    this.m_handler = null;
                }
            }
            catch(Exception EX)
            {
               // MessageBox.Show("hidegum"+EX.Message+EX.InnerException.Message);
            }
        }
        internal void add_items(GumballObject g, GumballDisplayConduit i, IGH_GeometricGoo p)
        {
            this.m_gumballs.Add(g);
            this.m_conduits.Add(i);
            this.m_points.Add(p);
        }
        public void SHOW_GUM()
        {
            try
            {
               ATT_CLASS.SHOW_GUMBALLS(this.Owner);
            }
            catch(System.Exception ex)
            {
                MessageBox.Show("gum"+ex.Message);
            }
        }
        public int GumballCount =>
            this.m_gumballs.Count;
        public override bool Selected
        {
            get =>
                base.Selected;
            set
            {
                if (base.Selected != value)
                {
                    base.Selected = value;
                    if (GUM)
                    {
                        if (value)
                        {
                            this.SHOW_GUM();
                            RhinoDoc.ActiveDoc.Views.Redraw();
                        }
                        else
                        {
                            this.HIDE_GUM();
                            RhinoDoc.ActiveDoc.Views.Redraw();
                        }
                    }
                }
            }
        }
        internal class MouseHandler : MouseCallback
        {
            private int index = -1;
            private readonly ATT_PARAM param;
            private bool undo = false;

            internal MouseHandler(ATT_PARAM owner)
            {
                this.param = owner;
                this.index = -1;
            }

            private PickContext CreatePickContext(RhinoViewport view,System.Drawing.Point windowPoint)
            {
                Line line;
                PickContext context1 = new PickContext {
                    View = view.ParentView,
                    PickStyle = PickStyle.PointPick
                };
                Transform pickTransform = view.GetPickTransform(windowPoint);
                context1.SetPickTransform(pickTransform);
                view.GetFrustumLine((double) windowPoint.X, (double) windowPoint.Y, out line);
                context1.PickLine = line;
                context1.UpdateClippingPlanes();
                return context1;
            }

            private IGH_GeometricGoo GhPoint()
            {
                if (this.index < 0)
                {
                    return null;
                }
                if (this.param == null)
                {
                    return null;
                }
                if (this.index >= this.param.m_points.Count)
                {
                    return null;
                }
                return this.param.m_points[this.index];
            }

            protected override void OnMouseDown(MouseCallbackEventArgs e)
            {
                this.index = -1;
#if rh6
                if ((this.param.m_conduits.Count != 0) && (e.MouseButton == MouseButton.Left))
#else
                if ((this.param.m_conduits.Count != 0) && (e.Button ==MouseButtons.Left))
#endif
                {
                    PickContext pickContext = this.CreatePickContext(e.View.MainViewport, e.ViewportPoint);
                    foreach (GumballDisplayConduit conduit in this.param.m_conduits)
                    {
                        if (conduit.PickGumball(pickContext, null))
                        {
                            this.index = this.param.m_conduits.IndexOf(conduit);
                            this.undo = true;
                            e.Cancel = true;
                            return;
                        }
                    }
                    base.OnMouseDown(e);
                }
            }

            protected override void OnMouseMove(MouseCallbackEventArgs e)
            {
                try
                {
                    if (((this.index >= 0) && (this.param.m_conduits.Count != 0)) && ((Control.MouseButtons == MouseButtons.Left) && (this.index < this.param.m_conduits.Count)))
                    {
                        if (this.undo)
                        {
                            this.param.Owner.RecordUndoEvent("Gumball drag");
                            this.undo = false;
                        }
                        GumballDisplayConduit conduit = this.param.m_conduits[this.index];
                        IGH_GeometricGoo point = this.GhPoint();
                        //conduit.Gumball.SetFromPlane(new Plane(point.Boundingbox.Center, Vector3d.XAxis, Vector3d.YAxis));
                        if (conduit.PickResult.Mode != GumballMode.None)
                        {
                            Line unset;
                            double num;
                            conduit.CheckShiftAndControlKeys();
                            if (!e.View.MainViewport.GetFrustumLine((double)e.ViewportPoint.X, (double)e.ViewportPoint.Y, out unset))
                            {
                                unset = Line.Unset;
                            }
                            Plane plane = e.View.MainViewport.GetConstructionPlane().Plane;
                            Intersection.LinePlane(unset, plane, out num);
                            Point3d pointd = unset.PointAt(num);
                            Plane plane0 = conduit.Gumball.Frame.Plane;
                            if (conduit.UpdateGumball(pointd, unset))
                            {

                                if (point != null)
                                {
                                    if (point.ReferenceID == System.Guid.Empty)
                                    {
                                        point.Transform(Transform.PlaneToPlane(plane0, conduit.Gumball.Frame.Plane));
                                    }
                                    else
                                    {
                                        point.ReferenceID = Rhino.RhinoDoc.ActiveDoc.Objects.Transform(point.ReferenceID, Transform.PlaneToPlane(plane0, conduit.Gumball.Frame.Plane), true);
                                        point.LoadGeometry();
                                    }
                                    this.param.Owner.ExpireSolution(true);
                                }
                                RhinoDoc.ActiveDoc.Views.Redraw();
                                e.Cancel = true;
                            }
                        }
                    }
                }
                catch(System.Exception ex)
                {
                    MessageBox.Show("pattparam onmusemove"+ex.Message);
                }
            }
    
            protected override void OnMouseUp(MouseCallbackEventArgs e)
            {
#if rh6
                if ((this.index >= 0) && (e.MouseButton == MouseButton.Left))
#else
                if ((this.index >= 0) && (e.Button == MouseButtons.Left))
#endif

                {
                    if (this.param != null)
                    {
                        //GumballDisplayConduit conduit = this.param.m_conduits[this.index];
                        //IGH_GeometricGoo point = this.GhPoint();
                        //conduit.Gumball.SetFromPlane(new Plane(point.Boundingbox.Center, Vector3d.XAxis, Vector3d.YAxis));
                        this.param.HIDE_GUM();
                        try
                        {
                            if (this.param.Selected)
                            {
                                this.param.SHOW_GUM();
                            }
                        }
                        catch
                        {

                        }
                        RhinoDoc.ActiveDoc.Views.Redraw();
                    }
                    this.index = -1;
                    e.Cancel = true;
                }
            }
        }
    }
}

