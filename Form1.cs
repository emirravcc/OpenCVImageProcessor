using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ObjectRecognitionApp;

public partial class Form1 : Form
{
    private string currentImagePath = "";
    private ObjectDetector detector;
    
    public Form1()
    {
        InitializeComponent();
        InitializeDetector();
        UpdateStatus("Hazır...");
    }
    
    private void InitializeDetector()
    {
        try
        {
            detector = new ObjectDetector();
            UpdateStatus("Model yüklendi.");
        }
        catch (Exception ex)
        {
            UpdateStatus($"Model yüklenirken hata: {ex.Message}");
            MessageBox.Show($"Model yüklenirken hata oluştu: {ex.Message}", "Hata", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void buttonLoadImage_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp|Tüm Dosyalar|*.*";
            openFileDialog.Title = "Resim Seçin";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentImagePath = openFileDialog.FileName;
                    pictureBoxImage.Image = Image.FromFile(currentImagePath);
                    buttonDetect.Enabled = true;
                    buttonSaveResult.Enabled = false;
                    listBoxResults.Items.Clear();
                    labelTotalObjects.Text = "Tespit Edilen: 0";
                    UpdateStatus($"Resim yüklendi: {Path.GetFileName(currentImagePath)}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Resim yüklenirken hata oluştu: {ex.Message}", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatus("Resim yükleme hatası.");
                }
            }
        }
    }
    
    private void buttonDetect_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(currentImagePath) || detector == null)
            return;
            
        try
        {
            UpdateStatus("Nesne tespiti başlatılıyor...");
            progressBar.Style = ProgressBarStyle.Marquee;
            
            // Nesne tespiti işlemi
            var results = detector.DetectObjects(currentImagePath);
            
            // Sonuçları göster
            DisplayResults(results);
            
            // İşlenmiş resmi göster
            var processedImage = detector.DrawDetections(currentImagePath, results);
            pictureBoxImage.Image = processedImage;
            
            buttonSaveResult.Enabled = true;
            progressBar.Style = ProgressBarStyle.Blocks;
            UpdateStatus($"Tespit tamamlandı. {results.Count} nesne bulundu.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Nesne tespiti sırasında hata oluştu: {ex.Message}", "Hata", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            UpdateStatus("Tespit hatası.");
            progressBar.Style = ProgressBarStyle.Blocks;
        }
    }
    
    private void buttonSaveResult_Click(object sender, EventArgs e)
    {
        if (detector?.LastResults == null || detector.LastResults.Count == 0)
        {
            MessageBox.Show("Kaydedilecek sonuç bulunamadı.", "Uyarı", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = "JSON Dosyası|*.json|Metin Dosyası|*.txt";
            saveFileDialog.Title = "Sonuçları Kaydet";
            saveFileDialog.FileName = $"detection_results_{DateTime.Now:yyyyMMdd_HHmmss}";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();
                    
                    if (extension == ".json")
                    {
                        detector.SaveResultsAsJson(saveFileDialog.FileName);
                    }
                    else if (extension == ".txt")
                    {
                        detector.SaveResultsAsTxt(saveFileDialog.FileName);
                    }
                    else
                    {
                        throw new ArgumentException("Desteklenen formatlar: .json, .txt");
                    }
                    
                    UpdateStatus($"Sonuçlar kaydedildi: {Path.GetFileName(saveFileDialog.FileName)}");
                    MessageBox.Show("Sonuçlar başarıyla kaydedildi.", "Bilgi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sonuçlar kaydedilirken hata oluştu: {ex.Message}", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatus("Kaydetme hatası.");
                }
            }
        }
    }
    
    private void DisplayResults(List<DetectionResult> results)
    {
        listBoxResults.Items.Clear();
        
        var objectCounts = new Dictionary<string, int>();
        
        foreach (var result in results)
        {
            listBoxResults.Items.Add($"{result.ClassName}: {result.Confidence:P1}");
            
            if (objectCounts.ContainsKey(result.ClassName))
                objectCounts[result.ClassName]++;
            else
                objectCounts[result.ClassName] = 1;
        }
        
        labelTotalObjects.Text = $"Tespit Edilen: {results.Count}";
        
        // Nesne sayılarını da göster
        foreach (var kvp in objectCounts)
        {
            listBoxResults.Items.Add($"  {kvp.Key}: {kvp.Value} adet");
        }
    }
    
    private void UpdateStatus(string message)
    {
        labelStatus.Text = message;
        Application.DoEvents();
    }
}
