using Customers.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace Customers.Services
{
    public class CustomerEventService : ICustomerEventService
    {
        private readonly ISubject<CustomerEvent> _eventStream = new
            ReplaySubject<CustomerEvent>(1);

        public CustomerEventService()
        {
            AllEvents = new ConcurrentStack<CustomerEvent>();
        }

        public ConcurrentStack<CustomerEvent> AllEvents { get; }

        public void AddError(Exception exception)
        {
            _eventStream.OnError(exception);
        }

        public CustomerEvent AddEvent(CustomerEvent CustomerEvent)
        {
            AllEvents.Push(CustomerEvent);
            _eventStream.OnNext(CustomerEvent);
            return CustomerEvent;
        }

        public IObservable<CustomerEvent> EventStream()
        {
            return _eventStream.AsObservable();
        }
    }

    public interface ICustomerEventService
    {
        ConcurrentStack<CustomerEvent> AllEvents { get; }
        void AddError(Exception exception);
        CustomerEvent AddEvent(CustomerEvent CustomerEvent);
        IObservable<CustomerEvent> EventStream();
    }
}
