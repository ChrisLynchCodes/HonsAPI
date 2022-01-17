using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.JWT;
using HonsBackendAPI.Models;
using HonsBackendAPI.ResourceParamaters;
using HonsBackendAPI.Services.Interfaces;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [APIKey]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerRepository _customersRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public CustomersController(CustomerRepository customersRepository, IMapper mapper, JwtSettings jwtSettings)
        {
            _customersRepository = customersRepository;
            _jwtSettings = jwtSettings;
            _mapper = mapper;
        }




        // GET: api/<CustomersController>
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            var customersFromRepository = await _customersRepository.GetAsync();

            //Without AutoMapper

            //var customers = new List<CustomerDto>();
            //foreach(var customer in customersFromService)
            //{
            //    customers.Add(new CustomerDto()
            //    {
            //        Id = customer.Id,
            //        Email = customer.Email,
            //        Password = customer.Password
            //    });
            //}


            //With AutoMapper
            //Map src object customersFromService to IEnumerable of CustomerDto's
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customersFromRepository));


        }


        // GET api/<CustomersController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CustomerDto>> Get(string id)
        {
            var customersFromRepository = await _customersRepository.GetAsync(id);

            if (customersFromRepository is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customersFromRepository));
        }





















        // POST api/<CustomersController>
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CustomerCreateDto newCustomer)
        {
            //Check pottential customer is not null
            if (newCustomer is null)
            {
                throw new ArgumentNullException(nameof(newCustomer));
            }
            var customer = await _customersRepository.CustomerExists(newCustomer.Email);

            if (customer is not null)
            {
                return Unauthorized("This email is already associated with an account");
            }

            //newCustomer.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),

            //map the createDTO to the model
            var customerToSave = _mapper.Map<Customer>(newCustomer);

            //Save the model in the database
            await _customersRepository.CreateAsync(customerToSave);

            //Map the model to output dto
            var customerToReturn = _mapper.Map<CustomerDto>(customerToSave);


            return CreatedAtAction(nameof(Get), new { id = customerToReturn.Id }, customerToReturn);
        }























        // POST api/<CustomersController>

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Post([FromBody] LoginDto loginDetails)
        {
            //Check customer logging in is not null
            if (loginDetails is null)
            {
                return NoContent();
            }

            //check customer exists in db by email
            var customer = await _customersRepository.CustomerExists(loginDetails.Email);

            //If no customer matches the email or the password does not match the returned customer return unauthorized
            if (customer is null || !customer.Password.Equals(loginDetails.Password))
            {
                return Unauthorized("Invalid Email or Password");
            }



            //JWT 
            try
            {
                //create token
                var Token = new UserToken();
                Token = JwtHelpers.GenTokenkey(new UserToken()
                {
                    Sub = customer.Id,
                    Email = customer.Email,
                    ConfirmPassword = customer.ConfirmPassword,
                    CreatedAt = customer.CreatedAt,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    UpdatedAt = customer.UpdatedAt
                   
                }, _jwtSettings);




              
               

                 return Token;

                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }


            
        }





































        // PUT api/<CustomersController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] Customer updatedCustomer)
        {
            var customerFromRepository = await _customersRepository.GetAsync(id);

            if (customerFromRepository is null)
            {
                return NotFound();
            }

            updatedCustomer.Id = customerFromRepository.Id;
            updatedCustomer.CreatedAt = customerFromRepository.CreatedAt;
            updatedCustomer.UpdatedAt = DateTime.Now;

            await _customersRepository.UpdateAsync(id, updatedCustomer);

            return NoContent();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customerFromRepository = await _customersRepository.GetAsync(id);

            if (customerFromRepository is null || customerFromRepository.Id is null)
            {
                return NotFound();
            }

            await _customersRepository.RemoveAsync(customerFromRepository.Id);

            return NoContent();
        }




    }
}
