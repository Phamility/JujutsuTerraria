
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;


namespace JujutsuTerraria.Ancients
{
    public class ExampleWorld : ModSystem
    {
        public int HowManyIceFormations;
        public int HowManyFingers;
        public int HowManyPicks;
        public int HowManyOceans;


        public override void PostWorldGen()
        {
            HowManyIceFormations = 0;
            HowManyFingers = 0;
            HowManyPicks = 0;
            HowManyOceans = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {


                Chest chest = Main.chest[chestIndex];
                int[] itemsToPlaceInChests4 = { Mod.Find<ModItem>("IceFormation").Type };
                // int IceFormationNum = Main.rand.Next(23, 51);
                int IceFormationNum = 1;

                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 11 * 36)
                {
                    if (Main.rand.Next(1, 101) <= 35 || HowManyIceFormations < 2)
                    {
                        HowManyIceFormations++;
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                            if (chest.item[inventoryIndex].type == 0)
                            {

                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChests4[0]);
                                chest.item[inventoryIndex].Prefix(-1);
                                chest.item[inventoryIndex].stack = IceFormationNum;

                                break;
                            }
                    }
                }


            }
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {


                Chest chest = Main.chest[chestIndex];
                int[] itemsToPlaceInChests4 = { Mod.Find<ModItem>("SukunaFinger").Type };
                // int IceFormationNum = Main.rand.Next(23, 51);
                int IceFormationNum = 1;
                //Gold, Locked Gold, Jungle, Locked Shadow, Jungle 2, Ice, Golem, Spider
                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && (Main.tile[chest.x, chest.y].TileFrameX == 1 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 2 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 8 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 11 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 5 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 10 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 15 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 16 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 13 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 3 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 12 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 32 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 4 * 36 ||
                    Main.tile[chest.x, chest.y].TileFrameX == 50 * 36||
                    Main.tile[chest.x, chest.y].TileFrameX == 51 * 36||

                    Main.tile[chest.x, chest.y].TileFrameX == 17 * 36))
                {
                    if (Main.rand.Next(1, 101) <= 40 || HowManyFingers < 20)
                    {
                        HowManyFingers++;
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChests4[0]);
                                chest.item[inventoryIndex].stack = IceFormationNum;

                                break;
                            }
                    }
                }
                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers2 && Main.tile[chest.x, chest.y].TileFrameX == 10 * 36)
                {
                    if (Main.rand.Next(1, 101) <= 40 || HowManyFingers < 20)
                    {
                        HowManyFingers++;

                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChests4[0]);
                                chest.item[inventoryIndex].stack = IceFormationNum;

                                break;
                            }
                    }
                }



            }
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {


                Chest chest = Main.chest[chestIndex];
                int[] itemsToPlaceInChests4 = { Mod.Find<ModItem>("RestrictionMine").Type };
                // int IceFormationNum = Main.rand.Next(23, 51);
                int IceFormationNum = 1;

                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 1 * 36)
                {
                    if (Main.rand.Next(1, 101) <= 20 || HowManyPicks < 2)
                    {
                        HowManyPicks++;
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                            if (chest.item[inventoryIndex].type == 0)
                            {

                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChests4[0]);
                                chest.item[inventoryIndex].stack = IceFormationNum;

                                break;
                            }
                    }
                }


            }
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {


                Chest chest = Main.chest[chestIndex];
                int[] itemsToPlaceInChests4 = { Mod.Find<ModItem>("DEOcean").Type };
                // int IceFormationNum = Main.rand.Next(23, 51);
                int IceFormationNum = 1;

                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 1 * 36)
                {
                    if (Main.rand.Next(1, 101) <= 20 || HowManyOceans < 2)
                    {
                        HowManyOceans++;
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                            if (chest.item[inventoryIndex].type == 0)
                            {

                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChests4[0]);
                                chest.item[inventoryIndex].stack = IceFormationNum;

                                break;
                            }
                    }
                }


            }
        }
    }
}
