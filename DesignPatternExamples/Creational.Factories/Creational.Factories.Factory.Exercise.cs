using System;

namespace Coding.Exercise
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonFactory
    {
        private static int index = 0;
        public static Person CreatePerson(string name)
        {
            return new Person
            {
                Name = name,
                Id = index++
            };
        }
    }
}
