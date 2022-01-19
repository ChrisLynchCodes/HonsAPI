using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HonsBackendAPI.Controllers
{
    [Route("api/customers/{customerId}/addresses")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _addressesRepository;
        private readonly ICustomerRepository _customersRepository;
        private readonly IMapper _mapper;

        public AddressesController(AddressRepository addressesRepository, CustomerRepository customersRepository, IMapper mapper)
        {
            _addressesRepository = addressesRepository;
            _customersRepository = customersRepository;
            _mapper = mapper;
        }




        // GET: api/<AddressesController>
        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<AddressDto>>> Get(string customerId)
        {
            var customer = await _customersRepository.GetOneAsync(customerId);

            if (customer is null || customer.Id is null)
            {
                return NotFound();
            }

            var addresses = await _addressesRepository.GetAllAsync(customer.Id);

            if (addresses is null)
            {
                return NotFound();
            }



            return Ok(_mapper.Map<IEnumerable<AddressDto>>(addresses));


        }



        // GET api/<AddressesController>/5
        [HttpGet("{addressId:length(24)}")]
        public async Task<ActionResult<AddressDto>> Get(string customerId, string addressId)
        {
            //Ensure customer is valid
            var customer = await _customersRepository.GetOneAsync(customerId);

            if (customer is null || customer.Id is null)
            {
                return NotFound();
            }

            //Get address from db
            var address = await _addressesRepository.GetOneAsync(addressId);

            if (address is null || address.Id is null)
            {
                return NotFound();
            }


            //Map model to output DTO & return
            return Ok(_mapper.Map<AddressDto>(address));


        }





        // POST api/<AddressesController>
        [HttpPost]
        public async Task<IActionResult> Post(string customerId, [FromBody] AddressCreateDto newAddress)
        {
            //Ensure customerId is a valid customer
            var customer = await _customersRepository.GetOneAsync(customerId);
            if (customer is null || customer.Id is null)
            {
                return NotFound();
            }



            //Map createDTO to model
            var addressModel = _mapper.Map<Address>(newAddress);


            //add the valid customer id to the addresses  customer id
            addressModel.CustomerId = customer.Id;

            //Save model in db
            await _addressesRepository.CreateAsync(addressModel);

            //Map model to output DTO
            var addressDto = _mapper.Map<AddressDto>(addressModel);

            //Ensure valid

            if (addressDto is null || addressDto.Id is null)
            {
                return NotFound();
            }



            return CreatedAtAction(nameof(Get), new { customerId = customer.Id, addressId = addressDto.Id }, addressDto);



        }


        // PUT api/<AddressesController>/5
        [HttpPut("{addressId:length(24)}")]
        public async Task<IActionResult> Update(string addressId, [FromBody] AddressCreateDto updatedAddress)
        {
            var address = await _addressesRepository.GetOneAsync(addressId);

            if (address is null)
            {
                return NotFound();
            }

            var addressToSave = _mapper.Map<Address>(updatedAddress);
            //AddressCreateDto does not contain id or customerId so isn't mapped by automapper
            addressToSave.Id = address.Id;
            addressToSave.CustomerId = address.CustomerId;
            addressToSave.UpdatedAt = DateTime.Now;
            addressToSave.CreatedAt = address.CreatedAt;
            if(addressToSave.Id is null)
            {
                return NotFound();
            }
            

            await _addressesRepository.UpdateAsync(addressToSave.Id, addressToSave);

            return NoContent();
        }

        // DELETE api/<AddressesController>/5
        [HttpDelete("{addressId:length(24)}")]
        public async Task<IActionResult> Delete(string customerId, string addressId)
        {
            var customer = await _customersRepository.GetOneAsync(customerId);

            if (customer is null || customer.Id is null)
            {
                return NotFound();
            }
            var address = await _addressesRepository.GetOneAsync(addressId);

            if (address is null || address.Id is null)
            {
                return NotFound();
            }

            

            await _addressesRepository.RemoveAsync(address.Id);

            return NoContent();
        }
    }
}
