namespace Sudoku_Game
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblNewGame = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblOut = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.SystemColors.Window;
            this.lblName.Location = new System.Drawing.Point(204, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(178, 42);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "SUDOKU";
            // 
            // lblNewGame
            // 
            this.lblNewGame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNewGame.AutoSize = true;
            this.lblNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewGame.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblNewGame.Location = new System.Drawing.Point(231, 80);
            this.lblNewGame.Name = "lblNewGame";
            this.lblNewGame.Size = new System.Drawing.Size(117, 25);
            this.lblNewGame.TabIndex = 1;
            this.lblNewGame.Text = "New Game";
            this.lblNewGame.Click += new System.EventHandler(this.lblNewGame_Click);
            // 
            // lblLevel
            // 
            this.lblLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblLevel.Location = new System.Drawing.Point(129, 130);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(86, 25);
            this.lblLevel.TabIndex = 2;
            this.lblLevel.Text = "Mức Độ";
            // 
            // lblOut
            // 
            this.lblOut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOut.AutoSize = true;
            this.lblOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOut.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblOut.Location = new System.Drawing.Point(273, 170);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(67, 25);
            this.lblOut.TabIndex = 3;
            this.lblOut.Text = "Thoát";
            this.lblOut.Click += new System.EventHandler(this.lblOut_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(173, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Trở lại thông tin đăng nhập";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Cyan;
            this.label2.Location = new System.Drawing.Point(332, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Đăng nhập";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cmbLevel
            // 
            this.cmbLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Items.AddRange(new object[] {
            "Dễ",
            "Vừa",
            "Khó"});
            this.cmbLevel.Location = new System.Drawing.Point(236, 130);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(173, 21);
            this.cmbLevel.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Sudoku_Game.Properties.Resources.z6445093696543_7476b5ce4b21f00b38927b355a28c777;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbLevel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblOut);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblNewGame);
            this.Controls.Add(this.lblName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblNewGame;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLevel;
    }
}

