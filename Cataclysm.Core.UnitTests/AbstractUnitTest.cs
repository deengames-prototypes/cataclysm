using Ninject;
using NUnit.Framework;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.Core.UnitTests
{
    [TestFixture]
    class AbstractUnitTest
    {
        protected IKernel kernel = new StandardKernel();

        [SetUp]
        public void SetupDependencyInjection()
        {
            kernel.Bind<IRandom>().To<RogueSharp.Random.DotNetRandom>();
        }
    }
}
