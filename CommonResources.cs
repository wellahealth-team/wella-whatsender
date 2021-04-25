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
    }
}