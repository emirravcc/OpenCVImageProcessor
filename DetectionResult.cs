using System.Drawing;

namespace ObjectRecognitionApp
{
    /// <summary>
    /// Nesne tespit sonucunu temsil eden sınıf
    /// </summary>
    public class DetectionResult
    {
        /// <summary>
        /// Tespit edilen nesnenin sınıf adı (örn: "person", "car", "bottle")
        /// </summary>
        public string ClassName { get; set; } = "";
        
        /// <summary>
        /// Tespit güven skoru (0.0 - 1.0 arası)
        /// </summary>
        public float Confidence { get; set; }
        
        /// <summary>
        /// Tespit edilen nesnenin koordinatları (x, y, width, height)
        /// </summary>
        public Rectangle BoundingBox { get; set; }
        
        /// <summary>
        /// Tespit edilen nesnenin merkez noktası
        /// </summary>
        public Point Center => new Point(
            BoundingBox.X + BoundingBox.Width / 2,
            BoundingBox.Y + BoundingBox.Height / 2
        );
        
        /// <summary>
        /// Tespit sonucunun string temsilini döndürür
        /// </summary>
        public override string ToString()
        {
            return $"{ClassName} ({Confidence:P1}) - [{BoundingBox.X}, {BoundingBox.Y}, {BoundingBox.Width}, {BoundingBox.Height}]";
        }
    }
}