using System.ComponentModel;

namespace DontOpenIt
{
    partial class AddDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDialog));
            this.name = new System.Windows.Forms.TextBox();
            this.killMethod = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.name, "name");
            this.name.Name = "name";
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            this.name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.name_KeyDown);
            this.name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.name_KeyPress);
            // 
            // killMethod
            // 
            this.killMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.killMethod.FormattingEnabled = true;
            this.killMethod.Items.AddRange(new object[] { resources.GetString("killMethod.Items"), resources.GetString("killMethod.Items1"), resources.GetString("killMethod.Items2") });
            resources.ApplyResources(this.killMethod, "killMethod");
            this.killMethod.Name = "killMethod";
            this.killMethod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.killMethod_KeyDown);
            // 
            // addButton
            // 
            resources.ApplyResources(this.addButton, "addButton");
            this.addButton.Name = "addButton";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            this.addButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addButton_KeyDown);
            // 
            // AddDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.killMethod);
            this.Controls.Add(this.name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AddDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox killMethod;

        private System.Windows.Forms.TextBox name;

        private System.Windows.Forms.Button addButton;

        #endregion
    }
}

