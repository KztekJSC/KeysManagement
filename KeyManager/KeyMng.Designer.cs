
namespace KztekKeyRegister
{
    partial class KeyMng
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyMng));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGetUsercode = new System.Windows.Forms.Button();
            this.btnCdkeyPaste = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCdkeyCopy = new System.Windows.Forms.Button();
            this.lblunregist = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportActiveKey = new System.Windows.Forms.ToolStripButton();
            this.txtActiveCode = new System.Windows.Forms.TextBox();
            this.btnActive = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCdkey = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnCopyUserCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportUsercode = new System.Windows.Forms.ToolStripButton();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetUsercode
            // 
            this.btnGetUsercode.BackColor = System.Drawing.Color.MediumBlue;
            this.btnGetUsercode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGetUsercode.BackgroundImage")));
            this.btnGetUsercode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetUsercode.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnGetUsercode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetUsercode.Font = new System.Drawing.Font("Adobe Fan Heiti Std B", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGetUsercode.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnGetUsercode.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGetUsercode.Location = new System.Drawing.Point(0, 0);
            this.btnGetUsercode.Name = "btnGetUsercode";
            this.btnGetUsercode.Size = new System.Drawing.Size(277, 41);
            this.btnGetUsercode.TabIndex = 32;
            this.btnGetUsercode.Text = "LẤY USERCODE";
            this.btnGetUsercode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnGetUsercode, "Tạo user code");
            this.btnGetUsercode.UseVisualStyleBackColor = false;
            this.btnGetUsercode.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCdkeyPaste
            // 
            this.btnCdkeyPaste.BackColor = System.Drawing.Color.Transparent;
            this.btnCdkeyPaste.BackgroundImage = global::KztekKeyRegister.Properties.Resources._768px_Gnome_edit_paste_svg;
            this.btnCdkeyPaste.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCdkeyPaste.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCdkeyPaste.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCdkeyPaste.Location = new System.Drawing.Point(607, 0);
            this.btnCdkeyPaste.Name = "btnCdkeyPaste";
            this.btnCdkeyPaste.Size = new System.Drawing.Size(38, 36);
            this.btnCdkeyPaste.TabIndex = 25;
            this.toolTip1.SetToolTip(this.btnCdkeyPaste, "Dán");
            this.btnCdkeyPaste.UseVisualStyleBackColor = false;
            this.btnCdkeyPaste.Click += new System.EventHandler(this.btnPasteCdKey_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 660);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCdkeyCopy);
            this.groupBox1.Controls.Add(this.lblunregist);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(0, 406);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(651, 218);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin License đã kích hoạt";
            // 
            // btnCdkeyCopy
            // 
            this.btnCdkeyCopy.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCdkeyCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCdkeyCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCdkeyCopy.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCdkeyCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCdkeyCopy.Image")));
            this.btnCdkeyCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCdkeyCopy.Location = new System.Drawing.Point(483, 154);
            this.btnCdkeyCopy.Name = "btnCdkeyCopy";
            this.btnCdkeyCopy.Size = new System.Drawing.Size(135, 25);
            this.btnCdkeyCopy.TabIndex = 30;
            this.btnCdkeyCopy.Text = "Copy CDKEY";
            this.btnCdkeyCopy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnCdkeyCopy.UseVisualStyleBackColor = false;
            this.btnCdkeyCopy.Visible = false;
            this.btnCdkeyCopy.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // lblunregist
            // 
            this.lblunregist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblunregist.Location = new System.Drawing.Point(3, 29);
            this.lblunregist.Name = "lblunregist";
            this.lblunregist.Size = new System.Drawing.Size(645, 186);
            this.lblunregist.TabIndex = 0;
            this.lblunregist.Text = "Phần mềm chưa được kích hoạt!";
            this.lblunregist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel2});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 624);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(651, 36);
            this.statusStrip1.TabIndex = 28;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BackgroundImage = global::KztekKeyRegister.Properties.Resources.kztekPurple;
            this.toolStripStatusLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripStatusLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(60, 25);
            this.toolStripStatusLabel1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel4.Image")));
            this.toolStripStatusLabel4.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(137, 31);
            this.toolStripStatusLabel4.Text = "0988637099";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel3.Image")));
            this.toolStripStatusLabel3.IsLink = true;
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(149, 31);
            this.toolStripStatusLabel3.Text = "http://kztek.net/";
            this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel3.Click += new System.EventHandler(this.toolStripStatusLabel3_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Image = global::KztekKeyRegister.Properties.Resources._1200px_Blue_question_mark_icon_svg;
            this.toolStripStatusLabel2.IsLink = true;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(236, 31);
            this.toolStripStatusLabel2.Text = "Hướng dẫn kích hoạt License";
            this.toolStripStatusLabel2.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox3.Controls.Add(this.panel4);
            this.groupBox3.Controls.Add(this.btnActive);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox3.Location = new System.Drawing.Point(0, 213);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(651, 193);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Active phần mềm";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.toolStrip1);
            this.panel4.Controls.Add(this.txtActiveCode);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(645, 120);
            this.panel4.TabIndex = 29;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.btnImportActiveKey});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(527, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(118, 33);
            this.toolStrip1.TabIndex = 31;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // btnImportActiveKey
            // 
            this.btnImportActiveKey.Image = ((System.Drawing.Image)(resources.GetObject("btnImportActiveKey.Image")));
            this.btnImportActiveKey.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportActiveKey.Name = "btnImportActiveKey";
            this.btnImportActiveKey.Size = new System.Drawing.Size(76, 30);
            this.btnImportActiveKey.Text = "....Mở file";
            this.btnImportActiveKey.Click += new System.EventHandler(this.btnImportActiveKey_Click);
            // 
            // txtActiveCode
            // 
            this.txtActiveCode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtActiveCode.Location = new System.Drawing.Point(0, 33);
            this.txtActiveCode.Multiline = true;
            this.txtActiveCode.Name = "txtActiveCode";
            this.txtActiveCode.PlaceholderText = "Active code";
            this.txtActiveCode.ReadOnly = true;
            this.txtActiveCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtActiveCode.Size = new System.Drawing.Size(645, 87);
            this.txtActiveCode.TabIndex = 30;
            this.txtActiveCode.TextChanged += new System.EventHandler(this.txtActiveCode_TextChanged);
            // 
            // btnActive
            // 
            this.btnActive.BackColor = System.Drawing.Color.MediumBlue;
            this.btnActive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnActive.BackgroundImage")));
            this.btnActive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnActive.Enabled = false;
            this.btnActive.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActive.Font = new System.Drawing.Font("Adobe Fan Heiti Std B", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnActive.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnActive.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnActive.Location = new System.Drawing.Point(3, 150);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(269, 40);
            this.btnActive.TabIndex = 28;
            this.btnActive.Text = "KÍCH HOẠT";
            this.btnActive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnActive.UseVisualStyleBackColor = false;
            this.btnActive.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(651, 213);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lấy Usercode";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCdkeyPaste);
            this.panel3.Controls.Add(this.txtCdkey);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 29);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(645, 36);
            this.panel3.TabIndex = 34;
            // 
            // txtCdkey
            // 
            this.txtCdkey.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtCdkey.Location = new System.Drawing.Point(0, 0);
            this.txtCdkey.Multiline = true;
            this.txtCdkey.Name = "txtCdkey";
            this.txtCdkey.PlaceholderText = "CD key";
            this.txtCdkey.Size = new System.Drawing.Size(607, 36);
            this.txtCdkey.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Controls.Add(this.btnGetUsercode);
            this.panel2.Controls.Add(this.txtUserCode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(645, 145);
            this.panel2.TabIndex = 33;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip2.Enabled = false;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCopyUserCode,
            this.toolStripSeparator1,
            this.btnExportUsercode});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(395, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip2.Size = new System.Drawing.Size(250, 41);
            this.toolStrip2.TabIndex = 33;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnCopyUserCode
            // 
            this.btnCopyUserCode.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyUserCode.Image")));
            this.btnCopyUserCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopyUserCode.Name = "btnCopyUserCode";
            this.btnCopyUserCode.Size = new System.Drawing.Size(107, 38);
            this.btnCopyUserCode.Text = "Copy Usercode";
            this.btnCopyUserCode.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
            // 
            // btnExportUsercode
            // 
            this.btnExportUsercode.Image = ((System.Drawing.Image)(resources.GetObject("btnExportUsercode.Image")));
            this.btnExportUsercode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportUsercode.Name = "btnExportUsercode";
            this.btnExportUsercode.Size = new System.Drawing.Size(88, 38);
            this.btnExportUsercode.Text = "Lưu về máy";
            this.btnExportUsercode.Click += new System.EventHandler(this.btnExportUsercode_Click);
            // 
            // txtUserCode
            // 
            this.txtUserCode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtUserCode.Location = new System.Drawing.Point(0, 41);
            this.txtUserCode.Multiline = true;
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.PlaceholderText = "Usercode";
            this.txtUserCode.ReadOnly = true;
            this.txtUserCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUserCode.Size = new System.Drawing.Size(645, 104);
            this.txtUserCode.TabIndex = 32;
            // 
            // KeyMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(651, 660);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "KeyMng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KzManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Manage_FormClosing);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnCopyUserCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportUsercode;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.Button btnGetUsercode;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImportActiveKey;
        private System.Windows.Forms.TextBox txtActiveCode;
        private System.Windows.Forms.Button btnActive;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCdkeyPaste;
        private System.Windows.Forms.TextBox txtCdkey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblunregist;
        private System.Windows.Forms.Button btnCdkeyCopy;
    }
}

