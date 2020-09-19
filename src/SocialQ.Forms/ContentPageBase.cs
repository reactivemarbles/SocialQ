using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace SocialQ.Forms
{
    public class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel>
        where TViewModel : ViewModelBase
    {
        private readonly AsyncSubject<Unit> _appearing = new AsyncSubject<Unit>();

        protected override void OnAppearing()
        {
            _appearing.OnNext(Unit.Default);
            _appearing.OnCompleted();
        }

        protected IObservable<Unit> WhenAppearing => _appearing.AsObservable();

        protected CompositeDisposable PageDisposables { get; } = new CompositeDisposable();
    }

    public class ContentViewBase<TViewModel> : ReactiveContentView<TViewModel>
        where TViewModel : ReactiveObject
    {
        protected CompositeDisposable ViewDisposables { get; } = new CompositeDisposable();
    }

    public class ViewCellBase<TViewModel> : ReactiveViewCell<TViewModel>
        where TViewModel : ReactiveObject
    {
        protected CompositeDisposable ViewCellDisposables { get; } = new CompositeDisposable();
    }
}