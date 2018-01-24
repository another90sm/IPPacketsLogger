# IPPacketsLogger

Before start!
 - Needed .NET framework v 4.5.2 or higher.
 - App is working only with SQLite database until this moment.
 - If you want this app to work without saving data in database set "WorkWithDataBase" to "false" in PLogger.exe.config file.
 - To properly save data in database set directory for database file in PLogger.exe.config file in format {Drive}:\{Folder}, for example "C:\PLogger".
 - To properly work app should be "Run as administrator".

PLogger v1.0.0.2
Summary:
 - Added filter functionality for source/destination IP addresses.
 - Added button to clear treeview content.
 - Added functionality for copying source/destination IP addresses into clipboard.
 - Minor bug fixing.
 
PLogger v1.0.0.1
Summary:
 - Successfuly saving data in database.
 - Asynchronous saving data.
 - Saving data over transaction.
 - Keep only 100 packets in treeview, otherwise memmory is constantly filling.
 - Not adding/removing nodes when app is minimized. 
 - Fixed treeview flickering on adding packet nodes.
 
PLogger v1.0.0.0
Summary:
 - The app is working but for now without saving incoming/outcoming packets in database
