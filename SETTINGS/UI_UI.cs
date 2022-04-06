using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using UI.ATTS;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel.Special;
using System.Windows.Forms;
namespace UI.SETTINGS
{
   internal class UI_UI:UI_SETTING_DROP
    {
        internal override string NAME => "PANDA_UI";
        private bool m_ui => (bool)m_value;
        protected override void value_changed(object value)
        {
            base.value_changed(value);
            MENU.Checked = m_ui;
            if (Instances.ActiveCanvas != null)
            {
               
                Instances.ActiveCanvas.DocumentChanged -= new GH_Canvas.DocumentChangedEventHandler(uiDocumentAddedEventHandler);
                Instances.ActiveCanvas.Document_ObjectsAdded -= new GH_Canvas.Document_ObjectsAddedEventHandler(uiObjectsAddedEventHandler);
                if (m_ui)
                {
                    Instances.ActiveCanvas.DocumentChanged += new GH_Canvas.DocumentChangedEventHandler(uiDocumentAddedEventHandler);
                    Instances.ActiveCanvas.Document_ObjectsAdded += new GH_Canvas.Document_ObjectsAddedEventHandler(uiObjectsAddedEventHandler);
                }
                if (Instances.ActiveCanvas.Document != null)
                    change(new Action<IGH_ActiveObject, bool>(uiChange), m_ui, Instances.ActiveCanvas.Document.Objects.ToList());
            }
        }
        protected override void click_changed(object sender, EventArgs e)
        {
            base.click_changed(sender, e);
            VALUE = MENU.Checked;
        }
        protected override List<IUI_SETTING_ITEM> GROUPS
        {
            get
            {
                if (m_groups == null)
                {
                    m_groups = new List<IUI_SETTING_ITEM>()
                    {
                        new UI_ATT()
                    };
                }
                return m_groups;
            }
        }
        internal UI_UI():base()
        {     
        }
        private void uiChange(IGH_ActiveObject com, bool va)
        {
            try
            {
                IGH_Attributes attributes = com.Attributes;
                if (!va)
                {
                    if (com is IGH_Component)
                        ((IGH_Component)com).AttributesChanged -= ParameterChanged_ui;
                    com.CreateAttributes();
                }
                else
                {
                    if (com.Attributes is I_ATT)
                    {

                        return;
                    }
                    string str = com.Attributes.GetType().ToString();
                    switch (str)
                    {
                        case "Grasshopper.Kernel.Attributes.GH_ComponentAttributes":
                            com.Attributes = new ATT_NORMAL(com as IGH_Component);
                            break;
                        case "AmebaCore.Attribute.Ameba_ComponentAttributes":
                            com.Attributes = new ATT_NORMAL(com as IGH_Component);
                            break;
                        case "ScriptComponents.GenericScriptAttributes":
                            com.Attributes = new ATT_SCRIPT(com as IGH_Component);
                            break;
                        case "Grasshopper.Kernel.Attributes.GH_FloatingParamAttributes":
                            com.Attributes = new ATT_PARAMN(com as IGH_Param);
                            break;
                        case "Grasshopper.Kernel.Parameters.Param_PointAttributes":
                            com.Attributes = new ATT_PARAMN(com as IGH_Param);
                            break;
                        case "Grasshopper.Kernel.Special.GH_ClusterAttributes":
                            com.Attributes = new ATT_CLUSTER(com as GH_Cluster);
                            break;
                        case "Grasshopper.Kernel.Special.GH_GraphMapperAttributes":
                            com.Attributes = new ATT_GRAPH(com as GH_GraphMapper);
                            break;
                            //case "Grasshopper.Kernel.Special.GH_PanelAttributes":
                            //    com.Attributes = new ATT_PANEL(com as GH_Panel);
                            //    break;
                            //case "Grasshopper.Kernel.Special.GH_NumberSliderAttributes":
                            //    com.Attributes = new ATT_SLIDER(com as GH_NumberSlider);
                            //    break;
                            //case "PANDA.ATTS.P_ATT_THREAD":
                            //    com.Attributes = new ATT_THREAD(com as PANDA.COMS.SET_THREADC);
                            //    break;
                    }
                }
                com.Attributes.Pivot = attributes.Pivot;
                com.Attributes.Bounds = attributes.Bounds;
                com.Attributes.Selected = attributes.Selected;
                Instances.ActiveCanvas.Document.DestroyAttributeCache();
                com.Attributes.ExpireLayout();
                com.Attributes.PerformLayout();
                if (com is IGH_Component)
                {
                    ((IGH_Component)com).Params.RepairParamAssociations();
                    if (va)
                    {
                        foreach (IGH_Param param in ((IGH_Component)com).Params)
                        {

                            GH_LinkedParamAttributes attributes2 = param.Attributes as GH_LinkedParamAttributes;
                            ATT_PARAML p_att_paraml1 = new ATT_PARAML(param, com.Attributes)
                            {
                                Pivot = attributes2.Pivot,
                                Bounds = attributes2.Bounds,
                                Selected = attributes2.Selected
                            };
                            param.Attributes = p_att_paraml1;
                        }
                        ((IGH_Component)com).AttributesChanged += ParameterChanged_ui;
                    }
                }
            }
            catch
            {
            }
        }
        private void ParameterChanged_ui(IGH_DocumentObject com, GH_AttributesChangedEventArgs e)
        {

            try
            {
                ((IGH_Component)com).Params.RepairParamAssociations();
                if (m_ui)
                {
                    foreach (IGH_Param param in ((IGH_Component)com).Params)
                    {
                        if (param.Attributes is I_PATT)
                            continue;
                        GH_LinkedParamAttributes attributes2 = param.Attributes as GH_LinkedParamAttributes;
                        ATT_PARAML p_att_paraml1 = new ATT_PARAML(param, com.Attributes)
                        {
                            Pivot = attributes2.Pivot,
                            Bounds = attributes2.Bounds,
                            Selected = attributes2.Selected
                        };
                        param.Attributes = p_att_paraml1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ParameterChanged_ui error:" + ex.Message);
            }
        }
        private void uiObjectsAddedEventHandler(object sender, GH_DocObjectEventArgs e)
        {
            change(new Action<IGH_ActiveObject, bool>(uiChange), m_ui, e.Objects.ToList());
        }
        private void uiDocumentAddedEventHandler(GH_Canvas sender, GH_CanvasDocumentChangedEventArgs e)
        {
            GH_Canvas c = sender as GH_Canvas;
            c.Document.Enabled = false;
            change(new Action<IGH_ActiveObject, bool>(uiChange), m_ui, e.NewDocument.Objects.ToList());
            c.Document.Enabled = true;
        }
    }
}
