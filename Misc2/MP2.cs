
using System; using JujutsuTerraria.Buffs; 
using Terraria;
using System.IO;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Utilities;
using System.Linq;
using Terraria.ModLoader.IO;
using System.Text;
using System.Threading.Tasks;


namespace JujutsuTerraria.Misc2
{
    class MP2 : ModPlayer
    {
        //------------------------------------------------------------------------------------------


        public const int Threat1Max = 1;
        public int FingersConsumed;
        public static int Quantified;
        public override void PostUpdateBuffs()
        {
            Quantified = FingersConsumed;
        }
        public override void ResetEffects()
        {

            Player.GetDamage(DamageClass.Generic) += (FingersConsumed * .01f); ;
            if (FingersConsumed > 0)
            {
                Player.AddBuff(ModContent.BuffType<SukunaBuff>(), 2);
            }

        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();

            packet.Write(FingersConsumed);
            packet.Send(toWho, fromWho);
        }
  
        public override void SaveData(TagCompound tag)
        {
            tag["FingersConsumed"] = FingersConsumed;
        }
        public override void LoadData(TagCompound tag)
        {
            FingersConsumed = tag.GetInt("FingersConsumed");
        }
    }
    //--------------------------------------------------------------------------------------------
}