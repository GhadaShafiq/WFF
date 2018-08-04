using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFF_Windows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BorderStyle = BorderStyle.Fixed3D;
            
           
        }

        static bool ParenMatch(string x)
        {
            Stack<char> S = new Stack<char>(100);
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == '(')
                {
                    S.Push(x[i]);
                }
                else if (x[i] == ')')
                {
                    if (S.IsEmpty())
                    {
                        return false;
                    }
                    //(: 40 ): 41
                    int j = ((int)S.Peek()) + 1;
                    if ((int)x[i] != j)
                        return false;

                    S.Pop();
                }

            }
            if (S.IsEmpty())
                return true;
            else
                return false;

        }
       
        private void check_Click(object sender, EventArgs e)
        {
            string exp = textBox1.Text;
            bool chk = Check(exp);
            if (chk)
                result.Text = "WFF";
            else
                result.Text = "NOT WFF";
        }
        static bool Check(string exp)
        {


            if (!ParenMatch(exp))
                return false;

            if (!Digits(exp))
                return false;

            if (String.IsNullOrWhiteSpace(exp))
                return false;

            if (!(exp[0] == '(' || exp[0] == '~' || exp[0] == '_' || char.IsLower(exp, 0)))
                return false;

            for (int i = 0; i < exp.Length; i++)
            {

                if (!(exp[i] == '^' || exp[i] == '|' || exp[i] == '~' ||
                    exp[i] == '_' || exp[i] == '(' || exp[i] == ')' ||
                    exp[i] == '→' || exp[i] == '↔' ||
                    char.IsLower(exp, i) || char.IsDigit(exp, i)))
                    return false;


                if (exp[i] == ')')
                {
                    if (i > 0)
                    {
                        if (!(char.IsLower(exp,i-1)|| char.IsDigit(exp, i - 1) || exp[i-1]==')' || exp[i-1]=='_'))
                            return false;
                    }
                }
                if (exp[i] == '~')
                {
                    
                    int var = exp.Length - 1;
                    if (i == var)
                        return false;
                    if (i < var)
                    {
                        if (!(char.IsLower(exp,i+1)|| exp[i + 1] == '(' || exp[i+1]=='_'))
                            return false;
                    }
                    if (i > 0)
                    {
                        if (char.IsLower(exp,i-1) || exp[i-1]==')')
                            return false;
                    }



                }

                //alt+26 for → , alt+29 for ↔

                if (exp[i] == '^' || exp[i] == '|' || exp[i] == '→' || exp[i] == '↔')
                {
                    if (i == 0 || i == exp.Length - 1)
                    {
                        return false;
                    }
                    
                    if (exp[i + 1] == '^' || exp[i + 1] == '|' || exp[i + 1] == '→' || exp[i + 1] == '↔')
                        return false;
                }
                if (char.IsLower(exp,i))
                {
                    int var = exp.Length - 1;
                    if (i < var)
                    {
                        //if it has a letter after it or ( or not , return false
                        if (char.IsLower(exp,i+1) || exp[i + 1] == '(' || exp[i + 1] == '~')
                            return false;
                        //  if (!(exp[i + 1] == '_' || char.IsDigit(exp,i+1)))
                        //  return false;

                    }
                    if(i > 0)
                    {
                        if (char.IsDigit(exp, i - 1))
                            return false;
                    }
                }
                if (exp[i] == '_')
                {

                    int var = exp.Length - 1;

                    if (i == var)
                    {
                        if (i == 0)
                            return false;
                    }

                    if (i < var)
                    {

                        if ( exp[i+1] =='(')
                            return false;
                    }
                    if (i > 0)
                    {
                        if (exp[i - 1] == ')')
                            return false;
                    }
                }



            }
            return true;
        }
        static bool Digits(string exp)
        {
            int var = exp.Length - 1;
            for (int i = 0; i < exp.Length; i++)
            {
                if (char.IsDigit(exp, i))
                {
                    if (i > 0)
                    {
                        //number muxt have a letter or _ or another number before it
                        if (!(char.IsLower(exp,i-1) || exp[i - 1] == '_' || char.IsDigit(exp, i - 1)))
                            return false;
                    }
                    if(i<var)
                    {
                        if (char.IsLower(exp, i + 1))
                            return false;
                    }
                    
                }

            }
            return true;

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
    class Stack<T>
    {
        private T[] m_items;
        private int top = -1;

        public Stack(int capacity)
        {
            m_items = new T[capacity];
        }

        public int GetSize()
        {
            return top + 1;
        }

        public bool IsEmpty()
        {
            return top < 0;
        }

        public void Push(T val)
        {

            if (GetSize() == m_items.Length)
                throw new Exception("Sorry, Full Stack, can't add new item");
            else
            {

                top++;
                m_items[top] = val;
            }
        }
        public T Pop()
        {
            if (IsEmpty())
                throw new Exception("Empty Stack, can't remove");
            else
            {
                T oldTop = m_items[top];
                top--;
                return oldTop;
            }
        }
        public T Peek()
        {
            if (IsEmpty())
                throw new Exception("Empty Stack, can't get top");
            else
            {
                return m_items[top];
            }
        }
    }
}
