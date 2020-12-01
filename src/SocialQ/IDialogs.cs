using System;
using System.Collections.Generic;
using System.Reactive;

namespace SocialQ
{
    /// <summary>
    /// An interface representing dialogs for user consumption.
    /// </summary>
    public interface IDialogs
    {
        /// <summary>
        /// Alerts the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <returns>A signal.</returns>
        IObservable<Unit> Alert(string message, string title = "Error");

        /// <summary>
        /// Displays an action sheet.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="allowCancel">Allow cancel.</param>
        /// <param name="actions">The actions.</param>
        /// <returns>A signal.</returns>
        IObservable<Unit> ActionSheet(string title, bool allowCancel, params (string Key, Action Action)[] actions);

        /// <summary>
        /// Displays an action sheet.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="actions">The actions.</param>
        /// <param name="allowCancel">Allow cancel.</param>
        /// <returns>A signal.</returns>
        IObservable<Unit> ActionSheet(string title, IDictionary<string, Action> actions, bool allowCancel = false);

        /// <summary>
        /// Displays a confirmation.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <param name="okText">The okay text.</param>
        /// <param name="cancelText">The cancel text.</param>
        /// <returns>A confirmation or denial.</returns>
        IObservable<bool> Confirm(string message, string title = "Confirm", string okText = "OK", string cancelText = "Cancel");

        /// <summary>
        /// Displays an input box.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <param name="title">The title.</param>
        /// <returns>The value of the input.</returns>
        IObservable<string> Input(string question, string? title = null);

        /// <summary>
        /// Displays a snackbar.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A signal.</returns>
        IObservable<Unit> Snackbar(string message);
    }
}