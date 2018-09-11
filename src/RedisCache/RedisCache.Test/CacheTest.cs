using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedisCache.Test
{
    [TestClass]
    public class CacheTest
    {
        [TestMethod]
        public void SingleObjectTest()
        {
            string key = "test";
            int value = new Random().Next(1, 100);
            CacheUtil.Save(key, value);

            int cachedValue = CacheUtil.Get<int>(key);
            Assert.AreEqual(value, cachedValue);
        }

        [TestMethod]
        public void ObjectTest()
        {
            string key = "person";
            Person person = new Person
            {
                FirstName = "Atif",
                Surname = "Qələmov",
                Age = 35,
                BirthDate = new DateTime(1982, 10, 27)
            };
            CacheUtil.Save(key, person);

            Person cachedPerson = CacheUtil.Get<Person>(key);
            Assert.AreEqual(person.FirstName, cachedPerson.FirstName);
            Assert.AreEqual(person.Surname, cachedPerson.Surname);
            Assert.AreEqual(person.Age, cachedPerson.Age);
            Assert.AreEqual(person.BirthDate, cachedPerson.BirthDate);
        }

        [TestMethod]
        public void CollectionTest()
        {
            string key = "list";
            List<Person> list = new List<Person>();

            for (int i = 0; i < 10; i++)
                list.Add(
                    new Person
                    {
                        FirstName = $"Atif - {i}",
                        Surname = $"Qələmov - {i}",
                        Age = 35 + i,
                        BirthDate = new DateTime(1982, 10, i + 1)
                    });
            CacheUtil.Save(key, list);

            var s = Stopwatch.StartNew();
            List<Person> cachedList = CacheUtil.Get<List<Person>>(key);
            s.Stop();
            string seconds = s.Elapsed.TotalSeconds.ToString();
            Assert.AreEqual(list.Count, cachedList.Count);
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
