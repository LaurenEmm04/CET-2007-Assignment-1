# CET 2007 Assignment 1

In this project, I have created a working Player manager system with the ability to create new players, add stats (high score and hours played), log new games and add stats to certain games as well as a functional leaderboard with the ability to sort via high score or hours played with the option to filter by games chosen if you wish.

## Important Information
As you can see, there are multiple folders in here. Please follow these instructions to find the code your looking for.

When I started making the program, I unknowingly made it using .NET 8.0 (Now named in the solution OLD CET 2007 Assignment 1). I realised when it came to testing that my fully working program will not test using the testing class instructed in lecture. To fix this, I created a new solution in the same project, named it NET CET 2007 Assignment 1 (the code and files like Stats.json, Player.json and Log.txt for it is in the folder ConsoleApp1, I apologise for it not having a name, I was unsure if I was able to fix it at the time, and by the time I did, the filepaths relied on the folder staying named ConsoleApp1)

OLD CET 2007 (the .NET 8.0 program) works the exact same way as the NET new one. I just had to convert it over and change some of the handling of private methods through Json. **The only thing missing is the Log.txt/Players.json/Stats.json from the old version. If I kept those logs, the program would try and read them. They would be unable to since its a different .NET version and would refuse to create new files. I have removed them from the project entirely but I still have access to the old logs, players and stats if required.**

## Table of Contents
- [How to Navigate the Commit History](#how-to-navigate-the-commit-history)
- [How to Run My Program](#how-to-run-my-program)
- [My Program: What and Why I made it this way](#my-program-what-and-why-i-made-it-this-way)
- [Design Choices](#design-choices)

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
Here I will discuss my process on why I created my program the way I did.

### Design Choices

