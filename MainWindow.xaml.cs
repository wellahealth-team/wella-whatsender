using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        //private void PhoneNumberAddButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(phoneNumber.Text) && !phoneNumbersListBox.Items.Contains(phoneNumber.Text))
        //        phoneNumbersListBox.Items.Add(phoneNumber.Text);
        //    else
        //        CommonResources.MessageBoxMethod("Phone number has already been added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //}

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
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

            //WIP
            foreach (var phoneNumber in phoneNumbersListBox.Items)
            {
                //work with the phone numbers here...
                try
                {
                    Thread.Sleep(sleepTime);
                    var searchLense = webDriver.FindElement(By.ClassName("C28xL"));
                    Thread.Sleep(sleepTime);
                    searchLense.Click();
                    Thread.Sleep(sleepTime);
                    webDriver.SwitchTo().ActiveElement().SendKeys(phoneNumber.ToString());
                    Thread.Sleep(sleepTime);

                    // Get the chat list. List size should be grater than 0.
                    var chatSearchResultList = webDriver.FindElements(By.ClassName("_2wP_Y"));
                    Thread.Sleep(sleepTime);
                    bool bMessageSent = false;

                    foreach (IWebElement webElement in chatSearchResultList)
                    {
                        if (webElement != null && !string.IsNullOrEmpty(webElement.Text))
                        {
                            string nameMsg = webElement.Text.Split(new[] { '\r', '\n' }).FirstOrDefault();
                            if (nameMsg.Equals(phoneNumber.ToString()))
                            {
                                webElement.Click();
                                bMessageSent = true;
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    CommonResources.MessageBoxMethod($"Error is {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void openWhatsappWeb_Click(object sender, RoutedEventArgs e)
        {
            webDriver.Url = baseUrlForWhatsappWeb;
            webDriver.Manage().Window.Maximize();
        }

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

            if(result == true)
            {
                PopulatePhoneNumberListBox(openFileDialog.FileName);
            }
        }
    }
}