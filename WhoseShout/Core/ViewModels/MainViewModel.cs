using System;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;

namespace WhoseShout.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        readonly Type[] _menuItemTypes = {
            typeof(ProfileViewModel),
            typeof(SettingsViewModel),
        };

        public IEnumerable<string> MenuItems { get; private set; } = new[] { "My List", "My Settings" };

        public void ShowDefaultMenuItem()
        {
            NavigateTo(0);
        }

        public void NavigateTo(int position)
        {
            ShowViewModel(_menuItemTypes[position]);
        }

        public void NavigateTo(int position, Dictionary<string, string> parameters)
        {
            ShowViewModel(_menuItemTypes[position], parameters);
        }
    }

    public class MenuItem : Tuple<string, Type>
    {
        public MenuItem(string displayName, Type viewModelType)
            : base(displayName, viewModelType)
        { }

        public string DisplayName
        {
            get { return Item1; }
        }

        public Type ViewModelType
        {
            get { return Item2; }
        }
    }
}