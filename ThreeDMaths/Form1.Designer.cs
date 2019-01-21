namespace ThreeDMaths
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
            this.btnBox = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnPerspective = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBox
            // 
            this.btnBox.Location = new System.Drawing.Point(28, 49);
            this.btnBox.Name = "btnBox";
            this.btnBox.Size = new System.Drawing.Size(75, 23);
            this.btnBox.TabIndex = 0;
            this.btnBox.Text = "Box";
            this.btnBox.UseVisualStyleBackColor = true;
            this.btnBox.Click += new System.EventHandler(this.btnBox_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(433, 321);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(75, 23);
            this.btnRight.TabIndex = 1;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnPerspective
            // 
            this.btnPerspective.Location = new System.Drawing.Point(28, 78);
            this.btnPerspective.Name = "btnPerspective";
            this.btnPerspective.Size = new System.Drawing.Size(75, 23);
            this.btnPerspective.TabIndex = 2;
            this.btnPerspective.Text = "Perspective";
            this.btnPerspective.UseVisualStyleBackColor = true;
            this.btnPerspective.Click += new System.EventHandler(this.btnPerspective_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPerspective);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBox;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnPerspective;
    }
}

