using System.ComponentModel;

namespace DontOpenIt
{
    partial class SettingsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.beginTime = new System.Windows.Forms.TextBox();
            this.endTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.appList = new System.Windows.Forms.ListView();
            this.name = new System.Windows.Forms.ColumnHeader();
            this.killMethod = new System.Windows.Forms.ColumnHeader();
            this.weekend = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // beginTime
            // 
            this.beginTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.beginTime, "beginTime");
            this.beginTime.Name = "beginTime";
            this.beginTime.TextChanged += new System.EventHandler(this.beginTime_TextChanged);
            // 
            // endTime
            // 
            this.endTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.endTime, "endTime");
            this.endTime.Name = "endTime";
            this.endTime.TextChanged += new System.EventHandler(this.endTime_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // addButton
            // 
            resources.ApplyResources(this.addButton, "addButton");
            this.addButton.Name = "addButton";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            resources.ApplyResources(this.removeButton, "removeButton");
            this.removeButton.Name = "removeButton";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // appList
            // 
            this.appList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.appList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.name, this.killMethod });
            this.appList.FullRowSelect = true;
            this.appList.GridLines = true;
            this.appList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            resources.ApplyResources(this.appList, "appList");
            this.appList.Name = "appList";
            this.appList.UseCompatibleStateImageBehavior = false;
            this.appList.View = System.Windows.Forms.View.Details;
            this.appList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.appList_ItemSelectionChanged);
            this.appList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.appList_MouseClick);
            // 
            // name
            // 
            resources.ApplyResources(this.name, "name");
            // 
            // killMethod
            // 
            resources.ApplyResources(this.killMethod, "killMethod");
            // 
            // weekend
            // 
            resources.ApplyResources(this.weekend, "weekend");
            this.weekend.Name = "weekend";
            this.weekend.UseVisualStyleBackColor = true;
            this.weekend.CheckedChanged += new System.EventHandler(this.weekend_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // SettingsWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.weekend);
            this.Controls.Add(this.appList);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.endTime);
            this.Controls.Add(this.beginTime);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.CheckBox weekend;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.CheckBox checkBox1;

        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader killMethod;

        internal System.Windows.Forms.ListView appList;

        private System.Windows.Forms.TextBox endTime;

        private System.Windows.Forms.TextBox beginTime;

        private System.Windows.Forms.Button removeButton;

        private System.Windows.Forms.Button addButton;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}

