namespace ObjectRecognitionApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.pictureBoxImage = new System.Windows.Forms.PictureBox();
        this.buttonLoadImage = new System.Windows.Forms.Button();
        this.buttonDetect = new System.Windows.Forms.Button();
        this.buttonSaveResult = new System.Windows.Forms.Button();
        this.listBoxResults = new System.Windows.Forms.ListBox();
        this.labelTotalObjects = new System.Windows.Forms.Label();
        this.labelStatus = new System.Windows.Forms.Label();
        this.progressBar = new System.Windows.Forms.ProgressBar();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
        this.SuspendLayout();
        // 
        // pictureBoxImage
        // 
        this.pictureBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBoxImage.Location = new System.Drawing.Point(12, 12);
        this.pictureBoxImage.Name = "pictureBoxImage";
        this.pictureBoxImage.Size = new System.Drawing.Size(600, 400);
        this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pictureBoxImage.TabIndex = 0;
        this.pictureBoxImage.TabStop = false;
        // 
        // buttonLoadImage
        // 
        this.buttonLoadImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
        this.buttonLoadImage.Location = new System.Drawing.Point(630, 12);
        this.buttonLoadImage.Name = "buttonLoadImage";
        this.buttonLoadImage.Size = new System.Drawing.Size(150, 40);
        this.buttonLoadImage.TabIndex = 1;
        this.buttonLoadImage.Text = "📂 Resim Yükle";
        this.buttonLoadImage.UseVisualStyleBackColor = true;
        this.buttonLoadImage.Click += new System.EventHandler(this.buttonLoadImage_Click);
        // 
        // buttonDetect
        // 
        this.buttonDetect.Enabled = false;
        this.buttonDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
        this.buttonDetect.Location = new System.Drawing.Point(630, 58);
        this.buttonDetect.Name = "buttonDetect";
        this.buttonDetect.Size = new System.Drawing.Size(150, 40);
        this.buttonDetect.TabIndex = 2;
        this.buttonDetect.Text = "▶️ Tespit Et";
        this.buttonDetect.UseVisualStyleBackColor = true;
        this.buttonDetect.Click += new System.EventHandler(this.buttonDetect_Click);
        // 
        // buttonSaveResult
        // 
        this.buttonSaveResult.Enabled = false;
        this.buttonSaveResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
        this.buttonSaveResult.Location = new System.Drawing.Point(630, 104);
        this.buttonSaveResult.Name = "buttonSaveResult";
        this.buttonSaveResult.Size = new System.Drawing.Size(150, 40);
        this.buttonSaveResult.TabIndex = 3;
        this.buttonSaveResult.Text = "💾 Sonucu Kaydet";
        this.buttonSaveResult.UseVisualStyleBackColor = true;
        this.buttonSaveResult.Click += new System.EventHandler(this.buttonSaveResult_Click);
        // 
        // listBoxResults
        // 
        this.listBoxResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
        this.listBoxResults.FormattingEnabled = true;
        this.listBoxResults.ItemHeight = 15;
        this.listBoxResults.Location = new System.Drawing.Point(630, 180);
        this.listBoxResults.Name = "listBoxResults";
        this.listBoxResults.Size = new System.Drawing.Size(150, 184);
        this.listBoxResults.TabIndex = 4;
        // 
        // labelTotalObjects
        // 
        this.labelTotalObjects.AutoSize = true;
        this.labelTotalObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
        this.labelTotalObjects.Location = new System.Drawing.Point(630, 162);
        this.labelTotalObjects.Name = "labelTotalObjects";
        this.labelTotalObjects.Size = new System.Drawing.Size(120, 15);
        this.labelTotalObjects.TabIndex = 5;
        this.labelTotalObjects.Text = "Tespit Edilen: 0";
        // 
        // labelStatus
        // 
        this.labelStatus.AutoSize = true;
        this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
        this.labelStatus.Location = new System.Drawing.Point(12, 420);
        this.labelStatus.Name = "labelStatus";
        this.labelStatus.Size = new System.Drawing.Size(41, 13);
        this.labelStatus.TabIndex = 6;
        this.labelStatus.Text = "Hazır...";
        // 
        // progressBar
        // 
        this.progressBar.Location = new System.Drawing.Point(12, 440);
        this.progressBar.Name = "progressBar";
        this.progressBar.Size = new System.Drawing.Size(768, 23);
        this.progressBar.TabIndex = 7;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 480);
        this.Controls.Add(this.progressBar);
        this.Controls.Add(this.labelStatus);
        this.Controls.Add(this.labelTotalObjects);
        this.Controls.Add(this.listBoxResults);
        this.Controls.Add(this.buttonSaveResult);
        this.Controls.Add(this.buttonDetect);
        this.Controls.Add(this.buttonLoadImage);
        this.Controls.Add(this.pictureBoxImage);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Nesne Tanıma Uygulaması - Emgu CV";
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBoxImage;
    private System.Windows.Forms.Button buttonLoadImage;
    private System.Windows.Forms.Button buttonDetect;
    private System.Windows.Forms.Button buttonSaveResult;
    private System.Windows.Forms.ListBox listBoxResults;
    private System.Windows.Forms.Label labelTotalObjects;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.ProgressBar progressBar;
}
