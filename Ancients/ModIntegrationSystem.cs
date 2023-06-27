using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenShadows.Items.Materials;
using System;
using TenShadows.Buffs;
using Terraria.Localization;
using Terraria.ID;

using TenShadows.Items;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using TenShadows.Tiles;

namespace TenShadows.Ancients
{
    // Showcases using Mod.Call of other mods to facilitate mod integration/compatibility/support
    // Mod.Call is explained here https://github.com/tModLoader/tModLoader/wiki/Expert-Cross-Mod-Content#call-aka-modcall-intermediate
    // This only showcases one way to implement such integrations, you are free to explore your own options and other mods examples

    // You need to look for resources the mod developers provide regarding how they want you to add mod compatibility
    // This can be their homepage, workshop page, wiki, github, discord, other contacts etc.
    // If the mod is open source, you can visit its code distribution platform (usually GitHub) and look for "Call" in its Mod class
    public class ModIntegrationsSystem : ModSystem
    {
        public static RecipeGroup ExampleRecipeGroup;

        public override void Unload()
        {
            ExampleRecipeGroup = null;
        }
        public override void AddRecipeGroups()
        {
            // Create a recipe group and store it
            // Language.GetTextValue("LegacyTiles.37") is the word "Any" in english, and the corresponding word in other languages
            ExampleRecipeGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyTiles.37")} {Lang.GetItemNameValue(ItemID.Tombstone)}",
              ItemID.Tombstone, ItemID.Gravestone, ItemID.GraveMarker, ItemID.CrossGraveMarker, ItemID.Headstone, ItemID.Obelisk);

            RecipeGroup.RegisterGroup("Grave", ExampleRecipeGroup);


        }
        public override void PostSetupContent()
        {


            // Boss Checklist shows comprehensive information about bosses in its own UI. We can customize it:
            // https://forums.terraria.org/index.php?threads/.50668/
            DoBossChecklistIntegration();

            // We can integrate with other mods here by following the same pattern. Some modders may prefer a ModSystem for each mod they integrate with, or some other design.
        }


        private void DoBossChecklistIntegration()
        {
            // The mods homepage links to its own wiki where the calls are explained: https://github.com/JavidPack/BossChecklist/wiki/Support-using-Mod-Call
            // If we navigate the wiki, we can find the "AddBoss" method, which we want in this case

            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }

            // For some messages, mods might not have them at release, so we need to verify when the last iteration of the method variation was first added to the mod, in this case 1.3.1
            // Usually mods either provide that information themselves in some way, or it's found on the github through commit history/blame
            if (bossChecklistMod.Version < new Version(1, 3, 1))
            {
                return;
            }

            // The "AddBoss" method requires many parameters, defined separately below:

            // The name used for the title of the page
            string bossName = "Nue";

            // The NPC type of the boss
            int bossType = ModContent.NPCType<NPCS.Nue>();

            // Value inferred from boss progression, see the wiki for details
            float weight = 0f;

            // Used for tracking checklist progress
            Func<bool> downed = () => DownedBossSystem.downedNue;

            // If the boss should show up on the checklist in the first place and when (here, always)
            Func<bool> available = () => true;

            // "collectibles" like relic, trophy, mask, pet
            List<int> collection = new List<int>()
            {
                ModContent.ItemType<NueFeather>(),

            };

            // The item used to summon the boss with (if available)
            int summonItem = ModContent.ItemType<NueSummon>();

            // Information for the player so he knows how to encounter the boss
            string spawnInfo = $"Use a [i:{summonItem}]";

            // The boss does not have a custom despawn message, so we omit it
            string despawnInfo = null;

            // By default, it draws the first frame of the boss, omit if you don't need custom drawing
            // But we want to draw the bestiary texture instead, so we create the code for that to draw centered on the intended location

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                bossName,
                bossType,
                weight,
                downed,
                available,
                collection,
                summonItem,
                spawnInfo,
                despawnInfo
            );

            // Other bosses or additional Mod.Call can be made here.
        }
    }
}