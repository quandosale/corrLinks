using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Corrlinks.models;
using System.Diagnostics;
using System.Net;
using System.IO;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace Corrlinks
{
    public partial class Form1 : Form
    {
        private const string statusSeperator = "-----------------------------------------------------------------------------------";
        private const string URL = "https://www.corrlinks.com";
        private const int messageBodyLimit = 13000;
        private ChromeUtil mChrome;
        private Thread openBrowserThread;
        private Thread mainProcessingThread;

        List<MessageIn> inbox = new List<MessageIn>();
        List<MessageOut> outbox = new List<MessageOut>();
        MessageOut messageOut = new MessageOut();

        public Form1()
        {
            InitializeComponent();
            txt_username.Text = "Info@rzero.org";
            txt_password.Text = "Rzero2017";
        }

        private void btn_open_browser_Click(object sender, EventArgs e)
        {
            openBrowserThread = new Thread(new ThreadStart(OpenBrowser));
            openBrowserThread.Start();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            mainProcessingThread = new Thread(new ThreadStart(Run));
            mainProcessingThread.Start();
        }

        private void OpenBrowser()
        {
            string username = txt_username.Text;
            string password = txt_password.Text;
            if (username == "" || password == "")
            {
                MessageBox.Show("Please fill out username and password");
                return;
            }

            mChrome = new ChromeUtil();
            // Open URL
            mChrome.GoToUrl(URL);
            // Enter username and password
            mChrome.FindById("ctl00_mainContentPlaceHolder_loginUserNameTextBox").SendKeys(txt_username.Text);
            mChrome.FindById("ctl00_mainContentPlaceHolder_loginPasswordTextBox").SendKeys(txt_password.Text);
            // Click on Login
            mChrome.FindById("ctl00_mainContentPlaceHolder_loginButton").Click();
        }
        
        private void Run()
        {
            mChrome.GoToUrl("https://www.corrlinks.com/Default.aspx");
            Thread.Sleep(1000);
            ReadFromInbox();
            SubmitInbox();
            ProcessOutbox();
            ValidateSentMessages();
            mChrome.GoToUrl("https://www.corrlinks.com/Default.aspx");
        }

        private void ReadFromInbox()
        {
            UpdateStatus("Start processing for new messages...");
            UpdateStatus(statusSeperator);
            // Open Email box
            mChrome.FindById("ctl00_mainContentPlaceHolder_mailboxImageButton").Click();
            mChrome.FindByAttr("a", "href", "Inbox.aspx", 1).Click();
            // Open new messages
            mChrome.FindById("ctl00_mainContentPlaceHolder_UnreadMessages").Click();

            Thread.Sleep(2000);

            IWebElement tbodyWebElement = mChrome.FindByXPath("//table[@class='MessageDataGrid']/tbody");
            if (tbodyWebElement == null)
            {
                UpdateStatus("No new messages found");
                UpdateStatus("Finished processing for new messages...");
                UpdateStatus(statusSeperator);
                return;
            }

            String htmlBody = tbodyWebElement.GetAttribute("innerHTML");
            int messageCount = Regex.Matches(htmlBody, "<tr").Count - 1;

            for (int i = 0; i < messageCount; i++)
            {
                try
                {
                    mChrome.FindByXPath("//table[@class='MessageDataGrid']/tbody/tr[" + (2).ToString() + "]").Click();
                    Thread.Sleep(1000);

                    MessageIn message = new MessageIn();
                    message.FROM = mChrome.FindById("ctl00_mainContentPlaceHolder_fromTextBox").GetAttribute("value");
                    message.INMATE_ID = MyUtil.GetMateID(message.FROM);
                    message.DATE = Convert.ToDateTime(mChrome.FindById("ctl00_mainContentPlaceHolder_dateTextBox").GetAttribute("value"));
                    message.SUBJECT = mChrome.FindById("ctl00_mainContentPlaceHolder_subjectTextBox").GetAttribute("value");
                    message.MESSAGE = mChrome.FindById("ctl00_mainContentPlaceHolder_messageTextBox").Text;
                    message.TIMESTAMP = new DateTime();

                    inbox.Add(message);

                    mChrome.FindById("ctl00_mainContentPlaceHolder_cancelButton").Click();

                    UpdateStatus("Read message from " + message.FROM);
                    Thread.Sleep(1000);
                }
                catch
                {

                }
            }

            UpdateStatus("Finished processing for new messages...");
            UpdateStatus(statusSeperator);

            mChrome.GoToUrl("https://www.corrlinks.com/Default.aspx");
        }

        private void SubmitInbox()
        {
            if (inbox.Count == 0) return;
            mChrome.GoToUrl("http://ddtext.com/corrlinks/inbox.php");
            for (int i = 0; i < inbox.Count; i++)
            {
                Thread.Sleep(1);
                MessageIn msg = inbox[i];
                mChrome.FindByAttr("input", "name", "sender", 1).SendKeys(msg.FROM);
                mChrome.FindByAttr("input", "name", "date", 1).SendKeys(msg.DATE.ToString());
                mChrome.FindByAttr("input", "name", "subject", 1).SendKeys(msg.SUBJECT);
                mChrome.FindByAttr("textarea", "name", "message", 1).SendKeys(msg.MESSAGE);
                mChrome.FindByAttr("input", "name", "submit", 1).Click();
            }
        }

        private void ProcessOutbox()
        {
            UpdateStatus("Start processing for outbox...");
            UpdateStatus(statusSeperator);

            mChrome.GoToUrl("https://www.corrlinks.com/NewMessage.aspx");
            Thread.Sleep(1000);
       
            messageOut = CheckOutbox();
            int k = 0;
            while(messageOut != null)
            {
                k++;
                SendMessage(messageOut);
                messageOut = CheckOutbox();
            }
            UpdateStatus("Message checked out " + k.ToString());

            UpdateStatus(statusSeperator);
            UpdateStatus("Finished processing for sending messages...");
        }

        private MessageOut CheckOutbox()
        {
            MessageOut msg = new MessageOut();

            WebRequest request = null;
            WebResponse response = null;
            Stream resStream = null;
            StreamReader resReader = null;
            try
            {
                string uristring = "http://ddtext.com/corrlinks/outbox.php";
                request = WebRequest.Create(uristring.Trim());
                response = request.GetResponse();
                resStream = response.GetResponseStream();
                resReader = new StreamReader(resStream);
                string resstring = resReader.ReadToEnd();

                string trimmed = resstring.Replace("<pre>", "");
                int lineBreakPos = trimmed.IndexOf("\n");
                String header = trimmed.Substring(0, lineBreakPos);
                msg.MESSAGE = trimmed.Substring(lineBreakPos + 1);
                String[] split = header.Split('|');

                try
                {
                    msg.INBOX_ID = Convert.ToInt32(split[0]);
                    msg.INMATE_ID = Convert.ToInt32(split[1]);
                    msg.SUBJECT = split[2];
                } catch
                {
                    UpdateStatus("No more messages in outbox");
                    return null;
                }
                if (split[2] == "")
                {
                    UpdateStatus("Message body is empty");
                }
            }
            catch (Exception ex)
            {
                UpdateStatus("Error while checking outbox");
                return null;
            }
            finally
            {
                if (resReader != null) resReader.Close();
                if (response != null) response.Close();
            }
            return msg;
        }

        private bool SendMessage(MessageOut msg)
        {
            mChrome.GoToUrl("https://www.corrlinks.com/NewMessage.aspx");
            Thread.Sleep(1000);
            
            mChrome.SetTextByID("ctl00_mainContentPlaceHolder_subjectTextBox", msg.SUBJECT);

            UpdateStatus("Fill the message body");

            try
            {
                mChrome.SetTextByID("ctl00_mainContentPlaceHolder_messageTextBox", msg.MESSAGE);
            } catch
            {
                UpdateStatus("Number of message characters or lines may have exceeded limit.");
            }

            mChrome.TryCloseAlert();

            try
            {
                mChrome.FindById("ctl00_mainContentPlaceHolder_addressBox_addressTextBox").Click();
            } catch
            {
                UpdateStatus("Error when clicking addressbox field");
                return false;
            }


            String addressHtmlBody = mChrome.FindByXPath("//table[@class='AddressBoxDataGrid']/tbody").GetAttribute("innerHTML");
            int addressCount = Regex.Matches(addressHtmlBody, "<tr").Count - 1;

            bool addressFound = false;
            for(int j = 0; j < addressCount; j ++)
            {
                String tr = "//table[@class='AddressBoxDataGrid']/tbody/tr[" + (j +2).ToString() + "]";
                String address = mChrome.FindByXPath(tr + "/th").GetAttribute("innerHTML");

                int inmate_id = MyUtil.GetMateID(address);
                if(inmate_id == msg.INMATE_ID )
                {
                    addressFound = true;
                    mChrome.FindByXPath(tr + "/td[1]//input").Click();  
                    mChrome.FindById("ctl00_mainContentPlaceHolder_addressBox_okButton").Click();
                    Thread.Sleep(1000);

                    mChrome.FindById("ctl00_mainContentPlaceHolder_sendMessageButton").Click();
                    UpdateStatus("Sent message to " + msg.INMATE_ID);
                    Thread.Sleep(1000);
                    break;
                }
            }
            if(!addressFound)
                UpdateStatus("Address not found" + msg.INMATE_ID.ToString());
            return addressFound;
        }

        private void ValidateSentMessages()
        {
            UpdateStatus("Validating Sent Messages");
            UpdateStatus(statusSeperator);
            mChrome.GoToUrl("https://www.corrlinks.com/SentMessages.aspx");
            Thread.Sleep(1000);

            List<string> messageIDs = new List<string>();
            try
            {
                for(int i = 2; i < 12; i ++)
                {
                    IWebElement ele = mChrome.FindByXPath("//table[@class='MessageDataGrid PhotoMessageDataGrid']//tr[" + i.ToString() + "]//td//a[@class='tooltip'][1]");
                    if (ele == null) break;
                    string subject = ele.GetAttribute("innerHTML");

                    bool messageIDAvailable = subject.IndexOf('[') == -1 ? false : true;
                    if (!messageIDAvailable) break;

                    string messageID = subject.Substring(subject.IndexOf('[') + 1, subject.IndexOf(']') - subject.IndexOf('[') - 1);
                    messageIDs.Add(messageID);
                }

                string validationURL = "http://ddtext.com/corrlinks/sentmessage-update.php?";
                for(int i = 0; i < messageIDs.Count; i ++)
                {
                    validationURL += "msgID[]=" + messageIDs[i] + "&";
                }

                string validationResult = MyUtil.GetRequest(validationURL);
                UpdateStatus(validationResult);
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateStatus(String status)
        {
            try
            {
                txt_status.Text += status + "\n";
            } catch
            {

            }
        }

        private void statusChaged(object sender, EventArgs e)
        {
            txt_status.SelectionStart = txt_status.Text.Length;
            txt_status.ScrollToCaret();
        }

        private void onClose(object sender, FormClosedEventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            if (openBrowserThread != null) openBrowserThread.Abort();
            if (mainProcessingThread != null) mainProcessingThread.Abort();
            if (mChrome != null) mChrome.Quit();
        }
    }
}
