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
            var orderModel = await _ordersRepository.GetOneAsync(orderId);

            if (orderModel is null || orderModel.Id is null)
            {
                return NotFound();
            }

            var orderLinesModels = await _orderLinesRepository.GetAllAsync(orderModel.Id);

            if (orderLinesModels is null)
            {
                return NotFound();
            }



            return Ok(_mapper.Map<IEnumerable<OrderLineDto>>(orderLinesModels));


        }





        // GET api/<OrderLinesController>/  //Get specific orderline on an order
        [HttpGet("{orderId}/{orderLineId}")]
        public async Task<ActionResult<OrderLineDto>> Get(string orderId, string orderLineId)
        {
            var orderModel = await _ordersRepository.GetOneAsync(orderId);
            if (orderModel is null || orderModel.Id is null)
            {
                return NotFound();
            }

            var orderLineModel = await _orderLinesRepository.GetOneAsync(orderId, orderLineId);
            if (orderLineModel is null || orderLineModel.Id is null)
            {
                return NotFound();
            }



            return Ok(_mapper.Map<OrderLineDto>(orderLineModel));
        }


        // POST api/<OrderLinesController>
        [HttpPost("{orderId}")]
        public async Task<IActionResult> Post(string orderId, [FromBody] OrderLineCreateDto newOrderLine)
        {
            //Ensure orderId is a valid order
            var order = await _ordersRepository.GetOneAsync(orderId);
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
            var orderLineModel = await _orderLinesRepository.GetOneAsync(orderId, orderLineId);

            if (orderLineModel is null)
            {
                return NotFound();
            }

            var orderLineToSave = _mapper.Map<OrderLine>(updatedOrderLine);
        
            orderLineToSave.Id = orderLineModel.Id;
            orderLineToSave.ProductId = orderLineModel.ProductId;
            orderLineToSave.OrderId = orderLineModel.OrderId;
            orderLineToSave.UpdatedAt = DateTime.Now;
            orderLineToSave.CreatedAt = orderLineModel.CreatedAt;

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
            var orderModel = await _ordersRepository.GetOneAsync(orderId);

            if (orderModel is null || orderModel.Id is null)
            {
                return NotFound();
            }
            var orderLineModel = await _orderLinesRepository.GetOneAsync(orderModel.Id, orderLineId);

            if (orderLineModel is null || orderLineModel.Id is null)
            {
                return NotFound();
            }



            await _orderLinesRepository.RemoveAsync(orderLineModel.Id);

            return NoContent();
        }
    }
}
