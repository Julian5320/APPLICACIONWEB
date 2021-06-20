using APPLICACIONWEB.APIConnections;
using APPLICACIONWEB.Models;
using Microsoft.AspNetCore.Mvc;
using MODELS.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APPLICACIONWEB.Controllers
{
    public class MecanicoController : Controller
    {
        public async Task<IActionResult> IndexAsync(string Mensaje)
        {
            MecanicoModel mecanicoModel = new MecanicoModel();
            mecanicoModel.mecanicos= await ObtenerMecanicos();
            ViewData["Mensaje"] = Mensaje;

            return View(mecanicoModel);
        }
        public async Task<List<Mecanico>> ObtenerMecanicos()
        {
            List<Mecanico> AllMecanicos = new List<Mecanico>();

            ApiControl service = new ApiControl();

            string jsonServices = "";

            HttpResponseMessage response = await service.GetRequest(ServicesURL.CargarMecanicos);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                jsonServices = await response.Content.ReadAsStringAsync();
            }
            AllMecanicos = JsonConvert.DeserializeObject<List<Mecanico>>(jsonServices);

            return AllMecanicos;
        }
        public async Task<IActionResult> BorrarMecanico(int id)
        {

            ApiControl service = new ApiControl();

            string jsonServices = "";

            HttpResponseMessage response = await service.PostRequest(new StringContent(""), ServicesURL.EliminarMecanico +"?mec="+id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                jsonServices = await response.Content.ReadAsStringAsync();
            }
            jsonServices = JsonConvert.DeserializeObject<string>(jsonServices);

            if (jsonServices == "Error")
            {
                return RedirectToAction("Index", new { Mensaje = "Error al Borrar usuario" });
            }
            else
            {
                return RedirectToAction("Index", new { Mensaje = "Usuario Borrado" });
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> Crear(Mecanico mecanico)
        {

            ApiControl service = new ApiControl();

            string jsonServices = "";

            HttpResponseMessage response = await service.PostRequest(new StringContent(JsonConvert.SerializeObject(mecanico), Encoding.UTF8, "application/json"), ServicesURL.CrearMecanico);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                jsonServices = await response.Content.ReadAsStringAsync();
                jsonServices = JsonConvert.DeserializeObject<string>(jsonServices);
            }
            if(jsonServices == "Error")
            {
                return RedirectToAction("Index", new { Mensaje = "Error al crear usuario" });
            }
            else
            {
                return RedirectToAction("Index", new { Mensaje = "Usuario Creado" });
            }

        }

        [HttpGet]

        public async Task<IActionResult> Crear()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Editar (int id)
        {
            MecanicoModel Mecanico = new MecanicoModel();

            ApiControl service = new ApiControl();

            string jsonServices = "";

            HttpResponseMessage response = await service.GetRequest(ServicesURL.CargarMecanico + "?id=" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                jsonServices = await response.Content.ReadAsStringAsync();
            }
            Mecanico.mecanico = JsonConvert.DeserializeObject<Mecanico>(jsonServices);

            return View(Mecanico);

        }
        [HttpPost]
        public async Task<IActionResult> Editar(Mecanico mecanico)
        {
            ApiControl service = new ApiControl();

            string jsonServices = "";

            HttpResponseMessage response = await service.PostRequest(new StringContent(JsonConvert.SerializeObject(mecanico), Encoding.UTF8, "application/json"), ServicesURL.EditarMecanico);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                jsonServices = await response.Content.ReadAsStringAsync();
                jsonServices = JsonConvert.DeserializeObject<string>(jsonServices);
            }
            if (jsonServices == "Error")
            {
                return RedirectToAction("Index", new { Mensaje = "Error al editar usuario" });
            }
            else
            {
                return RedirectToAction("Index", new { Mensaje = "Usuario editado" });
            }

        }
    }
}
