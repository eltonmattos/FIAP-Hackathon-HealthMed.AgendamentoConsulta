﻿using HealthMed.API.AgendamentoConsulta.Models;
using HealthMed.API.AgendamentoConsulta.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.API.AgendamentoConsulta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController(ILogger<MedicoController> logger, MedicoRepository medicoRepository) : ControllerBase
    {
        private readonly ILogger<MedicoController> _logger = logger;
        private readonly MedicoRepository _medicoRepository = medicoRepository;

        //// GET: MedicoController
        /// <summary>
        /// Obter Medicos
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Paciente")]
        [HttpGet("/api/Medico")]
        public IActionResult GetMedicos([FromQuery]String? especialidade, [FromQuery] String? estado, [FromQuery] String? crm)
        {
            IEnumerable<object> medicos = _medicoRepository.GetMedicos(especialidade, estado, crm);
            return Ok(medicos);
        }

        //// GET: MedicoController
        /// <summary>
        /// Obter Medico por Id
        /// 
        /// </summary>
        /// <returns></returns>
        //[Authorize] 
        [HttpGet("/api/Medico/{idMedico}")]
        public IActionResult Get(string idMedico)
        {
            Medico? medico = _medicoRepository.Get(idMedico);
            return Ok(medico);
        }

        //// GET: MedicoController/Details/5
        //public ActionResult Details(int id)
        //{
        //    //return View();
        //}

        //// GET: MedicoController/Create
        //public ActionResult Create()
        //{
        //    //return View();
        //}

        // POST: api/<MedicoController>
        /// <summary>
        /// Cadastro de Médico
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("/api/Medico/")]
        public IActionResult Post([FromBody] Medico value)
        {
            Guid idMedico = _medicoRepository.Post(value);
            _logger.LogInformation("Médico cadastrado com sucesso: {IdMedico}", idMedico);
            try
            {
                return Ok(new
                {
                    Message = "Médico cadastrado com sucesso.",
                    Id = idMedico
                });
            }
            catch (FormatException ex)
            {
                return BadRequest(ex);
            }
        }

        //// POST: MedicoController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        //return View();
        //    }
        //}

        //// GET: MedicoController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    //return View();
        //}

        //// POST: MedicoController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        //return View();
        //    }
        //}

        //// GET: MedicoController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    //return View();
        //}

        //// POST: MedicoController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        //return View();
        //    }
        //}
    }
}
