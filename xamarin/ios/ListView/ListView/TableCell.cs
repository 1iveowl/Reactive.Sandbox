using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;

using Foundation;
using ReactiveUI;
using UIKit;

namespace ListView
{
    public class TableCell : ReactiveTableViewCell<TableCellViewModel>
    {
        public static NSString ReuseKey = new NSString(nameof(TableCell));

        public CompositeDisposable ViewCellBindings = new CompositeDisposable();

        public UILabel Id { get; set; }

        public UILabel Type { get; set; }

        public UILabel IsToggled { get; set; }

        public TableCell()
        {
            this.OneWayBind(ViewModel, x => x.Id, x => x.Id.Text)
                .DisposeWith(ViewCellBindings);

            this.OneWayBind(ViewModel, x => x.Type, x => x.Type.Text)
                .DisposeWith(ViewCellBindings);
        }
    }

    public class TableCellViewModel : ReactiveObject
    {
        private ItemType _type;
        private bool _isToggled;
        private Guid _id;

        public TableCellViewModel(Item item)
        {
            Id = item.Id;
            Type = item.Type;
            IsToggled = item.IsToggled;
        }

        public Guid Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        public ItemType Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        public bool IsToggled
        {
            get => _isToggled;
            set => this.RaiseAndSetIfChanged(ref _isToggled, value);
        }
    }
}