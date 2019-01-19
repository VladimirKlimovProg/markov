using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
          
            //string[] rules = new[] { "*a->a*", "*b->b*", "*->.b", "Л->*" };
            string[] rules = textBoxRules.Lines;
            string word = textBoxWord.Text;

            Console.WriteLine(word);
            bool ruleApplied = false;
            bool lastRule = false;

            do
            {
                ruleApplied = false;

                //проверить правильность ввода правил
                string pattern = @" ";
                //пошаговое выполнение, подсветка


                foreach (string rule in rules)
                {
                    string[] splitters = new[] { "->.", "->" };
                    string[] ruleParts = rule.Split(splitters, StringSplitOptions.None);

                    string L = ruleParts[0];
                    string R = ruleParts[1];

                    lastRule = rule.Contains("->.");

                    if (L == "Л")
                    {
                        word = R + word;
                        ruleApplied = true;
                        Console.WriteLine($"{word} ({rule})");
                        break;
                    }
                    else if (word.Contains(L))
                    {
                        Regex search = new Regex(Regex.Escape(L));
                        word = search.Replace(word, R, 1, 0);
                        Console.WriteLine($"{word} ({rule})");

                        ruleApplied = true;
                        break;
                    }
                }

            } while (ruleApplied && !lastRule);

            textBoxWord.Text = word;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //начальные значения
            textBoxWord.Text = "abba";
            textBoxRules.AppendText("*a->a*");
            textBoxRules.AppendText("\r\n*b->b*");
            textBoxRules.AppendText("\r\n*->.b");
            textBoxRules.AppendText("\r\nЛ->*");
        }
    }
}
