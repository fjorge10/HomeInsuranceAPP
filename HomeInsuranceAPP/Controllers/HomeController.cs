using HomeInsuranceAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeInsuranceAPP.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;

        string apiURL = "http://localhost/api/Users";

        public HomeController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiURL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
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

        //
        // POST: /Account/Login
        [HttpPost]
       // [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            //http://localhost:56091/api/Users/Login?username=jorge&password=qwerty

            HttpResponseMessage response = await httpClient.GetAsync(apiURL+"/Login?username="+ model.UserName+"&password="+model.Password);

            if (!response.IsSuccessStatusCode)
            {
                return View("Error Getting Data");
            }

            string data = response.Content.ReadAsStringAsync().Result;

            User user = JsonConvert.DeserializeObject<User>(data);

            Session["User"] = (User)TempData["User"];

            return RedirectToAction("Index", "User", user);
        }
    }
}