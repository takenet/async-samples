using System;

namespace Take.Async.Samples
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SampleAttribute : Attribute
    {
        public SampleAttribute(int number, string name)
        {
            Number = number;
            Name = name;
        }

        public int Number { get; }

        public string Name { get; }        
    }
}