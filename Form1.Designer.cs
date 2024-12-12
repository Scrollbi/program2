namespace program
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
            this.authorization_button = new System.Windows.Forms.Button();
            this.registration_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // authorization_button
            // 
            this.authorization_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.authorization_button.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.authorization_button.Location = new System.Drawing.Point(56, 96);
            this.authorization_button.Name = "authorization_button";
            this.authorization_button.Size = new System.Drawing.Size(272, 39);
            this.authorization_button.TabIndex = 0;
            this.authorization_button.Text = "Авторизация";
            this.authorization_button.UseVisualStyleBackColor = true;
            this.authorization_button.Click += new System.EventHandler(this.authorization_button_Click);
            // 
            // registration_button
            // 
            this.registration_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.registration_button.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.registration_button.Location = new System.Drawing.Point(56, 183);
            this.registration_button.Name = "registration_button";
            this.registration_button.Size = new System.Drawing.Size(272, 39);
            this.registration_button.TabIndex = 1;
            this.registration_button.Text = "Регистрация";
            this.registration_button.UseVisualStyleBackColor = true;
            this.registration_button.Click += new System.EventHandler(this.registration_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::program.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.registration_button);
            this.Controls.Add(this.authorization_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Вход";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button authorization_button;
        private System.Windows.Forms.Button registration_button;
    }
}

