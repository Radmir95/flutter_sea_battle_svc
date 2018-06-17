using System;
using System.Collections.Generic;

namespace Sea_battle_flutter
{
    public class Game
    {
        Guid id;

        public int stepX = -1;
        public int stepY = -1;

        public Player pl1 = null;
        public Player pl2 = null;

        public List<List<int>> firstPlayerShips = null;
        public List<List<int>> secondPlayerShips = null;

        private Player currPlayer = null;
        public Player PlayerWhoWon = null;

        public List<List<int>> ParseShipsFromString(string ships)
        {
            var arrays = ships.ToCharArray();
            List<List<int>> shipss = new List<List<int>>();
            for (int i = 0; i < 10; i++)
            {
                List<int> tempArr = new List<int>(10);
                for (int j = 0; j < 10; j++)
                {
                    tempArr.Add(int.Parse(arrays[i * 10 + j].ToString()));
                }
                shipss.Add(tempArr);
            }
            return shipss;
        }

        public Game(Player pl1, Player pl2)
        {
            this.id = Guid.NewGuid();
            this.pl1 = pl1;
            this.pl2 = pl2;
            this.currPlayer = pl1;
        }
        
        public void NextPlayer()
        {
            if (currPlayer == pl1) currPlayer = pl2; 
            else currPlayer = pl1; 
        }

        public bool IsCurrentPlayer(Guid userId)
        {
            if (currPlayer.id == userId)
                return true;
            return false;
        }

        internal void SetShips(Guid userid, List<List<int>> ships)
        {
            if (pl1.id == userid)
                firstPlayerShips = ships;
            else
                secondPlayerShips = ships;
        }

        internal void UpdateGameStatus()
        {
            bool isFirstPlayerWin = true;
            bool isSecondPlayerWin = true;
            // logic to check is any player win the game
            foreach(List<int> l in firstPlayerShips)
                foreach(int val in l)
                    if (val == 1) isSecondPlayerWin = false;
            foreach (List<int> l in secondPlayerShips)
                foreach (int val in l)
                    if (val == 1) isFirstPlayerWin = false;
            if (isFirstPlayerWin) PlayerWhoWon = pl1;
            if (isSecondPlayerWin) PlayerWhoWon = pl2;
        }

        internal bool MakeStepByUser(Guid userid, int stepY, int stepX)
        {
            bool returnValue = false;
            if (pl1.id == userid)
            {
                if (secondPlayerShips[stepX][stepY] == 1)
                    returnValue = true;
                secondPlayerShips[stepX][stepY] = 2;
            }
            else
            {
                if (firstPlayerShips[stepX][stepY] == 1)
                    returnValue = true;
                firstPlayerShips[stepX][stepY] = 2;
            }
            return returnValue;
        }

    }
}