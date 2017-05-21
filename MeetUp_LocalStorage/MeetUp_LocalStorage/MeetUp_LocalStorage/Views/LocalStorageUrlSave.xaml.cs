using MeetUp_LocalStorage.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetUp_LocalStorage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalStorageUrlSave : ContentPage
    {
        private readonly LocalFileManager _lfm;

        public LocalStorageUrlSave()
        {
            InitializeComponent();
            _lfm = new LocalFileManager();
            txtURL.Text =
                "https://www.xamarin.com/content/images/pages/branding/assets/xamagon.png";
        }

        private async void BtnSave_OnClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtURL.Text))
            {
                var retVal = await _lfm.SaveFileFromUrl(txtURL.Text, App.FolderName);
                if (!string.IsNullOrWhiteSpace(retVal))
                {
                    var answer = await DisplayAlert("Başarılı", "Kayıt başarılı görüntülemek ister misiniz ?", "Evet", "Hayır");
                    if (answer)
                    {
                        await Navigation.PushAsync(new LocalStorageUrlView(retVal));
                    }
                }
            }
        }
    }
}