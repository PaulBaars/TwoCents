namespace CalculatorClient
{
    partial class CalculatorClient
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
            this.btnCallDirect = new System.Windows.Forms.Button();
            this.btnCallAPI = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCallDirect
            // 
            this.btnCallDirect.Location = new System.Drawing.Point(88, 63);
            this.btnCallDirect.Name = "btnCallDirect";
            this.btnCallDirect.Size = new System.Drawing.Size(75, 23);
            this.btnCallDirect.TabIndex = 0;
            this.btnCallDirect.Text = "CallDirect";
            this.btnCallDirect.UseVisualStyleBackColor = true;
            this.btnCallDirect.Click += new System.EventHandler(this.btnCallDirect_Click);
            // 
            // btnCallAPI
            // 
            this.btnCallAPI.Location = new System.Drawing.Point(88, 107);
            this.btnCallAPI.Name = "btnCallAPI";
            this.btnCallAPI.Size = new System.Drawing.Size(75, 23);
            this.btnCallAPI.TabIndex = 1;
            this.btnCallAPI.Text = "CallViaAPIMgmt";
            this.btnCallAPI.UseVisualStyleBackColor = true;
            this.btnCallAPI.Click += new System.EventHandler(this.button1_Click);
            // 
            // CalculatorClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnCallAPI);
            this.Controls.Add(this.btnCallDirect);
            this.Name = "CalculatorClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CalculatorClient";
            this.Load += new System.EventHandler(this.CalculatorClient_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCallDirect;
        private System.Windows.Forms.Button btnCallAPI;
    }
}

