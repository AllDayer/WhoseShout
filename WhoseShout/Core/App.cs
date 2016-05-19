using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using WhoseShout.Services;
using WhoseShout.Core.ViewModels;

namespace WhoseShout.Core
{
    public class App : MvxApplication
    {
        public App()
        {
            Mvx.RegisterType<IService, MockService>();
            //Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
            RegisterAppStart<MainViewModel>();

            CurrentApp.StartUp();
        }
    }
}