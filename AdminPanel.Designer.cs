namespace program
{
    partial class AdminPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_AllUsers = new System.Windows.Forms.TextBox();
            this.textBox_AllCollections = new System.Windows.Forms.TextBox();
            this.textBox_AllItems = new System.Windows.Forms.TextBox();
            this.button_Save = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.clock = new System.Windows.Forms.Label();
            this.button_blocking = new System.Windows.Forms.Button();
            this.button_unlock = new System.Windows.Forms.Button();
            this.comboBox_Users = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Всего учетных записей:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Всего предметов:";
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(15, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Сброс пользователей";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(9, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Всего коллекций:";
            // 
            // textBox_AllUsers
            // 
            this.textBox_AllUsers.Location = new System.Drawing.Point(185, 58);
            this.textBox_AllUsers.Name = "textBox_AllUsers";
            this.textBox_AllUsers.Size = new System.Drawing.Size(100, 20);
            this.textBox_AllUsers.TabIndex = 5;
            // 
            // textBox_AllCollections
            // 
            this.textBox_AllCollections.Location = new System.Drawing.Point(185, 84);
            this.textBox_AllCollections.Name = "textBox_AllCollections";
            this.textBox_AllCollections.Size = new System.Drawing.Size(100, 20);
            this.textBox_AllCollections.TabIndex = 6;
            // 
            // textBox_AllItems
            // 
            this.textBox_AllItems.Location = new System.Drawing.Point(185, 109);
            this.textBox_AllItems.Name = "textBox_AllItems";
            this.textBox_AllItems.Size = new System.Drawing.Size(100, 20);
            this.textBox_AllItems.TabIndex = 7;
            // 
            // button_Save
            // 
            this.button_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Save.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Save.Location = new System.Drawing.Point(15, 203);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(164, 23);
            this.button_Save.TabIndex = 8;
            this.button_Save.Text = "Сохранение данных";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(15, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(164, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Восстановление";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // clock
            // 
            this.clock.AutoSize = true;
            this.clock.BackColor = System.Drawing.Color.Transparent;
            this.clock.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clock.ForeColor = System.Drawing.Color.Orchid;
            this.clock.Location = new System.Drawing.Point(9, 9);
            this.clock.Name = "clock";
            this.clock.Size = new System.Drawing.Size(0, 16);
            this.clock.TabIndex = 10;
            // 
            // button_blocking
            // 
            this.button_blocking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_blocking.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_blocking.Location = new System.Drawing.Point(251, 215);
            this.button_blocking.Name = "button_blocking";
            this.button_blocking.Size = new System.Drawing.Size(40, 40);
            this.button_blocking.TabIndex = 11;
            this.button_blocking.Text = "❌";
            this.button_blocking.UseVisualStyleBackColor = true;
            this.button_blocking.Click += new System.EventHandler(this.button_blocking_Click);
            // 
            // button_unlock
            // 
            this.button_unlock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_unlock.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_unlock.Location = new System.Drawing.Point(332, 215);
            this.button_unlock.Name = "button_unlock";
            this.button_unlock.Size = new System.Drawing.Size(40, 40);
            this.button_unlock.TabIndex = 12;
            this.button_unlock.Text = "✔️";
            this.button_unlock.UseVisualStyleBackColor = true;
            this.button_unlock.Click += new System.EventHandler(this.button_unlock_Click_1);
            // 
            // comboBox_Users
            // 
            this.comboBox_Users.FormattingEnabled = true;
            this.comboBox_Users.Location = new System.Drawing.Point(251, 193);
            this.comboBox_Users.Name = "comboBox_Users";
            this.comboBox_Users.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Users.TabIndex = 13;
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::program.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.comboBox_Users);
            this.Controls.Add(this.button_unlock);
            this.Controls.Add(this.button_blocking);
            this.Controls.Add(this.clock);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.textBox_AllItems);
            this.Controls.Add(this.textBox_AllCollections);
            this.Controls.Add(this.textBox_AllUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AdminPanel";
            this.Text = "AdminPanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_AllUsers;
        private System.Windows.Forms.TextBox textBox_AllCollections;
        private System.Windows.Forms.TextBox textBox_AllItems;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label clock;
        private System.Windows.Forms.Button button_blocking;
        private System.Windows.Forms.Button button_unlock;
        private System.Windows.Forms.ComboBox comboBox_Users;
    }
}