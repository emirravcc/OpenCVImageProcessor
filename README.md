# 🖼️ OpenCV Image Processor (C# WinForms)

Bu proje, **OpenCV** kütüphanesini kullanarak dijital görüntüler üzerinde gerçek zamanlı filtreleme, dönüştürme ve analiz işlemleri gerçekleştiren bir masaüstü uygulamasıdır. Proje, görüntü işleme algoritmalarının temel mantığını ve C# ile entegrasyonunu göstermektedir.

## 🚀 Proje Özellikleri
* **Görüntü Filtreleme:** Resimleri Gri tonlamaya (Grayscale) çevirme, Blur (Bulanıklaştırma) ve Keskinleştirme.
* **Kenar Algılama:** Canny Edge Detection algoritması ile resimdeki nesnelerin hatlarını belirleme.
* **Geometrik İşlemler:** Resmi döndürme (Rotation), boyutlandırma (Resizing) ve aynalama.
* **Renk Uzayı Dönüşümleri:** RGB'den HSV veya Lab renk uzaylarına geçiş.

## 🛠️ Teknik Detaylar
* **Dil:** C#
* **Kütüphane:** OpenCVSharp4 (OpenCV'nin .NET sarmalayıcısı)
* **Platform:** Windows Forms (.NET Framework / .NET Core)
* **Algoritmalar:** Gaussian Blur, Canny Edge, Median Filter ve Renk Eşikleme (Thresholding).

## 🎮 Nasıl Kullanılır?
1. Uygulamayı çalıştırın.
2. "Dosya Aç" (Open File) butonu ile bilgisayarınızdan bir resim seçin.
3. Yan menüdeki filtrelerden (Gri, Canny, Blur vb.) birine tıklayın.
4. İşlenmiş görüntüyü anlık olarak ekranda görün ve isterseniz "Kaydet" butonu ile bilgisayarınıza kaydedin.

## 📁 Kurulum
Projenin çalışması için `NuGet Package Manager` üzerinden **OpenCvSharp4** ve **OpenCvSharp4.runtime.win** paketlerinin yüklü olması gerekmektedir. Proje dosyalarını açtıktan sonra paketler otomatik olarak geri yüklenecektir.

## 👨‍💻 Geliştirici
* **Emir**
