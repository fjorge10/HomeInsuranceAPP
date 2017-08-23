using HomeInsuranceAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HomeInsuranceAPP.Extensions;

namespace HomeInsuranceAPP.Controllers
{
    public class UserController : Controller
    {
        //string apiURL = "http://localhost:56091/";

        string apiURL = "http://localhost/";

        // GET: User
        public async Task<ActionResult> Index(User model)
        {
            User user = new Models.User();

            user = (User)Session["User"];

            var tuple = new Tuple<User, List<User>>(model, new List<Models.User>());

            var httpClient = HttpClientHelper.HttpClientInstance(apiURL);

            HttpResponseMessage response = await httpClient.GetAsync(apiURL + "/api/Users");

            if (!response.IsSuccessStatusCode)
                return View(tuple);

            string data = response.Content.ReadAsStringAsync().Result;

            List<Models.User> userList = JsonConvert.DeserializeObject<List<Models.User>>(data);

            tuple = new Tuple<User, List<User>>(model, userList);

            return View(tuple);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            User user = new Models.User();

            ViewData["userName"] = "";
            ViewData["password"] = "";

            return View(user);
        }

        // POST: User/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(apiURL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(model);

                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                HttpResponseMessage request = await httpClient.PostAsync("api/Users/create", content);

                if (request.IsSuccessStatusCode)
                {
                    string data = await request.Content.ReadAsStringAsync();

                    User user = JsonConvert.DeserializeObject<User>(data);

                    ViewData["userName"] = user.UserName;
                    ViewData["password"] = user.Password;
                    ViewBag.cenas = "cenas";

                    return RedirectToAction("Index", model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                var httpClient = HttpClientHelper.HttpClientInstance(apiURL);

                HttpResponseMessage response = await httpClient.GetAsync(apiURL + "api/Users/delete?id=" + id);

                if (!response.IsSuccessStatusCode)
                    return View();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //List Policies
        [HttpGet]
        public ActionResult PolicyList(int id)
        {
            List<PolicyModel> model = PolicyModel.List(id);

            var models = new Tuple<List<PolicyModel>, PolicyModel>(model,new PolicyModel() { ClientNumber = id});

            Session["ClientId"] = id; 

            return View(models);
        }

        public ActionResult PolicyCreate()
        {
            PolicyModel model = new PolicyModel();
            model.ClientNumber = Convert.ToInt32(Session["ClientId"]);

            return View(model);
        }

        [HttpPost]
        public ActionResult PolicyCreate(PolicyModel model)
        {
            int id = Convert.ToInt32(Session["ClientId"]);

            try
            {
                if (!ModelState.IsValid)
                    return View("Error");

                model.ClientNumber = id;

                model.Create();

                return RedirectToAction("PolicyList", new { id = id });
            }
            catch(Exception ex)
            {
                return RedirectToAction("PolicyList", new { id = id });
            }
        }
    }
}
