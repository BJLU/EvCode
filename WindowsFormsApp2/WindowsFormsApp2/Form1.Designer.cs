namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.outData = new System.Windows.Forms.TextBox();
            this.butto1 = new System.Windows.Forms.Button();
            this.namingSocket = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // outData
            // 
            this.outData.Location = new System.Drawing.Point(410, 12);
            this.outData.Multiline = true;
            this.outData.Name = "outData";
            this.outData.Size = new System.Drawing.Size(375, 312);
            this.outData.TabIndex = 0;
            this.outData.TextChanged += new System.EventHandler(this.OutputData);
            // 
            // butto1
            // 
            this.butto1.Location = new System.Drawing.Point(29, 12);
            this.butto1.Name = "butto1";
            this.butto1.Size = new System.Drawing.Size(206, 106);
            this.butto1.TabIndex = 1;
            this.butto1.Text = "OpenSocket";
            this.butto1.UseVisualStyleBackColor = true;
            this.butto1.Click += new System.EventHandler(this.OpenSocket);
            // 
            // namingSocket
            // 
            this.namingSocket.Location = new System.Drawing.Point(29, 153);
            this.namingSocket.Name = "namingSocket";
            this.namingSocket.Size = new System.Drawing.Size(206, 134);
            this.namingSocket.TabIndex = 2;
            this.namingSocket.Text = "NamingSocket";
            this.namingSocket.UseVisualStyleBackColor = true;
            this.namingSocket.Click += new System.EventHandler(this.NamingSocket_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 369);
            this.Controls.Add(this.namingSocket);
            this.Controls.Add(this.butto1);
            this.Controls.Add(this.outData);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outData;
        private System.Windows.Forms.Button butto1;
        private System.Windows.Forms.Button namingSocket;
    }
}

