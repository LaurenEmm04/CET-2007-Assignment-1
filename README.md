# CET 2007 Assignment 1

In this project, I have created a working Player manager system with the ability to create new players, add stats (high score and hours played), log new games and add stats to certain games as well as a functional leaderboard with the ability to sort via high score or hours played with the option to filter by games chosen if you wish.

## Table of Contents
- [Important Information](#important-information)
- [How to Navigate the Commit History](#how-to-navigate-the-commit-history)
- [How to Run My Program](#how-to-run-my-program)
- [My Program: What and Why I made it this way](#my-program-what-and-why-i-made-it-this-way)
  - [Design Choices](#design-choices)
- [Conclusion](#conclusion)


## Important Information
As you can see, there are multiple folders in here. Please follow these instructions to find the code your looking for.

When I started making the program, I unknowingly made it using .NET 8.0 (Now named in the solution OLD CET 2007 Assignment 1). I realised when it came to testing that my fully working program will not test using the testing class instructed in lecture. To fix this, I created a new solution in the same project, named it NET CET 2007 Assignment 1 (the code and files like Stats.json, Player.json and Log.txt for it is in the folder ConsoleApp1, I apologise for it not having a name, I was unsure if I was able to fix it at the time, and by the time I did, the filepaths relied on the folder staying named ConsoleApp1)

OLD CET 2007 (the .NET 8.0 program) works the exact same way as the NET new one. I just had to convert it over and change some of the handling of private methods through Json. **The only thing missing is the Log.txt/Players.json/Stats.json from the old version. If I kept those logs, the program would try and read them. They would be unable to since its a different .NET version and would refuse to create new files. I have removed them from the project entirely but I still have access to the old logs, players and stats if required.**


## How to navigate the Commit history

All _commits are still available for the older NET 8.0 version_. The folder the code is in is _CET 2007 Assignment 1_. Again, this is the old version.

You can access the Commit history for all versions (old and new) [Here](https://github.com/LaurenEmm04/CET-2007-Assignment-1/commits/master/).

The best way to navigate this programs creation is through the commit history. Due to the number of files (and the attempts I made to convert it to .NET framework 4.7.2) The CET2007_NET472VERSION folder may not be in use.  I attempted to delete it previously and my program broke entirely, therefore it must stay.


## How To Run my Program
You will be required to download my ZIP file of my project. When downloaded, unzip and run the __CET 2007 Assignment 1.sln__ solution.
<img width="942" height="727" alt="image" src="https://github.com/user-attachments/assets/d0c5e7a9-ccb4-4af0-afa8-7127859e1329" />
When opened, you may see this. If the NET CET 2007 Assignment 1 and/or OLD CET 2007 Assignment 1 is folded up as seen here, press the arrow on the left side of the program. It will then open the files of code associated with that program. 

**For the fully working code, only use NET CET 2007 Assignment 1**

To make sure your running the correct program, ensure you specify the program running as NET CET 2007 Assignment 1. You can do this by first ensuring you have a .cs file open in the program. Under extensions, there will be a cog icon and the name of the project that will run. Ensure it says NET CET (ect..) If it doesn't, click the dropdown arrow on the right and select it. This will run the correct program.
<img width="1183" height="384" alt="image" src="https://github.com/user-attachments/assets/e2ca8441-9e19-41d0-8a24-9d5d969227ee" />

The tests are stored in CET2007A1Tests. To see the tests themselves if you can't, press the arrow button on the left side of the program to unfold the files.
<img width="300" height="175" alt="image" src="https://github.com/user-attachments/assets/a2ba26dd-dce2-4647-b47a-6069f7a2f124" />


## My program. What and why i made it this way.
Here I will discuss my process on why I created my program the way I did and how I did it.

## Design Choices

---

### Player, PlayerManager and PlayerFactory
To begin, I decided to keep Player, Stats and Game separate to ensure they all handled only their own key information to keep the program easier to understand.

Later on, i would add classes like PlayerManager, which would focus on tasks such as holding the menu options, adding, updating and viewing players and their player stats. It also displays a players game library, the leaderboard, finds players by ID and Username, and manages saving/loading of player data. 

I found having PlayerManager handle most of the methods, including the menu, kept Program.cs a lot cleaner and made everything easier to understand and follow as each methods purpose is clear from its name. Program.cs only handles the projects startup and menu navigation choices, which then leads back to PlayerManager. This keeps the programs entry point clean and focused.

Moving on from PlayerManager, I created a factory pattern for player creation (PlayerFactory) as a way to streamline the consistent creation of players, reducing system load and speeding up actions.

Finally, within the Player class, the PlayerStats property is marked with [JsonProperty]. This allows the private Stats object to be serialised and deserialised when saving or loading players to JSON files, ensuring each players stats persist between sessions whilst maintaining encapsulation. Encapsulation is enforced because all other methods must access player stats through GetStatsInterface, which exposes only the public interface (IUpdatableStats) of PlayerStats. This stops external code from changing the Stats object directly, reducing the risk of accidental data corruption, and makes it easier to maintain and extend the Stats class in the future without breaking other parts of the program.


---

### Stats

For Stats, I wanted a class that would track a players progress across all their games. It handled things like the hours played and high scores for each game. Nothing else was added outside of that regard to keep it focused and simple. Each game gets its own GameStats object. This makes it easy to update or read stats for that particular game without affecting other games.

The Stats class has methods like UpdateHoursPlayed and UpdateHighScore, which checks if a GameStats entry exists first, if it doesnt, then it would create one. This prevents issues where stats wern't recorded because no entry existed. I also included methods such as GetTotalPlayedHours and GetTotalHighScore so I can quickly summarise a players progress.

DisplayStats lets the program print out stats in a clean, readable format, making it easy for players to see their performance. Overall, having my program like this keeps stats modular and easy to update, as well as easy to debug.

---

### Game, GameStats and GameLibrary
I kept Game simple and easy to understand, having it store only the name, genre and unique ID for each game. It was made as a container, so other parts of the program (like GameLibrary or Stats) could use it without any problems. The random ID system makes sure that each game has a unique identifier, even if multiple games have the same name. By keeping it small and easy to understand, I've reduced the program's complexity and made debugging easier.

GameStats works in tandem with Game by keeping track of the players interactions with each game. It stores the hours played and high score of each game. Instead of linking the full Game object, it only stores the games name, making JSON serialisation simple and reliable. This allows Stats to manage the tracking of player performance without needing to modify the actual Game objects.

GameLibrary is used as the area for all things relating to the game handling. It handles listing games, adding new ones (with checks for duplicate names and IDs) and finding games via Name or ID. Furthermore, it also handles saving and loading the library to a JSON file, which ensures the data persists between sessions.

The combination of Game, GameStats and GameLibrary ensures a clean separation between tracking player performance and managing the collection of games. This makes the program easier to read, test and debug.

---

### CustomExceptions
This contains exceptions such as PlayerNotFoundExeption, InvalidIDException and DuplicatePlayerException. They provide clear and specific error handling, which improves the usability of the program.

---

### IUpdatableStats
This defines key methods that must be used whenever it is called. This includes updating scores and hours, display stats and retrieving totals. Using this interface allows safe access to a players stats whilst maintaining encapsulation and limiting crashes from user input errors.

--- 
### Leaderboard
This manages sorting and displaying players by high score or hours played. It lets the user choose if they'd like to filter the searching by a specific game or not. Separating the leaderboard logic keeps the Player and Stats classes focused solely on their core responsibilities.

---

### Logger
This is a singleton pattern class that logs the programs interactions as it goes on (adding games, moving from menu options, updating information, closing the program ect..) by adding the code line Logger.GetInstance().Log("") wherever a particularly notable action takes place. It helps with tracing paths of what was done, which helps debugging.

---

## Conclusion
Overall, I feel like this project demonstrates good use of OOP principles to manage players, games and stats. JSON data persistance and organised code are used well and the choice to seperate responsibilities, use interfaces and to add design patterns helps keep the application maintainable, testable and easy to navigate.
