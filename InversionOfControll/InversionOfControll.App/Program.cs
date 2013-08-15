using System;
using Ninject;
using Ninject.Modules;

namespace InversionOfControll.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new SwordInject());
            var sam = kernel.Get<IWarrior>();
            sam.Attack("the unwashed");
            Console.ReadKey();
        }
    }

    internal interface IWarrior
    {
        void Attack(string target);
    }

    class Samurai : IWarrior
    {
        private Sword _sword;
        [Inject]
        public Samurai(Sword sword)
        {
            _sword = sword;
        }

        public void Attack(string target)
        {
            _sword.Hit(target);
        }
    }

    internal interface IWeapon
    {
        void Hit(string target);
    }

    internal class Sword : IWeapon
    {
        public void Hit(string target)
        {
            Console.WriteLine("Chopped {0} into halves",target);
        }
    }

    class Shuriken
    {
        public void Hit(string target)
        {
            Console.WriteLine("Pieces {0} armor",target);
        }
    }

    class SwordInject:NinjectModule
    {
        public override void Load()
        {
            Bind<IWarrior>().To<Samurai>().InSingletonScope();
            Bind<IWeapon>().To<Sword>().InSingletonScope();
        }
    }
}
