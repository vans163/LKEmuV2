using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.Scripts
{
    public static class Messages
    {
        public static void Load()
        {
            SetWorldSysOpBoard();
        }

        static void SetWorldSysOpBoard()
        {
            Board.MessageBoard SysOpBoard = new Board.MessageBoard();
            SysOpBoard.WriteEnabled = false;
            SysOpBoard.Messages.TryAdd(1, new Board.Message()
            {
                Id = 1,
                Title = "The New Server",
                Content =
                    "This is a new and rebuilt from scratch LK server. " + 
                    "The hope for this server is to fix all problems with the old one. " + 
                    "Many new features have been added such as: " + System.Environment.NewLine +
                    "  Mining and Smithing " + System.Environment.NewLine +
                    "  New Spells and Effects " + System.Environment.NewLine +
                    "  No more custom items " + System.Environment.NewLine +
                    "  Correct loading of maps " + System.Environment.NewLine +
                     System.Environment.NewLine +
                    "The content will be deployed in stages so it can be balanced and bugs can be fixed. " + 
                    "The current plan for content is Bronze Age (release) -> Iron Age (~2 weeks) " + 
                    "-> Hellenistic Age (~4 weeks) -> ? ."
            });
            SysOpBoard.Messages.TryAdd(2, new Board.Message()
            {
                Id = 2,
                Title = "Help required",
                Content =
                    "Mapping the correct x,y coords of portals at: " + System.Environment.NewLine +
                    "  - @go vv, cave " + System.Environment.NewLine +
                    "  - Fixing correct XP for Mines " + System.Environment.NewLine +
                    "  - Fixing correct # of hits for Mines " + System.Environment.NewLine +
                    "  - Telling me how Rubber, Square Bar " + System.Environment.NewLine +
                    "  and other missing mats were created " + System.Environment.NewLine +
                    "  - Fixing every non-code detail of crafting " + System.Environment.NewLine +
                    "  - Fixing spell damage details (I remember " + System.Environment.NewLine +
                    "  Twin Cobra being very weak) but the" + System.Environment.NewLine +
                    "  game guide has it doing 120+ damage " + System.Environment.NewLine
            });
            SysOpBoard.Messages.TryAdd(3, new Board.Message()
            {
                Id = 3,
                Title = "The Bronze Age",
                Content = 
                "Troubled times these last months have been; I as Merlot will inform you. " + 
                "I have began to notice strange and different citizens about the town. "  + 
                "As if they are not from here, as if they are some kind of creature. " + 
                "The parents have stopped letting their children play in the caves and mines, "  + 
                "as one too many have not returned. This was a safe place once. "  + 
                "The passage between the world of the dead and our world of the living must be " +
                "opening.  Those that should not be here are beginning to be able to cross over. " +
                "Make haste, at once. Bring peace to our lands, for I fear it soon be too late."
            });
            SysOpBoard.Messages.TryAdd(4, new Board.Message()
            {
                Id = 4,
                Title = "The Bronze Age (Content)",
                Content =
                    "@go weakly, skel, miner " + System.Environment.NewLine +
                    "Mining: Iron, Copper, Wax " + System.Environment.NewLine +
                    "Smithing: Up to level 10 recipies " + System.Environment.NewLine,
            });

            World.World.SysOpBoard = SysOpBoard;
        }
    }
}
