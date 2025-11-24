using System;
using CET2007A1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CET2007A1Tests
{
	[TestClass]
	public class TestFixtureGameLibraryPersistance
	{
		[TestMethod]
		public void SaveLoadLibrary_GamesPersistCorrectly()
		{
			GameLibrary library = new GameLibrary();
			Game NewGame = new Game("Celeste", "Platformer");
			library.AddGame(NewGame);

			library.SaveGameLibraryToFile();

			GameLibrary loadedLibrary = new GameLibrary();
			loadedLibrary.LoadGameLibraryFromFile();

			Game foundgame = loadedLibrary.FindGameByName("Celeste");
			Assert.IsNotNull(foundgame, "Game 'Celeste' should be foound after saving and loading");
			Assert.AreEqual("Platformer", foundgame.GameGenre, "Game genre should match after loading");
		}
	}
}
