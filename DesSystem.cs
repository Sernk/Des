using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Des
{
    public class DesSystem : ModSystem
    {
        public static int StartBridgeX = 0;
        public static int BridgeY = 0;
        public static int EndBridgeX = 0;
        public static int TempY = 0;

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int DesertIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Full Desert"));
            int MicroBiomesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            tasks.Insert(DesertIndex + 1, new PassLegacy("Desert Pit Setting", delegate (GenerationProgress progress, GameConfiguration configuration)
            {
                var desert = GenVars.UndergroundDesertLocation;
                int desertLeft = desert.Left;
                int desertRight = desert.Right;

                int CenterX = desertLeft + ((desertRight - desertLeft) / 2);

                int top = Math.Max((int)GenVars.worldSurfaceLow, desert.Top - 20);
                int bottom = Math.Min(Main.maxTilesY - 1, desert.Bottom + 50);
                int centerY = top;

                for (int j = top; j <= bottom; j++)
                {
                    if (WorldMethods.CheckWall(CenterX, j, WallID.HardenedSand) || WorldMethods.CheckWall(CenterX, j, WallID.Sandstone))
                    {
                        centerY = j - 22;
                        break;
                    }
                }

                StartBridgeX = CenterX;
                BridgeY = centerY;
                EndBridgeX = StartBridgeX + 2;
                DesertPitHelper.PlaceDesertPit(StartBridgeX, BridgeY);
            }));

        }
    }
}