# Favorite Band Tracker

Specification:

| Behavior | Input | Output | Description of Specification |
| :-------------     | :------------- | :------------- | :------------- |
| Start with nothing in database to begin with.| | | Verify that there is nothing in the database as we don't want to mess anything up if the database is currently already in use. |
| Has the ability to create Venues | "The House of Blues, San Diego" | The House of Blues San Diego | Add the name of the venue to the database. |
| Has the ability to update the venues information in database. | "The House of Blues, San Diego" | The House of Blues Portland | Update an venue's existing information in the database. |
| Can remove Specific venues | "The House of Blues, San Diego" | NULL | Simply deletes a venue and any bands associated with it.|
| Has the ability to add bands to a specific venue | "Paramore" | Band: Paramore is playing at [The House of Blues, The Hollywood Bowl, etc..] | Add a band and show which venue(s) it is playing at.|

## Server and Database Setup

This application utitizes Microsoft SQL Server Management Studio.
Connect to the server.


To recreate the database from the schema open the file in SSMS(SQL Server Management Studio).
Open the file, Scripts_test.sql and add the following lines of code to the top of the schema:


Recreating from Schema:
If there is currently no database create one with the command:

CREATE DATABASE band_tracker_test
Enter the command GO.

This ends the statement, allowing sql to execute and allows the user to type in a new command.

Lastly, Save and hit execute. Your server should now be up and running!

## Installation/Prerequisites

Git Clone or Download at: https://github.com/eluts15/crispy-waddle-band-tracker.git

In order to get server up and running, run the following command:

  dnx kestrel

Now, navigate to localhost:5004

Runs on the .Net Framework

Requires Nancy Web Framework located at: http://nancyfx.org/. You can also do this via Windows PowerShell with the Command:

Install-Package Nancy if you don't have it.

Run dnu restore if necessary to update dependencies.

#TODO
1. Add abilty to display when a particular band is playing by date.
2. Should show tour dates of bands by date in decending order.  This will come at a later time.
3. Allow the user to add important tour dates to a calender wiget.

## Usage

1. Behavior Driven Development with the  Nancy Web Framework.
2. Working with SQL queries using SSMS.

## Known Bugs

1. There are probably a few..

## License

The MIT License

Copyright <2017> <Ethan Luts>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
