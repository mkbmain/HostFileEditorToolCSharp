namespace HostFileEditor.Forms
{
    partial class HostFileViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.AddElementButton = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddElementButton
            // 
            this.AddElementButton.Location = new System.Drawing.Point(400, 84);
            this.AddElementButton.Name = "AddElementButton";
            this.AddElementButton.Size = new System.Drawing.Size(36, 26);
            this.AddElementButton.TabIndex = 0;
            this.AddElementButton.Text = "+";
            this.AddElementButton.UseVisualStyleBackColor = true;
            this.AddElementButton.Click += new System.EventHandler(this.AddElementButton_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(377, 230);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(59, 23);
            this.Save.TabIndex = 1;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // HostFileViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(438, 371);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.AddElementButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "HostFileViewer";
            this.Text = "HostFileEditor";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button AddElementButton;
        private System.Windows.Forms.Button Save;

        #endregion
    }
}