using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DynamicData;

namespace ListView
{
    public class ItemDataService
    {
        private readonly SourceCache<Item, Guid> _source;

        public ItemDataService()
        {
            try
            {

                _source = new SourceCache<Item, Guid>(item => item.Id);
                _source.AddOrUpdate(new Item[30]);

                ChangedItems = _source.Connect().RefCount().SubscribeOn(TaskPoolScheduler.Default);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public IObservable<IChangeSet<Item, Guid>> ChangedItems { get; }

        public void Add(Item item) => _source.Edit(innerList => innerList.AddOrUpdate(item));

        public void Add(IEnumerable<Item> item) => _source.Edit(innerList => innerList.AddOrUpdate(item));
    }
}