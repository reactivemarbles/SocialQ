using System;
using System.Collections.Generic;
using System.Reactive;

namespace SocialQ
{

    public interface IDialogs
    {
        IObservable<Unit> Alert(string message, string title = "Error");

        IObservable<Unit> ActionSheet(string title, bool allowCancel, params (string Key, Action Action)[] actions);

        IObservable<Unit> ActionSheet(string title, IDictionary<string, Action> actions, bool allowCancel = false);

        IObservable<bool> Confirm(string message, string title = "Confirm", string okText = "OK", string cancelText = "Cancel");

        IObservable<string> Input(string question, string? title = null);

        IObservable<Unit> Snackbar(string message);
    }
}