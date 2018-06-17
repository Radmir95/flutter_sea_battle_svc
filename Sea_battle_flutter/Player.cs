using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sea_battle_flutter
{
    public class Player
    {
        public Guid id;
        public bool inGame = false;
        public bool isWaitingSecondPlayer = false;
        public Guid secondPlayer;

        public Player(Guid newId) {
            id = newId;
        }


    }
}