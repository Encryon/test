using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace WebApplication2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected DataSet dsHolidays;
        protected string gmonth;
        protected string gyear;
        public static List<DateTime> list = new List<DateTime>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Calendar1.VisibleDate = DateTime.Today;
                gmonth = DateTime.Today.ToString("MM");
                gyear = DateTime.Today.ToString("yyyy");

                datemy.Value = DateTime.Today.ToString();
                FillHolidayDataset();
               // LoadLegende();
            }
        
        }

        protected void LoadLegende()
        {
            string _constr = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDb"].ToString();
            DAL.DAL _dal = new DAL.DAL(_constr, DAL.DAL.Moteurs.SQLSERVER.ToString());
            if (_dal.IsConnexionFermee())
                //Ouverture de la connection
                _dal.OuvrirConnexion();

            string[] _param = new string[0];

            DataTable _tbl_legende = _dal.GetInformationInDataTable("LoadLegende", CommandType.StoredProcedure, _param);

            _dal.FermeConnexion();

            BulletedList1.DataSource = _tbl_legende;
            BulletedList1.DataBind();

        }


        protected void FillHolidayDataset()
        {
            DateTime firstDate = new DateTime(Calendar1.VisibleDate.Year,
                Calendar1.VisibleDate.Month, 1);
            DateTime lastDate = GetFirstDayOfNextMonth();
            dsHolidays = GetCurrentMonthData(firstDate, lastDate);
        }

        protected DataSet GetCurrentMonthData(DateTime firstDate, 
     DateTime lastDate)
        {
            DataSet dsMonth = new DataSet();
    
    /*query = "SELECT HolidayDate FROM Holidays " + _
        " WHERE HolidayDate >= @firstDate AND HolidayDate < @lastDate";*/
    string _constr = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDb"].ToString();
    DAL.DAL _dal = new DAL.DAL(_constr, DAL.DAL.Moteurs.SQLSERVER.ToString());
    if (_dal.IsConnexionFermee())
        //Ouverture de la connection
        _dal.OuvrirConnexion();

    string[] _param = new string[2];

    _param[0] = "@MOIS" + ";" + SqlDbType.NVarChar + ";" + DateTime.Parse(datemy.Value).ToString("MM");
    _param[1] = "@ANNEE" + ";" + SqlDbType.NVarChar + ";" + DateTime.Parse(datemy.Value).ToString("yyyy");
    //_param[2] = "@RESSOURCE" + ";" + SqlDbType.Int + ";" + _ressource.ToString();
    //_param[3] = "@JOUR" + ";" + SqlDbType.NVarChar + ";" + _day.ToString();
    //_param[4] = "@JOURINFO" + ";" + SqlDbType.Int + ";" + _jourinfo.ToString();
    try
    {

     dsMonth = _dal.GetInformationInDataSet("LoadMonthValues", CommandType.StoredProcedure, _param);
    _dal.FermeConnexion();
    
    }
    catch {}
    return dsMonth;
}

        protected DateTime GetFirstDayOfNextMonth()
        {
            int monthNumber, yearNumber;
            if (Calendar1.VisibleDate.Month == 12)
            {
                monthNumber = 1;
                yearNumber = Calendar1.VisibleDate.Year + 1;
            }
            else
            {
                monthNumber = Calendar1.VisibleDate.Month + 1;
                yearNumber = Calendar1.VisibleDate.Year;
            }
            DateTime lastDate = new DateTime(yearNumber, monthNumber, 1);
            return lastDate;
        }


        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime nextDate;
            string er = e.ToString();
            if (dsHolidays != null)
            {
                foreach (DataRow dr in dsHolidays.Tables[0].Rows)
                {

                    /* nextDate = (DateTime)dr["HolidayDate"];
                     if (nextDate == e.Day.Date)
                     {
                         e.Cell.BackColor = System.Drawing.Color.Pink;
                     }*/
                    string dayval;
                    if (int.Parse(e.Day.DayNumberText) < 10)
                        dayval = "0" + e.Day.DayNumberText;
                    else dayval = e.Day.DayNumberText;

                    DataRow[] mulitirow = dsHolidays.Tables[0].Select("JOUR = " + dayval);
                    bool rowmulti = false;
                    if (mulitirow.Length > 1)
                        rowmulti = true;

                    if (dr["JOUR"].Equals(dayval) && e.Day.IsOtherMonth.Equals(false))
                    {
                        if (dr["JOURINFO"].Equals(0))
                            e.Cell.BackColor = System.Drawing.Color.HotPink;
                        else if (dr["JOURINFO"].Equals(1))
                            if (rowmulti)
                            {
                                e.Cell.BackColor = System.Drawing.Color.Yellow;
                            }
                            else
                            {
                                e.Cell.BackColor = System.Drawing.Color.Pink;
                            }

                        else if (dr["JOURINFO"].Equals(2))
                            e.Cell.BackColor = System.Drawing.Color.Green;
                        else if (dr["JOURINFO"].Equals(3))
                            if (rowmulti)
                            {
                                e.Cell.BackColor = System.Drawing.Color.Yellow;
                            }
                            else
                            {
                                e.Cell.BackColor = System.Drawing.Color.PaleGreen;
                            }

                        else e.Cell.BackColor = System.Drawing.Color.White;
                    }
                }
            }
             else
            {
                FillHolidayDataset();
                UpdateData(Calendar1.SelectedDate);
                Calendar1_DayRender(sender, e);
            }

            //if (e.Day.IsSelected == true)
            //{
            //    list.Add(e.Day.Date);
            //}
            //Session["SelectedDates"] = list;
        }
        protected void Calendar1_VisibleMonthChanged(object sender,
            MonthChangedEventArgs e)
        {
            //datemy.Value = Calendar1.SelectedDate.ToString();
            datemy.Value = Calendar1.VisibleDate.ToString();
            //gmonth = Calendar1.SelectedDate.Month.ToString("MM");
            //gyear = Calendar1.SelectedDate.Year.ToString("yyyy");
            FillHolidayDataset();
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

            //if (Session["SelectedDates"] != null)
            //{
            //    List<DateTime> newList = (List<DateTime>)Session["SelectedDates"];
            //    foreach (DateTime dt in newList)
            //    {
            //            Calendar1.SelectedDates.Add(dt);
            //    }
            //    list.Clear();
            //}
          
            if (Calendar1.SelectedDate.ToString("yyyy").Equals("0001"))
            {
               // Calendar1.VisibleDate = DateTime.Parse("01/" + DateTime.Parse(datemy.Value).ToString("MM") + "/" + DateTime.Parse(datemy.Value).ToString("yyyy"));
                Calendar1.SelectedDate = DateTime.Parse(datemy.Value);
            }
            //gmonth = Calendar1.SelectedDate.ToString("MM");
            //gyear = Calendar1.SelectedDate.ToString("yyyy");


            //Session["SelectMonth"] = gmonth;
            //Session["SelectYear"] = gyear;
            datemy.Value = Calendar1.VisibleDate.ToString();
            
            UpdateData(Calendar1.SelectedDate);

            FillHolidayDataset();
        }

        private void UpdateData(DateTime _dt)
        {
            DataTable _tbl = LoadMonthValues(DateTime.Parse(datemy.Value).ToString("MM"), DateTime.Parse(datemy.Value).ToString("yyyy"));
            string dayval = DropDownList1.SelectedValue;

            if (_tbl.AsEnumerable().Any(row => _dt.ToString("dd") == row.Field<String>("JOUR")))
            {

                DataRow[] _row = _tbl.Select("MOIS=" + _dt.ToString("MM") + " and ANNEE=" + _dt.ToString("yyyy") + " and JOUR=" + _dt.ToString("dd"));
                
                if (dayval.Equals(string.Empty))
                {
                    DeleteMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), _dt.ToString("dd"));
                }
                else if (_row[0][5].Equals(0))
                {
                    DeleteMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), _dt.ToString("dd"));
                    AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), int.Parse(dayval));
                }
                else if (_row[0][5].Equals(1))
                {
                    DeleteMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), _dt.ToString("dd"));
                    if (dayval.Equals("1"))
                    {
                        AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), 0);
                    }
                    else if (dayval.Equals("3"))
                    {
                        AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), 1);
                        AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), 3);
                    }
                    else
                        AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), 1);

                }
                else if (_row[0][5].Equals(2))
                {
                    DeleteMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), _dt.ToString("dd"));
                    AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), int.Parse(dayval));
                }
                else if (_row[0][5].Equals(3))
                {
                    DeleteMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), _dt.ToString("dd"));
                    if (dayval.Equals("3"))
                    {
                        AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), 2);
                    }
                    else if (dayval.Equals("1"))
                    {
                        AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), 1);
                        AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), 3);
                    }
                    else
                        AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), int.Parse(dayval));
                }

            }
            else
            {
                if (!String.IsNullOrEmpty(dayval))
                    AddMonthValues(_dt.ToString("MM"), _dt.ToString("yyyy"), "1", _dt.ToString("dd"), int.Parse(dayval));
            }
        }

        public static DataTable LoadMonthValues(string _month, string _year)
        {
            string _constr = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDb"].ToString();
            DAL.DAL _dal = new DAL.DAL(_constr, DAL.DAL.Moteurs.SQLSERVER.ToString());
            if (_dal.IsConnexionFermee())
                //Ouverture de la connection
                _dal.OuvrirConnexion();

            string[] _param = new string[2];

            _param[0] = "@MOIS" + ";" + SqlDbType.NVarChar + ";" + _month.ToString();
            _param[1] = "@ANNEE" + ";" + SqlDbType.NVarChar + ";" + _year.ToString();

            DataTable _tbl_activity = _dal.GetInformationInDataTable("LoadMonthValues", CommandType.StoredProcedure, _param);

            _dal.FermeConnexion();

            return _tbl_activity;
        }


        public static void DeleteMonthValues(string _month, string _year)
        {
            string _constr = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDb"].ToString();
            DAL.DAL _dal = new DAL.DAL(_constr, DAL.DAL.Moteurs.SQLSERVER.ToString());
            if (_dal.IsConnexionFermee())
                //Ouverture de la connection
                _dal.OuvrirConnexion();

            string[] _param = new string[2];

            _param[0] = "@MOIS" + ";" + SqlDbType.NVarChar + ";" + _month.ToString();
            _param[1] = "@ANNEE" + ";" + SqlDbType.NVarChar + ";" + _year.ToString();

            _dal.ExecuteQueryNoResult("DeleteMonthValues", CommandType.StoredProcedure, _param);

            _dal.FermeConnexion();
        }

        public static void AddMonthValues(string _month, string _year, string _ressource, string _day, int _jourinfo)
        {
            string _constr = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDb"].ToString();
            DAL.DAL _dal = new DAL.DAL(_constr, DAL.DAL.Moteurs.SQLSERVER.ToString());
            if (_dal.IsConnexionFermee())
                //Ouverture de la connection
                _dal.OuvrirConnexion();

            string[] _param = new string[5];

            _param[0] = "@MOIS" + ";" + SqlDbType.NVarChar + ";" + _month.ToString();
            _param[1] = "@ANNEE" + ";" + SqlDbType.NVarChar + ";" + _year.ToString();
            _param[2] = "@RESSOURCE" + ";" + SqlDbType.Int + ";" + _ressource.ToString();
            _param[3] = "@JOUR" + ";" + SqlDbType.NVarChar + ";" + _day.ToString();
            _param[4] = "@JOURINFO" + ";" + SqlDbType.Int + ";" + _jourinfo.ToString();

            _dal.ExecuteQueryNoResult("AddMonthValues", CommandType.StoredProcedure, _param);
            _dal.FermeConnexion();
        }


        
        public static void DeleteMonthValues(string _month, string _year, string _day)
        {
            string _constr = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDb"].ToString();
            DAL.DAL _dal = new DAL.DAL(_constr, DAL.DAL.Moteurs.SQLSERVER.ToString());
            if (_dal.IsConnexionFermee())
                //Ouverture de la connection
                _dal.OuvrirConnexion();

            string[] _param = new string[3];

            _param[0] = "@MOIS" + ";" + SqlDbType.NVarChar + ";" + _month.ToString();
            _param[1] = "@ANNEE" + ";" + SqlDbType.NVarChar + ";" + _year.ToString();
            _param[2] = "@JOUR" + ";" + SqlDbType.NVarChar + ";" + _day.ToString();
            
            _dal.ExecuteQueryNoResult("DeleteDayMonthValues", CommandType.StoredProcedure, _param);
            _dal.FermeConnexion();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //gmonth = Session["SelectMonth"].ToString();
            //gyear = Session["SelectYear"].ToString();
            datemy.Value = Calendar1.VisibleDate.ToString();
            Session["SelectMonth"] = DateTime.Parse(datemy.Value).ToString("MM");
            Session["SelectYear"] = DateTime.Parse(datemy.Value).ToString("yyyy");

            string url="WebForm3.aspx"; //url de la popup html 
            ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}','rapport','channelmode=1');</script>", url));
           // FillHolidayDataset();


            Calendar1_SelectionChanged(sender, new EventArgs());
            //Calendar1.DataBind();
        }

        protected void Calendar1_VisibleMonthChanged1(object sender, MonthChangedEventArgs e)
        {
            Calendar1_SelectionChanged(sender, new EventArgs());
        }
        protected void Button2_Click(object sender, EventArgs e)
        {

        }
}
 }
