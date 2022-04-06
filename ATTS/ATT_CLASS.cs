using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using ALIEN_DLL.GEOS;
using System.Reflection;
using System.Windows.Forms;
using Grasshopper;
using Rhino.UI.Gumball;
using System.Threading;
using Rhino.Geometry;
using Grasshopper.Kernel.Undo;
using Grasshopper.Kernel.Undo.Actions;
using GH_IO.Serialization;
using Grasshopper.Kernel.Attributes;
using UI.GEOS;
using UI;
namespace UI.ATTS
{
    internal class ATT_CLASS
    {

       internal static bool IS_GEO_PARAM(IGH_Param m_p)
        {
            try
            {
                object pt = Activator.CreateInstance(m_p.Type);
                if (pt is IGH_GeometricGoo)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        internal static void APPEND(IGH_Param m_p)
        {
            try
            {
                UI_SETTING.INS.UI_CLASS.GetMethod("APPEND").MakeGenericMethod(new Type[] { m_p.Type}).Invoke(null,new object[] {m_p });
            }
            catch
            {

            }
        }

        internal static void SET_BAKECOLOR(ToolStripMenuItem menu,Color fc,Color bc)
        {
            foreach (ToolStripItem control in menu.DropDownItems)
            {
                if (control.ForeColor == Color.Yellow || control.ForeColor == SystemColors.ControlText || control.ForeColor == Color.Black)
                    control.ForeColor = fc;
                if (!control.Enabled)
                    control.BackColor = Color.FromArgb(127, bc);
                else
                    control.BackColor = bc;
                if (control is ToolStripMenuItem)
                SET_BAKECOLOR(control as ToolStripMenuItem,fc,bc);
            }
        }
        internal static void APPEND_MENUA(ToolStripDropDown item, IGH_Param p)
        {
            ToolStripMenuItem item0 = GH_DocumentObject.Menu_AppendItem(item, "Append Data", new EventHandler(append(p)), null, true, false);
            item0.ForeColor = Color.Red;
            GH_DocumentObject.Menu_MoveItem(item0, new string[] { "Simplify" });
            ToolStripMenuItem item1 = GH_DocumentObject.Menu_AppendItem(item, "Select Data", new EventHandler(select(p)), null, true,false);
            item1.ForeColor = Color.Red;
            GH_DocumentObject.Menu_MoveItem(item1, new string[] { "Simplify" });
            ToolStripMenuItem item2 = GH_DocumentObject.Menu_AppendItem(item, "Hide Data", new EventHandler(hide(p)), null, true, false);
            item2.ForeColor = Color.Red;
            GH_DocumentObject.Menu_MoveItem(item2, new string[] { "Simplify" });
            ToolStripMenuItem item3 = GH_DocumentObject.Menu_AppendItem(item, "Lock Data", new EventHandler(locks(p)), null, true, false);
            item3.ForeColor = Color.Red;
            GH_DocumentObject.Menu_MoveItem(item3, new string[] { "Simplify" });
            ToolStripSeparator item10 = GH_DocumentObject.Menu_AppendSeparator(item);
            GH_DocumentObject.Menu_MoveItem(item10, new string[] { "Simplify" });
        }
        internal static void ADD_CONMENU<T>(ToolStripDropDown item, T t) where T : I_ATT
        {
            if (t.OWNER is IGH_Param)
            {
                IGH_Param p = t.OWNER as IGH_Param;
                GH_ParamKind kind = p.Kind;
                switch (kind)
                {
                    case GH_ParamKind.floating:
                        ToolStripMenuItem itemd = GH_DocumentObject.Menu_AppendItem(item, "Add Parameter");
                        itemd.ForeColor = Color.Red;
                        itemd.Image = ICONS.P_ADDP;
                        GH_DocumentObject.Menu_AppendItem(itemd.DropDown, "Right", new EventHandler(add_param(t, false)), true, false).Image = ICONS.P_ADDPR;
                        GH_DocumentObject.Menu_AppendItem(itemd.DropDown, "Left", new EventHandler(add_param(t, true)), true, false);
                        GH_DocumentObject.Menu_MoveItem(itemd, new string[] { "Simplify" });
                        break;
                    case GH_ParamKind.input:
                        ToolStripMenuItem iteml = GH_DocumentObject.Menu_AppendItem(item, "Add Parameter", new EventHandler(add_param(t, true)), null, true, false);
                        iteml.ForeColor = Color.Red;
                        GH_DocumentObject.Menu_MoveItem(iteml, new string[] { "Simplify" });
                        break;
                    case GH_ParamKind.output:
                        ToolStripMenuItem itemr = GH_DocumentObject.Menu_AppendItem(item, "Add Parameter", new EventHandler(add_param(t, false)), null, true, false);
                        itemr.ForeColor = Color.Red;
                        GH_DocumentObject.Menu_MoveItem(itemr, new string[] { "Simplify" });
                        break;
                    default:
                        break;
                }
            }
            if (t.OWNER.ToString() == "ScriptComponents.Component_CSNET_Script")
            {
                ToolStripMenuItem item0 = GH_DocumentObject.Menu_AppendItem(item, "Edit with Visual Studio", new EventHandler(p_event(t)));
                item0.ForeColor = Color.Red;
                item0.Image = ICONS.P_VS;
                ToolStripItem tool = GH_DocumentObject.Menu_AppendSeparator(item);
                GH_DocumentObject.Menu_MoveItem(item0, new string[] { "Enabled" });
                GH_DocumentObject.Menu_MoveItem(tool, new string[] { "Enabled" });
            }
            ToolStripMenuItem itema = GH_DocumentObject.Menu_AppendItem(item, "Panda Settings");
            itema.ForeColor = Color.Red;
            //itema.Image = ICON.PANDA;
            if (t.OWNER.Attributes.Parent == null)
            {

                GH_DocumentObject.Menu_MoveItem(itema, new string[] { "Enabled" });

                GH_DocumentObject.Menu_AppendItem(itema.DropDown, "Panda Att", new EventHandler(p_event(t, "ATT")), (bool)UI_SETTING.INS["ATT"], (bool)UI_SETTING.INS["ATT"] ? t.ATT : false);
                GH_DocumentObject.Menu_AppendItem(itema.DropDown, "Panda Tag", new EventHandler(p_event(t, "TAG")), (bool)UI_SETTING.INS["TAG"], (bool)UI_SETTING.INS["TAG"] ? t.TAG : false);
                if ((t.OWNER is IGH_Param) && (((IGH_Param)t.OWNER).SourceCount == 0))
                    GH_DocumentObject.Menu_AppendItem(itema.DropDown, "Panda Gumball", new EventHandler(p_event(t, "GUM")), (bool)UI_SETTING.INS["GUM"], (bool)UI_SETTING.INS["GUM"] ? (t as I_PATT).GUM : false);
            }
            else
                GH_DocumentObject.Menu_MoveItem(itema, new string[] { "Simplify" });
            GH_DocumentObject.Menu_AppendItem(itema.DropDown, "Panda Menu", new EventHandler(p_event(t, "MENU")), true, t.MENU || (bool)UI_SETTING.INS["MENU"]);

            //ToolStripMenuItem itemd = GH_DocumentObject.Menu_AppendItem(itema.DropDown, "Add Parameter");
            //GH_DocumentObject.Menu_AppendItem(itemd.DropDown, "Right", new EventHandler(ADD_PARAM(t, false)), true, false);
            //GH_DocumentObject.Menu_AppendItem(itemd.DropDown, "Left", new EventHandler(ADD_PARAM(t, true)), true, false);
            // GH_DocumentObject.Menu_MoveItem(itemd, new string[] { "Simplify" });
            // bool lic = P_LICENSE.LICENSE.LICENSED == P_LICENSE.LICENSE_TYPE.NORMAL;
            if (t.OWNER.Attributes.Parent != null)
                return;
            GH_DocumentObject.Menu_AppendSeparator(item);

            GH_DocumentObject.Menu_AppendItem(item, "Example", new EventHandler(OPEN_P(t, null)), null, true, false).ForeColor = Color.Red;
            GH_DocumentObject.Menu_AppendItem(item, "Video", new EventHandler(OPEN_P(t, null)), null, true, false);
        }
        internal static void SHOW_GUMBALLS(IGH_Param p)
        {
            try
            {
                if (System.Activator.CreateInstance(p.Type) is IGH_GeometricGoo)
                {
                    ATT_PARAM patt = (ATT_PARAM)(p.Attributes);
                    if (patt == null)
                        return;
                    patt.HIDE_GUM();
                    if ((!((IGH_PreviewObject)p).Hidden) && !p.Locked)
                    {
                        List<IGH_GeometricGoo> datas =(List<IGH_GeometricGoo>) UI_SETTING.INS.UI_CLASS.GetMethod("GET_DATAS").MakeGenericMethod(new Type[] { p.Type }).Invoke(null, new object[] { p });

                        if (((p != null) && (p.SourceCount <= 0)) && !p.VolatileData.IsEmpty)
                        {
                            foreach (IGH_GeometricGoo point2 in datas)
                            {
                                if (!point2.IsGeometryLoaded)
                                    point2.LoadGeometry();
                                GumballObject gumball = new GumballObject();
                                gumball.SetFromPlane(new Plane(point2.Boundingbox.Center, Vector3d.XAxis, Vector3d.YAxis));
                                GumballAppearanceSettings appearanceSettings = new GumballAppearanceSettings
                                {
                                    MenuEnabled =true,
                                    //RotateXEnabled = false,
                                    //RotateYEnabled = false,
                                    //RotateZEnabled = false,
                                    ScaleXEnabled = true,
                                    ScaleYEnabled = true,
                                    ScaleZEnabled = true,
                                    //TranslateXYEnabled = false,
                                    //TranslateYZEnabled = false,
                                    //TranslateZXEnabled = false,
                                    RelocateEnabled = true,
                                    Radius = 200
                                };
                                GumballDisplayConduit item = new GumballDisplayConduit();
                                item.SetBaseGumball(gumball, appearanceSettings);
                                item.Enabled = true;
                                patt.add_items(gumball, item, point2);
                            }
                        }
                        patt.m_handler = new ATT_PARAM.MouseHandler(patt);
                        patt.m_handler.Enabled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private static Action<object ,EventArgs> append(IGH_Param p)
        {
            return delegate
            {
                
                p.RecordUndoEvent("Append");
                APPEND(p);
            };
        }
        private static Action<object, EventArgs> select(IGH_Param p)
        {
            return delegate
            {
                p.RecordUndoEvent("Select");
                List<Guid> ss = slected_guids(p);
                int num = ss.Count;
                if (num > 0)
                {
                    Rhino.RhinoDoc.ActiveDoc.Objects.Select(ss);
                    Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.Redraw();
                }
            };
        }
        private static Action<object, EventArgs> hide(IGH_Param p)
        {
            return delegate
            {
                p.RecordUndoEvent("Hide");
                try
                {
                    int num = p.VolatileDataCount;
                    for (int i = 0; i < num; i++)
                    {
                        IGH_GeometricGoo t = p.VolatileData.AllData(false).ElementAt(i) as IGH_GeometricGoo;
                        if (t != null)
                        {
                            if (t.IsReferencedGeometry)
                            {
                                if(Rhino.RhinoDoc.ActiveDoc.Objects.Find(t.ReferenceID).IsHidden)
                                Rhino.RhinoDoc.ActiveDoc.Objects.Show(t.ReferenceID,true);
                                else
                                    Rhino.RhinoDoc.ActiveDoc.Objects.Hide(t.ReferenceID, true);
                            }
                        }
                    }
                    Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.Redraw();
                }
                catch
                {

                }              
            };
        }
        private static Action<object, EventArgs> locks(IGH_Param p)
        {
            return delegate
            {
                p.RecordUndoEvent("Lock");
                try
                {
                    int num = p.VolatileDataCount;
                    for (int i = 0; i < num; i++)
                    {
                        IGH_GeometricGoo t = p.VolatileData.AllData(false).ElementAt(i) as IGH_GeometricGoo;
                        if (t != null)
                        {
                            if (t.IsReferencedGeometry)
                            {
                                if (Rhino.RhinoDoc.ActiveDoc.Objects.Find(t.ReferenceID).IsLocked)
                                    Rhino.RhinoDoc.ActiveDoc.Objects.Unlock(t.ReferenceID, true);
                                else
                                    Rhino.RhinoDoc.ActiveDoc.Objects.Lock(t.ReferenceID, true);
                            }
                        }
                    }
                    Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.Redraw();
                }
                catch
                {

                }
            };
        }
        private static Action<object, EventArgs> OPEN_P<T>(T t,byte[] arr) where T : I_ATT
        {
            return delegate
            {
                //IGH_Component doce = CONTROL_IO.VALUE.A_CLUSTER(arr) as IGH_Component;
                //GH_Document ghdoc = new GH_Document();
                //ghdoc.AddObject(doce, false, 0);
                //doce.Params.Input[0].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, t.OWNER.ComponentGuid);
                //doce.CollectData();
                //doce.ComputeData();
                //Thread tt = new Thread(new ThreadStart(doce.ComputeData));
                //tt.Start();
            };
        }
        private static Action<object, EventArgs> add_param<T>(T t,bool left) where T : I_ATT
        {
            return delegate
            {
                try
                {
                    GH_UndoRecord undoRecord = new GH_UndoRecord("Insert descriptions right");
                    AddParam((IGH_Param)(t.OWNER), left, undoRecord);
                    if (undoRecord.ActionCount > 0)
                    {
                        t.OWNER.OnPingDocument().UndoUtil.RecordEvent(undoRecord);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("ADD_PARAM" + ex.Message);
                }
            };
        }
        private static Action<object, EventArgs> p_event<T>(T t, string p) where T : I_ATT
        {
            return delegate
            {
                PropertyInfo pi = t.GetType().GetProperty(p);
                bool b = !((bool)pi.GetValue(t));
                pi.SetValue(t,b);
                P_OBJECT<bool, object>.DOC_SETVALUE("SetValue", t.OWNER, p, b);
            };
        }
        private static Action<object, EventArgs> p_event<T>(T t) where T : I_ATT
        {
            return delegate
            {
                try
                {


                    P_CS _CS = new P_CS(t.OWNER);
                    Thread thread = new Thread(new ThreadStart(_CS.START));
                    thread.Start();
                   
                }
                catch
                {
                   
                }
            };
        }
        private static void AddParam(IGH_Param sourceParam, bool left, GH_UndoRecord undoRecord)
        {
            PointF pivot = sourceParam.Attributes.Pivot;
            float x = 0f;
            if (left)
            {
                x = sourceParam.Attributes.Bounds.Left - 120;
            }
            else
            {
                x = sourceParam.Attributes.Bounds.Right + 120;
            }
            //GH_Archive gH_Archive = new GH_Archive();
            //if (!gH_Archive.AppendObject(sourceParam, "Parameter"))
            //{
            //   // Tracing.Assert(new Guid("{96ACE3FC-F716-4b2e-B226-9E2D1F9DA229}"), "Parameter serialization failed.");
            //}
            //else
            //{
            IGH_DocumentObject iGH_DocumentObject = Instances.ComponentServer.EmitObject(sourceParam.ComponentGuid);
            if (iGH_DocumentObject != null)
            {
                IGH_Param iGH_Param = (IGH_Param)iGH_DocumentObject;
                //if (!gH_Archive.ExtractObject(iGH_Param, "Parameter"))
                //{
                //   // Tracing.Assert(new Guid("{2EA6E057-E390-4fc5-B9AB-1B74A8A17625}"), "Parameter deserialization failed.");
                //}
                //else
                //{
                iGH_Param.Name = sourceParam.Name;
                iGH_Param.NickName = sourceParam.NickName;
                iGH_Param.Description = sourceParam.Description;
                iGH_Param.DataMapping = sourceParam.DataMapping;
                iGH_Param.Access = sourceParam.Access;
                GH_Document gH_Document = sourceParam.OnPingDocument();
                iGH_Param.NewInstanceGuid();
                iGH_Param.CreateAttributes();
                iGH_Param.Attributes.Selected = false;
                iGH_Param.Attributes.Pivot = new PointF(x, pivot.Y);
                gH_Document.DestroyAttributeCache();
                iGH_Param.Attributes.ExpireLayout();
                iGH_Param.Attributes.PerformLayout();

                if (gH_Document == null)
                {
                    // Tracing.Assert(new Guid("{D74F80C4-CA72-4dbd-8597-450D27098F55}"), "Document could not be located.");
                }
                else
                {
                    if (undoRecord != null)
                    {
                        GH_AddObjectAction action = new GH_AddObjectAction(iGH_Param);
                        undoRecord.AddAction(action);
                    }
                    gH_Document.AddObject(iGH_Param, false, Instances.ActiveCanvas.Document.ObjectCount + 1);
                    if (left)
                    {

                        foreach (IGH_Param pi in sourceParam.Sources)
                            iGH_Param.AddSource(pi);
                        AddSource(sourceParam, iGH_Param);
                    }
                    else
                    {
                        //MessageBox.Show(sourceParam.Recipients.Count.ToString());

                        while (sourceParam.Recipients.Count > 0)
                        {
                            //MessageBox.Show(sourceParam.Recipients.Count.ToString());
                            IGH_Param p = sourceParam.Recipients[0];
                            p.AddSource(iGH_Param);
                            p.RemoveSource(sourceParam);
                        }
                        AddSource(iGH_Param, sourceParam);

                    }
                    //ExpireSolution(true);
                }
                //}
            }
            //}        
        }
        private static void AddSource(IGH_Param param, IGH_Param source)
        {
            param.RemoveAllSources();
            param.AddSource(source);
        }
        private static List<T> CopyList<T>(List<T> sourceList)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < sourceList.Count; i++)
            {
                list.Add(sourceList[i]);
            }
            return list;
        }
        private static List<Guid> slected_guids(IGH_Param p)
        {
            List<Guid> guids = new List<Guid>();
            try
            {
                int num = p.VolatileDataCount;
                for (int i = 0; i < num; i++)
                {
                    IGH_GeometricGoo t = p.VolatileData.AllData(false).ElementAt(i) as IGH_GeometricGoo;
                    if (t != null)
                    {
                        if (t.IsReferencedGeometry)
                        {
                            guids.Add(t.ReferenceID);
                        }
                    }
                }
            }
            finally
            {
            }
            return guids;
        }
     
    }
}
