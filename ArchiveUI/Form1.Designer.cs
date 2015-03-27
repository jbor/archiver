namespace ArchiveUI
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
            this.components = new System.ComponentModel.Container();
            this.buttonNext = new System.Windows.Forms.Button();
            this.ParameterNo = new System.Windows.Forms.Label();
            this.textBoxProcesDir = new System.Windows.Forms.TextBox();
            this.textBoxArchiveDir = new System.Windows.Forms.TextBox();
            this.checkBoxRecursive = new System.Windows.Forms.CheckBox();
            this.comboBoxTimespan = new System.Windows.Forms.ComboBox();
            this.textBoxExclude = new System.Windows.Forms.TextBox();
            this.textBoxInclude = new System.Windows.Forms.TextBox();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxRetention = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxInterfaceName = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripExit = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(166, 292);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // ParameterNo
            // 
            this.ParameterNo.AutoSize = true;
            this.ParameterNo.Location = new System.Drawing.Point(350, 302);
            this.ParameterNo.Name = "ParameterNo";
            this.ParameterNo.Size = new System.Drawing.Size(18, 13);
            this.ParameterNo.TabIndex = 2;
            this.ParameterNo.Text = "Nr";
            // 
            // textBoxProcesDir
            // 
            this.textBoxProcesDir.Location = new System.Drawing.Point(70, 98);
            this.textBoxProcesDir.Name = "textBoxProcesDir";
            this.textBoxProcesDir.Size = new System.Drawing.Size(283, 20);
            this.textBoxProcesDir.TabIndex = 3;
            this.textBoxProcesDir.TextChanged += new System.EventHandler(this.textBoxProcesDir_TextChanged);
            // 
            // textBoxArchiveDir
            // 
            this.textBoxArchiveDir.Location = new System.Drawing.Point(70, 120);
            this.textBoxArchiveDir.Name = "textBoxArchiveDir";
            this.textBoxArchiveDir.Size = new System.Drawing.Size(283, 20);
            this.textBoxArchiveDir.TabIndex = 4;
            this.textBoxArchiveDir.Validated += new System.EventHandler(this.textBoxArchiveDir_Validated);
            // 
            // checkBoxRecursive
            // 
            this.checkBoxRecursive.AutoSize = true;
            this.checkBoxRecursive.Location = new System.Drawing.Point(116, 146);
            this.checkBoxRecursive.Name = "checkBoxRecursive";
            this.checkBoxRecursive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxRecursive.Size = new System.Drawing.Size(74, 17);
            this.checkBoxRecursive.TabIndex = 6;
            this.checkBoxRecursive.Text = "Recursive";
            this.checkBoxRecursive.UseVisualStyleBackColor = true;
            this.checkBoxRecursive.Validated += new System.EventHandler(this.checkBoxRecursive_Validated);
            // 
            // comboBoxTimespan
            // 
            this.comboBoxTimespan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimespan.FormattingEnabled = true;
            this.comboBoxTimespan.Items.AddRange(new object[] {
            "D",
            "W",
            "M",
            "Y"});
            this.comboBoxTimespan.Location = new System.Drawing.Point(70, 144);
            this.comboBoxTimespan.MaxDropDownItems = 4;
            this.comboBoxTimespan.Name = "comboBoxTimespan";
            this.comboBoxTimespan.Size = new System.Drawing.Size(43, 21);
            this.comboBoxTimespan.TabIndex = 7;
            this.comboBoxTimespan.SelectedIndexChanged += new System.EventHandler(this.comboBoxTimespan_SelectedIndexChanged);
            this.comboBoxTimespan.Validated += new System.EventHandler(this.comboBoxTimespan_Validated);
            // 
            // textBoxExclude
            // 
            this.textBoxExclude.Location = new System.Drawing.Point(195, 195);
            this.textBoxExclude.Multiline = true;
            this.textBoxExclude.Name = "textBoxExclude";
            this.textBoxExclude.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExclude.Size = new System.Drawing.Size(158, 69);
            this.textBoxExclude.TabIndex = 8;
            this.textBoxExclude.Validated += new System.EventHandler(this.textBoxExclude_Validated);
            // 
            // textBoxInclude
            // 
            this.textBoxInclude.Location = new System.Drawing.Point(32, 195);
            this.textBoxInclude.Multiline = true;
            this.textBoxInclude.Name = "textBoxInclude";
            this.textBoxInclude.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInclude.Size = new System.Drawing.Size(157, 69);
            this.textBoxInclude.TabIndex = 9;
            this.textBoxInclude.Validated += new System.EventHandler(this.textBoxInclude_Validated);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(57, 292);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(75, 23);
            this.buttonPrev.TabIndex = 10;
            this.buttonPrev.Text = "Previous";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "ProcesDir";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "ArchiveDir";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Timespan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(207, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Exclude";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Include";
            // 
            // textBoxComments
            // 
            this.textBoxComments.Location = new System.Drawing.Point(70, 60);
            this.textBoxComments.Multiline = true;
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxComments.Size = new System.Drawing.Size(283, 35);
            this.textBoxComments.TabIndex = 17;
            this.textBoxComments.Validated += new System.EventHandler(this.textBoxComments_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Comment";
            // 
            // textBoxRetention
            // 
            this.textBoxRetention.Location = new System.Drawing.Point(256, 144);
            this.textBoxRetention.Name = "textBoxRetention";
            this.textBoxRetention.Size = new System.Drawing.Size(34, 20);
            this.textBoxRetention.TabIndex = 20;
            this.textBoxRetention.TextChanged += new System.EventHandler(this.textBoxRetention_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(201, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Retention";
            // 
            // comboBoxInterfaceName
            // 
            this.comboBoxInterfaceName.FormattingEnabled = true;
            this.comboBoxInterfaceName.Location = new System.Drawing.Point(70, 38);
            this.comboBoxInterfaceName.Name = "comboBoxInterfaceName";
            this.comboBoxInterfaceName.Size = new System.Drawing.Size(142, 21);
            this.comboBoxInterfaceName.TabIndex = 22;
            this.comboBoxInterfaceName.SelectionChangeCommitted += new System.EventHandler(this.comboBoxInterfaceName_SelectionChangeCommitted);
            this.comboBoxInterfaceName.Validated += new System.EventHandler(this.comboBoxInterfaceName_Validated);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(378, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAbout,
            this.toolStripSeparator1,
            this.toolStripLoad,
            this.toolStripSave,
            this.toolStripSeparator2,
            this.toolStripExit});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.loadToolStripMenuItem.Text = "File";
            // 
            // toolStripAbout
            // 
            this.toolStripAbout.Name = "toolStripAbout";
            this.toolStripAbout.Size = new System.Drawing.Size(107, 22);
            this.toolStripAbout.Text = "About";
            this.toolStripAbout.Click += new System.EventHandler(this.toolStripAbout_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
            // 
            // toolStripLoad
            // 
            this.toolStripLoad.Name = "toolStripLoad";
            this.toolStripLoad.Size = new System.Drawing.Size(107, 22);
            this.toolStripLoad.Text = "Load";
            this.toolStripLoad.Click += new System.EventHandler(this.toolStripMenuLoad_Click);
            // 
            // toolStripSave
            // 
            this.toolStripSave.Name = "toolStripSave";
            this.toolStripSave.Size = new System.Drawing.Size(107, 22);
            this.toolStripSave.Text = "Save";
            this.toolStripSave.Click += new System.EventHandler(this.toolStripSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(104, 6);
            // 
            // toolStripExit
            // 
            this.toolStripExit.Name = "toolStripExit";
            this.toolStripExit.Size = new System.Drawing.Size(107, 22);
            this.toolStripExit.Text = "Exit";
            this.toolStripExit.Click += new System.EventHandler(this.toolStripExit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(229, 36);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(23, 23);
            this.buttonAdd.TabIndex = 24;
            this.buttonAdd.Text = "+";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(251, 36);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(22, 23);
            this.buttonDel.TabIndex = 25;
            this.buttonDel.Text = "-";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 332);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBoxInterfaceName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxRetention);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxComments);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.textBoxInclude);
            this.Controls.Add(this.textBoxExclude);
            this.Controls.Add(this.comboBoxTimespan);
            this.Controls.Add(this.checkBoxRecursive);
            this.Controls.Add(this.textBoxArchiveDir);
            this.Controls.Add(this.textBoxProcesDir);
            this.Controls.Add(this.ParameterNo);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label ParameterNo;
        private System.Windows.Forms.TextBox textBoxProcesDir;
        private System.Windows.Forms.TextBox textBoxArchiveDir;
        private System.Windows.Forms.CheckBox checkBoxRecursive;
        private System.Windows.Forms.TextBox textBoxExclude;
        private System.Windows.Forms.TextBox textBoxInclude;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxRetention;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxInterfaceName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripLoad;
        private System.Windows.Forms.ToolStripMenuItem toolStripSave;
        private System.Windows.Forms.ToolStripMenuItem toolStripExit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.ToolStripMenuItem toolStripAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox comboBoxTimespan;

    }
}

