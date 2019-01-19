using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] rules = new[] { "*a->a*", "*b->b*", "*->.b", "Л->*" };

            string word = "abbaaa";
            Console.WriteLine(word);
            bool ruleApplied = false;
            bool lastRule = false;

            do
            {
                ruleApplied = false;

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
        }
    }
}
