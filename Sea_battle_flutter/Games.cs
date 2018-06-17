using System;
using System.Collections.Generic;

namespace Sea_battle_flutter
{
    public class Games
    {
        public List<Game> games = new List<Game>();

        public Game GetGameByUserId(Guid userId)
        {
            foreach (var g in games)
                if (g.pl1.id == userId || g.pl1.secondPlayer == userId)
                    return g;
            return null;
        }

        public bool AlreadyHaveGame(Player pl1, Player pl2)
        {
            foreach (Game g in games)
            {
                if (pl1.id == g.pl1.id || pl1.id == g.pl2.id)
                    return true;
            }
            return false;
        }

    }
}