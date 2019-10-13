using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookManager.ViewModels;

namespace BookManager.Mediator
{
    public static class MediatorMain
    {
        private static IDictionary<string, List<Action<object>>> _plDict = new Dictionary<string, List<Action<object>>>();

        public static void Subscribe(string token, Action<object> callback)
        {
            if (!_plDict.ContainsKey(token))
            {
                List<Action<object>> actionList = new List<Action<object>>();
                actionList.Add(callback);
                _plDict.Add(token, actionList);
            }
            else
            {
                bool found = false;
                foreach(var item in _plDict[token])
                {
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                    if (!found)
                        _plDict[token].Add(callback);
                }
            }
        }

        public static void Unsubscribe(string token, Action<object> callback)
        {
            if (_plDict.ContainsKey(token))
                _plDict[token].Remove(callback);
        }

        public static void Notify(string token, object args = null)
        {
            if (_plDict.ContainsKey(token))
                foreach (var callback in _plDict[token])
                    callback(args);
        }




    }
}
