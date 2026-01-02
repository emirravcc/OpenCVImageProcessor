using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ObjectRecognitionApp
{
    /// <summary>
    /// System.Drawing kullanarak basit nesne tespiti yapan sınıf
    /// </summary>
    public class ObjectDetector
    {
        private string[] _classNames;
        private readonly float _confidenceThreshold = 0.5f;
        private readonly float _nmsThreshold = 0.4f;
        
        /// <summary>
        /// Son tespit sonuçları
        /// </summary>
        public List<DetectionResult> LastResults { get; private set; } = new List<DetectionResult>();
        
        /// <summary>
        /// ObjectDetector sınıfının constructor'ı
        /// </summary>
        public ObjectDetector()
        {
            InitializeModel();
        }
        
        /// <summary>
        /// Model ve sınıf isimlerini yükler
        /// </summary>
        private void InitializeModel()
        {
            try
            {
                // Model dosyalarının yolları
                string modelsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models");
                string namesPath = Path.Combine(modelsPath, "coco.names");
                
                // Model dosyalarının varlığını kontrol et
                if (!Directory.Exists(modelsPath))
                {
                    Directory.CreateDirectory(modelsPath);
                }
                
                // Sınıf isimlerini yükle
                LoadClassNames(namesPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Model yüklenirken hata oluştu: {ex.Message}");
                // Varsayılan sınıf isimleri
                _classNames = new string[] { "person", "car", "bicycle", "dog", "cat", "bird", "horse", "sheep", "cow", "elephant" };
            }
        }
        
        /// <summary>
        /// Sınıf isimlerini dosyadan yükler
        /// </summary>
        private void LoadClassNames(string namesPath)
        {
            if (File.Exists(namesPath))
            {
                _classNames = File.ReadAllLines(namesPath);
            }
            else
            {
                // Varsayılan COCO sınıf isimleri
                _classNames = new string[]
                {
                    "person", "bicycle", "car", "motorbike", "aeroplane", "bus", "train", "truck", "boat",
                    "traffic light", "fire hydrant", "stop sign", "parking meter", "bench", "bird", "cat",
                    "dog", "horse", "sheep", "cow", "elephant", "bear", "zebra", "giraffe", "backpack",
                    "umbrella", "handbag", "tie", "suitcase", "frisbee", "skis", "snowboard", "sports ball",
                    "kite", "baseball bat", "baseball glove", "skateboard", "surfboard", "tennis racket",
                    "bottle", "wine glass", "cup", "fork", "knife", "spoon", "bowl", "banana", "apple",
                    "sandwich", "orange", "broccoli", "carrot", "hot dog", "pizza", "donut", "cake",
                    "chair", "sofa", "pottedplant", "bed", "diningtable", "toilet", "tvmonitor", "laptop",
                    "mouse", "remote", "keyboard", "cell phone", "microwave", "oven", "toaster", "sink",
                    "refrigerator", "book", "clock", "vase", "scissors", "teddy bear", "hair drier", "toothbrush"
                };
                
                // Dosyayı oluştur
                try
                {
                    File.WriteAllLines(namesPath, _classNames);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Sınıf isimleri dosyası oluşturulamadı: {ex.Message}");
                }
            }
        }
        
        /// <summary>
        /// Verilen resimde nesne tespiti yapar (demo modda)
        /// </summary>
        public List<DetectionResult> DetectObjects(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Resim dosyası bulunamadı: {imagePath}");
            
            try
            {
                // Resmi yükle
                using (Bitmap image = new Bitmap(imagePath))
                {
                    return DetectObjectsInBitmap(image);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Nesne tespiti sırasında hata: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Bitmap nesnesinde nesne tespiti yapar (demo modda)
        /// </summary>
        private List<DetectionResult> DetectObjectsInBitmap(Bitmap image)
        {
            var results = new List<DetectionResult>();
            
            try
            {
                // Demo modda rastgele nesneler tespit et
                results = CreateDemoResults(new Size(image.Width, image.Height));
                
                LastResults = results;
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Tespit hatası: {ex.Message}");
                return new List<DetectionResult>();
            }
        }
        
        /// <summary>
        /// Demo amaçlı rastgele tespit sonuçları oluşturur
        /// </summary>
        private List<DetectionResult> CreateDemoResults(Size imageSize)
        {
            var results = new List<DetectionResult>();
            var random = new Random();
            
            // 2-5 arası rastgele nesne sayısı
            int objectCount = random.Next(2, 6);
            
            for (int i = 0; i < objectCount; i++)
            {
                // Rastgele sınıf seç
                string className = _classNames[random.Next(_classNames.Length)];
                
                // Rastgele konum ve boyut
                int width = random.Next(50, Math.Min(200, imageSize.Width / 3));
                int height = random.Next(50, Math.Min(200, imageSize.Height / 3));
                int x = random.Next(0, Math.Max(1, imageSize.Width - width));
                int y = random.Next(0, Math.Max(1, imageSize.Height - height));
                
                // Rastgele güven skoru
                float confidence = (float)(random.NextDouble() * 0.4 + 0.6); // 0.6-1.0 arası
                
                results.Add(new DetectionResult
                {
                    ClassName = className,
                    Confidence = confidence,
                    BoundingBox = new Rectangle(x, y, width, height)
                });
            }
            
            return results;
        }
        
        /// <summary>
        /// Tespit sonuçlarını resim üzerine çizer
        /// </summary>
        public Bitmap DrawDetections(string imagePath, List<DetectionResult> detections = null)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Resim dosyası bulunamadı: {imagePath}");
            
            detections = detections ?? LastResults;
            
            using (Bitmap originalImage = new Bitmap(imagePath))
            {
                Bitmap resultImage = new Bitmap(originalImage);
                
                using (Graphics g = Graphics.FromImage(resultImage))
                {
                    // Çizim ayarları
                    var font = new Font("Arial", 12, FontStyle.Bold);
                    var brush = new SolidBrush(Color.White);
                    var backgroundBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0));
                    
                    foreach (var detection in detections)
                    {
                        // Rastgele renk seç
                        var random = new Random(detection.ClassName.GetHashCode());
                        var penColor = Color.FromArgb(255, random.Next(100, 255), random.Next(100, 255), random.Next(100, 255));
                        var pen = new Pen(penColor, 3);
                        
                        // Bounding box çiz
                        g.DrawRectangle(pen, detection.BoundingBox);
                        
                        // Label metni
                        string label = $"{detection.ClassName} ({detection.Confidence:P0})";
                        var textSize = g.MeasureString(label, font);
                        
                        // Label arka planı
                        var labelRect = new RectangleF(
                            detection.BoundingBox.X,
                            detection.BoundingBox.Y - textSize.Height - 5,
                            textSize.Width + 10,
                            textSize.Height + 5
                        );
                        
                        g.FillRectangle(backgroundBrush, labelRect);
                        
                        // Label metni
                        g.DrawString(label, font, brush, 
                            detection.BoundingBox.X + 5, 
                            detection.BoundingBox.Y - textSize.Height);
                        
                        pen.Dispose();
                    }
                    
                    font.Dispose();
                    brush.Dispose();
                    backgroundBrush.Dispose();
                }
                
                return resultImage;
            }
        }
        
        /// <summary>
        /// Tespit sonuçlarını JSON formatında kaydeder
        /// </summary>
        public void SaveResultsAsJson(string outputPath, List<DetectionResult> detections = null)
        {
            detections = detections ?? LastResults;
            
            var jsonData = new
            {
                Timestamp = DateTime.Now,
                DetectionCount = detections.Count,
                Detections = detections.Select(d => new
                {
                    ClassName = d.ClassName,
                    Confidence = Math.Round(d.Confidence, 3),
                    BoundingBox = new
                    {
                        X = d.BoundingBox.X,
                        Y = d.BoundingBox.Y,
                        Width = d.BoundingBox.Width,
                        Height = d.BoundingBox.Height
                    }
                }).ToArray()
            };
            
            string json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            File.WriteAllText(outputPath, json);
        }
        
        /// <summary>
        /// Tespit sonuçlarını TXT formatında kaydeder
        /// </summary>
        public void SaveResultsAsTxt(string outputPath, List<DetectionResult> detections = null)
        {
            detections = detections ?? LastResults;
            
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine($"Nesne Tespiti Sonuçları - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($"Toplam tespit edilen nesne sayısı: {detections.Count}");
                writer.WriteLine(new string('-', 50));
                
                for (int i = 0; i < detections.Count; i++)
                {
                    var detection = detections[i];
                    writer.WriteLine($"{i + 1}. {detection.ClassName}");
                    writer.WriteLine($"   Güven: {detection.Confidence:P1}");
                    writer.WriteLine($"   Konum: X={detection.BoundingBox.X}, Y={detection.BoundingBox.Y}");
                    writer.WriteLine($"   Boyut: W={detection.BoundingBox.Width}, H={detection.BoundingBox.Height}");
                    writer.WriteLine();
                }
            }
        }
        
        /// <summary>
        /// Kaynakları temizler
        /// </summary>
        public void Dispose()
        {
            // System.Drawing kullandığımız için özel temizlik gerekmiyor
        }
    }
}