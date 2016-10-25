using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace Converter_ { 
    public partial class MainWindow : Window {
        string dec1;
        public MainWindow() {
            InitializeComponent();
            Title = "Number System Converter v2.3";
            buttonClear.Click += clearAll; // Clear every textbox
            about.Click += aboutApp; // About button
            copy1.Click += clickCopy1;
            copy2.Click += clickCopy2;
            copy3.Click += clickCopy3;
            copy4.Click += clickCopy4;
            dec.KeyDown += new KeyEventHandler(decKeyDown); // Decimal Textbox
            bin.KeyDown += new KeyEventHandler(binKeyDown); // Binary Textbox
            oct.KeyDown += new KeyEventHandler(octKeyDown); // Octal Textbox
            hex.KeyDown += new KeyEventHandler(hexKeyDown); // Hexadecimal Textbox
        }

        private void clickCopy1(object sender, RoutedEventArgs e) {
            Clipboard.SetText(dec.Text);
        }
        private void clickCopy2(object sender, RoutedEventArgs e) {
            Clipboard.SetText(bin.Text);
        }
        private void clickCopy3(object sender, RoutedEventArgs e) {
            Clipboard.SetText(oct.Text);
        }
        private void clickCopy4(object sender, RoutedEventArgs e) {
            Clipboard.SetText(hex.Text);
        }

        private void clearAll(object sender, RoutedEventArgs e) {
            clear();
        }
        
        // About button
        public void aboutApp(object sender, RoutedEventArgs e) {            
            Window1 win2 = new Window1();
            win2.Show();
        }

        // Clears every textbox
        private void clear() {
            dec.Clear();
            bin.Clear();
            oct.Clear();
            hex.Clear();
        }              

        // Decimal 
        private void decKeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) {
                dec1 = dec.Text;
                decConvert(dec1);
            }
        }

        // Binary
        private void binKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                dec1 = bin.Text;
                binConvert(dec1);
            }
        }

        //Octal
        private void octKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                dec1 = oct.Text;
                octConvert(dec1);
            }
        }

        // Hexadecimal
        private void hexKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                dec1 = hex.Text;
                hexConvert(dec1);
            }
        }     
        
        // Decimal to other 
        private void decConvert(string a) {
            try {                 
                decBin(a);
                decHex(a);
                decOct(a);
            } catch (Exception e) {
                MessageBox.Show("Program encountered an error: " + e.Message);
            }

        }

        // Binary to other
        private void binConvert(string a) {
            try {
                int[] x = new int[a.Length + 1]; 
                int k = 0;
                for (int i = a.Length - 1; i >= 0; i--) {
                    x[k] = a[i] - '0';
                    if (x[k] > 1 | x[k] < 0) {
                        MessageBox.Show("Invalid input!\n" + a + " is not a binary number");
                        clear();
                        break;
                    }
                    k++;
                }
                long sum = 0;
                if (x[k] == 1) {
                    sum = 1;
                }
                for (int y = 0; y <= k; y++) {
                    if (x[y] == 1) {
                        sum = sum + (long)(Math.Pow(2, y));
                    }
                }                
                dec.Text = sum.ToString();
                // To convert the just binary-to-decimal converted number to other format; calls following methods:
                decOct(sum.ToString()); 
                decHex(sum.ToString());                
            }
            catch (Exception e) {
                MessageBox.Show("Program encountered an error: " + e.Message);
            }
        }

        //Oct to all
        private void octConvert(string a) {
            try {
                int[] x = new int[a.Length + 1];
                int k = 0, c = 0;
                long sum = 0;
                for (int i = a.Length - 1; i >= 0; i--) {
                    x[k] = a[i] - '0';
                    if (x[k] > 7 | x[k] < 0) {
                        clear();
                        MessageBox.Show("Invalid input!\n" + a + " is not an octal number");
                        break;
                    }
                    k++;
                }
                for (int i = 0; i <= k; i++) {
                    if (c >= 2) {
                        sum = sum + (x[c] * (long)(Math.Pow(8, c)));
                    } else if (c == 1) {
                        sum = sum + (x[c] * 8);
                    } else if (c == 0) {
                        sum = sum + (x[c] * 1);
                    }
                    c++;
                }
                if (true) {
                    dec.Text = sum.ToString();
                    // To convert the just octal-to-decimal converted number to other format; calls following methods:
                    decBin(sum.ToString());
                    decHex(sum.ToString());
                }
            }
            catch (Exception e) {
                MessageBox.Show("Program encountered an error: " + e.Message);
            }
        }

        //HEX to all
        private void hexConvert(string a) {
            try {
                bool check = true;
                long sum = 0, c = 0;
                //Converts the string to an int array
                int[] y = a.ToUpper().Select(s => Convert.ToInt32(s - 48)).ToArray();
                foreach (int o in y) {
                    if (o == 17) {
                        y[c] = 10;
                    } else if (o == 18) {
                        y[c] = 11;
                    } else if (o == 19) {
                        y[c] = 12;
                    } else if (o == 20) {
                        y[c] = 13;
                    } else if (o == 21) {
                        y[c] = 14;
                    } else if (o == 22) {
                        y[c] = 15;
                    }
                    if (o > 22 | o < 0) {
                        check = false; 
                        clear();
                        MessageBox.Show("Invalid input!\n" + a + " is not a hexadecimal number");
                        break;
                    }
                    c++;
                }

                Array.Reverse(y);
                for (int i = y.Length - 1; i >= 0; i--) {
                    if (i > 1) {
                        sum = sum + (y[i] * (long)(Math.Pow(16, i)));
                    } else if (i == 1) {
                        sum = sum + (y[i] * 16);
                    } else if (i == 0) {
                        sum = sum + (y[i] * 1);
                    }
                }
                if (check) { //If the number does not contain a non-hexa number/character
                    dec.Text = sum.ToString();
                    // To convert the just hexadecimal-to-decimal converted number to other format; calls following methods:
                    decOct(sum.ToString());
                    decHex(sum.ToString()); // This will display the hexa number user entered with uppercase A->F
                    decBin(sum.ToString());
                }
            }
            catch (Exception e) {
                MessageBox.Show("Program encountered an error: " + e.Message);
            }
        }  
        
        //  Dec > oct
        private void decOct(string a) {            
            int f = 0;
            long decOne = 0, count = 0;
            long.TryParse(a.Trim(), out decOne);
            long[] c = new long[64];
            if (decOne < 8) {
                oct.Text = decOne.ToString();
            } else {
                while (true) {
                    count = decOne % 8;
                    if (decOne / 8 <= 0) {
                        c[f] = decOne % 8;
                    } else if (count >= 0) {
                        c[f] = count;
                    }
                    decOne /= 8;
                    if (decOne == 0) {
                        break;
                    }
                    f++;
                }
                
                oct.Text = null;                
                //For insted of foreach to avoid displaying c[0] which is 0 -- find a solution!
                for (int i = f; i >= 0; i--) { 
                    oct.AppendText(c[i].ToString());                   
                }
            }
        }

        // Dec > hex
        private void decHex(string a) {            
            string[] heX = new string[a.Length + 1];
            char h = ' ';
            int f = 0;
            long count = 0, decOne = 0;
            long.TryParse(a, out decOne);
            if (decOne < 10) {
                hex.Text = decOne.ToString();
            } else if (decOne > 9 && decOne < 16) {
                if (decOne == 10) {
                    h = 'A';
                } else if (decOne == 11) {
                    h = 'B';
                } else if (decOne == 12) {
                    h = 'C';
                } else if (decOne == 13) {
                    h = 'D';
                } else if (decOne == 14) {
                    h = 'E';
                } else if (decOne == 15) {
                    h = 'F';
                }
                hex.Text = h.ToString();
            } else {
                while (true) {
                    count = decOne % 16;
                    if (decOne / 16 < -1) {
                        heX[f] = (decOne % 16).ToString();
                    } else if (count >= 0) {
                        heX[f] = count.ToString();
                        if (heX[f] == "10") {
                            heX[f] = "A";
                        } else if (heX[f] == "11") {
                            heX[f] = "B";
                        } else if (heX[f] == "12") {
                            heX[f] = "C";
                        } else if (heX[f] == "13") {
                            heX[f] = "D";
                        } else if (heX[f] == "14") {
                            heX[f] = "E";
                        } else if (heX[f] == "15") {
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
                foreach(string o in heX) {
                    hex.AppendText(o);                    
                }
            }
        }

        // Dec > bin
        private void decBin(string a) {
            long decOne = 0;
            bool check = false;
            long.TryParse(a, out decOne);
            if (decOne == 0) {
                check = true;
            }
            long[] output = new long[64];
            long count = 0;
            int i = 0;
            while (true) {
                if (decOne % 2 == 1) {
                    count = 1;
                } else if (decOne % 2 == 0) {
                    count = 0;
                }
                output[i] = count;
                decOne = decOne / 2;
                if (decOne < 1) {
                    break;
                }
                i++;
            }
            output[i] = 1;

            long[] binArray = new long[i + 1];
            int l = 0;
            for (int j = i; j >= 0; j--) {
                binArray[l] = output[j];
                l++;
            }            
            bin.Text = null;
            if(!check) {
                for (int f = 0; f < binArray.Length; f++) {
                    bin.AppendText(binArray[f].ToString());
                }
            } else {
                bin.Text = "0";
            }            
        }
                
        //Closing the app         
        protected override void OnClosed(EventArgs e) {            
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
    }
}