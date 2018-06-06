CalendarSite is a web app(web form based) developed using FullCalendar jquery plugin and asp.net.
I have used asp.net 3.5.

Here are the steps to get this app up and running
1) Just create a asp.net website in Microsoft Visual Studio(i have used VS2008 version). 

2) Extract the downloaded zip file.

3) COPY following things from the downloaded zip file: App_Code folder, css folder, fullcalendar folder, 
   jquery folder, scripts folder, Default.aspx file, Default.aspx.cs file, JsonResponse.ashx file
   and PASTE them to the root of your website.(VS will give warning about files being overwritten, click 'yes' to that)
   
4) For storing events, i have used sql server express database. 
   Just create a new database in SQL Server and in that database create a table named "event"
   Here is the table format:
   event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
   make event_id as the primary key and in column properties for event_id set (Is Identity) to 'yes'
   so that it autoincrements.
   
5) Make changes in EventDAO class in App_Code folder. 
   So all you have to do to get this app working is, to make changes in EventDAO class. 
   Basically just change the connection string and the names of the columns which you 
   have used in your database. The names of column used in my EventDAO class are as per 
   the table 'event' shown above.Plus see the comments in the class, if you have doubts.
   If you have used the same table name and column names as used by me in step 4, 
   then just change the 'connectionString' variable in EventDAO class to point to your database. 

mail me at: nashtry@gmail.com


