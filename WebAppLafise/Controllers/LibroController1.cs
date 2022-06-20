using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using WebAppLafise.Models;

namespace WebAppLafise.Controllers
{
    public class LibroController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Libro> libros = null;
            IEnumerable<LibroDto> librosDto = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("MyLibro");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Libro>>();
                    readTask.Wait();

                    libros = readTask.Result;
                }
                else
                {
                    libros = Enumerable.Empty<Libro>();

                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error verifica que el web api esté ejecurandose y si la conección a base de datos es correcta, Att Jhordan.");
                }
            }

            IEnumerable<Autor> autores = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("Autor");
                responseTask.Wait();


                var responseTask1 = client.GetAsync("Autor");
                responseTask.Wait();

                var result1 = responseTask.Result;
                if (result1.IsSuccessStatusCode)
                {
                    var readTask1 = result1.Content.ReadAsAsync<IList<Autor>>();
                    readTask1.Wait();

                    autores = readTask1.Result;
                }
                //autores.Select(year => new SelectListItem
                //{
                //    Text = year.ToString(),
                //    Value = year.ToString()
                //});
                ViewBag.Autores = autores.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Name.ToString(),
                                      Value = x.Id.ToString()
                                  });
            }

            var librosDto1 = libros.Select(x =>
                                  new LibroDto()
                                  {
                                      Id = x.Id,
                                      Titulo = x.Titulo,
                                      Autor = autores.FirstOrDefault(n=>n.Id.Equals(x.IdAutor)).Name,
                                  });
            return View(librosDto1);

        }
        [HttpPost]
        public IActionResult Index(string Titulo)
        {
            IEnumerable<Libro> libros = null;
            IEnumerable<LibroDto> librosDto = null;

           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("MyLibro?Name="+Titulo);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Libro>>();
                    readTask.Wait();

                    libros = readTask.Result;
                }
                else
                {
                    libros = Enumerable.Empty<Libro>();

                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error verifica que el web api esté ejecurandose y si la conección a base de datos es correcta, Att Jhordan.");
                }

                
            }

            IEnumerable<Autor> autores = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("Autor");
                responseTask.Wait();


                var responseTask1 = client.GetAsync("Autor");
                responseTask.Wait();

                var result1 = responseTask.Result;
                if (result1.IsSuccessStatusCode)
                {
                    var readTask1 = result1.Content.ReadAsAsync<IList<Autor>>();
                    readTask1.Wait();

                    autores = readTask1.Result;
                }
                //autores.Select(year => new SelectListItem
                //{
                //    Text = year.ToString(),
                //    Value = year.ToString()
                //});
                ViewBag.Autores = autores.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Name.ToString(),
                                      Value = x.Id.ToString()
                                  });
            }

            var librosDto1 = libros.Select(x =>
                                  new LibroDto()
                                  {
                                      Id = x.Id,
                                      Titulo = x.Titulo,
                                      Autor = autores.FirstOrDefault(n => n.Id.Equals(x.IdAutor)).Name,
                                  });
            return View(librosDto1);

        }

        [HttpGet]
        public ActionResult Create()
        {
            IEnumerable<Autor> autores = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("Autor");
                responseTask.Wait();

                
                var responseTask1 = client.GetAsync("Autor");
                responseTask.Wait();

                var result1 = responseTask.Result;
                if (result1.IsSuccessStatusCode)
                {
                    var readTask1 = result1.Content.ReadAsAsync<IList<Autor>>();
                    readTask1.Wait();

                    autores = readTask1.Result;
                }
                //autores.Select(year => new SelectListItem
                //{
                //    Text = year.ToString(),
                //    Value = year.ToString()
                //});
                ViewBag.Autores = autores.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Name.ToString(),
                                      Value = x.Id.ToString()
                                  });
            }
            return View();
        }


        [HttpPost]
        public ActionResult Create(Libro libro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Libro>("MyLibro", libro);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(libro);
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<Libro> libros = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("MyLibro");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Libro>>();
                    readTask.Wait();

                    libros = readTask.Result;
                }
                else
                {
                    libros = Enumerable.Empty<Libro>();

                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error verifica que el web api esté ejecurandose y si la conección a base de datos es correcta, Att Jhordan.");
                }

                
                }

            IEnumerable<Autor> autores = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("Autor");
                responseTask.Wait();


                var responseTask1 = client.GetAsync("Autor");
                responseTask.Wait();

                var result1 = responseTask.Result;
                if (result1.IsSuccessStatusCode)
                {
                    var readTask1 = result1.Content.ReadAsAsync<IList<Autor>>();
                    readTask1.Wait();

                    autores = readTask1.Result;
                }
                //autores.Select(year => new SelectListItem
                //{
                //    Text = year.ToString(),
                //    Value = year.ToString()
                //});
                ViewBag.Autores = autores.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Name.ToString(),
                                      Value = x.Id.ToString()
                                  });
            }

            return View(libros.FirstOrDefault(p => p.Id.Equals(id)));
        }

        [HttpPost]
        public ActionResult Edit(Libro libro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Libro>("MyLibro", libro);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(libro);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            IEnumerable<Libro> libros = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync("MyLibro");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Libro>>();
                    readTask.Wait();

                    libros = readTask.Result;
                }
                else
                {
                    libros = Enumerable.Empty<Libro>();

                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error verifica que el web api esté ejecurandose y si la conección a base de datos es correcta, Att Jhordan.");
                }
            }
            return View(libros.FirstOrDefault(p => p.Id.Equals(id)));
        }


        [HttpPost]
        public ActionResult Delete(Libro autor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("MyLibro" + autor.Id.ToString());
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
