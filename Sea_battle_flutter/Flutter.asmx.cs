using System;
using System.Web.Script.Services;
using System.Web.Services;

namespace Sea_battle_flutter
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Flutter : System.Web.Services.WebService
    {

        #region DEBUG METHODS
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetPlayers()
        {
            var result = "";
            foreach (var pl in GamePlayers.players)
            {
                result += pl.id + " " + pl.inGame + " " + pl.secondPlayer + "\n";
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetGames()
        {
            var result = "";
            foreach (var pl in GamePlayers.games.games)
            {
                result += pl.pl1 + " " + pl.pl2 + " " + pl.stepX + " " + pl.stepY + "\n";
            }
            return result;
        }
        #endregion

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AddNewPlayer(string id)
        {
            var newId = Guid.Parse(id);

            GamePlayers.AddNewPlayer(newId);
            GamePlayers.ArrangePlayers(newId);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public bool IsReadyForTheGame(string userID)
        {
            Guid userid = Guid.Parse(userID);
            return GamePlayers.IsPlayerReadyForTheGame(userid);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public bool MakeStep(string userId, int stepX, int stepY)
        {
            Guid userid = Guid.Parse(userId);
            var game = GamePlayers.games.GetGameByUserId(userid);
            var isGoodStep = game.MakeStepByUser(userid, stepX, stepY);
            game.UpdateGameStatus();
            game.NextPlayer();
            return isGoodStep;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public String GetGameStatus(string userId)
        {
            Guid userid = Guid.Parse(userId);
            var game = GamePlayers.games.GetGameByUserId(userid);
            if (game.PlayerWhoWon != null)
            {
                Player p = null;
                foreach (var pl in GamePlayers.players)
                {
                    if (pl.id == userid)
                        p = pl;
                }
                if (game.PlayerWhoWon == p)
                    return "true";
                return "false";
            }
            return "continue";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public bool CanMakeStep(string userId)
        {
            Guid userid = Guid.Parse(userId);
            var game = GamePlayers.games.GetGameByUserId(userid);
            return game.IsCurrentPlayer(userid); 
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void SetPlayerShips(string userId, string shipsToParse)
        {
            Guid userid = Guid.Parse(userId);
            var game = GamePlayers.games.GetGameByUserId(userid);
            var ships = game.ParseShipsFromString(shipsToParse);
            game.SetShips(userid, ships);
        }

    }
}
