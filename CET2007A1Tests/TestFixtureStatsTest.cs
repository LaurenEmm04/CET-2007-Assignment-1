using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CET2007A1;
using System.Linq;

namespace CET2007A1Tests
{
    [TestClass]
    public class TestFixture_StatsTest
    {
        [TestMethod]
        public void UpdateHoursPlayed_PositiveHours_CountsCorrectly()                                    //creates 2 games and a stats object
        {                                                       //updates hours/high score on both games
            Game game1 = new Game("Minecraft", "Sandbox");        //checks if both games hours/high score is correct 
            Game game2 = new Game("Stardew Valley", "Farming");     //total hours across all games and max high score
            Stats stats = new Stats();                              //accross all games

            stats.UpdateHoursPlayed(game1, 5);
            stats.UpdateHoursPlayed(game1, 3); //total should be 8


            stats.UpdateHoursPlayed(game2, 7);
            stats.UpdateHighScore(game2, 200);

            GameStats game1Stats = null;
            GameStats game2Stats = null;
            foreach (var gs in stats.GetGameStats())
            {
                if (gs.Game == "Minecraft") game1Stats = gs;
                if (gs.Game == "Stardew Valley") game2Stats = gs;
            }

            Assert.IsNotNull(game1Stats, "Minecraft stats not found");
            Assert.IsNotNull(game2Stats, "Stardew valley stats not found");

            Assert.AreEqual(8, game1Stats.HoursPlayed, "Game1 hours played is incorrect");
            Assert.AreEqual(0, game1Stats.HighScore, "Game1 high score is incorrect");
            Assert.AreEqual(7, game2Stats.HoursPlayed, "Game2 hours played is incorrect");
            Assert.AreEqual(200, game2Stats.HighScore, "Game2 high score is incorrect");
            Assert.AreEqual(15, stats.GetTotalPlayedHours(), "Total hours played is incorrect");
            Assert.AreEqual(200, stats.GetTotalHighScore(), "Total high score is incorrect");

        }



        [TestMethod]
        public void UpdateHighScore_NewScoreHigher_UpdatesHighScore()
        {
            Game game1 = new Game("Minecraft", "Sandbox");
            Stats stats = new Stats();

            stats.UpdateHighScore(game1, 100);
            stats.UpdateHighScore(game1, 150); //should update because its higher than 100. Should update to 150

            GameStats game1stats = null;
            foreach (var gs in stats.GetGameStats())
            {
                if(gs.Game =="Minecraft")
                {
                    game1stats = gs;
                    break;
                }
            }
            Assert.AreEqual(150, game1stats.HighScore, "High score should now be 150");
        }

        [TestMethod]
        public void UpdateHighScore_NewScoreLower_HighScoreDoesntChange()
        {
            Game game1 = new Game("Minecraft", "Sandbox");
            Stats stats = new Stats();
            stats.UpdateHighScore(game1, 100); //original high score

            stats.UpdateHighScore(game1, 50); //new high score. should throw an error as its lower than the first

            GameStats game1Stats = null;
            foreach(var gs in stats.GetGameStats())
            {
                if (gs.Game =="Minecraft")
                {
                    game1Stats = gs;
                    break;
                }
            }
            Assert.AreEqual(100, game1Stats.HighScore, "High score should stay as 100");
        }
    }
}


