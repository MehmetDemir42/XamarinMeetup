using MeetUp_LocalStorage.Views;
using Xamarin.Forms;

namespace MeetUp_LocalStorage
{
    public partial class App : Application
    {
        public static string FolderName => "MeetUp";
        public static string StringFile => "Participants";

        public App()
        {
            // The root page of your application

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}