using System.Collections.Generic;
using AgencyPI.Models;
using AgencyPI.Models.Dto;
using AgencyPI.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IAgentRepository _agentRepo;
        private readonly IOrderRepository _orderRepo;

        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepo, IAgentRepository agentRepo, IOrderRepository orderRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _agentRepo = agentRepo;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetCustomers()
        {
            List<Customer> customers = _customerRepo.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{customerId:int}", Name = "GetCustomer")]
        public IActionResult GetCustomer(int customerId)
        {
            Customer customer = _customerRepo.GetCustomer(customerId);
            return Ok(customer);
        }

        [HttpGet("CustomersByAgent/{agentId:int}")]
        public IActionResult GetCustomersByAgent(int agentId)
        {
            List<Customer> customers = _customerRepo.GetCustomersByAgent(agentId);

            return Ok(customers);
        }

        [HttpPost]
        [HttpPut("{customerId:int}")]
        public IActionResult CreateUpdateCustomer(int? customerId, CustomerCreateUpdateDto customerCreateUpdateDto)
        {

            Customer customer = null;

            if (customerCreateUpdateDto == null)
            {
                return BadRequest();
            }

            if (customerId != null)
            {
                customer = _customerRepo.GetCustomer(customerId);

                if (customer == null)
                {
                    return StatusCode(404);
                }
            }
            else
            {
                customer = new Customer();
            }

            List<Order> newOrders = new List<Order>();

            if (customerCreateUpdateDto.Orders != null)
            {
                foreach (Order order in customerCreateUpdateDto.Orders)
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
                customer.Orders = newOrders;
            }

            if (customerCreateUpdateDto.Agent != null)
            {
                if (customerCreateUpdateDto.Agent.Id > 0)
                {
                    customer.Agent = _agentRepo.GetAgent(customerCreateUpdateDto.Agent.Id);
                }
                else
                {
                    customer.Agent = customerCreateUpdateDto.Agent;
                }
            }

            customer.Grade = customerCreateUpdateDto.Grade;
            customer.City = customerCreateUpdateDto.City;
            customer.Name = customerCreateUpdateDto.Name;
            customer.PhoneNumber = customerCreateUpdateDto.PhoneNumber;
            customer.WorkingArea = customerCreateUpdateDto.WorkingArea;

            if (customer.Id > 0 ? _customerRepo.UpdateCustomer(customer) : _customerRepo.CreateCustomer(customer))
            {
                return Ok(customer);
            }
            return StatusCode(500);
        }

        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomer(int customerId)
        {
            if (!_customerRepo.CustomerExists(customerId))
            {
                return NotFound();
            }

            Customer customer = _customerRepo.GetCustomer(customerId);

            if (!_customerRepo.DeleteCustomer(customer))
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}