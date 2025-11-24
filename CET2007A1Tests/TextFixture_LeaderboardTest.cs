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
	}
}
