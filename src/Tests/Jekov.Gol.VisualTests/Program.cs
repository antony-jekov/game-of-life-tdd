using Jekov.Gol.Core;
using Jekov.Gol.VisualTests.Game;
using System;
using System.Threading.Tasks;

namespace Jekov.Gol.VisualTests
{
    internal class Program
    {
        private static async Task Main()
        {
            var width = Console.WindowWidth;
            var height = Console.WindowHeight;

            var origin = new Cell((width / 2), (height / 2));
            var renderer = new Renderer(width, height, origin);
            var game = new GameOfLife();

            while (!Console.KeyAvailable)
            {
                renderer.Draw(game.Cells);

                await Task.Delay(16);

                game.Tick();
            }

            renderer.Clear();
        }
    }
}