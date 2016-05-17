using MvvmCross.Core.ViewModels;

namespace WhoseShout.Core.ViewModels
{
    public class ProfileViewModel : MvxViewModel
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
                RaisePropertyChanged(() => Name);
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

        public void Init(string name)
        {
            Name = name;
        }
    }
}