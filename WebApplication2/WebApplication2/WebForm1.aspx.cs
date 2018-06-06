using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Net;
using HtmlAgilityPack;
using System.Text;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public static List<DateTime> list = new List<DateTime>();
        public static string valdrop;
        public Dictionary<int, dayWork> currMonth = new Dictionary<int, dayWork>();
        public enum dayWork { off = 0, middle_off = 1, working = 2, middle_working = 3, blank = 4 }; //Finalement ça résume
        public static List<int> daySelect = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SelectedDates"] = new List<DateTime>();
            }
            else
            {
                string[] test = Page.Request.Params.GetValues("lbltravdemi_18");
            }
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            // if (!IsPostBack)
            // {
            //HtmlGenericControl spandemi = new HtmlGenericControl("span");

            //}

            List<DateTime> list = (List<DateTime>)Session["SelectedDates"];
            //list.Clear();
            if (e.Day.IsSelected == true)
            {
                // if (!newList.Contains(e.Day.Date))
                list.Add(e.Day.Date);
            }
            /*
            CheckBox chk = new CheckBox();
            chk.ID = "chk_" + e.Day.DayNumberText;
            chk.AutoPostBack = true;
            chk.CheckedChanged += new EventHandler(Chk_Selectedchange);
            e.Cell.Controls.Add(new LiteralControl("<br />"));
            e.Cell.Controls.Add(chk);
           */
            Session["SelectedDates"] = list;


            HtmlGenericControl spandemi = new HtmlGenericControl();
            spandemi.ID = "lbltravdemi_" + e.Day.DayNumberText;
            spandemi.TagName = "lbltravdemi_" + e.Day.DayNumberText;

            spandemi.InnerText = "coucou";
            //spandemi.Text = String.Empty;
            //spandemi.Attributes.Add("runat", "server");
            e.Cell.Controls.Add(spandemi);
        }

        //private void Add_day(DayRenderEventArgs e)
        //{
        //    Label lblTravail = new Label();

        //    lblTravail.ID = "lbltrav_" + e.Day.DayNumberText;

        //    if (lblTravail.ID != null && !DropDownList1.SelectedValue.Equals("Travail Journée"))
        //    {
        //        if (DropDownList1.SelectedValue.Equals("Travail demi-Journée"))
        //            add_halfday(e);
        //        //else
        //    }
        //    else
        //    {
        //        lblTravail.Text = "Travail Journée";
        //        e.Cell.Controls.Add(new LiteralControl("<br />"));
        //        e.Cell.Controls.Add(lblTravail);
        //    }
        //}

        private void add_halfday(DayRenderEventArgs e)
        {//Label lblTravaildemi = new Label();
            //if (!this.Controls.ContainsKey("lbltravdemi_" + e.Day.DayNumberText))
            //  if (Page.Form.FindControl("lbltravdemi_" + e.Day.DayNumberText) != null)
            //if (this.Page.FindControl(lblTravaildemi.Attributes["lbltravdemi_" + e.Day.DayNumberText]) != null)

            //if ((Label)Page.FindControl("lbltravdemi_" + e.Day.DayNumberText) != null)
            // Label lbl = new Label();
            //   lbl.ID = "lbltravdemi_" + e.Day.DayNumberText ;

            ////(Label)Calendar1.FindControlRecursive("lbltravdemi_" + e.Day.DayNumberText);//this.FindControlRecursive("lbltravdemi_18");

            // var ctrl = e.Cell.Controls[0].ID;//.FindControl("lbltravdemi_18");// as HtmlGenericControl;
            string ctrl = string.Empty;
            try
            {
                ctrl = Page.Request.Params.GetValues("lbltravdemi_18")[0];
            }
            catch
            { }

            if (ctrl != "")
            //  if (lblTravaildemi.ID != null)
            {
                //Add_day(e);
            }
            else
            {
                HtmlGenericControl spandemi = new HtmlGenericControl("span");
                spandemi.ID = "lbltravdemi_" + e.Day.DayNumberText;
                spandemi.InnerText = DropDownList1.SelectedValue.ToString();
                spandemi.Attributes.Add("runat", "server");
                // e.Cell.Controls.Add(new LiteralControl("<br />"));
                e.Cell.Controls.Add(spandemi);

            }
        }

        protected void Chk_Selectedchange(object sender, EventArgs e)
        {
            CheckBox cbsend = (CheckBox)sender;
            string g = cbsend.ID;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime selection = Calendar1.SelectedDate;
            var x = Session["SelectedDates"];

            //if (Session["SelectedDates"] != null)
            if (!String.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                List<DateTime> newList = (List<DateTime>)Session["SelectedDates"];
                foreach (DateTime dt in newList)
                {
                    Calendar1.SelectedDates.Add(dt);
                    daySelect.Add(dt.Day);

                }

                if (searchdate(selection, newList))
                {
                    if (String.IsNullOrEmpty(DropDownList1.SelectedValue))
                        Calendar1.SelectedDates.Remove(selection);
                    //daySelect.Remove(dt.Day);
                }

                if (newList.Count == 0)
                {
                    newList.Add(selection);
                    daySelect.Add(selection.Day);
                }
                Session["daySelect"] = daySelect;
                Session["SelectedDates"] = newList;

                list.Clear();
                list = newList;
            }

        }

        public bool searchdate(DateTime date, List<DateTime> dates)
        {

            var query = from o in dates
                        where o.Date == date
                        select o;
            if (query.ToList().Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        protected void Calendar1_PreRender(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDates.Count == 1)
            {
                if (String.IsNullOrEmpty(DropDownList1.SelectedValue))
                {
                    foreach (DateTime dt in list)
                    {
                        if (searchdate(dt, list) && list.Count == 1)
                        {
                            Calendar1.SelectedDates.Clear();
                            break;
                        }
                    }
                }
            }

        }



        public void Button1_Click(object sender, EventArgs e)
        {
           
            // valdrop = DropDownList1.SelectedItem.Value;
            //object obj = new object();
            //DayRenderEventArgs dayr += new DayRenderEventArgs(Calendar1_DayRender);
            //new EventHandler += new EventHandler(ddlBlist_SelectedIndexChanged);
            //Calendar1_DayRender(obj, dayr);

            //Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);


            /*
                        if (valdrop == "Travail journée")
                        {

                            Add_day(e);

                        }

                        if (valdrop == "Travail demi-journée")
                        {
                            add_halfday(e);
                        }


                        if (valdrop == "Vacance")
                        {
                            Label lblVacance = new Label();
                            lblVacance.ID = "lbltravdemi_" + e.Day.DayNumberText;
                            lblVacance.Text = "Vacance";
                            e.Cell.Controls.Add(new LiteralControl("<br />"));

                            e.Cell.Controls.Add(lblVacance);

                        }

                        if (valdrop == "")
                        {
                            Label lblVacance = new Label();
                            lblVacance.ID = "lbltravdemi_" + e.Day.DayNumberText;
                            lblVacance.Text = "";
                            e.Cell.Controls.Add(new LiteralControl("<br />"));

                            e.Cell.Controls.Add(lblVacance);

                        }
             * */
            DataTable _tbl = new DataTable();//LoadMonthValues(DateTime.Parse(datemy.Value).ToString("MM"), DateTime.Parse(datemy.Value).ToString("yyyy"));

            // ConfigurationSettings.AppSettings["sourcehtml"];
            //2016 et //1 année et mois doivent être passés en paramètre
            int days = DateTime.DaysInMonth(Calendar1.SelectedDate.Year, Calendar1.SelectedDate.Month);//DateTime.DaysInMonth(2016, 2);
            for (int day = 1; day <= days; day++)

                // if (_tbl.Rows.Contains(day))
                if (_tbl.AsEnumerable().Any(row => day.ToString() == row.Field<String>("JOUR")))
                {

                    DataRow[] _row = _tbl.Select("MOIS=" + Calendar1.SelectedDate.Month.ToString() + " and ANNEE=" + Calendar1.SelectedDate.Year.ToString() + " and JOUR=" + day);
                    if (_row[0][5].Equals((int)dayWork.middle_off))
                    {
                        currMonth.Add(day, dayWork.off);
                    }
                    if (_row[0][5].Equals((int)dayWork.off))
                    {
                        currMonth.Add(day, dayWork.off);
                    }
                    if (_row[0][5].Equals((int)dayWork.working))
                    {
                        currMonth.Add(day, dayWork.working);
                    }
                    if (_row[0][5].Equals((int)dayWork.middle_working))
                    {
                        currMonth.Add(day, dayWork.working);
                    }


                }
                else
                {
                    string value = string.Empty;
                    if (DropDownList1.SelectedIndex.Equals(0))
                    {
                        value = string.Empty;
                    }
                    if (DropDownList1.SelectedIndex.Equals(1))
                    {
                        value = ((int)dayWork.working).ToString();
                    }
                    if (DropDownList1.SelectedIndex.Equals(2))
                    {
                        value = ((int)dayWork.middle_working).ToString();
                    }
                    if (DropDownList1.SelectedIndex.Equals(3))
                    {
                        value = ((int)dayWork.off).ToString();
                    }
                    if (DropDownList1.SelectedIndex.Equals(4))
                    {
                        value = ((int)dayWork.middle_working).ToString();
                    }
                    currMonth.Add(day, (dayWork)Enum.Parse(typeof(dayWork), value));
                }

            int i = 0;
            foreach (var item in currMonth)
            {

                List<int> mavalue = GetKeyList(currMonth, i.ToString());
                foreach (int itemlist in mavalue)
                {

                    ChargerActivite(Calendar1.SelectedDate.Month.ToString(), Calendar1.SelectedDate.Year.ToString(), "1", i.ToString(), itemlist);
                    {

                        //ReadHtml(@"D:\Users\09082009\Desktop\testcalendar\WebApplication2\WebApplication2\WebForm1.aspx");//HttpContext.Current.Request.Url.ToString());
                        Control foundControl = FindControlRecursive(Page.Form, "lbltravdemi_" + i);

                        /*  if (itemlist.Equals(0))
                              ((HtmlGenericControl)foundControl).InnerText = "Absent";
                          else if (itemlist.Equals(1))
                              ((HtmlGenericControl)foundControl).InnerText = "Absent demi-journée";
                          else if (itemlist.Equals(2))
                              ((HtmlGenericControl)foundControl).InnerText = "Journée";
                          else if (itemlist.Equals(3))
                              ((HtmlGenericControl)foundControl).InnerText = "Demi-journée";
                          else
                              ((HtmlGenericControl)foundControl).InnerText = string.Empty;*/
                    }
                }
                i++;
                //Console.WriteLine(currMonth[item.Key]);
            }
            //Console.WriteLine(i);
            //Console.ReadKey();
        }

        public static Control FindControlRecursive(Control rootControl, string id)
        {
            if (rootControl != null)
            {
                if (rootControl.ID == id)
                {
                    return rootControl;
                }

                for (int i = 0; i < rootControl.Controls.Count; i++)
                {
                    Control child;
                    if ((child = FindControlRecursive(rootControl.Controls[i], id)) != null)
                    {
                        return child;
                    }
                }
            }
            return null;
        }

        public List<int> GetKeyList(Dictionary<int, dayWork> dico, string lookUpKey)
        {
            List<int> keysList = new List<int>();
            foreach (KeyValuePair<int, dayWork> item in dico)
            {
                if (item.Key.ToString() == lookUpKey)
                {
                    keysList.Add((int)item.Value);
                }
            }
            return keysList;
        }

        private void ReadHtml(string filehtml)
        {
            string htmlCode;
            //System.IO.StreamReader srtHtml;
            //using (WebClient client = new WebClient())
            //{
            //    htmlCode = client.DownloadString(filehtml);
            //    srtHtml = new System.IO.StreamReader(htmlCode);
            //    // Write the string to a file.
            // //   System.IO.StreamWriter file = new System.IO.StreamWriter(filehtml);
            //  //  file.WriteLine(htmlCode);

            //    //file.Close();
            //}

            HtmlDocument doc = new HtmlDocument();
            //doc.Load(filehtml);
            doc.Load(filehtml);
            bool finderror = false;
            //   foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//table[@id='Calendar1']"))
            {
                try
                {
                    foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table[@id='Calendar1']"))
                    {
                        foreach (HtmlNode row in table.SelectNodes("tr"))
                        {
                            foreach (HtmlNode cell in row.SelectNodes("th|td"))
                            {

                                // Wrapper acts as a root element
                                string newContent = "<div>" + "toto" + "</div>";

                                // Create new node from newcontent
                                HtmlNode newNode = HtmlNode.CreateNode(newContent);

                                // Get body node
                                // HtmlNode body = html.DocumentNode.SelectSingleNode("//body");

                                // Add new node as first child of body
                                //body.PrependChild(newNode);
                                //table.PrependChild(newNode);
                                cell.AppendChild(newNode);
                            }

                        }
                    }

                    // Get contents with new node
                    string contents = doc.DocumentNode.InnerHtml;
                    //StringBuilder strbmsgerr = new StringBuilder();
                    //foreach (string server in servers)
                    //{
                    //    if (link.InnerText.IndexOf(server) > 0)
                    //    {
                    //        strbmsgerr.AppendLine(string.Format("le serveur {0} a été retrouvé dans la liste des serveurs down", server));

                    //    }

                    //    if (!String.IsNullOrEmpty(strbmsgerr.ToString()))
                    //    {
                    //        finderror = true;
                    //        System.IO.StreamWriter fileer = new System.IO.StreamWriter(fileerrormsg);
                    //        fileer.WriteLine(strbmsgerr);
                    //        fileer.Close();

                    //    }
                    //}
                }
                catch (Exception ex)
                { throw ex; }

                if (finderror)
                {

                }
                //System.IO.File.Delete(filehtml);
                //Console.ReadLine();


            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            valdrop = DropDownList1.SelectedItem.Value;
        }

        protected void Calendar1_Disposed(object sender, EventArgs e)
        {

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

        public static bool ChargerActivite(string _month, string _year, string _ressource, string _day, int _jourinfo)
        {
            try
            {
                DeleteMonthValues(_month, _year);
                AddMonthValues(_month, _year, _ressource, _day, _jourinfo);
            }
            finally
            {//
            }
            return true;
        }
    }

}
