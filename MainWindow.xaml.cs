using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;

namespace WellaWhatsender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IWebDriver webDriver;
        private bool stopSendingMessage = false;
        private string baseUrlForWhatsappWeb = "https://web.whatsapp.com/";

        public MainWindow()
        {
            InitializeComponent();
            webDriver = new ChromeDriver();
        }

        private bool CheckLoggedIn()
        {
            try
            {
                return webDriver.FindElement(By.Id("app")).Displayed;
            }
            catch (Exception ex)
            {
                CommonResources.MessageBoxMethod($"Error is {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(messageToSend.Text))
            {
                CommonResources.MessageBoxMethod("Please enter a message in the message field provided", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (phoneNumbersListBox.Items.IsEmpty)
            {
                CommonResources.MessageBoxMethod("Phone Number List cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int sleepTime = 1000;
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            foreach (string phoneNumber in phoneNumbersListBox.Items)
            {
                try
                {
                    Thread.Sleep(sleepTime);

                    string phone = CommonResources.FormatPhoneNumber(phoneNumber);

                    webDriver.Navigate().GoToUrl("https://api.whatsapp.com/send/?phone=" + phone + "&text=" + Uri.EscapeDataString(messageToSend.Text));
                    webDriver.FindElement(By.Id("action-button")).Click();
                }
                catch (Exception ex)
                {
                    CommonResources.MessageBoxMethod($"Error is {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //private void openWhatsappWeb_Click(object sender, RoutedEventArgs e)
        //{
        //    webDriver.Url = baseUrlForWhatsappWeb;
        //    webDriver.Manage().Window.Maximize();
        //}

        private void PopulatePhoneNumberListBox(string filePath)
        {
            var listOfPhoneNumbers = new List<string>();
            using (FileStream stream = File.OpenRead(filePath))
            using (TextReader reader = new StreamReader(stream))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    listOfPhoneNumbers.Add(line);
                }
            }

            //adds all phone numbers to the listbox
            phoneNumbersListBox.ItemsSource = listOfPhoneNumbers;
        }

        private void uploadPhoneNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                PopulatePhoneNumberListBox(openFileDialog.FileName);
            }
        }
    }
}