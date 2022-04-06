using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.CONS
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
    public class CON_PASSWORD : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;

        [System.Runtime.CompilerServices.AccessedThroughProperty("LayoutPanel")]
        private System.Windows.Forms.TableLayoutPanel _LayoutPanel;

        [System.Runtime.CompilerServices.AccessedThroughProperty("ButtonOK")]
        private System.Windows.Forms.Button _ButtonOK;

        [System.Runtime.CompilerServices.AccessedThroughProperty("ButtonCancel")]
        private System.Windows.Forms.Button _ButtonCancel;

        [System.Runtime.CompilerServices.AccessedThroughProperty("ToolTip")]
        private System.Windows.Forms.ToolTip _ToolTip;

        [System.Runtime.CompilerServices.AccessedThroughProperty("OldPasswordGroup")]
        private System.Windows.Forms.GroupBox _OldPasswordGroup;

        [System.Runtime.CompilerServices.AccessedThroughProperty("NewPasswordGroup")]
        private System.Windows.Forms.GroupBox _NewPasswordGroup;

        [System.Runtime.CompilerServices.AccessedThroughProperty("OldLayout")]
        private System.Windows.Forms.TableLayoutPanel _OldLayout;

        [System.Runtime.CompilerServices.AccessedThroughProperty("OldLabel")]
        private System.Windows.Forms.Label _OldLabel;

        [System.Runtime.CompilerServices.AccessedThroughProperty("OldPassword")]
        private Grasshopper.GUI.GH_PasswordBox _OldPassword;

        [System.Runtime.CompilerServices.AccessedThroughProperty("TableLayoutPanel1")]
        private System.Windows.Forms.TableLayoutPanel _TableLayoutPanel1;

        [System.Runtime.CompilerServices.AccessedThroughProperty("NewPasswordLabel")]
        private System.Windows.Forms.Label _NewPasswordLabel;

        [System.Runtime.CompilerServices.AccessedThroughProperty("NewPassword")]
        private Grasshopper.GUI.GH_PasswordBox _NewPassword;

        internal virtual System.Windows.Forms.TableLayoutPanel LayoutPanel
        {
            get
            {
                return this._LayoutPanel;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._LayoutPanel = value;
            }
        }

        internal virtual System.Windows.Forms.Button ButtonOK
        {
            get
            {
                return this._ButtonOK;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._ButtonOK = value;
            }
        }

        internal virtual System.Windows.Forms.Button ButtonCancel
        {
            get
            {
                return this._ButtonCancel;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._ButtonCancel = value;
            }
        }

        internal virtual System.Windows.Forms.ToolTip ToolTip
        {
            get
            {
                return this._ToolTip;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._ToolTip = value;
            }
        }

        internal virtual System.Windows.Forms.GroupBox OldPasswordGroup
        {
            get
            {
                return this._OldPasswordGroup;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._OldPasswordGroup = value;
            }
        }

        internal virtual System.Windows.Forms.GroupBox NewPasswordGroup
        {
            get
            {
                return this._NewPasswordGroup;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._NewPasswordGroup = value;
            }
        }

        internal virtual System.Windows.Forms.TableLayoutPanel OldLayout
        {
            get
            {
                return this._OldLayout;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._OldLayout = value;
            }
        }

        internal virtual System.Windows.Forms.Label OldLabel
        {
            get
            {
                return this._OldLabel;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._OldLabel = value;
            }
        }

        internal virtual Grasshopper.GUI.GH_PasswordBox OldPassword
        {
            get
            {
                return this._OldPassword;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._OldPassword = value;
            }
        }

        internal virtual System.Windows.Forms.TableLayoutPanel TableLayoutPanel1
        {
            get
            {
                return this._TableLayoutPanel1;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._TableLayoutPanel1 = value;
            }
        }

        internal virtual System.Windows.Forms.Label NewPasswordLabel
        {
            get
            {
                return this._NewPasswordLabel;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._NewPasswordLabel = value;
            }
        }

        internal virtual Grasshopper.GUI.GH_PasswordBox NewPassword
        {
            get
            {
                return this._NewPassword;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            set
            {
                this._NewPassword = value;
            }
        }

        public CON_PASSWORD()
        {
            base.Load += new System.EventHandler(this.GH_ClusterPasswordWindow_Load);
            this.InitializeComponent();
        }

        [System.Diagnostics.DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.components != null)
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._NewPasswordGroup = new System.Windows.Forms.GroupBox();
            this._TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._NewPasswordLabel = new System.Windows.Forms.Label();
            this._NewPassword = new Grasshopper.GUI.GH_PasswordBox();
            this._ButtonCancel = new System.Windows.Forms.Button();
            this._ButtonOK = new System.Windows.Forms.Button();
            this._OldPasswordGroup = new System.Windows.Forms.GroupBox();
            this._OldLayout = new System.Windows.Forms.TableLayoutPanel();
            this._OldLabel = new System.Windows.Forms.Label();
            this._OldPassword = new Grasshopper.GUI.GH_PasswordBox();
            this._ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this._LayoutPanel.SuspendLayout();
            this._NewPasswordGroup.SuspendLayout();
            this._TableLayoutPanel1.SuspendLayout();
            this._OldPasswordGroup.SuspendLayout();
            this._OldLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // _LayoutPanel
            // 
            this._LayoutPanel.ColumnCount = 2;
            this._LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._LayoutPanel.Controls.Add(this._NewPasswordGroup, 0, 2);
            this._LayoutPanel.Controls.Add(this._ButtonCancel, 1, 4);
            this._LayoutPanel.Controls.Add(this._ButtonOK, 0, 4);
            this._LayoutPanel.Controls.Add(this._OldPasswordGroup, 0, 0);
            this._LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._LayoutPanel.Name = "_LayoutPanel";
            this._LayoutPanel.RowCount = 5;
            this._LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this._LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this._LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._LayoutPanel.Size = new System.Drawing.Size(334, 246);
            this._LayoutPanel.TabIndex = 0;
            // 
            // _NewPasswordGroup
            // 
            this._LayoutPanel.SetColumnSpan(this._NewPasswordGroup, 2);
            this._NewPasswordGroup.Controls.Add(this._TableLayoutPanel1);
            this._NewPasswordGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NewPasswordGroup.Location = new System.Drawing.Point(3, 71);
            this._NewPasswordGroup.Name = "_NewPasswordGroup";
            this._NewPasswordGroup.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this._NewPasswordGroup.Size = new System.Drawing.Size(328, 53);
            this._NewPasswordGroup.TabIndex = 4;
            this._NewPasswordGroup.TabStop = false;
            this._NewPasswordGroup.Text = "New Password";
            // 
            // _TableLayoutPanel1
            // 
            this._TableLayoutPanel1.ColumnCount = 1;
            this._TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._TableLayoutPanel1.Controls.Add(this._NewPasswordLabel, 0, 0);
            this._TableLayoutPanel1.Controls.Add(this._NewPassword, 0, 1);
            this._TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TableLayoutPanel1.Location = new System.Drawing.Point(10, 23);
            this._TableLayoutPanel1.Name = "_TableLayoutPanel1";
            this._TableLayoutPanel1.RowCount = 2;
            this._TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this._TableLayoutPanel1.Size = new System.Drawing.Size(308, 21);
            this._TableLayoutPanel1.TabIndex = 1;
            // 
            // _NewPasswordLabel
            // 
            this._NewPasswordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NewPasswordLabel.Location = new System.Drawing.Point(3, 0);
            this._NewPasswordLabel.Name = "_NewPasswordLabel";
            this._NewPasswordLabel.Size = new System.Drawing.Size(302, 1);
            this._NewPasswordLabel.TabIndex = 0;
            this._NewPasswordLabel.Text = "Enter the new password for this cluster or leave blank to remove password protect" +
    "ion:";
            this._NewPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _NewPassword
            // 
            this._NewPassword.BackColor = System.Drawing.SystemColors.Window;
            this._NewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._NewPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NewPassword.Key = null;
            this._NewPassword.Location = new System.Drawing.Point(0, -1);
            this._NewPassword.Margin = new System.Windows.Forms.Padding(0);
            this._NewPassword.Name = "_NewPassword";
            this._NewPassword.Password = "";
            this._NewPassword.Size = new System.Drawing.Size(308, 22);
            this._NewPassword.TabIndex = 1;
            // 
            // _ButtonCancel
            // 
            this._ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._ButtonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ButtonCancel.Location = new System.Drawing.Point(169, 139);
            this._ButtonCancel.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
            this._ButtonCancel.Name = "_ButtonCancel";
            this._ButtonCancel.Size = new System.Drawing.Size(162, 24);
            this._ButtonCancel.TabIndex = 1;
            this._ButtonCancel.Text = "Cancel";
            this._ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // _ButtonOK
            // 
            this._ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._ButtonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ButtonOK.Location = new System.Drawing.Point(170, 229);
            this._ButtonOK.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this._ButtonOK.Name = "_ButtonOK";
            this._ButtonOK.Size = new System.Drawing.Size(163, 14);
            this._ButtonOK.TabIndex = 0;
            this._ButtonOK.Text = "OK";
            this._ButtonOK.UseVisualStyleBackColor = true;
            // 
            // _OldPasswordGroup
            // 
            this._LayoutPanel.SetColumnSpan(this._OldPasswordGroup, 2);
            this._OldPasswordGroup.Controls.Add(this._OldLayout);
            this._OldPasswordGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._OldPasswordGroup.Location = new System.Drawing.Point(3, 3);
            this._OldPasswordGroup.Name = "_OldPasswordGroup";
            this._OldPasswordGroup.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this._OldPasswordGroup.Size = new System.Drawing.Size(328, 53);
            this._OldPasswordGroup.TabIndex = 3;
            this._OldPasswordGroup.TabStop = false;
            this._OldPasswordGroup.Text = "Current Password";
            // 
            // _OldLayout
            // 
            this._OldLayout.ColumnCount = 1;
            this._OldLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._OldLayout.Controls.Add(this._OldLabel, 0, 0);
            this._OldLayout.Controls.Add(this._OldPassword, 0, 1);
            this._OldLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._OldLayout.Location = new System.Drawing.Point(10, 23);
            this._OldLayout.Name = "_OldLayout";
            this._OldLayout.RowCount = 2;
            this._OldLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._OldLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this._OldLayout.Size = new System.Drawing.Size(308, 21);
            this._OldLayout.TabIndex = 0;
            // 
            // _OldLabel
            // 
            this._OldLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._OldLabel.Location = new System.Drawing.Point(3, 0);
            this._OldLabel.Name = "_OldLabel";
            this._OldLabel.Size = new System.Drawing.Size(302, 1);
            this._OldLabel.TabIndex = 0;
            this._OldLabel.Text = "Enter the current password to unlock access to this file:";
            this._OldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _OldPassword
            // 
            this._OldPassword.BackColor = System.Drawing.SystemColors.Window;
            this._OldPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._OldPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this._OldPassword.Key = null;
            this._OldPassword.Location = new System.Drawing.Point(0, -1);
            this._OldPassword.Margin = new System.Windows.Forms.Padding(0);
            this._OldPassword.Name = "_OldPassword";
            this._OldPassword.Password = "";
            this._OldPassword.Size = new System.Drawing.Size(308, 22);
            this._OldPassword.TabIndex = 1;
            // 
            // _ToolTip
            // 
            this._ToolTip.AutoPopDelay = 32000;
            this._ToolTip.InitialDelay = 500;
            this._ToolTip.ReshowDelay = 100;
            // 
            // PasswordWindow
            // 
            this.AcceptButton = this._ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 246);
            this.Controls.Add(this._LayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 141);
            this.Name = "PasswordWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GH_Protector(Panda)";
            this._LayoutPanel.ResumeLayout(false);
            this._NewPasswordGroup.ResumeLayout(false);
            this._TableLayoutPanel1.ResumeLayout(false);
            this._OldPasswordGroup.ResumeLayout(false);
            this._OldLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void GH_ClusterPasswordWindow_Load(object sender, System.EventArgs e)
        {
            Grasshopper.GUI.GH_WindowsControlUtil.FixTextRenderingDefault(this.Controls);
            if (this.OldPassword.Key != null && this.OldPasswordGroup.Enabled)
            {
                this.NewPasswordGroup.Enabled = false;
                this.OldPassword.ValidPasswordEntered += new Grasshopper.GUI.GH_PasswordBox.ValidPasswordEnteredEventHandler(this.ValidPasswordEntered);
            }
            if (!this.NewPasswordGroup.Visible)
            {
                int height = this.NewPasswordGroup.Height;
                this.LayoutPanel.Controls.Remove(this.NewPasswordGroup);
                this.LayoutPanel.SetRowSpan(this.OldPasswordGroup, 3);
                this.Height -= height + 10;
            }
        }

        private void ValidPasswordEntered(object sender, System.EventArgs e)
        {
            this.NewPasswordGroup.Enabled = true;
        }
    }
}
