using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace JujutsuTerraria.Ancients
{
    internal class KeyBindSystem : ModSystem
    {
        public static ModKeybind JujutsuTerraria { get; private set; }
        public static ModKeybind JujutsuTerraria1 { get; private set; }

        public static ModKeybind JujutsuTerraria2 { get; private set; }
        public static ModKeybind JujutsuTerraria3 { get; private set; }
        public static ModKeybind JujutsuTerraria4 { get; private set; }


        public override void Load()
        {
            // Registers a new keybind
            // We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual text displayed to English users is in en-US.hjson
            JujutsuTerraria = KeybindLoader.RegisterKeybind(Mod, "Quick Use Bird Silhouette", "U");
            JujutsuTerraria1 = KeybindLoader.RegisterKeybind(Mod, "Quick Use Tiger Silhouette", "U");

            JujutsuTerraria2 = KeybindLoader.RegisterKeybind(Mod, "Quick Use Deer Silhouette", "U");
            JujutsuTerraria3 = KeybindLoader.RegisterKeybind(Mod, "Quick Use Restless Gambler", "U");
            JujutsuTerraria4 = KeybindLoader.RegisterKeybind(Mod, "Quick Use Simple Domain", "U");


        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            JujutsuTerraria = null;
            JujutsuTerraria1 = null;

            JujutsuTerraria2 = null;

            JujutsuTerraria3 = null;

        }
    }
}
