namespace UI.CONS
{
    using Grasshopper.GUI.SettingsControls;
    using Grasshopper.GUI;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class SETTING_MAIN : IGH_SettingFrontend
    {
        public Control SettingsUI() =>
            new UI_PSETTING();

        public string Category =>
            "Panda_UI";

        public IEnumerable<string> Keywords =>
            new string[] { "Widgets", "Panda Main", "Main" };

        public string Name =>
            "Panda Main";

    }
    public class MY_SETTING : GH_SettingsCategory
    {
        public override string Description => "Master settings of Panda_UI";

        public MY_SETTING()
            : base("Panda_UI",ICONS.ff)
        {
        }
    }
}

