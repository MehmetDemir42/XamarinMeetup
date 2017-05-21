using MeetUp_LocalStorage.Models;
using MeetUp_LocalStorage.Services;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetUp_LocalStorage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalStorageStringGet : ContentPage
    {
        private readonly LocalFileManager _lfm;

        public LocalStorageStringGet()
        {
            InitializeComponent();
            _lfm = new LocalFileManager();
        }

        private async void Get_OnClicked(object sender, EventArgs e)
        {
            var retValJson = await _lfm.GetSavedFile(App.FolderName, App.StringFile, ".txt");
            if (!string.IsNullOrWhiteSpace(retValJson))
            {
                var retVal = JsonConvert.DeserializeObject<Participant>(retValJson);
                lblResult.Text = retVal.Firstname + " " + retVal.Lastname;
            }
            else
            {
                lblResult.Text = "Veri getirilemedi.";
            }
        }
    }
}