using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Chronicle;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Operations.Sagas
{
    internal static class Extensions
    {

        public static bool IsAssignableTo<T>(this Type @this)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));

            return typeof(T).GetTypeInfo().IsAssignableFrom(@this.GetTypeInfo());
        }

        private static readonly Type[] SagaTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsAssignableTo<ISaga>())
            .ToArray();

        internal static bool BelongsToSaga<TMessage>(this TMessage _) where TMessage : IServiceBusMessage
            => SagaTypes.Any(t => t.IsAssignableTo<ISagaAction<TMessage>>());
    }
}
