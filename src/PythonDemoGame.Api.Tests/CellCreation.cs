using System;
using System.Runtime.InteropServices.ComTypes;
using PythonDemoGame.Api.WorldClasses;
using Xunit;

namespace PythonDemoGame.Api.Tests
{
    public class CellCreation
    {
        [Fact]
        public void ErrorWorldCreation()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { _ = new World(5); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { _ = new World(600); });
        }

        [Fact]
        public void WorldCreation()
        {
            var world = new World(24);
            world.Initialize();

            var emptyCells  = 24 * 24 - 2;
            var numberCells = 1;
            var pythonCells = 1;

            for (var x = 0; x < 24; x++)
            {
                for (var y = 0; y < 24; y++)
                {
                    switch (world.Map[x, y])
                    {
                        case EmptyCell:
                            emptyCells--;
                            break;
                        case PythonCell:
                            pythonCells--;
                            break;
                        case NumberCell:
                            numberCells--;
                            break;
                    }
                }
            }

            Assert.Equal(0, emptyCells);
            Assert.Equal(0, numberCells);
            Assert.Equal(0, pythonCells);
        }

        [Fact]
        public void GameOverTest()
        {
            var world = new World(24);
            world.Initialize();
            world.GameOver();

            Assert.True(world.IsGameOver);
        }

        [Fact]
        public void PythonHeadMove()
        {
            var world = new World(24);
            world.Python.SetDirection(Direction.Up);
            var grow = world.Python.Grow;

            var numCell = new NumberCell(world, world.Python.X, world.Python.Y - 1);
            world.Python.Move();
            grow--;

            Assert.Equal(numCell.Number + grow, world.Python.Grow);
        }

        [Fact]
        public void PythonHeadMoveGameOver()
        {
            var world = new World(24);
            world.Python.SetDirection(Direction.Up);
            var grow = world.Python.Grow;

            var numCell = new PythonBodyCell(world, world.Python.X, world.Python.Y - 1);
            world.Python.Move();

            Assert.True(world.IsGameOver);
        }

        [Fact]
        public void PythonHeadCheckMoveUp()
        {
            var world = new World(24);
            var x     = world.Python.X;
            var y     = world.Python.Y;
            world.Python.SetDirection(Direction.Up);
            world.Python.Move();

            Assert.Equal(y - 1, world.Python.Y);
            Assert.Equal(x, world.Python.X);
        }
    }
}