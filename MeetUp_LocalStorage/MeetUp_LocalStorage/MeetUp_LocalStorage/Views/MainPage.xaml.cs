using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetUp_LocalStorage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnKayit_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LocalStorageStringSave());
        }

        private void BtnGetir_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LocalStorageStringGet());
        }

        private void BtnStream_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LocalStorageUrlSave());
        }
    }
}