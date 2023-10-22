# Create-RapidStart

This example C# code uses LINQ https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/working-with-linq to convert a CSV file into XML and compresses that XML in the format the BC uses for RapidStart packages.

In input CSV that the code reads is included : FA.csv

The output files are FA.xml and FA.rapidstart - to open the RapidStart file just use 7zip or WinRAR

# Why?

Excel has a limit of just over 1 million rows, RapidStart files have no such limit and are much smaller!

Importing data via a RapidStart is faster than Excel. In this example there are just under 25k rows. Import from Excel is about 480 seconds while RapidStart is 360 seconds. So a marginal improvement.

The real difference is in exporting data which is orders of magnitude faster with exporting to a package vs excel. Checkout https://github.com/andywingate/Plunder-RapidStart

# About me

Check out my blog at https://blog.wingateuk.com
