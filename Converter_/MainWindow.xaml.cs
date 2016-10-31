using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Converter_ 
{ 
    public partial class MainWindow : Window
    {
        string dec1;
        bool minDecCheck = false;
        public MainWindow()
        {
            InitializeComponent();
            Title = "Number System Converter v2.3.5";
            buttonClear.Click += clearAll;
            copy1.Click += clickCopy1;
            copy2.Click += clickCopy2;
            copy3.Click += clickCopy3;
            copy4.Click += clickCopy4;
            dec.KeyDown += new KeyEventHandler(decKeyDown); // Decimal Textbox
            bin.KeyDown += new KeyEventHandler(binKeyDown); // Binary Textbox
            oct.KeyDown += new KeyEventHandler(octKeyDown); // Octal Textbox
            hex.KeyDown += new KeyEventHandler(hexKeyDown); // Hexadecimal Textbox
        }

        private void clickCopy1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(dec.Text);
        }
        private void clickCopy2(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(bin.Text);
        }
        private void clickCopy3(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(oct.Text);
        }
        private void clickCopy4(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(hex.Text);
        }

        private void clearAll(object sender, RoutedEventArgs e)
        {
            clear();
        }
        
        // About
        public void aboutApp(object sender, RoutedEventArgs e)
        {
            Window1 win2 = new Window1();
            win2.Show();
        }
        
        private void clear()
        {
            dec.Clear();
            bin.Clear();
            oct.Clear();
            hex.Clear();
        }              
        
        private void decKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (dec.Text.Any(Char.IsLetter))
                    {
                        MessageBox.Show("Invalid input!\n" + dec.Text + " is not a number", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        clear();
                    }
                    else
                    {
                        long dec2;
                        // Checks whether the decimal is a minus value:
                        if (dec.Text[0] == '-')
                        {
                            // Removes the "-" from the string before conversion:
                            string x = dec.Text.Replace(@"-", "");
                            dec2 = Convert.ToInt64(x);
                            minDecCheck = true;
                        }
                        else
                        {
                            dec2 = Convert.ToInt64(dec.Text);
                        }
                        decConvert(dec2);
                    }
                }
           } 
           catch (OverflowException)
           {
                MessageBox.Show("Value you entered is too long! Please Enter a value between -9223372036854775807 and +9223372036854775807.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
           }
        }
        
        private void binKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                dec1 = bin.Text;
                binConvert(dec1);
            }
        }
        
        private void octKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                dec1 = oct.Text;
                octConvert(dec1);
            }
        }
        
        private void hexKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                dec1 = hex.Text;
                hexConvert(dec1);
            }
        }     
        
        //Conversions -->

        // Decimal to other 
        private void decConvert(long b)
        {
            try
            {
                if (minDecCheck) // is a minus
                {
                    decBin(b);
                }
                else
                {
                    decBin(b);
                    decOct(b);
                    decHex(b);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Program encountered an error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        // Binary to dec
        private void binConvert(string a)
        {
            try
            {
                int[] x = new int[a.Length + 1]; 
                int k = 0;
                for (int i = a.Length - 1; i >= 0; i--)
                {
                    x[k] = a[i] - '0';
                    if (x[k] > 1 | x[k] < 0)
                    {
                        MessageBox.Show("Invalid input!\n" + a + " is not a binary number", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        clear();
                        break;
                    }
                    k++;
                }
                long sum = 0;
                if (x[k] == 1)
                {
                    sum = 1;
                }
                for (int y = 0; y <= k; y++)
                {
                    if (x[y] == 1)
                    {
                        sum = sum + (long)(Math.Pow(2, y));
                    }
                }
                if (sum < 0) // if the input binary is a minus
                {
                    oct.Clear();
                    hex.Clear();
                    dec.Text = sum.ToString(); 
                }
                else
                {
                    dec.Text = sum.ToString();
                    // To convert the just binary-to-decimal converted number to other format; calls following methods:
                    decOct(sum);
                    decHex(sum);
                }            
            }
            catch (Exception e)
            {
                MessageBox.Show("Program encountered an error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Oct to dec
        private void octConvert(string a)
        {
            try
            {
                int[] x = new int[a.Length + 1];
                int k = 0;
                long sum = 0;
                for (int i = a.Length - 1; i >= 0; i--)
                {
                    x[k] = a[i] - '0';                    
                    k++;
                }
                for (int i = 0; i <= a.Length - 1; i++)
                {
                    if (x[i] > 7 | x[i] < 0)
                    {
                        clear();
                        MessageBox.Show("Invalid input!\n" + a + " is not an octal number", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        break;
                    }
                    if (i >= 2)
                    {
                        sum = sum + (x[i] * (long)(Math.Pow(8, i)));
                    }
                    else if (i == 1)
                    {
                        sum = sum + (x[i] * 8);
                    }
                    else if (i == 0)
                    {
                        sum = sum + (x[i] * 1);
                    }
                }
                if (true)
                {
                    dec.Text = sum.ToString();
                    // To convert the just octal-to-decimal converted number to other format; calls following methods:
                    decBin(sum);
                    decHex(sum);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Program encountered an error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //HEX to dec
        private void hexConvert(string a)
        {
            try
            {
                bool check = true;
                long sum = 0;
                int c = 0;
                //Converts the string to an int array
                int[] y = a.ToUpper().Select(s => Convert.ToInt32(s - 48)).ToArray();
                foreach (int o in y)
                {
                    if (o == 17)
                    {
                        y[c] = 10;
                    }
                    else if (o == 18)
                    {
                        y[c] = 11;
                    }
                    else if (o == 19)
                    {
                        y[c] = 12;
                    }
                    else if (o == 20)
                    {
                        y[c] = 13;
                    }
                    else if (o == 21)
                    {
                        y[c] = 14;
                    }
                    else if (o == 22)
                    {
                        y[c] = 15;
                    }
                    if (o > 22 | o < 0)
                    {
                        check = false; 
                        clear();
                        MessageBox.Show("Invalid input!\n" + a + " is not a hexadecimal number", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        break;
                    }
                    c++;
                }
                Array.Reverse(y);
                for (int i = y.Length - 1; i >= 0; i--)
                {
                    if (i > 1)
                    {
                        sum = sum + (y[i] * (long)(Math.Pow(16, i)));
                    }
                    else if (i == 1)
                    {
                        sum = sum + (y[i] * 16);
                    }
                    else if (i == 0)
                    {
                        sum = sum + (y[i] * 1);
                    }
                }
                //If the number does not contain a non-hexa number/character
                if (check)  
                {
                    dec.Text = sum.ToString();
                    // To convert the just hexadecimal-to-decimal converted number to other format; calls following methods:
                    decOct(sum);                    
                    decBin(sum);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Program encountered an error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Dec -> bin
        private void decBin(long a)
        {
            bool check = false;
            if (a == 0)
            {
                check = true;
            }
            int[] output = new int[64];
            int count = 0;
            int i = 0;
            while (true)
            {
                if (a % 2 == 1)
                {
                    count = 1;
                }
                else if (a % 2 == 0)
                {
                    count = 0;
                }
                output[i] = count;
                a = a / 2;
                if (a < 1)
                {
                    break;
                }
                i++;
            }
            output[i] = 1;
            bin.Text = null;
            // If the decimal is a minus value:
            if (minDecCheck)
            {
                count = 0;
                foreach (int o in output)
                {
                    if (o == 1)
                    {
                        output[count] = 0;
                    }
                    else
                    {
                        output[count] = 1;
                    }
                    count++;
                }
                int k = 1;
                for (int j = 0; j < i; j++)
                {
                    if (output[j] == 0)
                    {
                        output[j] = 1;
                        break;
                    }
                    else if (output[j] == 1)
                    {
                        output[j] = 0;
                        if (output[k] == 0 && k <= output.Length)
                        {
                            output[k] = 1;
                            break;
                        }
                        else if (output[k] == 1 && k <= output.Length)
                        {
                            k++;
                            continue;
                        }
                        k++;
                    }
                }
                hex.Text = null;
                oct.Text = null;
                for (int j = output.Length - 1; j >= 0; j--)
                {
                    bin.AppendText(output[j].ToString());
                }
                minDecCheck = false;
            }
            else
            {
                if (!check)
                {
                    for (int f = i; f >= 0; f--)
                    {
                        bin.AppendText(output[f].ToString());
                    }
                }
                else
                {
                    bin.Text = "0";
                }
            }
        }

        //  Dec -> oct
        private void decOct(long decOne)
        {            
            int f = 0;
            long count = 0;            
            long[] c = new long[21];
            if (decOne < 8) {
                oct.Text = decOne.ToString();
            }
            else
            {
                while (true)
                {
                    count = decOne % 8;
                    if (decOne / 8 <= 0)
                    {
                        c[f] = decOne % 8;
                    }
                    else if (count >= 0)
                    {
                        c[f] = count;
                    }
                    decOne /= 8;
                    if (decOne == 0)
                    {
                        break;
                    }
                    f++;
                }                
                oct.Text = null;
                for (int i = f; i >= 0; i--)
                { 
                    oct.AppendText(c[i].ToString());                   
                }
            }
        }

        // Dec -> hex
        private void decHex(long decOne)
        {
            string[] heX = new string[16];
            char h = ' ';
            int f = 0;
            long count = 0;
            if (decOne < 10)
            {
                hex.Text = decOne.ToString();
            }
            else if (decOne > 9 && decOne < 16)
            {
                if (decOne == 10)
                {
                    h = 'A';
                }
                else if (decOne == 11)
                {
                    h = 'B';
                }
                else if (decOne == 12)
                {
                    h = 'C';
                }
                else if (decOne == 13)
                {
                    h = 'D';
                }
                else if (decOne == 14)
                {
                    h = 'E';
                }
                else if (decOne == 15)
                {
                    h = 'F';
                }
                hex.Text = h.ToString();
            }
            else
            {
                while (true)
                {
                    count = decOne % 16;
                    if (decOne / 16 < -1)
                    {
                        heX[f] = (decOne % 16).ToString();
                    }
                    else if (count >= 0)
                    {
                        heX[f] = count.ToString();
                        if (heX[f] == "10")
                        {
                            heX[f] = "A";
                        }
                        else if (heX[f] == "11")
                        {
                            heX[f] = "B";
                        }
                        else if (heX[f] == "12")
                        {
                            heX[f] = "C";
                        }
                        else if (heX[f] == "13")
                        {
                            heX[f] = "D";
                        }
                        else if (heX[f] == "14")
                        {
                            heX[f] = "E";
                        }
                        else if (heX[f] == "15")
                        {
                            heX[f] = "F";
                        }
                    }
                    decOne = decOne / 16;
                    if (decOne == 0) {
                        break;
                    }
                    f++;
                }               
                hex.Text = null;
                Array.Reverse(heX);
                foreach(string o in heX)
                {
                    hex.AppendText(o);                    
                }
            }
        }
                        
        protected override void OnClosed(EventArgs e)
        {            
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
    }
}