using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entries.Services;
using Entries.Views;

namespace Entries
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            
            var page = new MainPage();

            MainPage = page;

   
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private bool _NavigationBusy;
        public bool NavigationBusy
        {
            get { return _NavigationBusy; }
            set
            {
                if (_NavigationBusy != value)
                {
                    _NavigationBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public static new App Current
        {
            get
            {
                return (App)Application.Current;
            }
        }

        public static void OpenPage(Page page)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (App.Current.NavigationBusy)
                    return;

                App.Current.NavigationBusy = true;
                // Update the UI
                await App.NavigateToAsync(page);

                App.Current.NavigationBusy = false;
            });
        }

        public static async Task NavigateToAsync(Page page, bool closeFlyout = false)
        {
            if (closeFlyout)
            {
                //todo presented = false
                //await Shell.CloseFlyoutAsync();
            }
            await NavigationRoot.Navigation.PushAsync(page).ConfigureAwait(false);
        }

        private static NavigableElement navigationRoot;
        public static NavigableElement NavigationRoot
        {
            get { return navigationRoot; }
            set => navigationRoot = value;
        }

    }
}
