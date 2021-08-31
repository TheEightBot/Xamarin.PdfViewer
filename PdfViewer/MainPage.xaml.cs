using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PdfViewer
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            EmbeddedResourceLoader.LoadAssembly (typeof (MainPage).Assembly);

            var stream = EmbeddedResourceLoader.Load ("Sample.pdf");

            //var httpClient = new HttpClient();

            //var stream = await httpClient.GetStreamAsync("https://drive.google.com/uc?export=download&id=1eDAE8s7LhA4mPkSNil1Kzi4jpnDNCMp2");

            //var tmpFile = Path.Combine(FileSystem.CacheDirectory, Path.GetTempFileName());

            var tmpFile = Path.GetTempFileName();

            using(var fs = new FileStream(tmpFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                await stream.CopyToAsync(fs);
                await fs.FlushAsync();
            }

            pdfView.LoadDocumentFromFile(tmpFile);            

            //pdfView.LoadDocumentFromStream(stream);
        }
    }
}
