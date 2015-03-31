using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sprite_Demo
{
    public partial class Form2 : Form
    {
        int a, b, c, d,n1,n2,n3,n4;
        int time=0;
        Random random = new Random();
        List<string> _items = new List<string>();
        public Form1 form1;
        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = "猜數字";
            textBox1.Focus();
            do
            {
                a = random.Next(10);
                b = random.Next(10);
                c = random.Next(10);
                d = random.Next(10);
            } while ((a == b) || (a == c) || (a == d) || (b == c) || (b == d) || (c == d));
            label1.Text = a + "," + b + "," + c + "," + d;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str=textBox1.Text.Trim();
            if (str.Length < 4)
                MessageBox.Show("請輸入四位數字");
            else
            {
                char[] num=str.ToCharArray(0,4);
                for (int i = 0; i < 4; i++)
                {
                    if (num[i] < 48 || num[i] > 57)
                    {
                        MessageBox.Show("只能輸入數字");
                        textBox1.Text = "";
                        textBox1.Focus();
                        return;
                    }
                }
                n1 = Int16.Parse(num[0] + "");
                n2 = Int16.Parse(num[1] + "");
                n3 = Int16.Parse(num[2] + "");
                n4 = Int16.Parse(num[3] + "");
                if (n1 == a && n2 == b && n3 == c && n4 == d)
                {
                    timer1.Enabled = false;
                    this.Close();
                    MessageBox.Show("恭喜答對囉！\n花費時間:"+label2.Text);
                    form1.guessNumGame=label2.Text;
                    string path = form1.game1Path;
                    if (!File.Exists(path)) 
                    {
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            sw.WriteLine(label2.Text);
                        }
                    }
                    else{
                        int min;
                        using (StreamReader sr = new StreamReader(path)) 
                        {
                            min = Int16.Parse(sr.ReadLine());
                        }
                        if (time < min)
                        {
                            File.Delete(path);
                            using (StreamWriter sw = new StreamWriter(path))
                            {
                                sw.WriteLine(label2.Text);
                            }
                        }
                    }

                }
                else
                {
                    int x=0,y=0; 
             
					if(n1==a)
						x++;
					else if(n1==b || n1==c || n1==d)
						y++; 
    
					if(n2==b)
						x++;
					else if(n2==a || n2==c ||n2==d)
						y++;
    
					if(n3==c)
						x++;
					else if(n3==a || n3==b || n3==d)
						y++;
    
					if(n4==d)
						x++;
					else if(n4==a || n4==b || n4==c)
						y++;
                    _items.Insert(0,x+"A"+y+"B");
                    listBox1.DataSource = null;
                    listBox1.DataSource = _items;
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)   //計時器(工具箱元件)設定一豪秒一個動作顧*1000變成一秒一個動作
        {
            time++;
            label2.Text = time.ToString();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(button1, e);
        }
    }
}
