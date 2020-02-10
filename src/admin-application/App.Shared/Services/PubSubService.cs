using System.Collections.Generic;
using System;
using App.Shared.Models;
using System.Threading.Tasks;

namespace App.Shared.Services
{
    public class PubSubService : IPubSubService
    {
        private Dictionary<Type, List<Func<Task>>> _actionSubscriptions;

        public PubSubService()
        {
            _actionSubscriptions = new Dictionary<Type, List<Func<Task>>>();
        }

        public async Task Publish<T>() where T : IPubSubEvent
        {            
            if(_actionSubscriptions.TryGetValue(typeof(T), out var subs))
            {
                foreach(var sub in subs)
                    await sub.Invoke();
            }
        }

        public void Subscribe<T>(Func<Task> subscriber) where T : IPubSubEvent
        {
            if(_actionSubscriptions.ContainsKey(typeof(T)))
            {
                var subs = _actionSubscriptions[typeof(T)];
                subs.Add(subscriber);
                return;
            }
            _actionSubscriptions.Add(typeof(T), new List<Func<Task>>{ subscriber});
        }
    }
}