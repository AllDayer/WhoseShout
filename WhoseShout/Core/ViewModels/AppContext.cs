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

        private IList<FriendItem> m_Friends = new List<FriendItem>();
        public IList<FriendItem> Friends
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