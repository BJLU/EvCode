using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        public IEnumerable<Tender> list_tender { get; set; }
        TenderContext db = new TenderContext();
        

        [HttpGet]
        public ActionResult To(int page = 1)
        {
            

            if (list_tender is null)
            {
                list_tender = db.Tenders;
                ViewBag.Tenders = list_tender;
            }

            int pageSize = 3;
            IEnumerable<Tender> tendersPerPages = list_tender.OrderBy(tender => tender.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list_tender.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Tenders = tendersPerPages };


            return View(ivm);
        }
        [HttpGet]
        public ActionResult InfoTender(int id)
        {
            var tenders = db.Tenders;
            var tender = tenders.FirstOrDefault(ten => id == ten.Id);
            ViewBag.TenderId = tender;

            return View();
        }
        [HttpPost]
        public ActionResult To(string str)
        {
            list_tender = db.Tenders;
            list_tender = list_tender.Where(ten => (ten.Subject_tender.IndexOf(str, StringComparison.OrdinalIgnoreCase)) != -1 || (ten.Description_tender.IndexOf(str, StringComparison.OrdinalIgnoreCase)) != -1);
            ViewBag.Tenders = list_tender;


            if (list_tender is null)
            {
                list_tender = db.Tenders;
                ViewBag.Tenders = list_tender;
            }

            int pageSize = 3;
            int page = 1;
            IEnumerable<Tender> tendersPerPages = list_tender.OrderBy(tender => tender.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list_tender.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Tenders = tendersPerPages };


            return View(ivm);
        }

        [HttpPost]
        [ActionName("Organizer")]
        [Route("Organizer/{organizer}")]
        public ActionResult ToOrganizer(string organizer)
        {
            list_tender = db.Tenders;
            list_tender = list_tender.Where(ten => (ten.Organizer_tender.IndexOf(organizer, StringComparison.OrdinalIgnoreCase)) != -1);
            ViewBag.Tenders = list_tender;

            if (list_tender is null)
            {
                list_tender = db.Tenders;
                ViewBag.Tenders = list_tender;
            }

            int pageSize = 3;
            int page = 1;
            IEnumerable<Tender> tendersPerPages = list_tender.OrderBy(tender => tender.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list_tender.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Tenders = tendersPerPages };


            return View("To", ivm);
        }

        [HttpPost]
        [ActionName("Kind")]
        [Route("Kind/{kindTender}")]
        public ActionResult ToKind(string kindTender)
        {
            list_tender = db.Tenders;
            list_tender = list_tender.Where(element => (element.Kind_tender.IndexOf(kindTender, StringComparison.OrdinalIgnoreCase)) != -1);
            ViewBag.Tenders = list_tender;

            if (list_tender is null)
            {
                list_tender = db.Tenders;
                ViewBag.Tenders = list_tender;
            }

            int pageSize = 3;
            int page = 1;
            IEnumerable<Tender> tendersPerPages = list_tender.OrderBy(tender => tender.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list_tender.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Tenders = tendersPerPages };


            return View(ivm);
        }

        [HttpPost]
        [ActionName("Data")]
        [Route("Data/{dataTender}")]
        public ActionResult ToData(string dataTender)
        {
            DateTime data = DateTime.Parse(dataTender);
            list_tender = db.Tenders;
            list_tender = list_tender.Where(re => re.Data_tender.Equals(data));
            ViewBag.Tenders = list_tender;

            if (list_tender is null)
            {
                list_tender = db.Tenders;
                ViewBag.Tenders = list_tender;
            }

            int pageSize = 3;
            int page = 1;
            IEnumerable<Tender> tendersPerPages = list_tender.OrderBy(tender => tender.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list_tender.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Tenders = tendersPerPages };


            return View(ivm);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}