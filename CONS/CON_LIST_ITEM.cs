using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GH_IO;
using GH_IO.Serialization;
using Grasshopper.Kernel.Types;
using ALIEN_DLL.GEOS;

namespace UI.CONS
{
    public class CON_LIST_ITEM<T> where T : class, GH_ISerializable
    {
        public T ITEM
        {
            get;
            set;
        }
        public  GH_String NAME
        {
            get;
            set;
        }
        public  Font FONT
        {
            get;
            set;
        }
       public  GH_Colour COLOR
        {
            get;
            set;
        }
        public  GH_Boolean SELECTED
        {
            get;
            set;
        }
        public CON_LIST_ITEM()
        {
        }
        public bool Write(GH_IWriter writer)
        {
            if (ITEM != null)
            {
                GH_IWriter writer0 = writer.CreateChunk("item");
                ITEM.Write(writer0);
            }

            if (NAME != null)
            {
                GH_IWriter writer0 = writer.CreateChunk("name");
                NAME.Write(writer0);
            }

            if (FONT != null)
            {
                GH_IWriter writer0 = writer.CreateChunk("font");
                //FONT.Write(writer0);
            }

            if (COLOR != null)
            {
                GH_IWriter writer0 = writer.CreateChunk("color");
                COLOR.Write(writer0);
            }

            if (SELECTED != null)
            {
                GH_IWriter writer0 = writer.CreateChunk("selected");
                SELECTED.Write(writer0);
            }

            return true;
        }
        public bool Read(GH_IReader reader)
        {
            try
            {
                GH_IReader reader0 = reader.FindChunk("item");
                if (reader0 != null)
                {
                    ITEM = default(T);
                    ITEM.Read(reader0);
                }
                GH_IReader reader1 = reader.FindChunk("name");
                if (reader1 != null)
                {
                    NAME = new GH_String();
                    NAME.Read(reader);
                }
                GH_IReader reader2 = reader.FindChunk("font");
                if (reader2 != null)
                {
                    //FONT = new PG_FONT();
                    //FONT.Read(reader);
                }
                GH_IReader reader3 = reader.FindChunk("color");
                if (reader3 != null)
                {
                    COLOR = new GH_Colour();
                    COLOR.Read(reader);
                }
                GH_IReader reader4 = reader.FindChunk("selected");
                if (reader4 != null)
                {
                    SELECTED = new GH_Boolean();
                    SELECTED.Read(reader);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
