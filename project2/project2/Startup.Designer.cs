namespace project2
{
    partial class Startup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Startup));
            this.load = new System.Windows.Forms.Button();
            this.create = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // load
            // 
            this.load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.load.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load.Location = new System.Drawing.Point(22, 215);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(210, 39);
            this.load.TabIndex = 0;
            this.load.Text = "Load / Modify";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // create
            // 
            this.create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.create.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.create.Location = new System.Drawing.Point(22, 268);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(210, 39);
            this.create.TabIndex = 1;
            this.create.Text = "Create New";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.creat_Click);
            // 
            // Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(550, 330);
            this.Controls.Add(this.create);
            this.Controls.Add(this.load);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Startup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BADA Data Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Startup_FormClosing);
            this.Load += new System.EventHandler(this.Startup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button load;
        private System.Windows.Forms.Button create;
    }
}

