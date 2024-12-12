using System;
using System.Windows.Forms;

namespace program
{
    public abstract class Message
    {
        public abstract void Show(string message, string title);
    }

    public class InfoMessageBox : Message
    {
        public override void Show(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class WarningMessageBox : Message
    {
        public override void Show(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    public class ErrorMessageBox : Message
    {
        public override void Show(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public class SuccessMessageBox : Message
    {
        public override void Show(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class ConfirmationMessageBox : Message
    {
        public override void Show(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }

    public static class MessageBoxFactory
    {
        public static Message CreateMessageBox(string type)
        {
            switch (type.ToLower())
            {
                case "info":
                    return new InfoMessageBox();
                case "warning":
                    return new WarningMessageBox();
                case "error":
                    return new ErrorMessageBox();
                case "success":
                    return new SuccessMessageBox();
                case "confirmation":
                    return new ConfirmationMessageBox();
                default:
                    throw new ArgumentException("Неизвестный тип сообщения");
            }
        }
    }
}
