namespace GUI_PoLyCafe
{
    partial class frmMatkhau
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
            groupBox1 = new GroupBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            button1 = new Button();
            txtXacNhanMK = new TextBox();
            txtMatKhauMoi = new TextBox();
            txtMatKhauCu = new TextBox();
            textTaiKhoan = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(txtXacNhanMK);
            groupBox1.Controls.Add(txtMatKhauMoi);
            groupBox1.Controls.Add(txtMatKhauCu);
            groupBox1.Controls.Add(textTaiKhoan);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(244, 99);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1161, 732);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Đổi Mật Khẩu";
            // 
            // checkBox3
            // 
            checkBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(349, 280);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(248, 45);
            checkBox3.TabIndex = 13;
            checkBox3.Text = "Hiện mật Khẩu";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(349, 398);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(248, 45);
            checkBox2.TabIndex = 12;
            checkBox2.Text = "Hiện mật Khẩu";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(349, 528);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(248, 45);
            checkBox1.TabIndex = 11;
            checkBox1.Text = "Hiện mật Khẩu";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(511, 631);
            button1.Name = "button1";
            button1.Size = new Size(144, 53);
            button1.TabIndex = 10;
            button1.Text = "Đổi";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtXacNhanMK
            // 
            txtXacNhanMK.Location = new Point(349, 475);
            txtXacNhanMK.Name = "txtXacNhanMK";
            txtXacNhanMK.PasswordChar = '*';
            txtXacNhanMK.Size = new Size(571, 47);
            txtXacNhanMK.TabIndex = 9;
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.Location = new Point(349, 345);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.PasswordChar = '*';
            txtMatKhauMoi.Size = new Size(571, 47);
            txtMatKhauMoi.TabIndex = 8;
            // 
            // txtMatKhauCu
            // 
            txtMatKhauCu.Location = new Point(349, 227);
            txtMatKhauCu.Name = "txtMatKhauCu";
            txtMatKhauCu.PasswordChar = '*';
            txtMatKhauCu.Size = new Size(571, 47);
            txtMatKhauCu.TabIndex = 7;
            // 
            // textTaiKhoan
            // 
            textTaiKhoan.Location = new Point(349, 133);
            textTaiKhoan.Name = "textTaiKhoan";
            textTaiKhoan.Size = new Size(571, 47);
            textTaiKhoan.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 481);
            label5.Name = "label5";
            label5.Size = new Size(304, 41);
            label5.TabIndex = 5;
            label5.Text = "Xác Nhận Mật Khẩu ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 351);
            label4.Name = "label4";
            label4.Size = new Size(219, 41);
            label4.TabIndex = 4;
            label4.Text = "Mật Khẩu Mới";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 230);
            label3.Name = "label3";
            label3.Size = new Size(199, 41);
            label3.TabIndex = 3;
            label3.Text = "Mật Khẩu Cũ";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 133);
            label2.Name = "label2";
            label2.Size = new Size(153, 41);
            label2.TabIndex = 2;
            label2.Text = "Tài khoản";
            // 
            // frmMatkhau
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(1602, 868);
            Controls.Add(groupBox1);
            Name = "frmMatkhau";
            Text = "Form1";
            FormClosed += frmMatkhau_FormClosed;
            Load += frmMatkhau_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private Button button1;
        private TextBox txtXacNhanMK;
        private TextBox txtMatKhauMoi;
        private TextBox txtMatKhauCu;
        private TextBox textTaiKhoan;
        private Label label5;
        private Label label4;
        private CheckBox checkBox1;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
    }
}