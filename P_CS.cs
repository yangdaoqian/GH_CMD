using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grasshopper;
using Grasshopper.GUI.Script;
using System.Reflection;
using System.Runtime;
using System.Collections;

using System.Runtime.CompilerServices;
using Rhino.Geometry;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

using Grasshopper.Kernel.Geometry;
using Rhino;
using Rhino.Collections;
using Rhino.Geometry.Collections;
using System.Drawing;

using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace ALIEN_DLL.GEOS
{
  

    /// <summary>
    /// This class will be instantiated on demand by the Script component.
    /// </summary>
    public class Script_Instance : GH_ScriptInstance
    {
        #region Utility functions
        /// <summary>Print a String to the [Out] Parameter of the Script component.</summary>
        /// <param name="text">String to print.</param>
        private void Print(string text) { __out.Add(text); }
        /// <summary>Print a formatted String to the [Out] Parameter of the Script component.</summary>
        /// <param name="format">String format.</param>
        /// <param name="args">Formatting parameters.</param>
        private void Print(string format, params object[] args) { __out.Add(string.Format(format, args)); }
        /// <summary>Print useful information about an object instance to the [Out] Parameter of the Script component. </summary>
        /// <param name="obj">Object instance to parse.</param>
        private void Reflect(object obj) { __out.Add(GH_ScriptComponentUtilities.ReflectType_CS(obj)); }
        /// <summary>Print the signatures of all the overloads of a specific method to the [Out] Parameter of the Script component. </summary>
        /// <param name="obj">Object instance to parse.</param>
        private void Reflect(object obj, string method_name) { __out.Add(GH_ScriptComponentUtilities.ReflectType_CS(obj, method_name)); }
        #endregion

        #region Members
        /// <summary>Gets the current Rhino document.</summary>
        private RhinoDoc RhinoDocument;
        /// <summary>Gets the Grasshopper document that owns this script.</summary>
        private GH_Document GrasshopperDocument;
        /// <summary>Gets the Grasshopper script component that owns this script.</summary>
        private IGH_Component Component;
        /// <summary>
        /// Gets the current iteration count. The first call to RunScript() is associated with Iteration==0.
        /// Any subsequent call within the same solution will increment the Iteration count.
        /// </summary>
        private int Iteration;
        #endregion

        /// <summary>
        /// This procedure contains the user code. Input parameters are provided as regular arguments, 
        /// Output parameters as ref arguments. You don't have to assign output parameters, 
        /// they will have a default value.
        /// </summary>
        private void RunScript(object x, object y, ref object A)
        {

            Type type = Component.GetType();
            PropertyInfo property = type.GetProperty("ScriptSource");

            MethodInfo method = type.GetMethod("CreateSourceForCompile", BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo method0 = type.GetMethod("ClearTemplateTags", BindingFlags.Instance | BindingFlags.NonPublic);
            A = method0.Invoke(Component, new object[] { method.Invoke(Component, new object[1] { property.GetValue(Component) }) });
            //A = property.GetValue(Component);

        }

        // <Custom additional code> 

        // </Custom additional code> 

        private List<string> __err = new List<string>(); //Do not modify this list directly.
        private List<string> __out = new List<string>(); //Do not modify this list directly.
        private RhinoDoc doc = RhinoDoc.ActiveDoc;       //Legacy field.
        private IGH_ActiveObject owner;                  //Legacy field.
        private int runCount;                            //Legacy field.

        public override void InvokeRunScript(IGH_Component owner, object rhinoDocument, int iteration, List<object> inputs, IGH_DataAccess DA)
        {
            //Prepare for a new run...
            //1. Reset lists
            this.__out.Clear();
            this.__err.Clear();

            this.Component = owner;
            this.Iteration = iteration;
            this.GrasshopperDocument = owner.OnPingDocument();
            this.RhinoDocument = rhinoDocument as Rhino.RhinoDoc;

            this.owner = this.Component;
            this.runCount = this.Iteration;
            this.doc = this.RhinoDocument;

            //2. Assign input parameters
            object x = default(object);
            if (inputs[0] != null)
            {
                x = (object)(inputs[0]);
            }

            object y = default(object);
            if (inputs[1] != null)
            {
                y = (object)(inputs[1]);
            }



            //3. Declare output parameters
            object A = null;


            //4. Invoke RunScript
            RunScript(x, y, ref A);

            try
            {
                //5. Assign output parameters to component...
                if (A != null)
                {
                    if (GH_Format.TreatAsCollection(A))
                    {
                        IEnumerable __enum_A = (IEnumerable)(A);
                        DA.SetDataList(1, __enum_A);
                    }
                    else
                    {
                        if (A is Grasshopper.Kernel.Data.IGH_DataTree)
                        {
                            //merge tree
                            DA.SetDataTree(1, (Grasshopper.Kernel.Data.IGH_DataTree)(A));
                        }
                        else
                        {
                            //assign direct
                            DA.SetData(1, A);
                        }
                    }
                }
                else
                {
                    DA.SetData(1, null);
                }

            }
            catch (Exception ex)
            {
                this.__err.Add(string.Format("Script exception: {0}", ex.Message));
            }
            finally
            {
                //Add errors and messages... 
                if (owner.Params.Output.Count > 0)
                {
                    if (owner.Params.Output[0] is Grasshopper.Kernel.Parameters.Param_String)
                    {
                        List<string> __errors_plus_messages = new List<string>();
                        if (this.__err != null) { __errors_plus_messages.AddRange(this.__err); }
                        if (this.__out != null) { __errors_plus_messages.AddRange(this.__out); }
                        if (__errors_plus_messages.Count > 0)
                            DA.SetDataList(0, __errors_plus_messages);
                    }
                }
            }
        }
    }

    internal class P_CS
    {
        private object Component;
        private string m_path;
        private object ScriptSource;
        private string m_project;
        private string m_solution;
        private string m_pro;
        private string m_id;
        private  void WatcherStrat(string path, string filter)
        {

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.Filter = filter;
            watcher.Changed += new FileSystemEventHandler(OnProcess);
            watcher.Created += new FileSystemEventHandler(OnProcess);
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
              | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            watcher.IncludeSubdirectories = true;
        }

        private void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                OnChanged(source, e);
            }
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                OnChanged(source, e);
            }
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            set_script();
        }
        internal P_CS(object  component)
        {
            Component = component;
            get_path();
            FileInfo info = new FileInfo(m_path);         
            get_script();
            WatcherStrat(info.Directory.FullName, "*.cs");
            //DirectoryInfo directory = new DirectoryInfo();
            //directory.GetFiles()
        }
        private void get_path()
        {
            string df = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Type type = Component.GetType();
            m_id= type.GetProperty("InstanceGuid").GetValue(Component).ToString().Replace("-","_");
            string cp = df + @"\Panda\PANDA_CS\PANDA_CS\COMS";
            if (!Directory.Exists(cp))
                Directory.CreateDirectory(cp);
            m_path = df +@"\Panda\PANDA_CS\PANDA_CS\COMS\_"+ m_id+".cs";
            m_project = df + @"\Panda\PANDA_CS\PANDA_CS\PANDA_CS.csproj";
            m_solution = df + @"\Panda\PANDA_CS\PANDA_CS.sln";
            m_pro=df + @"\Panda\PANDA_CS\.vs\PANDA_CS\v15\Server";
        }
        private void get_script()
        {
            Type type = Component.GetType();
            PropertyInfo property = type.GetProperty("ScriptSource");
            ScriptSource = property.GetValue(Component);
            MethodInfo method = type.GetMethod("CreateSourceForCompile", BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo method0 = type.GetMethod("ClearTemplateTags", BindingFlags.Instance | BindingFlags.NonPublic);
            Type type0 = ScriptSource.GetType();
            string usingco = (string)(type0.GetProperty("UsingCode").GetValue(ScriptSource));
            string usingc = "#region CustomUsing" + "\r\n" + "// <Custom using>" + "\r\n" + "\r\n" + "\r\n" + usingco + "\r\n" + "// </Custom using>" + "\r\n" + "#endregion" + "\r\n";
            type0.GetProperty("UsingCode").SetValue(ScriptSource, usingc);
            string codeo = (string)(type0.GetProperty("ScriptCode").GetValue(ScriptSource));
            string code = "#region CustomCode" + "\r\n" + "// <Custom code>" + "\r\n" + "\r\n" + "\r\n" + codeo+ "\r\n" + "// </Custom code>" + "\r\n" + "#endregion" + "\r\n";
            type0.GetProperty("ScriptCode").SetValue(ScriptSource, code);
            string source= (string) method0.Invoke(Component, new object[] { method.Invoke(Component, new object[1] { ScriptSource}) });
            string r1 = "public class Script_Instance : GH_ScriptInstance";
            string guid = m_id.Replace("-","_");
            string rr1= "public class Script_Instance_" +guid+ " : GH_ScriptInstance";
            source = source.Replace(r1, rr1);
            type0.GetProperty("UsingCode").SetValue(ScriptSource, usingco);
            type0.GetProperty("ScriptCode").SetValue(ScriptSource, codeo);
            StreamWriter writer1 = new StreamWriter(m_path);
            writer1.Write(source);
            writer1.Close();
            add_com();       
        }
        private void add_using()
        {
            Type type = ScriptSource.GetType();
            type.GetField("References").GetValue(ScriptSource);
        }
        private void add_com()
        {
            string txt = "";
            using (StreamReader reader = new StreamReader(m_project))
            {
                txt = reader.ReadToEnd();
            }
            if (!txt.Contains("<Compile Include=\"COMS\\_" + m_id + ".cs\"/>"))
            {
                txt = txt.Replace("<!--COMS-->", "<!--COMS-->" + "\r\n" + "<Compile Include=\"COMS\\_" + m_id + ".cs\"/>");
                StreamWriter writer1 = new StreamWriter(m_project);
                writer1.Write(txt);
                writer1.Close();
            }
        }

        private void add_assembly()
        {

        }
        internal void START()
        {
            try
            {
                if(Directory.Exists(m_pro))
                Directory.Delete(m_pro,true);
            }
            catch
            {
                MessageBox.Show("Code named _"+ m_id+".cs has been successfully added to the PANDA_CS project!");
                return;
            }
            ProcessStartInfo startInfo = new ProcessStartInfo(m_solution)
            {

            };
            Process process = Process.Start(startInfo);
            // process.Exited += EventHandler;
        }
        private void set_script()
        {
            try { 
            string txt = "";
            using (StreamReader reader = new StreamReader(m_path))
            {
                txt = reader.ReadToEnd();
            }
            string[] strArray2 = txt.Split(new List<string> {
                        "// <Custom using>",
                        "// </Custom using>",
                        "// <Custom code>",
                        "// </Custom code>",
                        "// <Custom additional code>",
                        "// </Custom additional code>"
                    }.ToArray(), StringSplitOptions.None);

            string str1 = strArray2[1];
            string str2= strArray2[3];
            string str3 = strArray2[5];
            Type type = ScriptSource.GetType();
            type.GetProperty("UsingCode").SetValue(ScriptSource, str1);
            type.GetProperty("ScriptCode").SetValue(ScriptSource,str2);
            type.GetProperty("AdditionalCode").SetValue(ScriptSource, str3);
            Type type0 = Component.GetType();

            type0.GetMethod("ExpireSolution").Invoke(Component,new object[1]{ true});              

             }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
