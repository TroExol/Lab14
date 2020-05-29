using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab14.Tests
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void UnionListTest()
        {
            var list = new List<List<Experiment>>
            {
                new List<Experiment>
                {
                    new Experiment("Вова", true),
                    new Experiment("Вова", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Костя", true),
                },
                new List<Experiment>
                {
                    new Experiment("Юра", true),
                    new Experiment("Ваня", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Костя", true),
                },
                new List<Experiment>
                {
                    new Experiment("Аркадий", true),
                    new Experiment("Коля", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Ваня", true),
                }
            };

            var testList = new[]
            {
                "Вова",
                "Вова",
                "Кирилл",
                "Костя",
                "Юра",
                "Ваня",
                "Кирилл",
                "Костя",
                "Аркадий",
                "Коля",
                "Кирилл",
                "Ваня"
            };

            var unionList = Program.UnionList(list).Select(el => el.Person).ToList();

            CollectionAssert.AreEqual(unionList, testList);
        }

        [TestMethod]
        public void CreateListTest()
        {
            var list = Program.CreateList(10);

            Assert.AreEqual(Program.UnionList(list).Count, 40);
        }
    }

    [TestClass]
    public class ProgramQueryTest
    {
        private static List<List<Experiment>> list;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            list = new List<List<Experiment>>
            {
                new List<Experiment>
                {
                    new Experiment("Вова", true),
                    new Experiment("Вова", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Костя", true),
                },
                new List<Experiment>
                {
                    new Experiment("Юра", true),
                    new Experiment("Ваня", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Костя", true),
                },
                new List<Experiment>
                {
                    new Experiment("Аркадий", true),
                    new Experiment("Коля", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Ваня", true),
                }
            };
        }

        [TestMethod]
        public void CountByPersonTest()
        {
            var testCount = 3;

            Assert.AreEqual(Program.Query.CountByPerson(list, "Кирилл"), testCount);
        }

        [TestMethod]
        public void SearchByPersonTest()
        {
            var testPeople = new[]
            {
                "Ваня",
                "Ваня"
            };

            var people = Program.Query.SearchByPerson(list, "Ваня").Select(e => e.Person).ToList();

            CollectionAssert.AreEqual(people, testPeople);
        }

        [TestMethod]
        public void ExceptC2InC1Test()
        {
            var testPeople = new[]
            {
                "Вова"
            };

            var people = Program.Query.ExceptC2InC1(list);

            CollectionAssert.AreEqual(people, testPeople);
        }

        [TestMethod]
        public void UnionC1WithC2Test()
        {
            var testPeople = new[]
            {
                "Вова",
                "Вова",
                "Кирилл",
                "Костя",
                "Юра",
                "Ваня",
                "Кирилл",
                "Костя"
            };

            var people = Program.Query.UnionC1WithC2(list).Select(e => e.Person).ToList();

            CollectionAssert.AreEqual(people, testPeople);
        }

        [TestMethod]
        public void IntersectC2InC1Test()
        {
            var testPeople = new[]
            {
                "Кирилл",
                "Костя"
            };

            var people = Program.Query.IntersectC2InC1(list);

            CollectionAssert.AreEqual(people, testPeople);
        }

        [TestMethod]
        public void GroupByPersonTest()
        {
            var newList = new List<List<Experiment>>
            {
                new List<Experiment>
                {
                    new Experiment("Костя", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Костя", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Кирилл", true)
                }
            };

            var peopleKostya = new[]
            {
                "Костя",
                "Костя"
            };

            var peopleKirill = new[]
            {
                "Кирилл",
                "Кирилл",
                "Кирилл"
            };
            
            var people = Program.Query.GroupByPerson(newList);

            people.ForEach(exps =>
            {
                Assert.IsTrue(exps.Key == "Костя" || exps.Key == "Кирилл");

                var names = exps.ToList().Select(e => e.Person).ToList();
                if (exps.Key == "Костя")
                {
                    CollectionAssert.AreEqual(names, peopleKostya);
                }
                else
                {
                    CollectionAssert.AreEqual(names, peopleKirill);
                }
                
            });
        }
    }

    [TestClass]
    public class ProgramMethodsTest
    {
        private static List<List<Experiment>> list;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            list = new List<List<Experiment>>
            {
                new List<Experiment>
                {
                    new Experiment("Вова", true),
                    new Experiment("Вова", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Костя", true),
                },
                new List<Experiment>
                {
                    new Experiment("Юра", true),
                    new Experiment("Ваня", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Костя", true),
                },
                new List<Experiment>
                {
                    new Experiment("Аркадий", true),
                    new Experiment("Коля", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Ваня", true),
                }
            };
        }

        [TestMethod]
        public void CountByPersonTest()
        {
            var testCount = 3;

            Assert.AreEqual(Program.Methods.CountByPerson(list, "Кирилл"), testCount);
        }

        [TestMethod]
        public void SearchByPersonTest()
        {
            var testPeople = new[]
            {
                "Ваня",
                "Ваня"
            };

            var people = Program.Methods.SearchByPerson(list, "Ваня").Select(e => e.Person).ToList();

            CollectionAssert.AreEqual(people, testPeople);
        }

        [TestMethod]
        public void ExceptC2InC1Test()
        {
            var testPeople = new[]
            {
                "Вова"
            };

            var people = Program.Methods.ExceptC2InC1(list);

            CollectionAssert.AreEqual(people, testPeople);
        }

        [TestMethod]
        public void UnionC1WithC2Test()
        {
            var testPeople = new[]
            {
                "Вова",
                "Вова",
                "Кирилл",
                "Костя",
                "Юра",
                "Ваня",
                "Кирилл",
                "Костя"
            };

            var people = Program.Methods.UnionC1WithC2(list).Select(e => e.Person).ToList();

            CollectionAssert.AreEqual(people, testPeople);
        }

        [TestMethod]
        public void IntersectC2InC1Test()
        {
            var testPeople = new[]
            {
                "Кирилл",
                "Костя"
            };

            var people = Program.Methods.IntersectC2InC1(list);

            CollectionAssert.AreEqual(people, testPeople);
        }

        [TestMethod]
        public void GroupByPersonTest()
        {
            var newList = new List<List<Experiment>>
            {
                new List<Experiment>
                {
                    new Experiment("Костя", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Костя", true),
                    new Experiment("Кирилл", true),
                    new Experiment("Кирилл", true)
                }
            };

            var peopleKostya = new[]
            {
                "Костя",
                "Костя"
            };

            var peopleKirill = new[]
            {
                "Кирилл",
                "Кирилл",
                "Кирилл"
            };
            
            var people = Program.Methods.GroupByPerson(newList);

            people.ForEach(exps =>
            {
                Assert.IsTrue(exps.Key == "Костя" || exps.Key == "Кирилл");

                var names = exps.ToList().Select(e => e.Person).ToList();
                if (exps.Key == "Костя")
                {
                    CollectionAssert.AreEqual(names, peopleKostya);
                }
                else
                {
                    CollectionAssert.AreEqual(names, peopleKirill);
                }
                
            });
        }
    }
}