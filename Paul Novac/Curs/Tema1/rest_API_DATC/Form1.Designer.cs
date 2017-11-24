namespace rest_API_DATC
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.GetButton = new System.Windows.Forms.Button();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PostButton = new System.Windows.Forms.Button();
            this.txtBere = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GetButton
            // 
            this.GetButton.Location = new System.Drawing.Point(396, 50);
            this.GetButton.Name = "GetButton";
            this.GetButton.Size = new System.Drawing.Size(198, 67);
            this.GetButton.TabIndex = 0;
            this.GetButton.Text = "GET";
            this.GetButton.UseVisualStyleBackColor = true;
            this.GetButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(22, 50);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResponse.Size = new System.Drawing.Size(354, 202);
            this.txtResponse.TabIndex = 1;
            this.txtResponse.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // PostButton
            // 
            this.PostButton.Location = new System.Drawing.Point(396, 297);
            this.PostButton.Name = "PostButton";
            this.PostButton.Size = new System.Drawing.Size(198, 67);
            this.PostButton.TabIndex = 3;
            this.PostButton.Text = "POST";
            this.PostButton.UseVisualStyleBackColor = true;
            this.PostButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtBere
            // 
            this.txtBere.Location = new System.Drawing.Point(171, 317);
            this.txtBere.Name = "txtBere";
            this.txtBere.Size = new System.Drawing.Size(205, 26);
            this.txtBere.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Denumire bere:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 469);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBere);
            this.Controls.Add(this.PostButton);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.GetButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetButton;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button PostButton;
        private System.Windows.Forms.TextBox txtBere;
        private System.Windows.Forms.Label label1;
    }
}

