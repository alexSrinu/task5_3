using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task5.Models;

namespace task5.Controllers
{
    public class DetailsController : Controller
    {
        private Repository _repository = new Repository();
        private List<Details> model;

        //  private IEnumerable<object> model;

        [HttpGet]
        public ActionResult Index()
        {
            // Initialize ViewBag.States and ViewBag.Cities
            ViewBag.States = new SelectList(_repository.GetStates(), "StateId", "StateName");
            ViewBag.Cities = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            //  ViewBag.Cities = new SelectList(_repository.GetCities(StateId), "Value", "Text");
            //  ViewBag.Cities = Details.StateId > 0 ? GetCities(candidate.StateId) : new List<City>();
            // Initialize model
            var model = new Details();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Details s)
        {
            if (ModelState.IsValid)
            {
                _repository.Register(s);
                return RedirectToAction("GetDetails");
            }

            // Re-populate ViewBag on failed validation
            ViewBag.States = new SelectList(_repository.GetStates(), "StateId", "StateName");
            ViewBag.Cities = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");

            return View(s);
        }

        [HttpGet]



        public JsonResult GetCitiesByStateId(int stateId)
        {
            if (stateId == 0)
            {
                return Json(new List<City>(), JsonRequestBehavior.AllowGet);
            }
            int Id;
            Id = Convert.ToInt32(stateId);

            //var states = from a in _repository.GetCities where a.CountryId == Id select a;
            //return Json(states);

            var cities = _repository.GetCities(stateId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDeatils1(string stateName)
        {

            var obj = TempData["Data"];
            return RedirectToAction("GetDetails", obj);
            return View();
        }
        public JsonResult GetCitiesByState(string stateName)
        {
           
            var cities = _repository.GetCitiesByState(stateName); 

            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDetails(string searchString, string stateName, string stname, int? PageNumber, int pageSize = 2)
        {
            Details obj = new Details();
            obj.pagesize = pageSize;
            int currentPage = PageNumber ?? 1;
            int totalCount;
            TempData["Page"] = currentPage;
            IEnumerable<Details> model;

            model = _repository.GetPagedData(obj.pagesize, currentPage, out totalCount).ToList();
            if (!string.IsNullOrEmpty(stateName))
            {

                model = _repository.GetDetails1(stateName).ToList();
                ViewBag.States = _repository.GetStates();
                TempData["Data"] = model;
                var cities = _repository.GetCitiesByState(stateName).ToList();
                return Json(cities, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("GetDetails", model);
                return View(model);
            }

            else
            {

                model = _repository.GetPagedData(obj.pagesize, currentPage, out totalCount).ToList();

            }
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(r => r.Name.Contains(searchString)).ToList();
            }

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
          
            ViewBag.TotalCount = totalCount;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = currentPage;
            ViewBag.States = _repository.GetStates();
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id, int? pageNumber)
        {
            var detailsList = _repository.GetDetails(id);
            var details = detailsList.FirstOrDefault(emp => emp.Id == id);

            if (details == null)
            {
                return HttpNotFound();
            }
            pageNumber = ViewBag.CurrentPage;

            ViewBag.States = new SelectList(_repository.GetStates(), "StateId", "StateName", details.StateId);
            ViewBag.Cities = new SelectList(_repository.GetCities(details.StateId), "CityId", "CityName", details.CityId);
            ViewBag.CurrentPage = pageNumber; 

            return View(details);
        }
        [HttpPost]
        public ActionResult Edit(int id, Details r, int? pageNumber)
        {
            if (ModelState.IsValid)
            {
                _repository.Edit(id, r);
                return RedirectToAction("GetDetails", new { Page = pageNumber }); 
            }

            ViewBag.States = new SelectList(_repository.GetStates(), "StateId", "StateName", r.StateId);
            ViewBag.Cities = new SelectList(_repository.GetCities(r.StateId), "CityId", "CityName", r.CityId);
            ViewBag.CurrentPage = pageNumber;

            return View(r);
        }
        /*  [HttpGet]
          public ActionResult Edit(int id)
          {

              var detailsList = _repository.GetDetails(id);
              var details = detailsList.FirstOrDefault(emp => emp.Id == id);
              var page= TempData["Data"];

              if (details == null)
              {
                  return HttpNotFound();
              }

              ViewBag.States = new SelectList(_repository.GetStates(), "StateId", "StateName", details.StateId);
              ViewBag.Cities = new SelectList(_repository.GetCities(details.StateId), "CityId", "CityName", details.CityId);

              return View(details);
          }

          [HttpPost]
          public ActionResult Edit(int id, Details r)
          {
              if (ModelState.IsValid)
              {
                  _repository.Edit(id, r);
                  return RedirectToAction("GetDetails");
              }

              ViewBag.States = new SelectList(_repository.GetStates(), "StateId", "StateName", r.StateId);
              ViewBag.Cities = new SelectList(_repository.GetCities(r.StateId), "CityId", "CityName", r.CityId);

              return View(r);
          }*/




        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _repository.Delete(id);
                return RedirectToAction("GetDetails");
            }
            return View();
        }



    }



}
