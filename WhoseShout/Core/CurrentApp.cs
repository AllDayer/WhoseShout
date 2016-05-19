using System;
using System.Collections.Generic;

using WhoseShout.Helpers;
using WhoseShout.Services;

namespace WhoseShout.Core
{

    public class CurrentApp
    {
        public static WhoseShout.Core.ViewModels.AppContext AppContext { get; set; }

        public static WhoseShout.Core.ViewModels.MainViewModel MainViewModel { get; set; }

        public static void StartUp()
        {
            AppContext = new WhoseShout.Core.ViewModels.AppContext();
            ServiceLocator.Instance.Add<IService, MockService>();
        }
    }
}