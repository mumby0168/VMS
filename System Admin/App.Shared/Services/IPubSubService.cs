using System;
using System.Threading.Tasks;
using App.Shared.Models;

namespace App.Shared.Services
{
    public interface IPubSubService
    {
         Task Publish<T>() where T : IPubSubEvent;

         void Subscribe<T>(Func<Task> subscriber) where T : IPubSubEvent;
    }
}