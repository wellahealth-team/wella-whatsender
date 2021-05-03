using System.Text.RegularExpressions;
using System.Windows;

namespace WellaWhatsender
{
    public class CommonResources
    {
        //Message Box
        public static MessageBoxResult MessageBoxMethod(string messageBoxText, string messageBoxCaption, MessageBoxButton messageBoxButton, MessageBoxImage messageBoxImage)
        {
            string messageBoxTextData = messageBoxText;
            string messageBoxCaptionData = messageBoxCaption;
            MessageBoxButton messageBoxButtonData = messageBoxButton;
            MessageBoxImage messageBoxImageData = messageBoxImage;
            return MessageBox.Show(messageBoxTextData, messageBoxCaptionData, messageBoxButtonData, messageBoxImageData);
        }

        public static string CleanNumber(string value)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(value, "");
        }

        public static string FormatPhoneNumber(string phoneNumber, string code = "234")
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return string.Empty;
            }

            string phone = CleanNumber(phoneNumber);

            if (phone.StartsWith(code) || phone.StartsWith($"+{code}"))
            {
                return phone;
            }

            if (phone.StartsWith("0"))
            {
                phone = $"{code}{phone.Remove(0, 1)}";
            }
            else
            {
                phone = $"{code}{phone}";
            }

            return phone;
        }
    }
}