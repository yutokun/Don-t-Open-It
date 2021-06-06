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
            this.killMethod = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.processes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
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
            // processes
            // 
            this.processes.DropDownHeight = 800;
            this.processes.FormattingEnabled = true;
            resources.ApplyResources(this.processes, "processes");
            this.processes.Name = "processes";
            this.processes.Sorted = true;
            this.processes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.processes_KeyDown);
            this.processes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.processes_KeyPress);
            // 
            // AddDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.processes);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.killMethod);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AddDialog_Load);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox processes;

        private System.Windows.Forms.ComboBox killMethod;

        private System.Windows.Forms.Button addButton;

        #endregion
    }
}

