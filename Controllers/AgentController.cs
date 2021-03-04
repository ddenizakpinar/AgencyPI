using System.Collections.Generic;
using AgencyPI.Models;
using AgencyPI.Models.Dto;
using AgencyPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace AgencyPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepository _agentRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;

        public AgentController(IAgentRepository agentRepo, IOrderRepository orderRepo, ICustomerRepository customerRepo, IMapper mapper)
        {
            _agentRepo = agentRepo;
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAgents()
        {
            List<Agent> agents = _agentRepo.GetAgents();
            List<AgentDto> agentsDto = new List<AgentDto>();
            foreach (var agent in agents)
            {
                agentsDto.Add(_mapper.Map<AgentDto>(agent));
            }
            return Ok(agentsDto);
        }

        [HttpGet("{agentId:int}", Name = "GetAgent")]
        public IActionResult GetAgent(int agentId)
        {
            Agent agent = _agentRepo.GetAgent(agentId);
            AgentDto agentDto = _mapper.Map<AgentDto>(agent);
            return Ok(agentDto);
        }

        [HttpGet("AgentsByOrder/{orderId:int}")]
        public IActionResult GetAgentsByOrder(int orderId)
        {
            List<Agent> agents = _agentRepo.GetAgentsByOrder(orderId);

            List<AgentDto> agentsDto = new List<AgentDto>();
            foreach (var agent in agents)
            {
                agentsDto.Add(_mapper.Map<AgentDto>(agent));
            }
            return Ok(agentsDto);
        }

        [HttpPost]
        [HttpPut("{agentId:int}")]
        public IActionResult CreateUpdateAgent(int? agentId, AgentCreateUpdateDto agentCreateUpdateDto)
        {

            Agent agent = null;

            if (agentCreateUpdateDto == null)
            {
                return BadRequest();
            }

            if (agentId != null)
            {
                agent = _agentRepo.GetAgent(agentId);
                if (agent == null)
                {
                    return StatusCode(404);
                }
            }
            else
            {
                agent = new Agent();
            }

            List<Order> newOrders = new List<Order>();

            if (agentCreateUpdateDto.Orders != null)
            {
                foreach (Order order in agentCreateUpdateDto.Orders)
                {
                    if (order.Id > 0)
                    {
                        Order existingOrder = _orderRepo.GetOrder(order.Id);
                        newOrders.Add(existingOrder);
                    }
                    else
                    {
                        newOrders.Add(order);
                    }
                }
                agent.Orders = newOrders;
            }

            List<Customer> newCustomers = new List<Customer>();

            if (agentCreateUpdateDto.Customers != null)
            {
                foreach (Customer customer in agentCreateUpdateDto.Customers)
                {
                    if (customer.Id > 0)
                    {
                        Customer existingCustomer = _customerRepo.GetCustomer(customer.Id);
                        newCustomers.Add(existingCustomer);
                    }
                    else
                    {
                        newCustomers.Add(customer);
                    }
                }
                agent.Customers = newCustomers;
            }

            agent.Name = agentCreateUpdateDto.Name;
            agent.Commission = agentCreateUpdateDto.Commission;
            agent.Country = agentCreateUpdateDto.Country;
            agent.PhoneNumber = agentCreateUpdateDto.PhoneNumber;

            if (agent.Id > 0 ? _agentRepo.UpdateAgent(agent) : _agentRepo.CreateAgent(agent))
            {
                return Ok(agent);
            }
            return StatusCode(500);
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