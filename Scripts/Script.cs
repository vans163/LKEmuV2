using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Scripts
{
    public static class Script
    {
        public static void Load()
        {
            Experience.Load();
            Spells.Load();
            Maps.Load();
            Chat.Load();
            Monsters.Load();
            Items.Load();
            Npcs.Load();
            Minerals.Load();
            Recipe.Load();
            Messages.Load();
        }
    }
}
