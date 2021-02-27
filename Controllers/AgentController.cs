using System.Collections.Generic;
using AgencyPI.Models;
using AgencyPI.Models.Dto;
using AgencyPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepository _agentRepo;
        private readonly IMapper _mapper;

        public AgentController(IAgentRepository agentRepo, IMapper mapper)
        {
            _agentRepo = agentRepo;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAgents()
        {
            List<Agent> agents = _agentRepo.GetAgents();
            return Ok(agents);
        }

        [HttpGet("{agentId:int}", Name = "GetAgent")]
        public IActionResult GetAgent(int agentId)
        {
            Agent agent = _agentRepo.GetAgent(agentId);
            return Ok(agent);
        }

        [HttpPost]
        public IActionResult CreateAgent(AgentCreateDto agentCreateDto)
        {
            if (agentCreateDto == null)
            {
                return BadRequest();
            }

            Agent agent = _mapper.Map<Agent>(agentCreateDto);

            if (!_agentRepo.CreateAgent(agent))
            {
                return StatusCode(500);
            }

            return CreatedAtRoute(nameof(GetAgent), new { agentId = agent.Id }, agent);
        }

        [HttpPatch("{agentId:int}")]
        public IActionResult UpdateAgent(int agentId, AgentDto agentDto)
        {
            if (agentDto == null)
            {
                return BadRequest();
            }

            if (agentDto.Id != agentId)
            {
                return BadRequest();
            }

            Agent agent = _mapper.Map<Agent>(agentDto);

            if (!_agentRepo.UpdateAgent(agent))
            {
                return StatusCode(500);
            }

            return Ok(agent);
        }

        [HttpDelete("{agentId}")]
        public IActionResult DeleteAgent(int agentId)
        {
            if (!_agentRepo.AgentExists(agentId))
            {
                return NotFound();
            }

            Agent agent = _agentRepo.GetAgent(agentId);

            if (!_agentRepo.DeleteAgent(agent))
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}