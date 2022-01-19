using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _ordersRepository;
        private readonly IMapper _mapper;
        private readonly IOrderLineRepository _orderLinesRepository;
        private readonly ICustomerRepository _customersRepository;

        public OrdersController(OrderRepository ordersRepository, OrderLineRepository orderLinesRepository, IMapper mapper, CustomerRepository customerRepository)
        {
            _ordersRepository = ordersRepository;
            _orderLinesRepository = orderLinesRepository;
            _customersRepository = customerRepository;
            _mapper = mapper;

        }

        // GET: api/<OrdersController>
        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            var orders = await _ordersRepository.GetAllAsync();


            //Map src object ordersFromService to IEnumerable of OrderDto's
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));


        }

        // GET api/<OrdersController>/5
        [HttpGet("{orderId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin, Customer")]
        public async Task<ActionResult<OrderDto>> Get(string orderId)
        {
            var orders = await _ordersRepository.GetOneAsync(orderId);

            if (orders is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderDto>(orders));
        }

        // GET api/<OrdersController>/5  Get all orders for customer
        [HttpGet]
        [Route("[action]/{customerId}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin, Customer")]
        public async Task<ActionResult<OrderDto>> GetOrders(string customerId)
        {
            var orders = await _ordersRepository.GetOrdersForCustomerAsync(customerId);

            if (orders is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }


        // POST api/<OrdersController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin, Customer")]
        public async Task<IActionResult> Post([FromBody] OrderCreateDto newOrder)
        {
            if(newOrder is null || newOrder.CustomerId is null)
            {
                return NotFound();
            }
            var customer = await  _customersRepository.GetOneAsync(newOrder.CustomerId);
            if (customer is null)
            {
                return NotFound();
            }

            var orderModel = _mapper.Map<Order>(newOrder);
            //orderModel.CustomerId = newOrder.CustomerId;

            await _ordersRepository.CreateAsync(orderModel);

            var orderDto = _mapper.Map<OrderDto>(orderModel);

            return CreatedAtAction(nameof(Get), new { id = orderDto.Id }, orderDto);
        }



        // PUT api/<OrdersController>/5
        [HttpPut("{orderId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Update(string orderId, [FromBody] OrderCreateDto updatedOrder)
        {
            var orderModel = await _ordersRepository.GetOneAsync(orderId);

            if (orderModel is null)
            {
                return NotFound();
            }

            var orderToSave = _mapper.Map<Order>(updatedOrder);

            orderToSave.Id = orderModel.Id;
            orderToSave.UpdatedAt = DateTime.Now;
            orderToSave.CreatedAt = orderModel.CreatedAt;
            if (orderToSave.Id is null)
            {
                return NotFound();
            }


            await _ordersRepository.UpdateAsync(orderToSave.Id, orderToSave);

            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{orderId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(string orderId)
        {
            var orderModel = await _ordersRepository.GetOneAsync(orderId);

            if (orderModel is null || orderModel.Id is null)
            {
                return NotFound();
            }

            await _orderLinesRepository.RemoveManyAsync(orderModel.Id);

            await _ordersRepository.RemoveAsync(orderModel.Id);

            return NoContent();
        }


    }
}
