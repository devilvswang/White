
namespace White.WinForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_timestamp = new System.Windows.Forms.Button();
            this.txt_timestamp = new System.Windows.Forms.TextBox();
            this.txt_nonce = new System.Windows.Forms.TextBox();
            this.btn_nonce = new System.Windows.Forms.Button();
            this.txt_sign = new System.Windows.Forms.TextBox();
            this.btn_sign = new System.Windows.Forms.Button();
            this.txt_cryp = new System.Windows.Forms.TextBox();
            this.btn_cryp = new System.Windows.Forms.Button();
            this.btn_request = new System.Windows.Forms.Button();
            this.txt_request = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_timestamp
            // 
            this.btn_timestamp.Location = new System.Drawing.Point(34, 28);
            this.btn_timestamp.Name = "btn_timestamp";
            this.btn_timestamp.Size = new System.Drawing.Size(75, 25);
            this.btn_timestamp.TabIndex = 0;
            this.btn_timestamp.Text = "时间戳";
            this.btn_timestamp.UseVisualStyleBackColor = true;
            this.btn_timestamp.Click += new System.EventHandler(this.btn_timestamp_Click);
            // 
            // txt_timestamp
            // 
            this.txt_timestamp.Location = new System.Drawing.Point(138, 28);
            this.txt_timestamp.Name = "txt_timestamp";
            this.txt_timestamp.Size = new System.Drawing.Size(383, 25);
            this.txt_timestamp.TabIndex = 1;
            // 
            // txt_nonce
            // 
            this.txt_nonce.Location = new System.Drawing.Point(138, 83);
            this.txt_nonce.Name = "txt_nonce";
            this.txt_nonce.Size = new System.Drawing.Size(383, 25);
            this.txt_nonce.TabIndex = 3;
            // 
            // btn_nonce
            // 
            this.btn_nonce.Location = new System.Drawing.Point(34, 85);
            this.btn_nonce.Name = "btn_nonce";
            this.btn_nonce.Size = new System.Drawing.Size(75, 25);
            this.btn_nonce.TabIndex = 2;
            this.btn_nonce.Text = "随机串";
            this.btn_nonce.UseVisualStyleBackColor = true;
            this.btn_nonce.Click += new System.EventHandler(this.btn_nonce_Click);
            // 
            // txt_sign
            // 
            this.txt_sign.Location = new System.Drawing.Point(138, 143);
            this.txt_sign.Multiline = true;
            this.txt_sign.Name = "txt_sign";
            this.txt_sign.Size = new System.Drawing.Size(383, 159);
            this.txt_sign.TabIndex = 5;
            // 
            // btn_sign
            // 
            this.btn_sign.Location = new System.Drawing.Point(34, 145);
            this.btn_sign.Name = "btn_sign";
            this.btn_sign.Size = new System.Drawing.Size(75, 25);
            this.btn_sign.TabIndex = 4;
            this.btn_sign.Text = "签名";
            this.btn_sign.UseVisualStyleBackColor = true;
            this.btn_sign.Click += new System.EventHandler(this.btn_sign_Click);
            // 
            // txt_cryp
            // 
            this.txt_cryp.Location = new System.Drawing.Point(661, 143);
            this.txt_cryp.Multiline = true;
            this.txt_cryp.Name = "txt_cryp";
            this.txt_cryp.Size = new System.Drawing.Size(383, 159);
            this.txt_cryp.TabIndex = 7;
            // 
            // btn_cryp
            // 
            this.btn_cryp.Location = new System.Drawing.Point(557, 145);
            this.btn_cryp.Name = "btn_cryp";
            this.btn_cryp.Size = new System.Drawing.Size(75, 25);
            this.btn_cryp.TabIndex = 6;
            this.btn_cryp.Text = "加密";
            this.btn_cryp.UseVisualStyleBackColor = true;
            this.btn_cryp.Click += new System.EventHandler(this.btn_cryp_Click);
            // 
            // btn_request
            // 
            this.btn_request.Location = new System.Drawing.Point(34, 330);
            this.btn_request.Name = "btn_request";
            this.btn_request.Size = new System.Drawing.Size(75, 25);
            this.btn_request.TabIndex = 8;
            this.btn_request.Text = "请求";
            this.btn_request.UseVisualStyleBackColor = true;
            this.btn_request.Click += new System.EventHandler(this.btn_request_Click);
            // 
            // txt_request
            // 
            this.txt_request.Location = new System.Drawing.Point(138, 332);
            this.txt_request.Multiline = true;
            this.txt_request.Name = "txt_request";
            this.txt_request.Size = new System.Drawing.Size(383, 159);
            this.txt_request.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 581);
            this.Controls.Add(this.txt_request);
            this.Controls.Add(this.btn_request);
            this.Controls.Add(this.txt_cryp);
            this.Controls.Add(this.btn_cryp);
            this.Controls.Add(this.txt_sign);
            this.Controls.Add(this.btn_sign);
            this.Controls.Add(this.txt_nonce);
            this.Controls.Add(this.btn_nonce);
            this.Controls.Add(this.txt_timestamp);
            this.Controls.Add(this.btn_timestamp);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_timestamp;
        private System.Windows.Forms.TextBox txt_timestamp;
        private System.Windows.Forms.TextBox txt_nonce;
        private System.Windows.Forms.Button btn_nonce;
        private System.Windows.Forms.TextBox txt_sign;
        private System.Windows.Forms.Button btn_sign;
        private System.Windows.Forms.TextBox txt_cryp;
        private System.Windows.Forms.Button btn_cryp;
        private System.Windows.Forms.Button btn_request;
        private System.Windows.Forms.TextBox txt_request;
    }
}

