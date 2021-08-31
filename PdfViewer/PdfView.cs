using System;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace PdfViewer
{

    public class PdfView : ContentView
    {
        internal event EventHandler<Stream> LoadStream;
        internal event EventHandler<string> LoadFile;

        public static BindableProperty PageSpacingProperty =
            BindableProperty.Create(nameof(PageSpacing), typeof(int), typeof(PdfView), default(int));

        public int PageSpacing
        {
            get => (int)GetValue(PageSpacingProperty);
            set => SetValue(PageSpacingProperty, value);
        }

        public void LoadDocumentFromStream(Stream stream)
        {
            LoadStream?.Invoke(this, stream);
        }

        public void LoadDocumentFromFile(string filePath)
        {
            LoadFile?.Invoke(this, filePath);
        }
    }
}
