using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace LKCamelotV2.DB
{
    public class LoadPlayerResponse
    {
        public Player.Player Player;
        public Result Reslt;

        public enum Result
        {
            New = 1,
            WrongPass = 2,
            Success = 3
        }
    }

    public static class Loader
    {
        static string PlayerPath = Path.Combine("db", "players");
        static string BoardPath = Path.Combine("db", "board");

        public static LoadPlayerResponse LoadPlayer(string username, string usernamelower, string passwd)
        {
            if (!Directory.Exists(PlayerPath))
                Directory.CreateDirectory(PlayerPath);

            var charFile = Path.Combine(PlayerPath, usernamelower);
            if (File.Exists(charFile))
            {
                try
                {
                    var stream = File.OpenRead(charFile);
                    var playr = ProtoBuf.Serializer.Deserialize<Player.Player>(stream);
                    if (playr.Identity.Password == passwd)
                        return new LoadPlayerResponse() { Reslt = LoadPlayerResponse.Result.Success, Player = playr };
                    else
                        return new LoadPlayerResponse() { Reslt = LoadPlayerResponse.Result.WrongPass };
                }
                catch (Exception e)
                {
                    Log.LogException(e, "Corrupt player save file");
                }
            }
            else
            {
                return new LoadPlayerResponse() { Player = new Player.Player(username, usernamelower, passwd), Reslt = LoadPlayerResponse.Result.New };
            }
            return null;
        }

        public static void SavePlayer(Player.Player play)
        {
            if (!Directory.Exists(PlayerPath))
                Directory.CreateDirectory(PlayerPath);

            var charFile = Path.Combine(PlayerPath, play.Identity.UsernameLower);
            var fileHndle = File.Create(charFile);

            ProtoBuf.Serializer.Serialize<Player.Player>(fileHndle, play);
            fileHndle.Close();
        }

        public static void SaveBoard(Board.MessageBoard msgb, string file)
        {
            if (!Directory.Exists(BoardPath))
                Directory.CreateDirectory(BoardPath);

            var wrldFile = Path.Combine(BoardPath, file);
            var fileHndle = File.Create(wrldFile);

            ProtoBuf.Serializer.Serialize<Board.MessageBoard>(fileHndle, msgb);
            fileHndle.Close();
        }

        public static Board.MessageBoard LoadBoard(string file, bool write = true)
        {
            if (!Directory.Exists(BoardPath))
                Directory.CreateDirectory(BoardPath);

            var brdFile = Path.Combine(BoardPath, file);
            if (!File.Exists(brdFile))
                return new Board.MessageBoard(){WriteEnabled = write};

            try
            {
                var stream = File.OpenRead(brdFile);
                var brd = ProtoBuf.Serializer.Deserialize<Board.MessageBoard>(stream);
                return brd;
            }
            catch (Exception e)
            {
                Log.LogException(e, "Corrupt board save file");
            }
            return null;
        }
    }
}
