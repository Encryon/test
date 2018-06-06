using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CalendarEvent
/// </summary>
public class CalendarEvent
{
    public int id { get; set; }
    public int ressource { get; set; }
    public int jourinfo { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }  
}
