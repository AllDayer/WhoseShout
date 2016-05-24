using MvvmCross.Core.ViewModels;

namespace WhoseShout.Core.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
				RaisePropertyChanged(nameof(Name));
            }
        }

		private string m_Email;
		public string Email
		{
			get
			{
				return m_Email;
			}
			set
			{
				m_Email = value;
				RaisePropertyChanged(nameof(Email));
			}
		}

        //protected override void InitFromBundle(IMvxBundle parameters)
        //{
        //    if (parameters.Data.ContainsKey(nameof(Name)))
        //    {
        //        Name = parameters.Data[nameof(Name)];
        //    }
        //    base.InitFromBundle(parameters);
        //}

        public void Init(string name, string email)
        {
            Name = name;
			Email = email;
        }
    }
}