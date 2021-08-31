using System;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Foundation;
using PdfViewer;
using PdfViewer.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PdfView), typeof(PdfViewRenderer))]
namespace PdfViewer.iOS
{
    public class PdfViewRenderer : ViewRenderer<PdfView, PdfKit.PdfView>
    {
        public PdfViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PdfView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var pdfView =
                    new PdfKit.PdfView
                    {
                        AutoScales = true,
                    };

                SetNativeControl(pdfView);

                Element.LoadStream += Element_LoadStream;
                Element.LoadFile += Element_LoadFile;
            }
        }

        private void Element_LoadFile(object sender, string e)
        {
            LoadFromFile(e);
        }

        private async void Element_LoadStream(object sender, System.IO.Stream e)
        {
            await LoadFromStream(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        private async Task LoadFromStream(Stream stream)
        {
            if (Control != null && stream != null && stream != System.IO.Stream.Null)
            {
                using(var nsData = NSData.FromStream(stream))
                {
                    Control.Document = new PdfKit.PdfDocument(nsData);
                    stream?.Dispose();
                }
            }
        }

        private void LoadFromFile(string filePath)
        {
            if (Control != null && !string.IsNullOrEmpty(filePath))
            {
                var url = NSUrl.FromFilename(filePath);
                Control.Document = new PdfKit.PdfDocument(url);
            }
        }
    }
}
