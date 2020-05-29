using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lab14
{
    public class Experiment/* : ICloneable, IComparer, IComparer<Experiment>*/
    {
        // Испытуемый
        public string Person;

        // Результат испытания
        public bool IsPassed;

        protected Random rand = new Random();
        private string[] _persons = {"Вова", "Кирилл", "Андрей", "Костя", "Юра"};

        public Experiment()
        {
            Person = _persons[rand.Next(_persons.Length)];
            IsPassed = Convert.ToBoolean(rand.Next(1));
        }

        public Experiment(string person, bool isPassed)
        {
            Person = person;
            IsPassed = isPassed;
        }

        public override string ToString()
        {
            return $"Это испытание. Меня выполняет {Person}";
        }

//         public Experiment(bool isManual)
//         {
//             // Ввод имени
//             Console.WriteLine("Введите имя испытуемого:");
//
//             string person = Console.ReadLine();
//             Regex regName = new Regex(@"[A-Za-zА-Яа-я]+");
//
//             while (string.IsNullOrEmpty(person) || !regName.IsMatch(person))
//             {
//                 Console.Error.WriteLine("Введите корректное имя");
//                 person = Console.ReadLine();
//             }
//
//             // Ввод сдал ли
//             Console.WriteLine(@"Введите, сдал ли он тест:
// 1) Да
// 2) Нет");
//
//             int isPassed;
//
//             while (!int.TryParse(Console.ReadLine(), out isPassed) && !(isPassed == 1 || isPassed == 2))
//             {
//                 Console.Error.WriteLine("Введите 1 или 2");
//             }
//
//             Person = person;
//             IsPassed = isPassed == 1;
//         }

        // public virtual object Clone()
        // {
        //     return new Experiment(Person, IsPassed);
        // }

        // public int Compare(object x, object y)
        // {
        //     Experiment exp1 = new Experiment(((Experiment) x).Person, ((Experiment) x).IsPassed);
        //
        //     if (y is Experiment)
        //     {
        //         Experiment exp2 = new Experiment(((Experiment) y).Person, ((Experiment) y).IsPassed);
        //         return string.CompareOrdinal(exp1.Person.ToLower(), exp2.Person.ToLower());
        //     }
        //     else
        //     {
        //         return string.CompareOrdinal(exp1.Person.ToLower(), ((String) y).ToLower());
        //     }
        // }
        //
        // public int Compare(Experiment x, Experiment y)
        // {
        //     Experiment exp1 = new Experiment(x.Person, x.IsPassed);
        //     Experiment exp2 = new Experiment(y.Person, y.IsPassed);
        //     
        //     return string.CompareOrdinal(exp1.Person.ToLower(), exp2.Person.ToLower());
        // }
    }
}