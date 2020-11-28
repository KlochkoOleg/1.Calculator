using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {

        TextBox txt = new TextBox();
       
        
        Calc C;
        public Form1()
        {
            InitializeComponent();
            C = new Calc();
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
            KeyValuePair<string, EventHandler>[] ButtonMass =
            {
                new KeyValuePair<string, EventHandler>("MC", MCEvent),
                new KeyValuePair<string, EventHandler>("7", NumEvent),
                new KeyValuePair<string, EventHandler>("4", NumEvent),
                new KeyValuePair<string, EventHandler>("1", NumEvent),
                new KeyValuePair<string, EventHandler>("±", UnaryOperatorEvent),
                new KeyValuePair<string, EventHandler>("MR", MREvent),
                new KeyValuePair<string, EventHandler>("8", NumEvent),
                new KeyValuePair<string, EventHandler>("5", NumEvent),
                new KeyValuePair<string, EventHandler>("2", NumEvent),
                new KeyValuePair<string, EventHandler>("0", NumEvent),
                new KeyValuePair<string, EventHandler>("M+/-", MPlusEvent),
                new KeyValuePair<string, EventHandler>("9", NumEvent),
                new KeyValuePair<string, EventHandler>("6", NumEvent),
                new KeyValuePair<string, EventHandler>("3", NumEvent),
                new KeyValuePair<string, EventHandler>(".", PointEvent),
                new KeyValuePair<string, EventHandler>("CE", CEEvent),
                new KeyValuePair<string, EventHandler>("÷", BinaryOperatorEvent),
                new KeyValuePair<string, EventHandler>("+", BinaryOperatorEvent),
                new KeyValuePair<string, EventHandler>("-", BinaryOperatorEvent),
                new KeyValuePair<string, EventHandler>("*", BinaryOperatorEvent),
                new KeyValuePair<string, EventHandler>("⌫", EraseEvent),
                new KeyValuePair<string, EventHandler>("C", CEvent),
                new KeyValuePair<string, EventHandler>("√", UnaryOperatorEvent),
                new KeyValuePair<string, EventHandler>("x²", UnaryOperatorEvent),
                new KeyValuePair<string, EventHandler>("=", ResultEvent)

            };
           

            int k = 30;
            int g = 0, w = 0;


            for (int s = 0; s < 5; s++)
            {
                for (int i = 0; i < 5; i++)
                {

                    Button button = new Button();
                    button.Size = new Size(40, 40);
                    button.Click += ButtonMass[w].Value;
                    button.Location = new Point(g, k);
                    button.Text = ButtonMass[w].Key;
                    this.Controls.Add(button);
                    k += 40;
                    w++;
                }
                g += 40;
                k = 30;
            }
            txt.Multiline =false;
            txt.ReadOnly = true;
            txt.Size = new Size(200, 30);
            txt.Location = new Point(0, 0);
            txt.TextAlign = HorizontalAlignment.Right;
            txt.Font = new Font("Arial Unicode MS", 18f);
            txt.Text = "0";
            this.Controls.Add(txt);
            txt.TextChanged += Txt_TextChanged;
        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {
            while (txt.Text.Length >= 10)
                txt.Text = txt.Text.Remove(txt.Text.Length-1, 1);
        }

        char znak;
        

        private void MCEvent(object sender, EventArgs e) 
        { C.Memory_Clear(); 
        }

        private void MREvent(object sender, EventArgs e)
        { txt.Text = C.MemoryShow().ToString(); 
        }
        private void MPlusEvent(object sender, EventArgs e)
        {
            C.M_Sum(Convert.ToDouble(txt.Text));
            txt.Text = "0";
        }
        private void MMinusEvent(object sender, EventArgs e) {
            C.M_Subtraction(Convert.ToDouble(txt.Text));
            txt.Text = "0";
        }
        private void MSEvent(object sender, EventArgs e) { C.Memory_Save(Convert.ToDouble(txt.Text)); }
        private void PercentEvent(object sender, EventArgs e) { }
        private void UnaryOperatorEvent(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "√")
            {
                try
                {
                    C.Put_A(Convert.ToDouble(txt.Text));
                    if (double.IsNaN(C.Sqrt()))
                        throw new Exception();
                    else
                        txt.Text = C.Sqrt().ToString();
                        C.Clear_A();
                }
                catch
                {
                    txt.Text = "Ошибка";
                }
                
            }
            if ((sender as Button).Text == "x²")
            {
                C.Put_A(Convert.ToDouble(txt.Text));
                txt.Text = C.Square().ToString();
                C.Clear_A();
            }
            if ((sender as Button).Text == "±")
            {
                if (txt.Text[0] == '-')
                    txt.Text = txt.Text.Remove(0, 1);
                else
                    txt.Text = "-" + txt.Text;
            }
        }
        private void CEEvent(object sender, EventArgs e) {
            txt.Text = "0";
            C.Memory_Clear();
            C.Clear_A();
        }
        private void CEvent(object sender, EventArgs e) { txt.Clear();txt.Text = "0"; }
        private void EraseEvent(object sender, EventArgs e)
        {
            if (txt.Text != "")
                txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1);
        }
        private void BinaryOperatorEvent(object sender, EventArgs e)
        {
            C.Put_A(Convert.ToDouble(txt.Text));
            znak = (sender as Button).Text[0];
            txt.Text = "0";

        }
        private void ResultEvent(object sender, EventArgs e)
        {
            switch (znak)
            {
                case '*':
                    txt.Text = C.Multiplication(Convert.ToDouble(txt.Text)).ToString();


                    break;
                case '÷':
                   try
                    {
                        if (double.IsInfinity(C.Division(Convert.ToDouble(txt.Text)))) throw new Exception();
                        else
                        {
                            txt.Text = C.Division(Convert.ToDouble(txt.Text)).ToString();
                        }
                       
                    }
                    catch
                    {
                        txt.Text = "Деление на ноль невозможно";
                    }
                    
                   

                    break;
                case '+':
                    txt.Text = C.Sum(Convert.ToDouble(txt.Text)).ToString();


                    break;
                case '-':
                    txt.Text = C.Subtraction(Convert.ToDouble(txt.Text)).ToString();


                    break;
            }

            C.Clear_A();
        }
        private void PointEvent(object sender, EventArgs e)
        {
            if (txt.Text.IndexOf('.') == -1)
                txt.Text = txt.Text + "."; 
        }
        private void NumEvent(object sender, EventArgs e)
        {
           
            if ((txt.Text[0] == '0') && (txt.Text != ""))
                txt.Text = txt.Text.Remove(0, 1);
            txt.Text += (sender as Button).Text;
        }
        
    }

}
