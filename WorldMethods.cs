using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Des
{
    public class WorldMethods
    {
        public static void RavageChest(int X, int Y)
        {
            int chest = Chest.FindChest(X, Y);
            if(chest != -1)
            {
                for (int k = 0; k < 40; k++)
                {
                    Item.NewItem(Main.LocalPlayer.GetSource_FromThis(), new Vector2(X, Y) * 16, Main.chest[chest].item[k].type, Main.chest[chest].item[k].stack);
                    Main.chest[chest].item[k].stack = 0;
                }
            }
        }
        public static void RoundHole(int X, int Y, int Xmult, int Ymult, int strength, bool initialdig)
        {
            if (initialdig)
                WorldGen.digTunnel((float)X, (float)Y, 0.0f, 0.0f, strength, strength, false);
            for (int index = 0; index < 350; ++index)
            {
                int num1 = (int)(0.0 - Math.Sin((double)index) * (double)Xmult);
                int num2 = (int)(0.0 - Math.Cos((double)index) * (double)Ymult);
                WorldGen.digTunnel((float)(X + num1), (float)(Y + num2), 0.0f, 0.0f, strength, strength, false);
            }
        }
        public static bool CheckTile(int X, int Y, int Type)//Использовать вместо встроенной проверки!
        {
            Tile tile = Framing.GetTileSafely(X, Y);
            return (tile.TileType == Type);
        }
        public static bool CheckLiquid(int X, int Y, int Type)//Использовать вместо встроенной проверки!
        {
            Tile tile = Framing.GetTileSafely(X, Y);
            return (tile.LiquidAmount == Type);
        }
        public static bool CheckWall(int X, int Y, int Type)
        {
            Tile tile = Framing.GetTileSafely(X, Y);
            return (tile.WallType == Type);
        }      
        public static void Swap(ref int a, ref int b)
        {
            int c;
            c = a;
            a = b;
            b = c;

        }       
        public static void BresenhamLineKillWall(int x0, int y0, int x1, int y1)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек
                                                               // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x0, ref y0); // Перетасовка координат вынесена в отдельную функцию 
                Swap(ref x1, ref y1);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {

                WorldGen.KillWall(steep ? y : x, steep ? x : y); // Не забываем вернуть координаты на место
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }
        public static void BresenhamLineTunnel(int x0, int y0, int x1, int y1, int strenght)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек
                                                               // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x0, ref y0); // Перетасовка координат вынесена в отдельную функцию 
                Swap(ref x1, ref y1);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                WorldGen.digTunnel(steep ? y : x, steep ? x : y, 0f, 0f, 1, strenght); // Не забываем вернуть координаты на место
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }
        public static void BresenhamLineSandOverride(int x0, int y0, int x1, int y1, ushort type)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек
                                                               // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x0, ref y0); // Перетасовка координат вынесена в отдельную функцию 
                Swap(ref x1, ref y1);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                if (Main.tile[steep ? y : x, steep ? x : y].TileType == TileID.Sand)
                    Main.tile[steep ? y : x, steep ? x : y].TileType = type;
                if (Main.tile[steep ? y : x, steep ? x + 1 : y + 1].TileType == TileID.Sand)
                    Main.tile[steep ? y : x, steep ? x + 1 : y + 1].TileType = type;
                if (Main.tile[steep ? y : x, steep ? x - 1 : y - 1].TileType == TileID.Sand)
                    Main.tile[steep ? y : x, steep ? x - 1 : y - 1].TileType = type;
                if (Main.tile[steep ? y : x, steep ? x - 2 : y - 2].TileType == TileID.Sand)
                    Main.tile[steep ? y : x, steep ? x - 2 : y - 2].TileType = type;
                if (Main.tile[steep ? y : x, steep ? x - 3 : y - 3].TileType == TileID.Sand)
                    Main.tile[steep ? y : x, steep ? x - 3 : y - 3].TileType = type;
                if (Main.tile[steep ? y : x, steep ? x - 4 : y - 4].TileType == TileID.Sand)
                    Main.tile[steep ? y : x, steep ? x - 4 : y - 4].TileType = type;
                if (Main.tile[steep ? y : x, steep ? x - 5 : y - 5].TileType == TileID.Sand)
                    Main.tile[steep ? y : x, steep ? x - 5 : y - 5].TileType = type;
                if (Main.tile[steep ? y : x, steep ? x - 6 : y - 6].TileType == TileID.Sand)
                    Main.tile[steep ? y : x, steep ? x - 6 : y - 6].TileType = type;
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }
        public static void BresenhamLineTile(int x0, int y0, int x1, int y1, int strenght, int type, int random)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек
                                                               // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x0, ref y0); // Перетасовка координат вынесена в отдельную функцию 
                Swap(ref x1, ref y1);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                WorldGen.TileRunner(steep ? y : x, steep ? x : y, strenght + Main.rand.Next(0, random), 1, type, true, 0f, 0f, false, true); // Не забываем вернуть координаты на место
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }
        public static void BresenhamLineWall(int x0, int y0, int x1, int y1, int type)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек
                                                               // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x0, ref y0); // Перетасовка координат вынесена в отдельную функцию 
                Swap(ref x1, ref y1);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                WorldGen.PlaceWall(steep ? y : x, steep ? x : y, type); // Не забываем вернуть координаты на место
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }
        public static void BresenhamLineSlope(int x0, int y0, int x1, int y1, int slope, bool halfbrick)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек
                                                               // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x0, ref y0); // Перетасовка координат вынесена в отдельную функцию 
                Swap(ref x1, ref y1);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                WorldGen.SlopeTile(steep ? y : x, steep ? x : y, slope); // Не забываем вернуть координаты на место
                Tile tile = Framing.GetTileSafely(steep ? y : x, steep ? x : y);
                tile.IsHalfBlock = halfbrick;
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }    
        public static void BresenhamLineTileNoOverride(int x0, int y0, int x1, int y1, int strenght, int type, int random)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек
                                                               // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x0, ref y0); // Перетасовка координат вынесена в отдельную функцию 
                Swap(ref x1, ref y1);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                WorldGen.TileRunner(steep ? y : x, steep ? x : y, strenght + Main.rand.Next(0, random), 1, type, true, 0f, 0f, false, false); // Не забываем вернуть координаты на место
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }
        public static void CragSpike(int X, int Y, int length, int height, ushort type2, float slope, float sloperight)
        {
            float num1 = 1f / slope;
            float num2 = 1f / sloperight;
            int num3 = length / 2;
            for (int index1 = 0; index1 <= height; ++index1)
            {
                Tile tile = Main.tile[X, (int)((double)(Y + index1) - (double)slope / 2.0)];
                tile.HasTile = true;
                tile.TileType = type2;
                Main.tile[X, (int)((double)(Y + index1) - (double)slope / 2.0)].TileType = type2;
                for (int index2 = X - (int)((double)length + (double)index1 * (double)num1); index2 < X + (int)((double)length + (double)index1 * (double)num2); ++index2)
                {
                    Tile tile2 = Main.tile[index2, Y + index1];
                    tile.HasTile = true;
                    tile.TileType = type2;

                    Main.tile[index2, Y + index1].TileType = type2;
                }
            }
        }

        public static void Island(int X, int Y, int Xsize, float slope, ushort tile)
        {
            for (int index1 = X; index1 < X + Xsize; ++index1)
            {
                int num = (int)((double)Math.Abs(Main.rand.Next(Xsize / 2 - Xsize / 12, Xsize / 2 + Xsize / 12) - Math.Abs(index1 - X - Main.rand.Next(Xsize / 2 - Xsize / 12, Xsize / 2 + Xsize / 12))) * (double)slope);
                num.ToString();
                for (int index2 = 0; index2 < num; ++index2)
                    WorldMethods.TileRunner(index1, Y + index2, (double)(Xsize / 5), 1, (int)tile, true, 0.0f, 0.0f, true, true);
                WorldMethods.TileRunner(index1, Y, (double)(Xsize / 5), 1, (int)tile, true, 0.0f, 0.0f, true, true);
                if (Main.rand.Next(5) == 0)
                    WorldMethods.RoundHill(index1, Y, Xsize / 8, Xsize / 12, Xsize / 5, true, tile);
            }
        }

        public static void RoundHill(int X, int Y, int Xmult, int Ymult, int strength, bool initialplace, ushort type)
        {
            if (initialplace)
                WorldMethods.TileRunner(X, Y, (double)strength * 5.0, 1, (int)type, true, 0.0f, 0.0f, true, true);
            for (int index = 0; index < 350; ++index)
            {
                int num1 = (int)(0.0 - Math.Sin((double)index) * (double)Xmult);
                int num2 = (int)(0.0 - Math.Cos((double)index) * (double)Ymult);
                WorldMethods.TileRunner(X + num1, Y + num2, (double)strength, 1, (int)type, true, 0.0f, 0.0f, true, false);
            }
        }
        public static void TileRunner(int i, int j, double strength, int steps, int type, bool addTile = false, float speedX = 0f, float speedY = 0f, bool noYChange = false, bool overRide = true)
        {
            double num1 = strength;
            float num2 = (float)steps;
            Vector2 vector2_1;
            vector2_1.X = (float)i;
            vector2_1.Y = (float)j;
            Vector2 vector2_2;
            vector2_2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            vector2_2.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            if ((double)speedX != 0.0 || (double)speedY != 0.0)
            {
                vector2_2.X = speedX;
                vector2_2.Y = speedY;
            }
            while (num1 > 0.0 && (double)num2 > 0.0)
            {
                if ((double)vector2_1.Y < 0.0 && (double)num2 > 0.0 && type == 59)
                    num2 = 0.0f;
                num1 = strength * ((double)num2 / (double)steps);
                --num2;
                int num3 = (int)((double)vector2_1.X - num1 * 0.5);
                int num4 = (int)((double)vector2_1.X + num1 * 0.5);
                int num5 = (int)((double)vector2_1.Y - num1 * 0.5);
                int num6 = (int)((double)vector2_1.Y + num1 * 0.5);
                if (num3 < 1)
                    num3 = 1;
                if (num5 < 1)
                    num5 = 1;
                if (num6 > Main.maxTilesY - 1)
                    num6 = Main.maxTilesY - 1;
                for (int index1 = num3; index1 < num4; ++index1)
                {
                    for (int index2 = num5; index2 < num6; ++index2)
                    {
                        if ((double)Math.Abs((float)index1 - vector2_1.X) + (double)Math.Abs((float)index2 - vector2_1.Y) < strength * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
                        {
                            if (type < 0)
                            {
                                if (type == -2 && Main.tile[index1, index2].HasTile && (index2 < GenVars.waterLine || index2 > GenVars.lavaLine))
                                {
                                    Main.tile[index1, index2].LiquidAmount = byte.MaxValue;
                                    if (index2 > GenVars.lavaLine)
                                    {
                                        Tile tile = Main.tile[index1, index2];
                                        tile.LiquidType = LiquidID.Lava;

                                    }
                                }
                                Tile tile2 = Main.tile[index1, index2];
                                tile2.HasTile = false;

                            }
                            else
                            {
                                if (overRide || !Main.tile[index1, index2].HasTile)
                                {
                                    Tile tile = Main.tile[index1, index2];
                                    bool flag = tile.TileType == 0 || tile.TileType == 1 || tile.TileType == 2 || tile.TileType == 23 || tile.TileType == 25 || tile.TileType == 40 || tile.TileType == 53 || tile.TileType == 59 || tile.TileType == 60 || tile.TileType == 112 || tile.TileType == 147 || tile.TileType == 161 || tile.TileType == 163 || tile.TileType == 164 || tile.TileType == 199 || tile.TileType == 200 || tile.TileType == 203 || tile.TileType == 234;                                                               
                                    if (flag)
                                    {
                                        tile.TileType = (ushort)type;
                                        goto label_47;
                                    }
                                    else
                                        goto label_47;
                                  
                                }
                                label_47:
                                if (addTile)
                                {
                                    Tile tile = Main.tile[index1, index2];
                                    tile.HasTile = true;
                                    Main.tile[index1, index2].LiquidAmount = (byte)0;
                                    Tile tile2 = Main.tile[index1, index2];
                                    tile.LiquidType = LiquidID.Lava;


                                }
                                if (type == 59 && index2 > GenVars.waterLine && (int)Main.tile[index1, index2].LiquidAmount > 0)
                                {
                                    Main.tile[index1, index2].LiquidAmount = (byte)0;
                                    Tile tile = Main.tile[index1, index2];
                                    tile.LiquidType = LiquidID.Lava;
                                }
                            }
                        }
                    }
                }
                vector2_1 += vector2_2;
                if (num1 > 50.0)
                {
                    vector2_1 += vector2_2;
                    --num2;
                    vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                    vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                    if (num1 > 100.0)
                    {
                        vector2_1 += vector2_2;
                        --num2;
                        vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                        vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                        if (num1 > 150.0)
                        {
                            vector2_1 += vector2_2;
                            --num2;
                            vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                            vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                            if (num1 > 200.0)
                            {
                                vector2_1 += vector2_2;
                                --num2;
                                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                if (num1 > 250.0)
                                {
                                    vector2_1 += vector2_2;
                                    --num2;
                                    vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    if (num1 > 300.0)
                                    {
                                        vector2_1 += vector2_2;
                                        --num2;
                                        vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        if (num1 > 400.0)
                                        {
                                            vector2_1 += vector2_2;
                                            --num2;
                                            vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            if (num1 > 500.0)
                                            {
                                                vector2_1 += vector2_2;
                                                --num2;
                                                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                if (num1 > 600.0)
                                                {
                                                    vector2_1 += vector2_2;
                                                    --num2;
                                                    vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    if (num1 > 700.0)
                                                    {
                                                        vector2_1 += vector2_2;
                                                        --num2;
                                                        vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        if (num1 > 800.0)
                                                        {
                                                            vector2_1 += vector2_2;
                                                            --num2;
                                                            vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            if (num1 > 900.0)
                                                            {
                                                                vector2_1 += vector2_2;
                                                                --num2;
                                                                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if ((double)vector2_2.X > 1.0)
                    vector2_2.X = 1f;
                if ((double)vector2_2.X < -1.0)
                    vector2_2.X = -1f;
                if (!noYChange)
                {
                    vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                    if ((double)vector2_2.Y > 1.0)
                        vector2_2.Y = 1f;
                    if ((double)vector2_2.Y < -1.0)
                        vector2_2.Y = -1f;
                }
                else if (type != 59 && num1 < 3.0)
                {
                    if ((double)vector2_2.Y > 1.0)
                        vector2_2.Y = 1f;
                    if ((double)vector2_2.Y < -1.0)
                        vector2_2.Y = -1f;
                }
                if (type == 59 && !noYChange)
                {
                    if ((double)vector2_2.Y > 0.5)
                        vector2_2.Y = 0.5f;
                    if ((double)vector2_2.Y < -0.5)
                        vector2_2.Y = -0.5f;
                    if ((double)vector2_1.Y < Main.rockLayer + 100.0)
                        vector2_2.Y = 1f;
                    if ((double)vector2_1.Y > (double)(Main.maxTilesY - 300))
                        vector2_2.Y = -1f;
                }
            }
        }
    }    
}
