using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab14
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Работа с LINQ запросами");

            List<List<Experiment>> list = new List<List<Experiment>>();

            ShowDialog(list);
        }

        private static void ShowDialog(List<List<Experiment>> list)
        {
            while (true)
            {
                Console.WriteLine();

                int min = 1;
                int max = 8;

                Console.WriteLine(@"Выберите действие:
1) Создание коллекции с коллекциями
2) Печать коллекции
3) Выборка данных
4) Получение счетчика (кол-ва элементов с заданным параметром)
5) Использование операций над множествами (пересечение, объединение, разность)
6) Агрегирование данных
7) Группировка данных
8) Выход из программы");

                int method = InputInt(min, max);

                Console.Clear();

                switch (method)
                {
                    // Создание коллекции
                    case 1:
                        Console.WriteLine("Введите размер коллекций от 1 до 50:");
                        int size = InputInt(1, 50);
                        
                        list = CreateList(size);

                        break;
                    // Печать коллекции
                    case 2:
                        Print(list);

                        break;
                    // Выборка данных
                    case 3:
                        Console.WriteLine("Введите имя для поиска");
                        string nameSearch = InputString();
                        
                        var expsSearch1 = Query.SearchByPerson(list, nameSearch);
                        var expsSearch2 = Methods.SearchByPerson(list, nameSearch);
            
                        Console.WriteLine(expsSearch1.Count() != 0 ? "Найдены следующие элементы: " : "Элементы не найдены");
                        if (expsSearch1.Count() != 0)
                        {
                            expsSearch1.ForEach(e => Console.WriteLine(e));
                        }
                        
                        Console.WriteLine(expsSearch2.Count() != 0 ? "Найдены следующие элементы: " : "Элементы не найдены");
                        if (expsSearch2.Count() != 0)
                        {
                            expsSearch2.ForEach(e => Console.WriteLine(e));
                        }
                        
                        break;
                    // Получение счетчика (кол-ва элементов с заданным параметром)
                    case 4:
                        Console.WriteLine("Введите имя для подсчета");

                        string nameCount = InputString();

                        var expsCount1 = Query.CountByPerson(list, nameCount);
                        var expsCount2 = Methods.CountByPerson(list, nameCount);

                        Console.WriteLine($"Кол-во найденных элементов: {expsCount1}");
                        Console.WriteLine($"Кол-во найденных элементов: {expsCount2}");
                        
                        break;
                    // Использование операций над множествами (пересечение, объединение, разность)
                    case 5:
                        var expsExcept1 = Query.ExceptC2InC1(list);
                        var expsExcept2 = Methods.ExceptC2InC1(list);

                        Console.WriteLine("Имена, которые есть в коллекции 1, но нет в коллекции 2: ");
                        if (expsExcept1.Count != 0)
                        {
                            expsExcept1.ForEach(e => Console.WriteLine(e));
                        }
                        else
                        {
                            Console.WriteLine("Элементы не найдены");
                        }

                        Console.WriteLine("Имена, которые есть в коллекции 1, но нет в коллекции 2: ");
                        if (expsExcept2.Count != 0)
                        {
                            expsExcept2.ForEach(e => Console.WriteLine(e));
                        }
                        else
                        {
                            Console.WriteLine("Элементы не найдены");
                        }
                        
                        var expsUnion1 = Query.UnionC1WithC2(list);
                        var expsUnion2 = Methods.UnionC1WithC2(list);

                        Console.WriteLine("Объединение коллекции 1 и коллекции 2: ");
                        if (expsUnion1.Count != 0)
                        {
                            expsUnion1.ForEach(e => Console.WriteLine(e));
                        }
                        else
                        {
                            Console.WriteLine("Коллекции пусты");
                        }

                        Console.WriteLine("Объединение коллекции 1 и коллекции 2: ");
                        if (expsUnion2.Count != 0)
                        {
                            expsUnion2.ForEach(e => Console.WriteLine(e));
                        }
                        else
                        {
                            Console.WriteLine("Коллекции пусты");
                        }
                        
                        var expsInt1 =  Query.IntersectC2InC1(list);
                        var expsInt2 =  Methods.IntersectC2InC1(list);

                        Console.WriteLine("Имена, которые есть в коллекции 1, и есть в коллекции 2: ");
                        if (expsInt1.Count != 0)
                        {
                            expsInt1.ForEach(e => Console.WriteLine(e));
                        }
                        else
                        {
                            Console.WriteLine("Элементы не найдены");
                        }

                        Console.WriteLine("Имена, которые есть в коллекции 1, и есть в коллекции 2: ");
                        if (expsInt2.Count != 0)
                        {
                            expsInt2.ForEach(e => Console.WriteLine(e));
                        }
                        else
                        {
                            Console.WriteLine("Элементы не найдены");
                        }
                        
                        break;
                    // Агрегирование данных
                    case 6:

                        var listUnion = UnionList(list);

                        var people = listUnion.Aggregate(new List<string>(), (newList, el) =>
                        {
                            newList.Add(el.Person);
                            return newList;
                        });

                        Console.WriteLine("Люди из всех коллекций: ");
                        if (people.Count() != 0)
                        {
                            people.ForEach(person => Console.WriteLine(person));
                        }
                        else
                        {
                            Console.WriteLine("Элементы не найдены");
                        }
                        
                        break;
                    // Группировка данных
                    case 7:
                        var expGroups1 = Query.GroupByPerson(list);
                        var expGroups2 = Methods.GroupByPerson(list);

                        if (expGroups1.Count != 0)
                        {
                            Console.WriteLine("Сгруппированные коллекции по имени:");
                    
                            expGroups1.ForEach(exps =>
                            {
                                Console.WriteLine(exps.Key);
                                exps.ToList().ForEach(e => Console.WriteLine(e));
                                Console.WriteLine();
                            });
                        }
                        else
                        {
                            Console.WriteLine("Список пуст");
                        }

                        if (expGroups2.Count != 0)
                        {
                            Console.WriteLine("Сгруппированные коллекции по имени:");
                    
                            expGroups2.ForEach(exps =>
                            {
                                Console.WriteLine(exps.Key);
                                exps.ToList().ForEach(e => Console.WriteLine(e));
                                Console.WriteLine();
                            });
                        }
                        else
                        {
                            Console.WriteLine("Список пуст");
                        }
                        
                        break;
                    // Выход из программы
                    case 8:
                        return;
                }
            }
        }

        public static List<List<Experiment>> CreateList(int size)
        {
            var list = new List<List<Experiment>>();

            for (int i = 0; i < 4; i++)
            {
                List<Experiment> newList = new List<Experiment>();

                for (int j = 0; j < size; j++)
                {
                    newList.Add(new Experiment());
                }

                list.Add(new List<Experiment>(newList));
            }

            Console.WriteLine("Коллекция успешно создана");

            return list;
        }

        public static void Print(List<List<Experiment>> list)
        {
            if (list.Count == 0 || !(from l in list select (from exp in l select exp)).Any())
            {
                Console.WriteLine("Список пуст");
                return;
            }

            int order = 1;
            list.ForEach((l) =>
            {
                Console.WriteLine($"Коллекция {order}:");
                l.ForEach(e => Console.WriteLine(e));
                order++;
            });
        }

        public static class Query
        {
            public static List<Experiment> SearchByPerson(List<List<Experiment>> list, string name)
            {
                return (from exp in UnionList(list) where exp.Person == name select exp).ToList();
            }
            
            public static int CountByPerson(List<List<Experiment>> list, string name)
            {
                return (from exp in UnionList(list) where exp.Person == name select exp).Count();
            }
            
            public static List<string> ExceptC2InC1(List<List<Experiment>> list)
            {
                return (from l1 in list[0] select l1.Person).Except((from l2 in list[1] select l2.Person)).ToList();
            }

            public static List<Experiment> UnionC1WithC2(List<List<Experiment>> list)
            {
                return (from l1 in list[0] select l1).Union((from l2 in list[1] select l2)).ToList();
            }

            public static List<string> IntersectC2InC1(List<List<Experiment>> list)
            {
               return (from l1 in list[0] select l1.Person).Intersect((from l2 in list[1] select l2.Person)).ToList();
            }

            public static List<IGrouping<string, Experiment>> GroupByPerson(List<List<Experiment>> list)
            {
               return (from exp in UnionList(list) group exp by exp.Person).ToList();
            }
        }

        public static class Methods
        {
            public static List<Experiment> SearchByPerson(List<List<Experiment>> list, string name)
            {
               return UnionList(list).Where(exp => exp.Person == name).ToList();
            }
            
            public static int CountByPerson(List<List<Experiment>> list, string name)
            {
                return UnionList(list).Count(exp => exp.Person == name);
            }
            
            public static List<string> ExceptC2InC1(List<List<Experiment>> list)
            {
                return list[0].Select(exp => exp.Person).Except(list[1].Select(exp => exp.Person)).ToList();
            }

            public static List<Experiment> UnionC1WithC2(List<List<Experiment>> list)
            {
                return list[0].Union(list[1]).ToList();
            }

            public static List<string> IntersectC2InC1(List<List<Experiment>> list)
            {
               return list[0].Select(exp => exp.Person).Intersect(list[1].Select(exp => exp.Person)).ToList();
            }

            public static List<IGrouping<string, Experiment>> GroupByPerson(List<List<Experiment>> list)
            {
                return UnionList(list).GroupBy(exp => exp.Person).ToList();
            }
        }

        public static List<Experiment> UnionList(List<List<Experiment>> list)
        {
            return list.Aggregate(new List<Experiment>(), (newList, el) => newList.Union(el).ToList());
        }

        private static int InputInt(int min, int max)
        {
            int output;

            while (!int.TryParse(Console.ReadLine(), out output) ||
                   !(output >= min && output <= max))
            {
                Console.Error.WriteLine($"Введите целое число от {min} до {max}");
            }

            return output;
        }

        private static int InputInt()
        {
            int output;

            while (!int.TryParse(Console.ReadLine(), out output))
            {
                Console.Error.WriteLine("Введите целое число");
            }

            return output;
        }

        private static string InputString()
        {
            string output = Console.ReadLine();

            Regex regName = new Regex(@"^[A-Za-zА-Яа-я]+$");

            while (string.IsNullOrEmpty(output) || !regName.IsMatch(output))
            {
                Console.Error.WriteLine("Введите корректную строку");
                output = Console.ReadLine();
            }

            return output;
        }
    }
}