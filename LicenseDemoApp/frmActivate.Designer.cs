
namespace LicenseDemoApp
{
    partial class frmActivate
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnActive = new System.Windows.Forms.Button();
            this.btnGetCode = new System.Windows.Forms.Button();
            this.txtActiveCode = new System.Windows.Forms.TextBox();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.txtCDKEY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCDKEY);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnActive);
            this.groupBox1.Controls.Add(this.btnGetCode);
            this.groupBox1.Controls.Add(this.txtActiveCode);
            this.groupBox1.Controls.Add(this.txtUserCode);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 465);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnActive
            // 
            this.btnActive.Location = new System.Drawing.Point(282, 401);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(150, 38);
            this.btnActive.TabIndex = 15;
            this.btnActive.Text = "Activate";
            this.btnActive.UseVisualStyleBackColor = true;
            this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
            // 
            // btnGetCode
            // 
            this.btnGetCode.Location = new System.Drawing.Point(282, 217);
            this.btnGetCode.Name = "btnGetCode";
            this.btnGetCode.Size = new System.Drawing.Size(150, 38);
            this.btnGetCode.TabIndex = 16;
            this.btnGetCode.Text = "Get Code";
            this.btnGetCode.UseVisualStyleBackColor = true;
            this.btnGetCode.Click += new System.EventHandler(this.btnGetCode_Click);
            // 
            // txtActiveCode
            // 
            this.txtActiveCode.Location = new System.Drawing.Point(120, 261);
            this.txtActiveCode.Multiline = true;
            this.txtActiveCode.Name = "txtActiveCode";
            this.txtActiveCode.Size = new System.Drawing.Size(312, 134);
            this.txtActiveCode.TabIndex = 13;
            // 
            // txtUserCode
            // 
            this.txtUserCode.Location = new System.Drawing.Point(120, 77);
            this.txtUserCode.Multiline = true;
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.ReadOnly = true;
            this.txtUserCode.Size = new System.Drawing.Size(312, 134);
            this.txtUserCode.TabIndex = 14;
            // 
            // txtCDKEY
            // 
            this.txtCDKEY.Location = new System.Drawing.Point(120, 42);
            this.txtCDKEY.Name = "txtCDKEY";
            this.txtCDKEY.Size = new System.Drawing.Size(312, 29);
            this.txtCDKEY.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 21);
            this.label2.TabIndex = 17;
            this.label2.Text = "CDKEY :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 21);
            this.label1.TabIndex = 17;
            this.label1.Text = "UserCode :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 21);
            this.label3.TabIndex = 17;
            this.label3.Text = "ActiveCode :";
            // 
            // frmActivate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 489);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmActivate";
            this.Text = "Activation Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnActive;
        private System.Windows.Forms.Button btnGetCode;
        private System.Windows.Forms.TextBox txtActiveCode;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.TextBox txtCDKEY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}