//using System.Collections.Generic;
//using System.Linq;
//using Android.App;
//using Android.Views;
//using Android.Widget;
//using Java.Lang;
//using Object = Java.Lang.Object;
//using WhoseShout.Models;

//namespace WhoseShout.Resources.Adapters
//{

//    public class FriendAdapter : BaseAdapter<User>, IFilterable
//    {
//        private List<User> m_Users;
//        private List<User> m_OriginalUsers;

//        public override User this[int position]
//        {
//            get
//            {
//                return m_Users[position];
//            }
//        }

//        public override int Count
//        {
//            get
//            {
//                return m_Users.Count;
//            }
//        }

//        public Filter Filter { get; private set; }

//        public override long GetItemId(int position)
//        {
//            return position;
//        }

//        public FriendAdapter(Activity activity, IEnumerable<User> users)
//        {
//            m_Users = users.ToList();
//            m_OriginalUsers = users.ToList();
//        }


//        public override View GetView(int position, View convertView, ViewGroup parent)
//        {

//        }

//        private class FriendFilter : Filter
//        {
//            private readonly FriendAdapter m_Adapter;
//            public FriendFilter(FriendAdapter adapter)
//            {
//                m_Adapter = adapter;
//            }

//            protected override FilterResults PerformFiltering(ICharSequence constraint)
//            {
//                var returnObj = new FilterResults();
//                var results = new List<User>();
//                if (m_Adapter.m_OriginalUsers == null)
//                    m_Adapter.m_OriginalUsers = m_Adapter.m_Users;

//                if (constraint == null) return returnObj;

//                if (m_Adapter.m_OriginalUsers != null && m_Adapter.m_OriginalUsers.Any())
//                {
//                    // Compare constraint to all names lowercased. 
//                    // It they are contained they are added to results.
//                    results.AddRange(
//                        m_Adapter.m_OriginalUsers.Where(
//                            user => user.Name.ToLower().Contains(constraint.ToString())));
//                }

//                // Nasty piece of .NET to Java wrapping, be careful with this!
//                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
//                returnObj.Count = results.Count;

//                constraint.Dispose();

//                return returnObj;
//            }

//            protected override void PublishResults(ICharSequence constraint, FilterResults results)
//            {
//                using (var values = results.Values)
//                    m_Adapter.m_Users = values.ToArray<Object>()
//                        .Select(r => r.ToNetObject<User>()).ToList();

//                m_Adapter.NotifyDataSetChanged();

//                // Don't do this and see GREF counts rising
//                constraint.Dispose();
//                results.Dispose();
//            }
//        }

//    }
//}