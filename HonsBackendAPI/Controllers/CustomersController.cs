using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.JWT;
using HonsBackendAPI.Models;
using HonsBackendAPI.ResourceParamaters;
using HonsBackendAPI.Services;
using HonsBackendAPI.Services.Interfaces;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerRepository _customersRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderRepository _ordersRepository;
        private readonly IReviewRepository _reviewsRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public CustomersController(CustomerRepository customersRepository, IMapper mapper, JwtSettings jwtSettings, 
            AddressRepository addressRepository, OrderRepository orderRepository, ReviewRepository reviewRepository)
        {
            _customersRepository = customersRepository;
            _addressRepository = addressRepository;
            _ordersRepository = orderRepository;
            _reviewsRepository = reviewRepository;
            _jwtSettings = jwtSettings;
            _mapper = mapper;
        }




        // GET: api/<CustomersController>

        [HttpGet]
 
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            var customerModels = await _customersRepository.GetAllAsync();

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
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customerModels));


        }


        // GET api/<CustomersController>/5
        [HttpGet("{customerId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin, Customer")]
        public async Task<ActionResult<CustomerDto>> Get(string customerId)
        {
            var customersFromRepository = await _customersRepository.GetOneAsync(customerId);

            if (customersFromRepository is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customersFromRepository));
        }











        // PUT api/<CustomersController>/5
        [HttpPut("{customerId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin, Customer")]
        public async Task<IActionResult> Update(string customerId, [FromBody] Customer updatedCustomer)
        {
            var customerModel = await _customersRepository.GetOneAsync(customerId);

            if (customerModel is null || customerModel.Id is null)
            {
                return NotFound();
            }

            updatedCustomer.Id = customerModel.Id;
            updatedCustomer.CreatedAt = customerModel.CreatedAt;
            updatedCustomer.UpdatedAt = DateTime.Now;

            await _customersRepository.UpdateAsync(updatedCustomer.Id, updatedCustomer);

            return NoContent();
        }

        // DELETE api/<CustomersController>/5
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin, Customer")]
        [HttpDelete("{customerId:length(24)}")]
        public async Task<IActionResult> Delete(string customerId)
        {
            var customerModel = await _customersRepository.GetOneAsync(customerId);


            if (customerModel is null || customerModel.Id is null)
            {
                return NotFound();
            }
         //Remove a customers orders, addresses, and reviews when their account is deleted
            await  _ordersRepository.RemoveManyAsync(customerModel.Id);
            await _addressRepository.RemoveManyAsync(customerModel.Id);
            await _reviewsRepository.RemoveManyAsync(customerModel.Id);

            await _customersRepository.RemoveAsync(customerModel.Id);

            return NoContent();
        }



        [HttpPost("login")]
        [APIKey]
        public async Task<IActionResult> Login(LoginDto loginDetails)
        {

            //Check customer logging in is not null
            if (loginDetails is null)
            {
                return NoContent();
            }

            //check customer exists in db by email
            var customerModel = await _customersRepository.GetByEmailAsync(loginDetails.Email);

            //If no customer matches the email or the password does not match the returned customer return unauthorized
            byte[] salt = new byte[128 / 8];

            salt = Encoding.ASCII.GetBytes(customerModel.PasswordSalt);

            //If no customer matches the email or the password does not match the returned admin return unauthorized
            if (customerModel is null || customerModel.PasswordHash.Equals(EncryptPassword.HashPassword(loginDetails.Password, salt)))
            {
                return Unauthorized("Invalid Email or Password");
            }


            try
            {
                var Token = new UserToken();

                Token = JwtHelpers.GenTokenkey(new UserToken()
                {
                    Id = customerModel.Id,
                    Email = customerModel.Email,
                    Role = customerModel.Role,

                }, _jwtSettings);

                return Ok(Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }






        // POST api/<CustomersController>
        [HttpPost("register")]
        [APIKey]
        public async Task<IActionResult> Post([FromBody] CustomerCreateDto newCustomer)
        {
            if (ModelState.IsValid)
            {
                //Check pottential newCustomer is not null
                if (newCustomer is null || newCustomer.Email is null)
                {
                    return NotFound();
                }

                var customerModel = await _customersRepository.GetByEmailAsync(newCustomer.Email);


                if (customerModel is not null)
                {
                    return Unauthorized("This email is already associated with an account");
                }



                //map the createDTO to the model
                var customerToSave = _mapper.Map<Customer>(newCustomer);

                //salt and hash newCustomer password and assign to the model salt as a string
                if (newCustomer.Password is not null)
                {
                    //Generate salt 
                    var salt = EncryptPassword.GenerateSalt(newCustomer.Password);
                    //then convert to string for saving in db
                    customerToSave.PasswordSalt = Convert.ToBase64String(salt);
                    //hash password&salt
                    customerToSave.PasswordHash = EncryptPassword.HashPassword(newCustomer.Password, salt);

                }




                //Save the model in the database
                await _customersRepository.CreateAsync(customerToSave);

                //Map the model to output dto
                var customerToReturn = _mapper.Map<CustomerDto>(customerToSave);


                return CreatedAtAction(nameof(Get), new { id = customerToReturn.Id }, customerToReturn);
            }

            return NotFound();

        }





    }
}
