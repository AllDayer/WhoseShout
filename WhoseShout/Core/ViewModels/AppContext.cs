using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WhoseShout.Models;

namespace WhoseShout.Core.ViewModels
{
    public class AppContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IList<Friend> m_Friends = new List<Friend>();
        public IList<Friend> Friends
        {
            get
            {
                return m_Friends;
            }
            set
            {
                m_Friends = value;
                OnPropertyChanged(nameof(Friends));
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}