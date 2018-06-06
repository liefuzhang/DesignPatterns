using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDesignPatternDemos.Structural.Decorator
{
    public interface IBird
    {
        void Fly();
    }

    public class Bird : IBird
    {
        public void Fly()
        {

        }
    }

    public interface ILizard
    {
        void Crawl();
    }

    public class Lizard : ILizard
    {
        public void Crawl()
        {

        }
    }

    public class Dragon : IBird, ILizard
    {
        private Bird bird;
        private Lizard lizard;

        public Dragon(Bird bird, Lizard lizard)
        {
            this.bird = bird ?? throw new ArgumentNullException(paramName: nameof(bird));
            this.lizard = lizard ?? throw new ArgumentNullException(paramName: nameof(lizard));
        }

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }
    }
}
