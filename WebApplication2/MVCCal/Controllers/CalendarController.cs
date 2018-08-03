using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCal.Controllers
{
    public class calebndarItem
    {
//        [JsonProperty("id")]
        public int Id { get; set; }
  //      [JsonProperty("title")]
        public string Title { get; set;}
        public string Url { get; set; }
    //    [JsonProperty("class")]
        public string Class { get; set; }
      //  [JsonProperty("start")]
        public long? Start { get; set; }
        //[JsonProperty("end")]
        public long? End { get; set; }

    }
    public class CalendarController : Controller
    {

        private DevRedSolCRA db = new DevRedSolCRA();

        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Calendar()
        {
            return View();
        }

        public JsonResult GetAllEvent()
        {
            DevRedSolCRA obj = new DevRedSolCRA();

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();
            //var json = JsonConvert.SerializeObject(authority, Formatting.Indented, settings);

            var calevent = obj.T_CRA.Select(x => new calebndarItem
            {
                Id = x.ID,
                Title = x.RESSOURCE.ToString(),
                Url = "http://loveit.com",
                Class = "event-info",
                Start = x.DATEMS,
                End = x.DATEMS
            }).ToList();
            //string tmp = ();

            //return Json(JsonConvert.SerializeObject(calevent, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MetadataPropertyHandling = MetadataPropertyHandling.Ignore }).Replace("\"","")); 
            //return Json(calevent, JsonRequestBehavior.AllowGet);
            var jason = Json(JsonConvert.SerializeObject(calevent, Formatting.None, settings).Replace("\"", "").Replace("[", "").Replace("]", ""));
            return jason;
//            JToken.Parse(str).ToString()
//yourString.Replace("\\", "");
//            https://stackoverflow.com/questions/13833900/return-json-but-it-includes-backward-slashes-which-i-dont-want 
//            new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            // return Json(JsonConvert.SerializeObject(calevent,Formatting.None));
        }


        public class EventModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public string @class { get; set; }
            public string start { get; set; }//Milliseconds
            public string end { get; set; } // Milliseconds

        }

        //I have use dummy list events in controller.you can query from database and return the similar example

        public JsonResult GetEvents()
        {

            DevRedSolCRA obj = new DevRedSolCRA();

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();
            //var json = JsonConvert.SerializeObject(authority, Formatting.Indented, settings);

            var calevent = obj.T_CRA.Select(x => new calebndarItem
            {
                Id = x.ID,
                Title = x.RESSOURCE.ToString(),
                Url = "http://loveit.com",
                Class = "event-info",
                Start = x.DATEMS,
                End = x.DATEMS
            }).ToList();

            List<EventModel> evt = new List<EventModel>();
            foreach  (calebndarItem eventd in calevent)
            {
                evt.Add(new EventModel { id = eventd.Id, title = eventd.Title, url = eventd.Url, @class = eventd.Class, start = eventd.Start.ToString(), end = eventd.End.ToString() });
            }

            //evt.Add(new EventModel { id = 1, title = "evt 1", url = "home/GetEventDetail/1", @class = "event-important", start = ConvertToMiliSeconds(DateTime.Now), end = ConvertToMiliSeconds(DateTime.Now.AddHours(5)) });
            //evt.Add(new EventModel { id = 2, title = "evt 2", url = "home/GetEventDetail/2", @class = "event-important", start = ConvertToMiliSeconds(DateTime.Now.AddDays(1)), end = ConvertToMiliSeconds(DateTime.Now.AddDays(1).AddHours(5)) });
            //evt.Add(new EventModel { id = 3, title = "evt 3", url = "home/GetEventDetail/3", @class = "event-important", start = ConvertToMiliSeconds(DateTime.Now.AddDays(2)), end = ConvertToMiliSeconds(DateTime.Now.AddDays(2).AddHours(5)) });
            //evt.Add(new EventModel { id = 4, title = "evt 4", url = "home/GetEventDetail/4", @class = "event-important", start = ConvertToMiliSeconds(DateTime.Now.AddDays(3)), end = ConvertToMiliSeconds(DateTime.Now.AddDays(3).AddHours(5)) });


                return Json(new { success = 1, result = evt }, JsonRequestBehavior.AllowGet);
        }

        public string ConvertToMiliSeconds(DateTime current)
        {
            DateTime dt1970 = new DateTime(1970, 1, 1);

            TimeSpan span = current - dt1970;
            return span.TotalMilliseconds.ToString();

        }

        public PartialViewResult GetEventDetail(int id)
        {
            //query from database to show the full detail of event In bootstrap model
            return PartialView();
        }

        public PartialViewResult ViewCreate()
        {
            T_CRA cra = new T_CRA();
            //cra.RESSOURCE = new SelectList(db.T_CONSULTANT, "ID", "NOM");
            ViewBag.RESSOURCE = new SelectList(db.T_CONSULTANT, "ID", "NOM");
          //  return PartialView(new T_CRA());
            //  return PartialView("~/Views/T_CRA/CreateEvent.cshtml");
            return PartialView("~/Views/T_CRA/CreateEvent.cshtml",cra);
        }

        [HttpPost]
        public ActionResult CreateEvent()
        {
            return RedirectToAction("~/Views/T_CRA/CreateEvent.cshtml");
        }

    }
}