using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        List<string> items = new List<string>();  //Specifying string values for the listbox
        public Form1()
        {
            InitializeComponent();
            items.Add("Australia"); //Values for listboc, i.e., this item will be index 0
            items.Add("Canada");
            items.Add("India");
            items.Add("Japan");
            items.Add("United Kingdom");
            items.Add("United States");
            

            listBox1.DataSource = items;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.WebProxy proxy = new System.Net.WebProxy(@"http://proxy.dkit.ie:3128"); //Proxy for DKIT

            int countrycode = listBox1.SelectedIndex; //The variable countrycode containes the same value as the selected listbox item

             
            //Stringbuilder is used connect different string values to make a single string value
            //Append is used to append (ADD) data to the string
            //The google object is specifying the the start of a string, which in this case is a url to the google maps search bar
            //which means any additional string values added to the string, via stringbuilder, will be searched on google maps
            StringBuilder google = new StringBuilder();
            google.Append("https://www.google.ie/maps?q=");

            //The following code is the web services, for example, the webservice global weather was added to the application, as a 
            //service to be referenced, by calling the service, ie CallWebService, it will return a number of values gathered from online resources etc.
            net.webservicex.www.GlobalWeather CallWebService =
            new net.webservicex.www.GlobalWeather();


            net.webservicex.www2.country CallWebService1 =
            new net.webservicex.www2.country();


            net.webservicex.www1.CurrencyConvertor CallWebService2 =
            new net.webservicex.www1.CurrencyConvertor();
            double GetCurrency = 0.0;

            string translate = null; //The variable translate is given a default value of null

            TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();
            client = new TranslatorService.LanguageServiceClient();

            if (countrycode == 0) //An if statement allows for certain code to be executed based on the actions of the user, in this case, which country the user has selected
            {
                //This is where the Currency web service is being called to return a specified value, in this case the exchange rate of the Australian dollar compared to the Euro
                GetCurrency = CallWebService2.ConversionRate(net.webservicex.www1.Currency.EUR, net.webservicex.www1.Currency.AUD);
                textBox7.Text = ("The Exchange Rate is $" + GetCurrency.ToString("C") + " for €1"); //Textbox7 will recieve a string containing the requested value, the "C" means the value will be formated into currency form, ie two decimal places

                //As above these webservices are returning requested info
                string GetCountry = CallWebService.GetCitiesByCountry("Australia");
                string GetWeather = CallWebService.GetWeather("Melbourne Airport", "Australia");
                textBox1.Text = getInfo(GetCountry, "<Country>", "</Country>"); //The GetCitiesByCountry service returns its values bookended with tags
                textBox2.Text = getInfo(GetWeather, "<Location>", "</Location>"); //Using the GetInfo method these tags will be removed, so only the requested
                textBox3.Text = getInfo(GetWeather, "<Time>", "</Time>"); //info will be returned to each textbox
                textBox4.Text = getInfo(GetWeather, "<SkyConditions>", "</SkyConditions>");
                textBox5.Text = getInfo(GetWeather, "<Wind>", "</Wind>");
                textBox6.Text = getInfo(GetWeather, "<Temperature>", "</Temperature>");
                
                //This webservice is similar to previous ones only it returns the international dialing code of the specified country
                string GetDialNum = CallWebService1.GetISD("Australia");
                textBox8.Text = getInfo(GetDialNum, "<code>", "</code>");

                google.Append(listBox1.SelectedItem);// This will add the value of the selected listbox item to the stringbuilder string, so the browser will open up a search for the specified country
                webBrowser1.ScriptErrorsSuppressed = true; //Initially the browser would show a script error, but this error will be suppressed with this line of code
                webBrowser1.Navigate(google.ToString()); //Navigate means that when the program is ran the web browser item will navigate to the address specified by the google stringbuilder item

                //The try and catch function can be used for error handling, ie you specify the piece of code you want tried each time the program is ran
                //and should this code return an error the program will not crash because the error will be caught and you can specify what to do incase of error in the catch function
                try
                {
                    //This is the bing translater web service, this allows the user to input text into the textboxtranslate field and this text will then be outputed to textbox9 in the specified language
                    //The random characters represent the APIkey, en means the specified language is english
                    translate = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", textBoxTranslate.Text, "", "en");
                    textBox9.Text = translate;
                }
                catch //If the APIkey was invalid or the quota for the day is met or the user just chose not to translate any text an error was caused due to a null value being passed
                {
                    textBox9.Text = "Not Available"; //but with the catch function the program will not crash because textbox9 will have get the specified error message string rather than a null value
                }
                
            }
            else if (countrycode == 1) 
            {
                GetCurrency = CallWebService2.ConversionRate(net.webservicex.www1.Currency.EUR, net.webservicex.www1.Currency.CAD);
                textBox7.Text = ("The Exchange Rate is $" + GetCurrency.ToString("C") + " for €1");

                string GetCountry = CallWebService.GetCitiesByCountry("Canada");
                string GetWeather = CallWebService.GetWeather("Ottawa Int'L. Ont.", "Canada");
                textBox1.Text = getInfo(GetCountry, "<Country>", "</Country>");
                textBox2.Text = getInfo(GetWeather, "<Location>", "</Location>");
                textBox3.Text = getInfo(GetWeather, "<Time>", "</Time>");
                textBox4.Text = getInfo(GetWeather, "<SkyConditions>", "</SkyConditions>");
                textBox5.Text = getInfo(GetWeather, "<Wind>", "</Wind>");
                textBox6.Text = getInfo(GetWeather, "<Temperature>", "</Temperature>");

                string GetDialNum = CallWebService1.GetISD("Canada");
                textBox8.Text = getInfo(GetDialNum, "<code>", "</code>");

                google.Append(listBox1.SelectedItem);
                webBrowser1.ScriptErrorsSuppressed = true;
                webBrowser1.Navigate(google.ToString());

                try
                {
                    translate = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", textBoxTranslate.Text, "", "en");
                    textBox9.Text = translate;
                }
                catch
                {
                    textBox9.Text = "Not Available";
                }

            }
            else if (countrycode == 2)
            {
                GetCurrency = CallWebService2.ConversionRate(net.webservicex.www1.Currency.EUR, net.webservicex.www1.Currency.INR);
                textBox7.Text = ("The Exchange Rate is ₹" + GetCurrency.ToString("C") + " for €1");

                string GetCountry = CallWebService.GetCitiesByCountry("India");
                string GetWeather = CallWebService.GetWeather("Airport", "India");
                textBox1.Text = getInfo(GetCountry, "<Country>", "</Country>");
                textBox2.Text = getInfo(GetWeather, "<Location>", "</Location>");
                textBox3.Text = getInfo(GetWeather, "<Time>", "</Time>");
                textBox4.Text = "Conditions not available at this time";
                textBox5.Text = getInfo(GetWeather, "<Wind>", "</Wind>");
                textBox6.Text = getInfo(GetWeather, "<Temperature>", "</Temperature>");

                string GetDialNum = CallWebService1.GetISD("India");
                textBox8.Text = getInfo(GetDialNum, "<code>", "</code>");

                google.Append(listBox1.SelectedItem);
                webBrowser1.ScriptErrorsSuppressed = true;
                webBrowser1.Navigate(google.ToString());

                try
                {
                    translate = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", textBoxTranslate.Text, "", "hi");
                    textBox9.Text = translate;
                }
                catch
                {
                    textBox9.Text = "Not Available";
                }

            }
            else if (countrycode == 3)
            {
                GetCurrency = CallWebService2.ConversionRate(net.webservicex.www1.Currency.EUR, net.webservicex.www1.Currency.JPY);
                textBox7.Text = ("The Exchange Rate is ¥" + GetCurrency.ToString("C") + " for €1");

                string GetCountry = CallWebService.GetCitiesByCountry("Japan");
                string GetWeather = CallWebService.GetWeather("Tokyo International Airport", "Japan");
                textBox1.Text = getInfo(GetCountry, "<Country>", "</Country>");
                textBox2.Text = getInfo(GetWeather, "<Location>", "</Location>");
                textBox3.Text = getInfo(GetWeather, "<Time>", "</Time>");
                textBox4.Text = getInfo(GetWeather, "<SkyConditions>", "</SkyConditions>");
                textBox5.Text = getInfo(GetWeather, "<Wind>", "</Wind>");
                textBox6.Text = getInfo(GetWeather, "<Temperature>", "</Temperature>");

                string GetDialNum = CallWebService1.GetISD("Japan");
                textBox8.Text = getInfo(GetDialNum, "<code>", "</code>");

                google.Append(listBox1.SelectedItem);
                webBrowser1.ScriptErrorsSuppressed = true;
                webBrowser1.Navigate(google.ToString());

                try
                {
                translate = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", textBoxTranslate.Text, "", "ja");
                textBox9.Text = translate;
                }
               catch {
                   textBox9.Text = "Not Available";
               }
            }
            else if (countrycode == 4)
            {
                GetCurrency = CallWebService2.ConversionRate(net.webservicex.www1.Currency.EUR, net.webservicex.www1.Currency.GBP);
                textBox7.Text = ("The Exchange Rate is £" + GetCurrency.ToString("C") + " for €1");

                string GetCountry = CallWebService.GetCitiesByCountry("United Kingdom");
                string GetWeather = CallWebService.GetWeather("Heathrow Airport", "United Kingdom");
                textBox1.Text = getInfo(GetCountry, "<Country>", "</Country>");
                textBox2.Text = getInfo(GetWeather, "<Location>", "</Location>");
                textBox3.Text = getInfo(GetWeather, "<Time>", "</Time>");
                textBox4.Text = getInfo(GetWeather, "<SkyConditions>", "</SkyConditions>");
                textBox5.Text = getInfo(GetWeather, "<Wind>", "</Wind>");
                textBox6.Text = getInfo(GetWeather, "<Temperature>", "</Temperature>");

                string GetDialNum = CallWebService1.GetISD("United Kingdom");
                textBox8.Text = getInfo(GetDialNum, "<code>", "</code>");

                google.Append(listBox1.SelectedItem);
                webBrowser1.ScriptErrorsSuppressed = true;
                webBrowser1.Navigate(google.ToString());

                try
                {
                    translate = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", textBoxTranslate.Text, "", "en");
                    textBox9.Text = translate;
                }
                catch
                {
                    textBox9.Text = "Not Available";
                }

            }
            else if (countrycode == 5)
            {
                GetCurrency = CallWebService2.ConversionRate(net.webservicex.www1.Currency.EUR, net.webservicex.www1.Currency.USD);
                textBox7.Text = ("The Exchange Rate is $" + GetCurrency.ToString("C") + " for €1");

                string GetCountry = CallWebService.GetCitiesByCountry("United States");
                string GetWeather = CallWebService.GetWeather("Reagan National Airport", "United States");
                textBox1.Text = getInfo(GetCountry, "<Country>", "</Country>");
                textBox2.Text = getInfo(GetWeather, "<Location>", "</Location>");
                textBox3.Text = getInfo(GetWeather, "<Time>", "</Time>");
                textBox4.Text = getInfo(GetWeather, "<SkyConditions>", "</SkyConditions>");
                textBox5.Text = getInfo(GetWeather, "<Wind>", "</Wind>");
                textBox6.Text = getInfo(GetWeather, "<Temperature>", "</Temperature>");

                string GetDialNum = CallWebService1.GetISD("United States");
                textBox8.Text = getInfo(GetDialNum, "<code>", "</code>");

                google.Append(listBox1.SelectedItem);
                webBrowser1.ScriptErrorsSuppressed = true;
                webBrowser1.Navigate(google.ToString());

                try
                {
                    translate = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", textBoxTranslate.Text, "", "en");
                    textBox9.Text = translate;
                }
                catch { }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please select a country");
            }
        }
             public static String getInfo(String w, String st, String nd)
        {

            //Substring finds & copies a value of a string to another variable
            int start = w.IndexOf(st); //specifies the starting index of the selected string
            start += st.Length;  //specifies the length of the selected string
            int end = w.IndexOf(nd); //specifies the end of the selected string
            try
            {
                //This substring will remove the tags from the returned websevice values every time the getinfo method is called
                //In references it states this is being called 41 times throughout the program
                String myInfo = w.Substring(start, end - start);
                return myInfo;
            }
            catch 
            {
                return "";
            }
                 
        }


    }
}
