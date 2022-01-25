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
using System.Text.RegularExpressions;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double LastNo, results;
        selectOp selectOp;
        string res;
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
            SqrtBtn.Content = "\u221Ax";
            backspace.Content = "\u232B";

            C.Click += C_Click;
            CE.Click += CE_Click;
            backspace.Click += Backspace_Click;

            percent.Click += Percent_Click;
            equalto.Click += Equalto_Click;
            x2.Click += X2_Click;
            SqrtBtn.Click += SqrtBtn_Click;
            ONEbyX.Click += ONEbyX_Click;
            equalto.MouseDoubleClick += Equalto_MouseDoubleClick;
            twozeroes.Click += Twozeroes_Click;

        }


        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            double tempno;
            if (double.TryParse(result.Content.ToString(), out tempno))
            {
                tempno = tempno / 100;
                if (LastNo != 0)
                {
                    tempno *= LastNo;
                }
                result.Content = tempno.ToString();
            }
        }
        
        private void C_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(prresult.Content.ToString(), @"[+-/*](\d+)$"))
            {
                string s = Regex.Match(prresult.Content.ToString(), @"(\d+)$").ToString();
                if (s == result.Content.ToString())
                {
                    prresult.Content = Regex.Replace(prresult.Content.ToString(), @"(\d+)$", "").ToString();

                }
            }
            result.Content = "0";
        }
        private void CE_Click(object sender, RoutedEventArgs e)
        {
            result.Content = "0";
            prresult.Content = "0";
            LastNo = 0;

        }


        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            int stlen = result.Content.ToString().Length;
            result.Content = result.Content.ToString().Remove(stlen - 1);
            if (result.Content == "")
            {
                result.Content = "0";
            }
        }

        private void Twozeroes_Click(object sender, RoutedEventArgs e)
        {
            if (result.Content.ToString()!="0")
            {
                result.Content = result.Content.ToString() + "0";
            }
        }


        private void ONEbyX_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(result.Content.ToString(), out LastNo))
            {
                if (LastNo != 0)
                {
                    LastNo = 1 / LastNo;
                    result.Content = LastNo.ToString();
                }
                else
                {
                    MessageBox.Show("Division by 0 is not supported!!!", "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void SqrtBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(result.Content.ToString(), out LastNo))
            {
                LastNo = Math.Sqrt(LastNo);
                result.Content = LastNo.ToString();
            }
        }

        private void X2_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(result.Content.ToString(), out LastNo))
            {
                LastNo = LastNo * LastNo ;
                result.Content = LastNo.ToString();
            }
        }

        private void Equalto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            result.Content = "0";
            prresult.Content = "0";
        }


        private void Equalto_Click(object sender, RoutedEventArgs e)
        {
            double NewNo;
            if (double.TryParse(result.Content.ToString(), out NewNo))
            {
                switch (selectOp)
                {
                    case selectOp.add:
                        res = LastNo + "+" + NewNo;
                        results = SimpleMath.add(LastNo,NewNo);
                        res = LastNo + "+" + NewNo + "=" + results;
                        break;
                    case selectOp.sub:
                        res = LastNo + "-" + NewNo;
                        results = SimpleMath.sub(LastNo, NewNo);
                        res = LastNo + "-" + NewNo + "=" + results;
                        break;
                    case selectOp.mul:
                        res = LastNo + "*" + NewNo;
                        results = SimpleMath.mul(LastNo, NewNo);
                        res = LastNo + "*" + NewNo + "=" + results;
                        break;
                    case selectOp.div:
                        res = LastNo + "/" + NewNo;
                        results = SimpleMath.div(LastNo, NewNo);
                        res = LastNo + "/" + NewNo + "=" + results;
                        break;
                }
                prresult.Content = res.ToString();
                result.Content = results.ToString();
            }
        }


        private void dot_Click(object sender, RoutedEventArgs e)
        {
            if (result.Content.ToString().Contains(".")) {  } else { result.Content = $"{result.Content}."; }
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(result.Content.ToString(), out LastNo))
            {
                result.Content = "0";
            }
            if (sender == plus)
            {
                prresult.Content = LastNo + "+";
                selectOp = selectOp.add;
            }
            else if (sender==minus)
            {
                prresult.Content = LastNo + "-";
                selectOp = selectOp.sub;
            }
            else if (sender == multiply)
            {
                prresult.Content = LastNo + "*";
                selectOp = selectOp.mul;
            }
            else if (sender == divide)
            {
                prresult.Content = LastNo + "/";
                selectOp = selectOp.div;
            }
            
        }
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            int selectedvalue = int.Parse((sender as Button).Content.ToString());

            //if (sender == one) { selectedvalue = 1;}

            if ((prresult.Content.ToString() != "0" && Regex.IsMatch(prresult.Content.ToString(), @"((\+)|(\-)|(\*)|(\\))$")))
            {
                prresult.Content = $"{prresult.Content}{selectedvalue}";
            }

            if (result.Content.ToString() == "0")
            {
                result.Content = $"{selectedvalue}";
            }           
            else
            {
                result.Content = $"{result.Content}{selectedvalue}";
            }
            prresult.Content = prresult.Content.ToString();
            


        }
    }
    public enum selectOp
    {
        add,
        sub,
        mul,
        div,
    }
    public class SimpleMath
    {
        public static double add(double n1, double n2)
        {
            return n1 + n2;
        }
        public static double sub(double n1, double n2)
        {
            return n1 - n2;
        }
        public static double mul(double n1, double n2)
        {
            return n1 * n2;
        }
        public static double div(double n1, double n2)
        {
            if (n2==0)
            {
                MessageBox.Show("Division by 0 is not supported!!!","Invalid operation",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return 0;
            }
            return n1 / n2;
        }
        
    }
    
}
