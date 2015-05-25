using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace LKCamelotV2.Board
{
    [ProtoContract]
    public class MessageBoard
    {
        [ProtoMember(1)]
        public ConcurrentDictionary<int, Message> Messages = new ConcurrentDictionary<int, Message>();
        [ProtoMember(2)]
        public bool WriteEnabled = true;

        public MessageBoard()
        {
        }

        public bool WriteMessage(Player.Player plyr, string title, string content)
        {
            if (!WriteEnabled)
                return false;

            var msg = new Message();
            msg.Title = title;
            msg.Content = content;
            Messages.TryAdd(msg.Id, msg);
            return true;
        }

        public void DrawBoard(Player.Player plyr)
        {
            if (Messages.Count == 0)
                plyr.gameLink.Send(new Network.GameOutMessage.OpenBoard().Compile());
              //  plyr.gameLink.Send(new Network.GameOutMessage.UpdateBoardHead(null).Compile());

            foreach (var msg in Messages.Skip(0).Select(xe => xe.Value).ToArray())
            {
                plyr.gameLink.Send(new Network.GameOutMessage.UpdateBoardHead(msg).Compile());
            }
        }

        public void DrawBoardDetail(Player.Player plyr, int id)
        {
            Message outm = null;
            if (Messages.TryGetValue(id, out outm))
                plyr.gameLink.Send(new Network.GameOutMessage.UpdateBoardDetail(outm).Compile());
        }
    }
}
