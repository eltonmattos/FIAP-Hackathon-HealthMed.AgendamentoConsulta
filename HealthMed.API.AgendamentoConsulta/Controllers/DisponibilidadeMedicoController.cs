﻿using HealthMed.API.AgendamentoConsulta.Models;
using HealthMed.API.AgendamentoConsulta.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HealthMed.API.AgendamentoConsulta.Controllers
{
    [Route("api/Medico/[controller]")]
    [ApiController]
    public class DisponibilidadeMedicoController(ILogger<DisponibilidadeMedicoController> logger, DisponibilidadeMedicoRepository disponibilidadeMedicoRepository) : ControllerBase
    {
        private readonly ILogger<DisponibilidadeMedicoController> _logger = logger;
        private readonly DisponibilidadeMedicoRepository _disponibilidadeMedicoRepository = disponibilidadeMedicoRepository;

        // GET: DisponibilidadeMedicoController
        /// <summary>
        /// Obter Horários de Disponibilidade do Médico
        /// </summary>
        /// <param name="idMedico"></param>
        /// <returns></returns>
        [Authorize(Roles = "Paciente")]
        [HttpGet("/api/DisponibilidadeMedico/{idMedico}")]
        public IActionResult Get(String idMedico)
        {
            IEnumerable<DisponibilidadeMedico> agendamentos = _disponibilidadeMedicoRepository.Get(idMedico);
            return Ok(agendamentos);
        }

        // POST: DisponibilidadeMedicoController
        /// <summary>
        /// Cadastrar Horários de Disponibilidade do Médico
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize(Roles = "Medico")]
        [HttpPost("/api/DisponibilidadeMedico/")]
        public IActionResult Post([FromBody] IEnumerable<DisponibilidadeMedico> value)
        {
            IEnumerable<object> idsDisponibilidadeMedico = _disponibilidadeMedicoRepository.Post(value);
            return Ok(idsDisponibilidadeMedico);
        }


        // POST: DisponibilidadeMedicoController
        /// <summary>
        /// Cadastrar Horários de Disponibilidade do Médico
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="idMedico"></param>
        /// <returns></returns>
        [Authorize(Roles = "Medico")]
        [HttpPost("/api/DisponibilidadeMedico/{idMedico}")]
        public IActionResult Post(String idMedico, [FromBody][BindRequired] String prompt)
        {
            String idsDisponibilidadeMedico = _disponibilidadeMedicoRepository.AIPost(prompt, idMedico);
            return Ok(idsDisponibilidadeMedico);
        }

        // PUT: DisponibilidadeMedicoController
        /// <summary>
        /// Editar Horários de Disponibilidade do Médico
        /// </summary>
        /// <param name="idMedico"></param>
        /// <param name="idDisponibilidadeMedico"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize(Roles = "Medico")]
        [HttpPut("/api/DisponibilidadeMedico/{idMedico}/{idDisponibilidadeMedico}")]
        public IActionResult Put(String idMedico, String idDisponibilidadeMedico, [FromBody] DisponibilidadeMedico value)
        {
            _disponibilidadeMedicoRepository.Put(idMedico, idDisponibilidadeMedico, value);
            _logger.LogInformation("Período de Disponibilidade cadastrado com sucesso.");
            return Ok(new
            {
                Message = "Período de Disponibilidade cadastrado com sucesso.",
                Id = idDisponibilidadeMedico
            });
        }
    }
}
