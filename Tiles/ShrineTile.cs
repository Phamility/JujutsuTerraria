﻿using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Microsoft.Xna.Framework;

using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace TenShadows.Tiles
{
    public class ShrineTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;


            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18, 18 };
            TileObjectData.addTile(Type);

            //  AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            DustType = DustID.ManaRegeneration;

            // Etc
            AddMapEntry(new Color(255, 105, 73), Language.GetText("Jujutsu Shrine"));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 48, ModContent.ItemType<Shrine>());
        }



    }
}