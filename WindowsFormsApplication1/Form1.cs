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

        /// <summary>
        /// Валидация правил
        /// </summary>
        /// <param name="rules">Массив правил</param>
        /// <returns>Проверка прошла?</returns>
        private bool validateRules(string[] rules)
        {
            string pattern = @"[\w\*\+\#]+(->|->.)[\w\*\+\#]+";
            bool validation = false;
            foreach (string rule in rules)
            {
                if (Regex.IsMatch(rule, pattern))
                {
                    validation = true;
                }
                else
                {
                    validation = false;
                    break;                   
                }
            }
            return validation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            //string[] rules = new[] { "*a->a*", "*b->b*", "*->.b", "Л->*" };
            string[] rules = textBoxRules.Lines;
            string word = textBoxWord.Text;

            //подсветка
            //textBoxRules.Focus();
            //int startIndex = 0;
            //int lengthOfLine = textBoxRules.Lines[0].Length;
            //textBoxRules.Select(startIndex,lengthOfLine);

            //проверить правильность ввода правил
            bool validation = validateRules(rules);
            if (!validation)
            {
                MessageBox.Show("Ошибка ввода правил.");
                return;
            }

            Console.WriteLine(word);
            bool ruleApplied = false;
            bool lastRule = false;

            do
            {
                ruleApplied = false;
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
