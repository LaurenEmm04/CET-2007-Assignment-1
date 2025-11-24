using System;
using System.Collections.Generic;
using CET2007A1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CET2007A1Tests
{
	[TestClass]
	public class TextFixture_LeaderboardTest
	{
		[TestMethod]
		public void Leaderboard_Unfiltered_SortedByHighScore()
		{
			Stats stats = new Stats();
			Game game1 = new Game("Minecraft", "Sandbox");
			Game game2 = new Game("Stardew Valley", "Farming");
			Game game3 = new Game("Skyrim", "RPG");

			stats.UpdateHighScore(game1, 100);
			stats.UpdateHighScore(game2, 150);
			stats.UpdateHighScore(game3, 120);

			List<GameStats> leaderboard = new List<GameStats>();
			foreach (var gs in stats.GetGameStats())
			{
				leaderboard.Add(gs);
			}

			//sorting so we can see this test work. selection sort.
			for (int i = 0; i < leaderboard.Count -1; i++)
			{
				for (int j = i + 1; j < leaderboard.Count; j++)
				{
					if (leaderboard[j].HighScore > leaderboard[i].HighScore)
					{
						GameStats temp = leaderboard[i];
						leaderboard[i] = leaderboard[j];
						leaderboard[j] = temp;
					}
				}
			}

			Assert.AreEqual("Stardew Valley", leaderboard[0].Game, "Top high score should be Stardew Valley");
			Assert.AreEqual("Skyrim", leaderboard[1].Game, "Second should be Skyrim");
			Assert.AreEqual("Minecraft", leaderboard[2].Game,"Third should be Minecraft");

		}

        [TestMethod]
        public void SortByHoursPlayed_Filtered_CorrectOrder()
		{
            Game game1 = new Game("Minecraft", "Sandbox");

			Player player1 = new Player(1, "Steve");
			Player player2 = new Player(2, "Alex");
			Player player3 = new Player(3, "Jeff");

            player1.GetStatsInterface().UpdateHoursPlayed(game1, 10); //highest
			player2.GetStatsInterface().UpdateHoursPlayed(game1, 5); //second
			player3.GetStatsInterface().UpdateHoursPlayed(game1, 2); //third

			List<Player> players = new List<Player>();
			players.Add(player1);
			players.Add(player2);
			players.Add(player3);

			List<Player> filteredPlayers = new List<Player>();
			foreach(var p in players)
			{
				var stats = p.GetStatsInterface();
				foreach (var gs in stats.GetGameStats())
				{
					if (gs.Game == "Minecraft")
					{
						filteredPlayers.Add(p);
							break;
					}
				}
			}

			//selection sort. filteres list by hours played
			for (int i =0; i< filteredPlayers.Count -1; i++)
			{
				for (int j = i+1; j < filteredPlayers.Count; j++)
				{
					int hoursI = filteredPlayers[i].GetStatsInterface().GetGameStats()[0].HoursPlayed;
                    int hoursj = filteredPlayers[j].GetStatsInterface().GetGameStats()[0].HoursPlayed;

					if (hoursj > hoursI)
					{
						Player temp = filteredPlayers[i];
						filteredPlayers[i] = filteredPlayers[j];
						filteredPlayers[j] = temp;
					}
                }
			}

			Assert.AreEqual("Steve", filteredPlayers[0].Username, "Player with the most hours should be first");
			Assert.AreEqual("Alex", filteredPlayers[1].Username, "Second most hours incorrect");
			Assert.AreEqual("Jeff", filteredPlayers[2].Username, "Lowest hours should be last");
		}
	}
}
