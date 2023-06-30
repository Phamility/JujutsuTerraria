using Microsoft.Xna.Framework;
using TenShadows.Items.Techniques.AEquip;
using TenShadows.Items.Techniques.ARestrictions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenShadows.Ancients
{
    public class ExampleModAccessorySlot1 : ModAccessorySlot
    {
        // If the class is empty, everything will default to a basic vanilla slot.
    }

    public class ExampleModWingSlot : ModAccessorySlot
    {
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            if (checkItem.type == ModContent.ItemType<RestrictionDefense>() ||
                checkItem.type == ModContent.ItemType<RestrictionDamage>() ||
                checkItem.type == ModContent.ItemType<RestrictionBoots>() ||
                checkItem.type == ModContent.ItemType<RestrictionHeart>() ||
                checkItem.type == ModContent.ItemType<HeavenlyPhysical>()) // if is Wing, then can go in slot
                return true;

            return false; // Otherwise nothing in slot
        }

        // Designates our slot to be a priority for putting wings in to. NOTE: use ItemLoader.CanEquipAccessory if aiming for restricting other slots from having wings!
        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            if (item.type == ModContent.ItemType<RestrictionDefense>() ||
                item.type == ModContent.ItemType<RestrictionDamage>() ||
                item.type == ModContent.ItemType<RestrictionBoots>() ||
                item.type == ModContent.ItemType<RestrictionHeart>() ||
                item.type == ModContent.ItemType<HeavenlyPhysical>()) // If is Wing, then we want to prioritize it to go in to our slot.
                return true;

            return false;
        }


        // Overrides the default behaviour where a disabled accessory slot will allow retrieve items if it contains items
        public override bool IsVisibleWhenNotEnabled()
        {
            return false; // We set to false to just not display if not Enabled. NOTE: this does not affect behavour when mod is unloaded!
        }
        public override string FunctionalBackgroundTexture => "Terraria/Images/Inventory_Back11";
        public override bool DrawVanitySlot => false;
        public override bool DrawDyeSlot => false;

        // Icon textures. Nominal image size is 32x32. Will be centered on the slot.
        //   public override string FunctionalTexture => "Terraria/Images/Item_" + ItemID.ShadowKey;
        public override string FunctionalTexture => base.FunctionalTexture;

        // Can be used to modify stuff while the Mouse is hovering over the slot.
        public override void OnMouseHover(AccessorySlotType context)
        {
            // We will modify the hover text while an item is not in the slot, so that it says "Wings".
            switch (context)
            {
                case AccessorySlotType.FunctionalSlot:
                    Main.hoverItemName = "Binding Vow";
                    break;
       
            }
        }
    }
}