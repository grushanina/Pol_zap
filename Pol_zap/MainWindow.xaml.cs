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

namespace Pol_zap
{
    public partial class MainWindow : Window
    {
        Stack<char> myStack = new Stack<char>();
        Stack<int> Operands = new Stack<int>();

        string answer;

        int OpeartionPriority(char a)
        {
            switch (a)
            {
                case '+': return 1;
                case '-': return 1;
                case '*': return 2;
                case '/': return 2;
                case '^': return 3;
                default: return 0;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            answer = "";
            bool error = false;
            for (int i = 0; i < TextBox1.Text.Length; i++)
            {
                if (Char.IsDigit(TextBox1.Text[i]))
                {
                    answer += TextBox1.Text[i];
                    if (i != TextBox1.Text.Length -1 && Char.IsDigit(TextBox1.Text[i + 1]))
                    {
                        continue;
                    }
                    answer += ' ';
                    
                }

                if (TextBox1.Text[i] == '+' || TextBox1.Text[i] == '-' || TextBox1.Text[i] == '*' || TextBox1.Text[i] == '/' || TextBox1.Text[i] == '^')
                {
                    if (myStack.Count == 0)
                    {
                        myStack.Push(TextBox1.Text[i]);
                    }
                    else
                    {
                        if (OpeartionPriority(myStack.Peek()) >= OpeartionPriority(TextBox1.Text[i]))
                        {
                            while (myStack.Count != 0 && OpeartionPriority(myStack.Peek()) >= OpeartionPriority(TextBox1.Text[i]))
                            {
                              answer += myStack.Pop();
                              answer += ' ';
                            }

                            myStack.Push(TextBox1.Text[i]);
                        }
                        else
                        {
                            myStack.Push(TextBox1.Text[i]);
                        }
                    }
                }

                if (TextBox1.Text[i] == '(')
                {
                    myStack.Push(TextBox1.Text[i]);
                }

                if (TextBox1.Text[i] == ')')
                {
                    while (myStack.Count != 0 && myStack.Peek() != '(')
                    {
                        if (myStack.Count == 1 && myStack.Peek() != '(')
                        {
                            error = true;
                        }
                        answer += myStack.Pop();
                        answer += ' ';
                    }
                    if (!error)
                    {
                        myStack.Pop();
                    }
                    
                }

            }

            while(myStack.Count != 0)
            {
                if (myStack.Peek() == '(')
                {
                    error = true;
                    break;
                }
                answer += myStack.Pop();
                answer += ' ';
            }

            TextBox2.Clear();
            if (error)
            {
                TextBox2.Text = "ошибка!";
            }
            else
            {
                TextBox2.Text = answer;
            }
            
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int a;
            int b;
            int j;
            bool error = false;
            string digit = "";
            for (int i = 0; i < TextBox2.Text.Length; i++)
            {
                if (Char.IsDigit(TextBox2.Text[i]))
                {
                    j = i;
                    digit = "";
                    while (Char.IsDigit(TextBox2.Text[j]))
                    {
                        digit += TextBox2.Text[j];
                        j += 1;
                    }
                    i = j;
                    Operands.Push(int.Parse(digit));
                }

                if (TextBox2.Text[i] == '+')
                {
                    a = Operands.Pop();
                    b = Operands.Pop();
                    Operands.Push(a + b);
                }

                if (TextBox2.Text[i] == '-')
                {
                    a = Operands.Pop();
                    b = Operands.Pop();
                    Operands.Push(b - a);
                }

                if (TextBox2.Text[i] == '*')
                {
                    a = Operands.Pop();
                    b = Operands.Pop();
                    Operands.Push(a * b);
                }

                if (TextBox2.Text[i] == '/')
                {
                    a = Operands.Pop();
                    b = Operands.Pop();
                    if (a == 0)
                    {
                        error = true;
                        Operands.Push(b);
                        Operands.Push(a);
                        continue;
                    }
                    Operands.Push(b / a);
                }

                if (TextBox2.Text[i] == '^')
                {
                    a = Operands.Pop();
                    b = Operands.Pop();
                    Operands.Push(Convert.ToInt32(Math.Pow(b, a)));
                }
            }
            if (error)
            {
                TextBox3.Text = "ошибка!";
            }
            else
            {
                TextBox3.Text = Operands.Pop().ToString();
            }
            
            
        }

        private void TextBox3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
