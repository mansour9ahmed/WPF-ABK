using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public static class Mediator
    {
        private static IDictionary<string, List<Action<string,object>>> pl_dict =
         new Dictionary<string, List<Action<string,object>>>();

        public static void Subscribe(string token, Action<string,object> callback)
        {
            if (!pl_dict.ContainsKey(token))
            {
                var list = new List<Action<string,object>>();
                list.Add(callback);
                pl_dict.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach (var item in pl_dict[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    pl_dict[token].Add(callback);
            }
        }

        public static void Unsubscribe(string token, Action<string,object> callback)
        {
            if (pl_dict.ContainsKey(token))
                pl_dict[token].Remove(callback);
        }

        public static void Notify(string token,string viewName, object args = null)
        {
            if (pl_dict.ContainsKey(token))
                foreach (var callback in pl_dict[token])
                    callback(viewName,args);
        }
    }
}
