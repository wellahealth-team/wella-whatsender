using System.Windows;

namespace WellaWhatsender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PhoneNumberAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber.Text) && !phoneNumbersListBox.Items.Contains(phoneNumber.Text))
                phoneNumbersListBox.Items.Add(phoneNumber.Text);
            else
                CommonResources.MessageBoxMethod("Phone number has already been added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(messageToSend.Text))
            {
                CommonResources.MessageBoxMethod("Please enter a message in the message field provided", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (phoneNumbersListBox.Items.IsEmpty)
            {
                CommonResources.MessageBoxMethod("Phone Number List cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //WIP
            foreach (var phoneNumber in phoneNumbersListBox.Items)
            {
                //work with the phone numbers here...
            }
        }
    }
}