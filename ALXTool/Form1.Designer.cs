namespace ALXTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.logArea = new System.Windows.Forms.RichTextBox();
            this.label_connectStatus = new System.Windows.Forms.Label();
            this.button_ConnectPrintBox = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // logArea
            // 
            this.logArea.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.logArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logArea.Font = new System.Drawing.Font("新宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logArea.Location = new System.Drawing.Point(13, 13);
            this.logArea.Name = "logArea";
            this.logArea.ReadOnly = true;
            this.logArea.Size = new System.Drawing.Size(713, 431);
            this.logArea.TabIndex = 0;
            this.logArea.Text = "";
            // 
            // label_connectStatus
            // 
            this.label_connectStatus.AutoSize = true;
            this.label_connectStatus.BackColor = System.Drawing.Color.Red;
            this.label_connectStatus.Location = new System.Drawing.Point(574, 480);
            this.label_connectStatus.Name = "label_connectStatus";
            this.label_connectStatus.Size = new System.Drawing.Size(11, 12);
            this.label_connectStatus.TabIndex = 11;
            this.label_connectStatus.Text = " ";
            // 
            // button_ConnectPrintBox
            // 
            this.button_ConnectPrintBox.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ConnectPrintBox.Location = new System.Drawing.Point(591, 450);
            this.button_ConnectPrintBox.Name = "button_ConnectPrintBox";
            this.button_ConnectPrintBox.Size = new System.Drawing.Size(135, 58);
            this.button_ConnectPrintBox.TabIndex = 12;
            this.button_ConnectPrintBox.Text = "开始升级";
            this.button_ConnectPrintBox.UseVisualStyleBackColor = true;
            this.button_ConnectPrintBox.Click += new System.EventHandler(this.button_ConnectPrintBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 450);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "前置条件：将升级包 upgrade.fac放在 C:\\tmp文件夹下";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 468);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "电脑连接盒子WiFi，不需要给盒子配网";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 487);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "准备好后直接点击开始升级";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 517);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_ConnectPrintBox);
            this.Controls.Add(this.label_connectStatus);
            this.Controls.Add(this.logArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "云盒升级工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox logArea;
        private System.Windows.Forms.Label label_connectStatus;
        private System.Windows.Forms.Button button_ConnectPrintBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

