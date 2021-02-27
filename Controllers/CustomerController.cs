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
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
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

        [HttpPost]
        public IActionResult CreateCustomer(CustomerCreateDto customerCreateDto)
        {
            if (customerCreateDto == null)
            {
                return BadRequest();
            }

            Customer customer = _mapper.Map<Customer>(customerCreateDto);

            if (!_customerRepo.CreateCustomer(customer))
            {
                return StatusCode(500);
            }

            return CreatedAtRoute(nameof(GetCustomer), new { customerId = customer.Id }, customer);
        }

        [HttpPatch("{customerId:int}")]
        public IActionResult UpdateCustomer(int customerId, CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest();
            }

            if (customerDto.Id != customerId)
            {
                return BadRequest();
            }

            Customer customerFromDb = _customerRepo.GetCustomer(customerId);

            Customer customer = _mapper.Map<Customer>(customerDto);
            customer.CreatedAt = customerFromDb.CreatedAt;

            if (!_customerRepo.UpdateCustomer(customer))
            {
                return StatusCode(500);
            }

            return Ok(customer);
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