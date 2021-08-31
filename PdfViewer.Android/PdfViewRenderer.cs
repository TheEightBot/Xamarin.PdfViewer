using System;
using System.ComponentModel;
using System.Net.Http;
using Android.Content;
using Android.Media;
using Android.Views;
using Com.Github.Barteksc.Pdfviewer;
using Com.Github.Barteksc.Pdfviewer.Util;
using Org.Apache.Http.Client.Params;
using PdfViewer;
using PdfViewer.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(PdfView), typeof(PdfViewRenderer))]
namespace PdfViewer.Droid
{
    public class PdfViewRenderer : ViewRenderer<PdfView, PDFView>, Com.Github.Barteksc.Pdfviewer.Listener.IOnLoadCompleteListener
    {
        public PdfViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PdfView> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement != null)
            {
                e.OldElement.LoadStream -= Element_LoadStream;
                e.OldElement.LoadFile -= Element_LoadFile;
            }

            if(e.NewElement != null)
            {
                var pdfView = new PDFView(this.Context, null);
                pdfView.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

                SetNativeControl(pdfView);

                e.NewElement.LoadStream += Element_LoadStream;
                e.NewElement.LoadFile += Element_LoadFile;
            }
        }

        private void Element_LoadFile(object sender, string e)
        {
            LoadFromFile(e);
        }

        private void Element_LoadStream(object sender, System.IO.Stream e)
        {
            LoadFromStream(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        private void LoadFromStream(System.IO.Stream stream)
        {
            if(Control != null && stream != null && stream != System.IO.Stream.Null)
            {
                LoadFromConfigurator(Control.FromStream(stream));
            }
        }

        private void LoadFromFile(string filePath)
        {
            if (Control != null && !string.IsNullOrEmpty(filePath))
            {
                LoadFromConfigurator(Control.FromFile(new Java.IO.File(filePath)));
            }
        }

        private void LoadFromConfigurator(PDFView.Configurator configurator)
        {
            configurator
                .EnableAntialiasing(true)
                .PageFitPolicy(FitPolicy.Width)
                .PageSnap(false)
                .Spacing(Element.PageSpacing)
                .EnableSwipe(true)
                .PageFling(false)
                .OnLoad(this)
                .Load();
        }

        public void LoadComplete(int p0)
        {
        }
    }
}
