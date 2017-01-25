using DeenGames.Cataclysm.Core.Maps;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Parameters;

namespace DeenGames.Cataclysm.Core.UnitTests.Maps
{
    [TestFixture]
    class DungeonFloorMapTests : AbstractUnitTest
    {
        [Test]
        public void IsWalkableReturnsTrueForWalkableTiles()
        {
            var floor = kernel.Get<DungeonFloorMap>(new ConstructorArgument("width", 20), new ConstructorArgument("height", 20));
            var foundFloor = false;
            var foundWall = false;
            
            // There must be at least one walkable tile and one non-walkable tile, based on the current generation strategy
            for (var x = 0; x < 20; x++)
            {
                for (var y = 0; y < 20; y++)
                {
                    var walkable = floor.IsWalkable(x, y);
                    if (walkable)
                    {
                        foundFloor = true;
                    }
                    else
                    {
                        foundWall = true;
                    }
                }
            }

            Assert.That(foundFloor, Is.EqualTo(true));
            Assert.That(foundWall, Is.EqualTo(true));
        }
    }
}
