using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetUp_LocalStorage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalStorageUrlView : ContentPage
    {
        public LocalStorageUrlView(string imgURL)
        {
            InitializeComponent();
            this.img.Source = imgURL;
        }
    }
}