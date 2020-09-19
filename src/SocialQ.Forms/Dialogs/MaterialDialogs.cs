using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using XF.Material.Forms.UI.Dialogs;

namespace SocialQ.Forms.Dialogs
{

    public class MaterialDialogs : IDialogs
    {
        public IObservable<Unit> Alert(string message, string title = "Confirm") =>
            Observable
                .FromAsync(() => MaterialDialog.Instance.AlertAsync(message, title), RxApp.MainThreadScheduler)
                .SubscribeOn(RxApp.MainThreadScheduler);


        public IObservable<bool> Confirm(string message, string title = "Confirm", string okText = "OK", string cancelText = "Cancel") =>
            Observable
                .FromAsync(() => MaterialDialog.Instance.ConfirmAsync(message, title, okText, cancelText), RxApp.MainThreadScheduler)
                .Select(x => x ?? false)
                .SubscribeOn(RxApp.MainThreadScheduler);


        public IObservable<string> Input(string message, string? title = null) =>
            Observable.FromAsync(() => MaterialDialog.Instance.InputAsync(title, message), RxApp.MainThreadScheduler).ObserveOn(RxApp.MainThreadScheduler);


        public IObservable<Unit> ActionSheet(string title, bool allowCancel, params (string Key, Action Action)[] actions) =>
            Observable.Create<Unit>(observer => 
            {
                var dict = actions.ToDictionary(
                    x => x.Key,
                    x => x.Action
                );
                return ActionSheet(title, dict, allowCancel).Subscribe(observer);
            });

        public IObservable<Unit> ActionSheet(string title, IDictionary<string, Action> actions, bool allowCancel = false) =>
            Observable.FromAsync(async () =>
            {

                var task = allowCancel
                    ? await MaterialDialog.Instance.SelectChoiceAsync(title, actions.Keys.ToList())
                    : await MaterialDialog.Instance.SelectActionAsync(title, actions.Keys.ToList());

                if (task >= 0)
                    actions.Values.ElementAt(task).Invoke();
            }, RxApp.MainThreadScheduler);

        public IObservable<Unit> Snackbar(string message) =>
            Observable.FromAsync(() => MaterialDialog.Instance.SnackbarAsync(message), RxApp.MainThreadScheduler);
    }
}