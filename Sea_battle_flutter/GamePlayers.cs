using System;
using System.Collections.Generic;

namespace Sea_battle_flutter
{
    public static class GamePlayers
    {
        public static List<Player> players = new List<Player>();
        public static Games games = new Games();

        static object mutex_obj = new object();

        static Player GetPlayerById(Guid id)
        {
            foreach (var pl in players)
            {
                if (pl.id.Equals(id))
                    return pl;
            }
            return null;
        }

        public static void ArrangePlayers(Guid id)
        {
            lock (mutex_obj)
            {
                var currPlayer = GetPlayerById(id);

                foreach(var player in players)
                {
                    if (!player.id.Equals(id) && !player.inGame)
                    {
                        //assign players
                        player.inGame = true;
                        currPlayer.inGame = true;
                        player.secondPlayer = id;
                        currPlayer.secondPlayer = player.id;
                        //create game
                        if (!games.AlreadyHaveGame(currPlayer, player))
                        {
                            var g = new Game(currPlayer, player);
                            games.games.Add(g);
                        }
                        break;
                    }
                }
            }
        }

        public static void AddNewPlayer(Guid playerID)
        {
            players.Add(new Player(playerID));
        }

        public static bool IsPlayerReadyForTheGame(Guid userID)
        {
            var curr_player = GetPlayerById(userID);
            if (curr_player == null) return false;
            foreach(var pl in players)
            {
                if (curr_player.id == pl.secondPlayer)
                    return true;
            }
            return false;
        }

    }
}