using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.JWT;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HonsBackendAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
  
    public class AccountsController : ControllerBase
    {
        ////Dependencies
        //private readonly JwtSettings _jwtSettings;
        //private readonly ICustomerRepository _customersRepository;
        //private readonly IAdminRepository _adminsRepository;
        //private readonly IMapper _mapper;
        //public AccountsController(JwtSettings jwtSettings, CustomerRepository customerRepository, IMapper mapper, AdminRepository adminsRepository)
        //{
        //    this._jwtSettings = jwtSettings;
        //    this._customersRepository = customerRepository;
        //    this._adminsRepository = adminsRepository;
        //    _mapper = mapper;
        //}




        ////// POST api/<CustomersController>

        ////[HttpPost("login")]

        ////public async Task<ActionResult<UserToken>> Post([FromBody] LoginDto loginDetails)
        ////{
        ////    //Check customer logging in is not null
        ////    if (loginDetails is null)
        ////    {
        ////        return NoContent();
        ////    }

        ////    //check customer exists in db by email
        ////    var customer = await _customersRepository.CustomerExists(loginDetails.Email);

        ////    //If no customer matches the email or the password does not match the returned customer return unauthorized
        ////    if (customer is null || !customer.Password.Equals(loginDetails.Password))
        ////    {
        ////        return Unauthorized("Invalid Email or Password");
        ////    }


        ////}

      


        /////// <summary>
        /////// Get List of UserAccounts
        /////// </summary>
        /////// <returns>List Of UserAccounts</returns>
        ////[HttpGet]
        ////[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        ////public async Task<IActionResult> GetList()
        ////{
        ////    var customers = await _customerRepository.GetAsync();
        ////    return Ok(customers);
        ////}
    }
}
