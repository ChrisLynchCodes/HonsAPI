using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [APIKey]
    public class OrderLinesController : ControllerBase
    {
        private readonly IOrderLineRepository _orderLinesRepository;
        private readonly IOrderRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrderLinesController(OrderLineRepository orderLinesRepository, OrderRepository ordersRepository, IMapper mapper)
        {
            _orderLinesRepository = orderLinesRepository;
            _mapper = mapper;
            _ordersRepository = ordersRepository;
        }



        [HttpGet("{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderLineDto>>> Get(string orderId)
        {
            var order = await _ordersRepository.GetAsync(orderId);

            if (order is null || order.Id is null)
            {
                return NotFound();
            }

            var orderLines = await _orderLinesRepository.GetAllAsync(order.Id);

            if (orderLines is null)
            {
                return NotFound();
            }



            return Ok(_mapper.Map<IEnumerable<OrderLineDto>>(orderLines));


        }





        // GET api/<OrderLinesController>/  //Get specific orderline on an order
        [HttpGet("{orderId}/{orderLineId}")]
        public async Task<ActionResult<OrderLineDto>> Get(string orderId, string orderLineId)
        {
            var orderFromRepository = await _ordersRepository.GetAsync(orderId);
            if (orderFromRepository is null || orderFromRepository.Id is null)
            {
                return NotFound();
            }

            var orderLineFromRepository = await _orderLinesRepository.GetOneAsync(orderId, orderLineId);
            if (orderLineFromRepository is null || orderLineFromRepository.Id is null)
            {
                return NotFound();
            }



            return Ok(_mapper.Map<OrderLineDto>(orderLineFromRepository));
        }


        // POST api/<OrderLinesController>
        [HttpPost("{orderId}")]
        public async Task<IActionResult> Post(string orderId, [FromBody] OrderLineCreateDto newOrderLine)
        {
            //Ensure orderId is a valid order
            var order = await _ordersRepository.GetAsync(orderId);
            if (order is null || order.Id is null)
            {
                return NotFound();
            }



            //Map createDTO to model
            var orderLineModel = _mapper.Map<OrderLine>(newOrderLine);


            //add the valid order id to the orderlines  order id field
            orderLineModel.OrderId = order.Id;

            //Save model in db
            await _orderLinesRepository.CreateAsync(orderLineModel);

            //Map model to output DTO
            var orderLineDto = _mapper.Map<OrderLineDto>(orderLineModel);

            //Ensure valid

            if (orderLineDto is null || orderLineDto.Id is null)
            {
                return NotFound();
            }



            return CreatedAtAction(nameof(Get), new { orderId = order.Id, orderLineId = orderLineDto.Id }, orderLineDto);



        }


        // PUT api/<OrderLinesController>/5
        [HttpPut("{orderId}/{orderLineId}")]
        public async Task<IActionResult> Update(string orderId, string orderLineId, [FromBody] OrderLineCreateDto updatedOrderLine)
        {
            var orderLine = await _orderLinesRepository.GetOneAsync(orderId, orderLineId);

            if (orderLine is null)
            {
                return NotFound();
            }

            var orderLineToSave = _mapper.Map<OrderLine>(updatedOrderLine);
        
            orderLineToSave.Id = orderLine.Id;
            orderLineToSave.ProductId = orderLine.ProductId;
            orderLineToSave.OrderId = orderLine.OrderId;
            orderLineToSave.UpdatedAt = DateTime.Now;
            orderLineToSave.CreatedAt = orderLine.CreatedAt;
            if (orderLineToSave.Id is null)
            {
                return NotFound();
            }


            await _orderLinesRepository.UpdateAsync(orderLineToSave.Id, orderLineToSave);

            return NoContent();
        }

        // DELETE api/<OrderLinesController>/5
        [HttpDelete("{orderId}/{orderLineId}")]
        public async Task<IActionResult> Delete(string orderId, string orderLineId)
        {
            var order = await _ordersRepository.GetAsync(orderId);

            if (order is null || order.Id is null)
            {
                return NotFound();
            }
            var orderLine = await _orderLinesRepository.GetOneAsync(order.Id, orderLineId);

            if (orderLine is null || orderLine.Id is null)
            {
                return NotFound();
            }



            await _orderLinesRepository.RemoveAsync(orderLine.Id);

            return NoContent();
        }
    }
}
