using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WebApi.Customers.Models;

namespace WebApi.Customers.Services
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

        public CustomerEvent AddEvent(CustomerEvent customerEvent)
        {
            AllEvents.Push(customerEvent);
            _eventStream.OnNext(customerEvent);
            return customerEvent;
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
        CustomerEvent AddEvent(CustomerEvent customerEvent);
        IObservable<CustomerEvent> EventStream();
    }
}
