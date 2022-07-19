using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<String> words = new List<string>();
        public Form1()
        {
            InitializeComponent();
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox3.ScrollBars = ScrollBars.Vertical;
            words = File.ReadAllLines("words.txt").ToList();
            for (int i = 0; i < words.Count; i++)
            {
                words[i] = words[i].ToLower();
            }
            progressBar1.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Show();
            textBox2.Text = "";
            char[] split = {' ','\r','\n'};
            char[] trim = { ',', '.', ';', ':', '!', '?' };
            String[] input = textBox1.Text.ToLower().Split(split);
            progressBar1.Maximum = input.Length *3;
            for (int i = 0; i < input.Length; i++)
            {
                progressBar1.Increment(1);
                input[i] = input[i].Trim(trim);
            }
             
            var count = new Dictionary<String, int>();
            List<String> errors = new List<string>();

            
            foreach (var word in input)
            {
                progressBar1.Increment(1);

                if (words.Contains(word))
                {
                    if(count.ContainsKey(word))
                        count[word]++;
                    else
                    {
                        count.Add(word, 1);
                    }
                }
                else if(word.Length > 0)
                {
                    errors.Add(word);
                }
            }
            
            foreach (var error in errors)
            {
                progressBar1.Increment(1);
                textBox2.Text += error + "\r\n";               
            }
          
            foreach (var item in count)
            {
                progressBar1.Increment(1);
                textBox3.Text += item.Key.ToString() + " - " + item.Value.ToString() + "\r\n";
            }

            progressBar1.Hide();
        }

        
    }
}
