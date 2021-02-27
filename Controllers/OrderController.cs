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
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
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

        [HttpPost]
        public IActionResult CreateOrder(OrderCreateDto orderCreateDto)
        {
            if (orderCreateDto == null)
            {
                return BadRequest();
            }

            Order order = _mapper.Map<Order>(orderCreateDto);

            if (!_orderRepo.CreateOrder(order))
            {
                return StatusCode(500);
            }

            return CreatedAtRoute(nameof(GetOrder), new { orderId = order.Id }, order);
        }

        [HttpPatch("{orderId:int}")]
        public IActionResult UpdateOrder(int orderId, OrderDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest();
            }

            if (orderDto.Id != orderId)
            {
                return BadRequest();
            }

            Order order = _mapper.Map<Order>(orderDto);

            if (!_orderRepo.UpdateOrder(order))
            {
                return StatusCode(500);
            }

            return Ok(order);
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