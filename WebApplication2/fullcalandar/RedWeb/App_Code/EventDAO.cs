using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// EventDAO class is the main class which interacts with the database. SQL Server express edition
/// has been used.
/// the event information is stored in a table named 'event' in the database.
///
/// Here is the table format:
/// event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
/// event_id is the primary key
/// </summary>
/// 


public class EventDAO
{
    private static string dbchain = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

    //change the connection string as per your database connection.
    //private static string connectionString = "Data Source=PEUWUL24667\\MSSQLSERVER2008;Initial Catalog=NewRedSol;User ID=sa;Password=Pepsico2008" ;

	//this method retrieves all events within range start-end
    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
       
        List<CalendarEvent> events = new List<CalendarEvent>();
        DAL.DAL _dal = new DAL.DAL(dbchain, DAL.DAL.Moteurs.SQLSERVER.ToString());
        if (_dal.IsConnexionFermee())
            //Ouverture de la connection
            _dal.OuvrirConnexion();

        string[] _param = new string[2];
        _param[0] = "@date_start" + ";" + SqlDbType.DateTime + ";" + start.ToString();
        _param[1] = "@date_end" + ";" + SqlDbType.DateTime+ ";" + end.ToString();

        DataTable _tbl_loadevent = _dal.GetInformationInDataTable("LoadCRAEvent", CommandType.StoredProcedure, _param);

        _dal.FermeConnexion();

        //SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end FROM event where event_start>=@start AND event_end<=@end", con);
        //cmd.Parameters.AddWithValue("@start", start);
        //cmd.Parameters.AddWithValue("@end", end);
        
        //using (con)
        //{
        //    con.Open();
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
            foreach (DataRow _row in _tbl_loadevent.Rows)
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = (int)_row["id"];
                cevent.ressource = (int)_row["ressource"];
                cevent.jourinfo = (int)_row["jourinfo"];
                cevent.start = (DateTime)_row["event_start"];
                cevent.end = (DateTime)_row["event_end"];
                events.Add(cevent);
           }
        //}
        return events;
        //side note: if you want to show events only related to particular users,
        //if user id of that user is stored in session as Session["userid"]
        //the event table also contains a extra field named 'user_id' to mark the event for that particular user
        //then you can modify the SQL as:
        //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
        //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
    }

	//this method updates the event title and description
    public static void updateEvent(int id, String title, String description)
    {
        /*SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE event SET title=@title, description=@description WHERE event_id=@event_id", con);
        cmd.Parameters.AddWithValue("@title", title);
        cmd.Parameters.AddWithValue("@description", description);
        cmd.Parameters.AddWithValue("@event_id", id);
        */
        DAL.DAL _dal = new DAL.DAL(dbchain, DAL.DAL.Moteurs.SQLSERVER.ToString());
        if (_dal.IsConnexionFermee())
            //Ouverture de la connection
            _dal.OuvrirConnexion();

        string[] _param = new string[3];
        _param[0] = "@ID" + ";" + SqlDbType.Int+ ";" + id.ToString();
        _param[1] = "@RESSOURCE" + ";" + SqlDbType.Int+ ";" + title.ToString();
        _param[2] = "@JOURINFO" + ";" + SqlDbType.Int+ ";" + description.ToString();

        _dal.ExecuteQueryNoResult("UpdateCRAEvent", CommandType.StoredProcedure, _param);

       /* using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        */

    }

	//this method updates the event start and end time
    public static void updateEventTime(int id, DateTime start, DateTime end)
    {
        //SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("UPDATE event SET event_start=@event_start, event_end=@event_end WHERE event_id=@event_id", con);
        //cmd.Parameters.AddWithValue("@event_start", start);
        //cmd.Parameters.AddWithValue("@event_end", end);
        //cmd.Parameters.AddWithValue("@event_id", id);
        //using (con)
        //{
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //}

        DAL.DAL _dal = new DAL.DAL(dbchain, DAL.DAL.Moteurs.SQLSERVER.ToString());
        if (_dal.IsConnexionFermee())
            //Ouverture de la connection
            _dal.OuvrirConnexion();

        string[] _param = new string[2];
        _param[0] = "@ID" + ";" + SqlDbType.Int + ";" + id.ToString();
        _param[3] = "@START" + ";" + SqlDbType.DateTime + ";" + end.ToString();
        _param[5] = "@END" + ";" + SqlDbType.DateTime + ";" + end.ToString();

        _dal.ExecuteQueryNoResult("UpdateCRAEventTime", CommandType.StoredProcedure, _param);
    }

	//this mehtod deletes event with the id passed in.
    public static void deleteEvent(int id)
    {
        //SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("DELETE FROM event WHERE (event_id = @event_id)", con);
        //cmd.Parameters.AddWithValue("@event_id", id);
        //using (con)
        //{
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //}

        DAL.DAL _dal = new DAL.DAL(dbchain, DAL.DAL.Moteurs.SQLSERVER.ToString());
        if (_dal.IsConnexionFermee())
            //Ouverture de la connection
            _dal.OuvrirConnexion();

        string[] _param = new string[1];
        _param[0] = "@ID" + ";" + SqlDbType.Int + ";" + id.ToString();
        
        _dal.ExecuteQueryNoResult("DeleteCRAEvent", CommandType.StoredProcedure, _param);
    }

	//this method adds events to the database
    public static int addEvent(CalendarEvent cevent)
    {
        //add event to the database and return the primary key of the added event row

        //insert
        //SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("INSERT INTO event(title, description, event_start, event_end) VALUES(@title, @description, @event_start, @event_end)", con);
        //cmd.Parameters.AddWithValue("@title", cevent.title);
        //cmd.Parameters.AddWithValue("@description", cevent.description);
        //cmd.Parameters.AddWithValue("@event_start", cevent.start);
        //cmd.Parameters.AddWithValue("@event_end", cevent.end);

     
        for (int i = 0; i < DateDifference(cevent.start, cevent.end); i++)
        {
            DAL.DAL _dal = new DAL.DAL(dbchain, DAL.DAL.Moteurs.SQLSERVER.ToString());
            if (_dal.IsConnexionFermee())
                //Ouverture de la connection
                _dal.OuvrirConnexion();

            //generation de la date
            string[] _param = new string[4];
            _param[0] = "@RESSOURCE" + ";" + SqlDbType.Int + ";" + cevent.ressource.ToString();
            _param[1] = "@JOURINFO" + ";" + SqlDbType.Int + ";" + cevent.jourinfo.ToString();
            
            if (cevent.start.Equals(cevent.end))
            {
                _param[2] = "@START" + ";" + SqlDbType.DateTime + ";" + cevent.start.ToString();
                _param[3] = "@END" + ";" + SqlDbType.DateTime + ";" + cevent.end.ToString();
            }
            else
            {
                DateTime dateval;
                if (i.Equals(0))
                     dateval = cevent.start;
                else
                     dateval = cevent.start.AddDays(i);

                _param[2] = "@START" + ";" + SqlDbType.DateTime + ";" + dateval.ToString();
                _param[3] = "@END" + ";" + SqlDbType.DateTime + ";" + dateval.ToString();
            }
    
            _dal.ExecuteQueryNoResult("InsertCRAEvent", CommandType.StoredProcedure, _param);
            _dal.FermeConnexion();
        }

            
        int key = 0;
        //using (con)
        //{
            //con.Open();
            //cmd.ExecuteNonQuery();

            ////get primary key of inserted row
            //cmd = new SqlCommand("SELECT max(event_id) FROM event where title=@title AND description=@description AND event_start=@event_start AND event_end=@event_end", con);
            //cmd.Parameters.AddWithValue("@title", cevent.title);
            //cmd.Parameters.AddWithValue("@description", cevent.description);
            //cmd.Parameters.AddWithValue("@event_start", cevent.start);
            //cmd.Parameters.AddWithValue("@event_end", cevent.end);

           // key = (int)cmd.ExecuteScalar();
    //}
        DAL.DAL _dal2 = new DAL.DAL(dbchain, DAL.DAL.Moteurs.SQLSERVER.ToString());
        if (_dal2.IsConnexionFermee())
            //Ouverture de la connection
            _dal2.OuvrirConnexion();


        string[] _parammax = new string[3];
        _parammax[0] = "@START" + ";" + SqlDbType.DateTime + ";" + cevent.start.ToString();
        _parammax[1] = "@END" + ";" + SqlDbType.DateTime + ";" + cevent.end.ToString();
        _parammax[2] = "@JOURINFO" + ";" + SqlDbType.Int + ";" + cevent.jourinfo.ToString();

        DataTable _tbl_idmax = _dal2.GetInformationInDataTable("MaxIDCRAEvent", CommandType.StoredProcedure, _parammax);

        key = (int)_tbl_idmax.Rows[0]["ID"];

        _dal2.FermeConnexion();
        return key;

    }


    private static int DateDifference(DateTime _datedeb, DateTime _datefin)
    {
        int _dif = 0;
        if (_datedeb.Equals(_datefin))
            _dif = 1;
        else 
        {
            TimeSpan difference = _datefin.Date - _datedeb.Date;
            _dif = (int)difference.Days + 1;
        }
        return _dif;
    }

    
}
