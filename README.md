
 1.Clone this repository to the desktop.
 2.Open the Solution in Visual Studio 2013. 
 3.Build the Solution to enable nuget packages. 
 4.Change the Connection string Web.config in the Web Api Project.
 5.Configure Web Api in IIS with Port No:9093
 6.Do Migration Steps with following commands
 a.Delete Migration File Inside the SMEasy.Data project
 b.Open Tools-> Nu-Get Package Manager -> Package Manager Console
 c.Select Project SMEasy.WebApi
 d. Type Command Enable-Migrations (First Time Only)
 e. Type Command Add-Migration Migration
 f. Type Command Update-Database
 7.Rebuild the solution and Run
