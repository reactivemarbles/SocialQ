using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace SocialQ.Forms.Dialogs
{
    /// <summary>
    /// Material design implementation of <see cref="IDialogs"/>.
    /// </summary>
    public class MaterialDialogs : IDialogs
    {
        /// <inheritdoc/>
        public IObservable<Unit> Alert(string message, string title = "Confirm") =>
            Observable
                .FromAsync(() => MaterialDialog.Instance.AlertAsync(message, title), RxApp.MainThreadScheduler)
                .SubscribeOn(RxApp.MainThreadScheduler);

        /// <inheritdoc/>
        public IObservable<bool> Confirm(string message, string title = "Confirm", string okText = "OK", string cancelText = "Cancel") =>
            Observable
                .FromAsync(() => MaterialDialog.Instance.ConfirmAsync(message, title, okText, cancelText), RxApp.MainThreadScheduler)
                .Select(x => x ?? false)
                .SubscribeOn(RxApp.MainThreadScheduler);

        /// <inheritdoc/>
        public IObservable<string> Input(string message, string? title = null) =>
            Observable
                .FromAsync(() => MaterialDialog.Instance.InputAsync(title, message), RxApp.MainThreadScheduler)
                .SubscribeOn(RxApp.MainThreadScheduler);

        /// <inheritdoc/>
        public IObservable<Unit> ActionSheet(string title, bool allowCancel, params (string Key, Action Action)[] actions) =>
            Observable.Create<Unit>(observer =>
            {
                var dict = actions.ToDictionary(
                    x => x.Key,
                    x => x.Action);
                return ActionSheet(title, dict, allowCancel).Subscribe(observer);
            });

        /// <inheritdoc/>
        public IObservable<Unit> ActionSheet(
            string title,
            IDictionary<string, Action> actions,
            bool allowCancel = false) =>
            Observable
           .FromAsync(
                async () =>
                {
                    var task = allowCancel
                        ? await MaterialDialog.Instance.SelectChoiceAsync(title, actions.Keys.ToList()).ConfigureAwait(true)
                        : await MaterialDialog.Instance.SelectActionAsync(title, actions.Keys.ToList()).ConfigureAwait(true);

                    if (task >= 0)
                    {
                        actions.Values.ElementAt(task).Invoke();
                    }
                },
                RxApp.MainThreadScheduler)
           .SubscribeOn(RxApp.MainThreadScheduler);

        /// <inheritdoc/>
        public IObservable<Unit> Snackbar(string message) => Observable
           .FromAsync(
               () => MaterialDialog.Instance.SnackbarAsync(message, configuration: new MaterialSnackbarConfiguration { Margin = new Thickness(0, 0, 0, 625) }),
               RxApp.MainThreadScheduler)
           .SubscribeOn(RxApp.MainThreadScheduler);
    }
}