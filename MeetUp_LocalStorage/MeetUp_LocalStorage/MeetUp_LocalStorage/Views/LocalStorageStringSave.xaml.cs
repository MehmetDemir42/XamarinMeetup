using MeetUp_LocalStorage.Models;
using MeetUp_LocalStorage.Services;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetUp_LocalStorage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalStorageStringSave : ContentPage
    {
        private readonly LocalFileManager _lfm;

        public LocalStorageStringSave()
        {
            InitializeComponent();
            _lfm = new LocalFileManager();
        }

        private async void BtnSave_OnClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                var participant = new Participant
                {
                    Firstname = txtName.Text,
                    Lastname = txtSurname.Text
                };
                var jsonValue = JsonConvert.SerializeObject(participant);
                var retVal = await _lfm.SaveFileFromString(jsonValue, "txt", App.FolderName, App.StringFile);
                //retVal size pathi dönmektedir.
                if (!string.IsNullOrWhiteSpace(retVal))
                {
                    await DisplayAlert("Başarılı", "Kayıt Başarılı Oldu", "Tamam");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Hata", "Kayıt sırasında bir hata oluştu.", "Tamam");
                }
            }
        }
    }
}