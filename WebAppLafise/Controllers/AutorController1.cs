using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using WebAppLafise.Models;

namespace WebAppLafise.Controllers
{
    public class AutorController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Autor> autors = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("Autor");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Autor>>();
                    readTask.Wait();

                    autors = readTask.Result;
                }
                else  
                {
                    autors = Enumerable.Empty<Autor>();

                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error verifica que el web api esté ejecurandose y si la conección a base de datos es correcta, Att Jhordan.");
                }
            }
            return View(autors);
            
        }

        [HttpPost]
        public IActionResult Index(string Titulo)
        {
            IEnumerable<Autor> autors = null;
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("Autor?Name=" + Titulo);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Autor>>();
                    readTask.Wait();

                    autors = readTask.Result;
                }
                else
                {
                    autors = Enumerable.Empty<Autor>();

                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error verifica que el web api esté ejecurandose y si la conección a base de datos es correcta, Att Jhordan.");
                }

            }

            return View(autors);

        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
            }

            
        [HttpPost]
        public ActionResult Create(Autor autor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Autor>("Autor", autor);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(autor);
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<Autor> autors = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("Autor");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Autor>>();
                    readTask.Wait();

                    autors = readTask.Result;
                }
                else
                {
                    autors = Enumerable.Empty<Autor>();

                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error verifica que el web api esté ejecurandose y si la conección a base de datos es correcta, Att Jhordan.");
                }
            }
            return View(autors.FirstOrDefault(p => p.Id.Equals(id)));
        }

        [HttpPost]
        public ActionResult Edit(Autor autor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Autor>("Autor", autor);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(autor);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            IEnumerable<Autor> autors = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("Autor");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Autor>>();
                    readTask.Wait();

                    autors = readTask.Result;
                }
                else
                {
                    autors = Enumerable.Empty<Autor>();

                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error verifica que el web api esté ejecurandose y si la conección a base de datos es correcta, Att Jhordan.");
                }
            }
            return View(autors.FirstOrDefault(p => p.Id.Equals(id)));
        }


        [HttpPost]
        public ActionResult Delete(Autor autor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Autor/" + autor.Id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }


}
