using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyApp.Controllers
{
    public class TravelController : Controller
    {
        // GET: Travel
        public ActionResult Index()
        {
            
            List<PlacesModel> places = new List<PlacesModel>();
            places = SqlPojo.getAllPlaces("ASC", "id");
            return View(places);
        }
        public ActionResult UpdateThePlace(PlacesModel places)
        {
            if(!ModelState.IsValid)
            {
                return View("Update");
            }
            else{

            SqlPojo.updatePlace(places.id, places.name, places.isPrime, places.description, places.image_url, places.price, places.days);
            return RedirectToAction("getTheDashboard");

            }
        }

        public ActionResult getTheUpdatePage(int id)
        {
            PlacesModel places = SqlPojo.getSelectedPlace(id);
            return View("Update", places);
        }

        public ActionResult DeleteThePlace(int id)
        {
            SqlPojo.deletePlace(id);
            return RedirectToAction("getTheDashboard");
        }
        public ActionResult getTheDashboard()
        {
            List<PlacesModel> places = new List<PlacesModel>();
            places = SqlPojo.getAllPlaces("ASC", "id");
            return View("Dashboard", places);
        }
        public ActionResult InsertTheShow(PlacesModel place)
        {
            //System.Diagnostics.Debug.WriteLine($"{m.name} {m.year} {m.image_url}");
            if (!ModelState.IsValid)
            {
                return View("Insert");
            }
            else
            {
                SqlPojo.insertAPlace(place.name,place.description,place.isPrime,place.image_url,place.price,place.days);
                return RedirectToAction("getTheDashboard");
            }
        }

        public ActionResult getInsert()
        {
            return View("Insert");
        }

        public ActionResult getTheSort(int id)
        {
            List<PlacesModel> places = new List<PlacesModel>();
            if (id == 1)
            {
                places = SqlPojo.getAllPlaces("ASC", "name");
            }
            else if (id == 2)
            {
                places = SqlPojo.getAllPlaces("DESC", "name");
            }
            else if (id == 3)
            {
                places = SqlPojo.getAllPlaces("ASC", "id");

            }
            else
            {
                places = SqlPojo.getAllPlaces("DESC", "id");

            }
            return View("Dashboard", places);
        }

        public ActionResult AddtoWishlist(int id)
        {
            SqlPojo.insertPlaceinWishlist(id);
            return RedirectToAction("getTheWishlist");
        }
        public ActionResult RemoveFromWishlist(int id)
        {
            SqlPojo.deleteFromWishlist(id);
            return RedirectToAction("getTheWishlist");
        }
        public ActionResult getTheWishlist()
        {
            List<PlacesModel> places = new List<PlacesModel>();
            places = SqlPojo.getAllWishlist();

            return View("Wishlist", places);
        }
    }

}