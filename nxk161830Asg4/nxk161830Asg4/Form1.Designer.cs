namespace nxk161830Asg4
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.textBoxSearchString = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelError = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelSearchCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewSearchResults = new System.Windows.Forms.ListView();
            this.LineNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LineText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search String";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(94, 18);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(467, 20);
            this.textBoxFileName.TabIndex = 1;
            this.textBoxFileName.TextChanged += new System.EventHandler(this.enableSearchButton);
            this.textBoxFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onEnterKeySearch);
            // 
            // textBoxSearchString
            // 
            this.textBoxSearchString.Location = new System.Drawing.Point(94, 50);
            this.textBoxSearchString.Name = "textBoxSearchString";
            this.textBoxSearchString.Size = new System.Drawing.Size(467, 20);
            this.textBoxSearchString.TabIndex = 3;
            this.textBoxSearchString.TextChanged += new System.EventHandler(this.enableSearchButton);
            this.textBoxSearchString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onEnterKeySearch);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(567, 15);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(567, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(16, 106);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(319, 15);
            this.labelError.TabIndex = 7;
            this.labelError.Text = "Please Select a File and Enter a string to search in the file";
            this.labelError.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(94, 77);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(467, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // labelSearchCount
            // 
            this.labelSearchCount.AutoSize = true;
            this.labelSearchCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSearchCount.Location = new System.Drawing.Point(531, 106);
            this.labelSearchCount.Name = "labelSearchCount";
            this.labelSearchCount.Size = new System.Drawing.Size(111, 15);
            this.labelSearchCount.TabIndex = 9;
            this.labelSearchCount.Text = "No Matches Found";
            this.labelSearchCount.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Progress Bar";
            // 
            // listViewSearchResults
            // 
            this.listViewSearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LineNo,
            this.LineText});
            this.listViewSearchResults.Cursor = System.Windows.Forms.Cursors.Default;
            this.listViewSearchResults.Location = new System.Drawing.Point(19, 124);
            this.listViewSearchResults.Name = "listViewSearchResults";
            this.listViewSearchResults.Size = new System.Drawing.Size(623, 427);
            this.listViewSearchResults.TabIndex = 11;
            this.listViewSearchResults.UseCompatibleStateImageBehavior = false;
            this.listViewSearchResults.View = System.Windows.Forms.View.Details;
            // 
            // LineNo
            // 
            this.LineNo.Text = "Line No.";
            this.LineNo.Width = 63;
            // 
            // LineText
            // 
            this.LineText.Text = "Line Text";
            this.LineText.Width = 537;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 575);
            this.Controls.Add(this.listViewSearchResults);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelSearchCount);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textBoxSearchString);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.TextBox textBoxSearchString;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelSearchCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewSearchResults;
        private System.Windows.Forms.ColumnHeader LineNo;
        private System.Windows.Forms.ColumnHeader LineText;
    }
}

