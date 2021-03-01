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
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IAgentRepository _agentRepo;
        private readonly IMapper _mapper;

        public OrderController(ICustomerRepository customerRepo, IAgentRepository agentRepo, IOrderRepository orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
            _agentRepo = agentRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            List<Order> orders = _orderRepo.GetOrders();
            return Ok(orders);
        }

        [HttpGet("{orderId:int}", Name = "GetOrder")]
        public IActionResult GetOrder(int orderId)
        {
            Order order = _orderRepo.GetOrder(orderId);
            return Ok(order);
        }

        [HttpGet("OrdersByCustomer/{customerId:int}")]
        public IActionResult GetOrdersByCustomer(int customerId)
        {
            List<Order> orders = _orderRepo.GetOrdersByCustomer(customerId);

            return Ok(orders);
        }

        [HttpPost]
        [HttpPut("{orderId:int}")]
        public IActionResult CreateUpdateOrder(int? orderId, OrderCreateUpdateDto orderCreateUpdateDto)
        {

            Order order = null;

            if (orderCreateUpdateDto == null)
            {
                return BadRequest();
            }

            if (orderId != null)
            {
                order = _orderRepo.GetOrder(orderId);

                if (order == null)
                {
                    return StatusCode(404);
                }
            }
            else
            {
                order = new Order();
            }

            if (orderCreateUpdateDto.Customer != null)
            {
                if (orderCreateUpdateDto.Customer.Id > 0)
                {
                    order.Customer = _customerRepo.GetCustomer(orderCreateUpdateDto.Customer.Id);
                }
                else
                {
                    order.Customer = orderCreateUpdateDto.Customer;
                }
            }

            order.Amount = orderCreateUpdateDto.Amount;
            order.Description = orderCreateUpdateDto.Description;
            order.Date = orderCreateUpdateDto.Date;

            if (order.Id > 0 ? _orderRepo.UpdateOrder(order) : _orderRepo.CreateOrder(order))
            {
                return Ok(order);
            }
            return StatusCode(500);
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            if (!_orderRepo.OrderExists(orderId))
            {
                return NotFound();
            }

            Order order = _orderRepo.GetOrder(orderId);

            if (!_orderRepo.DeleteOrder(order))
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}