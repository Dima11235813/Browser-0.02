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
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.Speech.Recognition;


namespace Browser_0._02
{
    public partial class Form1 : Form
    {
        //creates speech synth
        SpeechSynthesizer broSynth = new SpeechSynthesizer();
        PromptBuilder broPrompt = new PromptBuilder();
        SpeechRecognitionEngine broRecognize = new SpeechRecognitionEngine();

        public Form1()
        {
            InitializeComponent();
           // Thread broIsListening = new Thread(new ThreadStart(broIsListening));
            //broIsListening.Start();
            

          
        }
        /*
        public void broIsListening()
        {

            Thread.Sleep(30000);
            userInput = 
            PopUpMessage(string userInput);
        }
        */
        public void PopUpMessage(string userInput)
        {
             
            MessageBox.Show(userInput);
        }

        string encryption = "";

        private void button1_Click(object sender, EventArgs e)
        {
            NavigateToPage();
        }

        //goes to web address entered into url text box
        private void NavigateToPage()
        {
            //disables button and text box while waiting for URL to load
            button1.Enabled = false;
            textBox1.Enabled = false;
            //assigns current level of encryption to global encryption variable - to be compared when new destination loaded
            encryption = webBrowser1.EncryptionLevel.ToString();
            //assignes the text in the text box to the navigate funtion in webBrowswer object
            webBrowser1.Navigate(textBox1.Text);
            //updates status bar with "going" message - signifyies waiting for response
            toolStripStatusLabel1.Text = "Going";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //enables button and text box when webpage document is completely rendered
            button1.Enabled = true;
            textBox1.Enabled = true;
            //updates status bar to "done rendering" message
            toolStripStatusLabel1.Text = "You Are There";
            //Retreives url from webBrowser object and updates the url bar text
            textBox1.Text = webBrowser1.Url.ToString();

            


            //Stores current URLs level of encryption into string
            string currentURLEncryption = webBrowser1.EncryptionLevel.ToString();

           
         
            //displays security change alert if notification is enabled under "info" > "encryption"
            if (enableNotificationToolStripMenuItem.Text.ToString() == ("Disable Notifications"))// && (currentURLEncryption.ToString() != encryption))
            {
                SecurityChange();
            }

            

            //changes all images on webpage to image in src quotes
            /*
            foreach ( HtmlElement image in webBrowser1.Document.Images)
            {
                image.SetAttribute("src", "https://www.gravatar.com/avatar/82b369fc2214d71ae9d2317f7de76528");
            }
            */
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        // triggers "Go" button click if "Enter" key is pressed
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //checks for console key enter and if pressed submits runs the "url go" button click function
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                button1_Click(null, null);
            }
        }

        /// <summary>
        /// updates progress bar while web browser object loads document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        { 
            

            if (e.CurrentProgress > 0 && e.MaximumProgress > 0)
            {
                toolStripProgressBar1.ProgressBar.Value = (int)(e.CurrentProgress * 100 / e.MaximumProgress);
            }
        }
        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_ProgressChanged_1(object sender, WebBrowserProgressChangedEventArgs e)
        {

        }

        //shows message if "about" menu item clicked
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Browser Made by Learn Internet Grow Team");
        }

        //
        /*
        Help

        */
        //attempts to close app if escape is pressed but fails - using form object
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar = (Char)Keys.L)
            {
                

            }
            */
        }

        
        /*
        Help

        */

        //attempts to close app if escape is pressed but fails - using menu object
        private void menuStrip1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Escape)
            {
                this.Close();
            }
        }

        private void fIleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        //retrieves level of security and outputs it in a message box
        private void encryptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string encryption = webBrowser1.EncryptionLevel.ToString();
            MessageBox.Show(encryption);

        }

        private void enableNotificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enableNotificationToolStripMenuItem.Text == "Enable Notification")
            {
                enableNotificationToolStripMenuItem.Text = "Disable Notification";
            }
            else
            {
                enableNotificationToolStripMenuItem.Text = "Enable Notification";
            }
        }
        private void SecurityChange()
        {
            MessageBox.Show("Security Changed from" + encryption + " to "+ webBrowser1.EncryptionLevel.ToString());
        }

        private void titleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creates button with document title
            string titleOfCurrentPage = webBrowser1.DocumentTitle.ToString();
            MessageBox.Show("The title of this page is \n" + titleOfCurrentPage);
            
        }

        private void parseURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdUrlVariables();

            //MessageBox.Show("TThe full URL is" + urlOfCurrentPage);


        }

        private void IdUrlVariables()
        {
            //creates string storing current page url
            string urlOfCurrentPage = webBrowser1.Url.ToString();

            //creates delimited for name variable pair block and another for seperating the block into name and variable
            char NameVarPair = ('&');
            char EqualsDelim = ('=');
            char QuestionMark = ('?');
            //splitts up the URl by the & symbol and stores everything in UrlBlock array
            string[] UrlBlock = urlOfCurrentPage.Split(NameVarPair);

            //creates a array of strings called TempStrings serve as buffer
            string[] TempStrings = new string[(int)UrlBlock.Length];
            //creates a array of strings called NameVar to hold all the variable names in the URL
            string[] NameVar = new string[(int)UrlBlock.Length];
            //creates an array of string called VarVal to hold all the variable values
            string[] VarVal = new string[(int)UrlBlock.Length];

            string temp;
            string value;
            string[] block;

            for (int i=1;i<UrlBlock.Length;i++)
            {
                temp = (string)UrlBlock[i];
                block = temp.Split(EqualsDelim);
                value = (block[1]);
                VarVal[i] = value;
                NameVar[i] = block[0];
            }


            StringBuilder ParsedUrlString = new StringBuilder();


            for (int i = 1; i < UrlBlock.Length;i++)
            {
               
                ParsedUrlString.Append((string)("Variable Name = " + NameVar[i] + "\nVariable Value = {1}" + VarVal[i] + "\n"));
                
            }

            
            
            MessageBox.Show(ParsedUrlString.ToString());

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test();
        }

        private void Test()
        {
            //creates string storing current page url
            string urlOfCurrentPage = webBrowser1.Url.ToString();

            //creates delimited for name variable pair block and another for seperating the block into name and variable
            char NameVarPair = ('&');
            char EqualsDelim = ('=');
            char QuestionMark = ('?');
            //splitts up the URl by the & symbol and stores everything in UrlBlock array
            string[] UrlBlock = urlOfCurrentPage.Split(NameVarPair);

            //creates a array of strings called TempStrings serve as buffer
            string[] TempStrings = new string[(int)UrlBlock.Length];
            //creates a array of strings called NameVar to hold all the variable names in the URL
            string[] NameVar = new string[(int)UrlBlock.Length];
            //creates an array of string called VarVal to hold all the variable values
            string[] VarVal = new string[(int)UrlBlock.Length];

            string temp;
            string value;
            string[] block;

            for (int i = 1; i < UrlBlock.Length; i++)
            {
                temp = (string)UrlBlock[i];
                block = temp.Split(EqualsDelim);
                value = (block[1]);
                VarVal[i] = value;
                NameVar[i] = block[0];
            }

            foreach(string s in VarVal)
            {
                MessageBox.Show(s);
                
            }
            foreach (string s in NameVar)
            {
                MessageBox.Show(s);

            }

            StringBuilder ParsedUrlString = new StringBuilder();

            for (int i = 1; i < UrlBlock.Length;i++)
            {

                ParsedUrlString.Append((string)("Variable Name = " + NameVar[i] + "\nVariable Value = {1}" + VarVal[i] + "\n"));

            }



            MessageBox.Show(ParsedUrlString.ToString());

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
/*
namespace Browser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        //when button clicked the object takes data from text box and navigates web broser
        private void button2_Click(object sender, EventArgs e)
        {
            NavigateToPage();

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void NavigateToPage()
        {
            button2.Enabled = false;
            textBox1.Enaled = false;
            webBrowser1.Navigate(textBox1.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                NavigateToPage();
            }
        }

    }
}
*/