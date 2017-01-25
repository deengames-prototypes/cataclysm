using System;
using Microsoft.Xna.Framework;
using SadConsole.Input;
using SadConsole.Effects;
using SadConsole.Game;
using SadConsole;
using DeenGames.Cataclysm.ConsoleUi.SadConsoleHelpers.Extensions;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Ninject.Parameters;
using Ninject;
using DeenGames.Cataclysm.ConsoleUi.ViewExtensions;
using DeenGames.Cataclysm.Core.Maps;

namespace DeenGames.Cataclysm.ConsoleUi.Consoles
{
    class AreaViewConsole : SadConsole.Consoles.Console
    {
        public GameObject playerEntity { get; private set; }
        private DungeonFloorMap currentMap;

        private ICellEffect DiscoveredEffect = new Recolor() { Foreground = Color.LightGray * 0.5f, Background = Color.Black, DoForeground = true, DoBackground = true, CloneOnApply = false };
        private ICellEffect HiddenEffect = new Recolor() { Foreground = Color.Black, Background = Color.Black, DoForeground = true, DoBackground = true, CloneOnApply = false };
        
        // TODO: should be a model
        public int PlayerLightRadius = 10;

        public AreaViewConsole(int width, int height, int mapWidth, int mapHeight) : base(mapWidth, mapHeight)
        {
            this.TextSurface.RenderArea = new Rectangle(0, 0, width, height);
            this.playerEntity = new GameObject(Engine.DefaultFont);
            this.playerEntity.Move(1, 1);
            this.playerEntity.DrawAs('@', Color.Orange);

            SadConsole.Engine.ActiveConsole = this;
            // Keyboard setup
            SadConsole.Engine.Keyboard.RepeatDelay = 0.07f;
            SadConsole.Engine.Keyboard.InitialRepeatDelay = 0.1f;

            this.GenerateMap();

            var currentFieldOfView = new RogueSharp.FieldOfView(this.currentMap.GetIMap());
            var fovTiles = currentFieldOfView.ComputeFov(playerEntity.Position.X, playerEntity.Position.Y, PlayerLightRadius, true);
            this.MarkCurrentFovAsVisible(fovTiles);
        }

        public override void Render()
        {
            base.Render();
            playerEntity.Render();
        }

        public override void Update()
        {
            base.Update();
            playerEntity.Update();
        }

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Down)) || info.KeysPressed.Contains(AsciiKey.Get(Keys.S)))
            {
                this.MovePlayerBy(new Point(0, 1));
            }
            else if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Up)) || info.KeysPressed.Contains(AsciiKey.Get(Keys.W)))
            {
                this.MovePlayerBy(new Point(0, -1));
            }

            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Right)) || info.KeysPressed.Contains(AsciiKey.Get(Keys.D)))
            {
                this.MovePlayerBy(new Point(1, 0));
            }
            else if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Left)) || info.KeysPressed.Contains(AsciiKey.Get(Keys.A)))
            {
                this.MovePlayerBy(new Point(-1, 0));
            }

            return false;
        }

        private void MovePlayerBy(Point amount)
        {
            var currentFieldOfView = new RogueSharp.FieldOfView(this.currentMap.GetIMap());
            var fovTiles = currentFieldOfView.ComputeFov(playerEntity.Position.X, playerEntity.Position.Y, PlayerLightRadius, true);

            this.MarkCurrentFovAsDiscovered(fovTiles);

            // Get the position the player will be at
            Microsoft.Xna.Framework.Point newPosition = playerEntity.Position + amount;

            // Check to see if the position is within the map
            if (new Rectangle(0, 0, Width, Height).Contains(newPosition) && currentMap.IsWalkable(newPosition.X, newPosition.Y))
            {
                // Move the player
                playerEntity.Position += amount;
                CenterViewToPlayer();
            }

            fovTiles = currentFieldOfView.ComputeFov(playerEntity.Position.X, playerEntity.Position.Y, PlayerLightRadius, true);
            this.MarkCurrentFovAsVisible(fovTiles);
        }

        private void MarkCurrentFovAsVisible(IReadOnlyCollection<RogueSharp.Cell> fovTiles)
        {
            foreach (var cell in fovTiles)
            {
                var tile = this[cell.X, cell.Y];
                tile.ClearEffect();
            }
        }

        private void MarkCurrentFovAsDiscovered(IReadOnlyCollection<RogueSharp.Cell> fovTiles)
        {
            foreach (var cell in fovTiles)
            {
                // Tell the map data (for FOV calculations) that we've discovered thist ile
                this.currentMap.MarkAsDiscovered(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable);

                // Update view rendering to the appropriate effect
                var tile = this[cell.X, cell.Y];
                tile.ApplyEffect(DiscoveredEffect);
            }
        }

        private void CenterViewToPlayer()
        {
            // Scroll the view area to center the player on the screen
            TextSurface.RenderArea = new Rectangle(playerEntity.Position.X - (TextSurface.RenderArea.Width / 2),
                                                    playerEntity.Position.Y - (TextSurface.RenderArea.Height / 2),
                                                    TextSurface.RenderArea.Width, TextSurface.RenderArea.Height);

            // If he view area moved, we'll keep our entity in sync with it.
            playerEntity.RenderOffset = this.Position - TextSurface.RenderArea.Location;
        }

        private void GenerateMap()
        {
            // Create the map
            currentMap = Game.Kernel.Get<DungeonFloorMap>(new ConstructorArgument("width", this.Width), new ConstructorArgument("height", this.Height));

            // Loop through the map information generated by RogueSharp and update our view
            for (var j = 0; j < this.Height; j++)
            {
                for (var i = 0; i < this.Width; i++)
                {
                    var tile = this[i, j];
                    this.CreateCellFor(i, j).CopyAppearanceTo(tile);
                    tile.ApplyEffect(HiddenEffect);
                }
            }

            var start = currentMap.GetPlayerStartingPosition();
            playerEntity.Position = new Point(start.X, start.Y);
            this.CenterViewToPlayer();
        }

        public ICellAppearance CreateCellFor(int x, int y)
        {
            if (this.currentMap.IsWalkable(x, y))
            {
                return new CellAppearance(Color.DarkGray, Color.Transparent, '.');
            }
            else
            {
                return new CellAppearance(Color.LightGray, Color.Transparent, '#');

            }
        }
    }
}
