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
        //private readonly ICustomerRepository _customerRepository;
        //public AccountsController(JwtSettings jwtSettings, CustomerRepository customerRepository)
        //{
        //    this._jwtSettings = jwtSettings;
        //    this._customerRepository = customerRepository;
        //}


   



        //[HttpPost]
        //public async Task<IActionResult> Login(LoginDto userLogin)
        //{
        //    try
        //    {
        //        var Token = new UserToken();
        //        var customers = await _customerRepository.GetAsync();

        //        var Valid = customers.Any(x => x.Email.Equals(userLogin.Email, StringComparison.OrdinalIgnoreCase));
        //        if (Valid)
        //        {
        //            var customer = customers.FirstOrDefault(x => x.Email.Equals(userLogin.Email, StringComparison.OrdinalIgnoreCase));
                   
                 
                    
        //            if (customer is not null)
        //            {
        //                Token = JwtHelpers.GenTokenkey(new UserToken()
        //                {
        //                    Sub = customer.Id,
        //                    Email = customer.Email,

                          
                           


        //                }, _jwtSettings);
        //            }
             
        //        }
        //        else
        //        {
        //            return BadRequest($"wrong password");
        //        }
        //        return Ok(Token);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}


        ///// <summary>
        ///// Get List of UserAccounts
        ///// </summary>
        ///// <returns>List Of UserAccounts</returns>
        //[HttpGet]
        //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> GetList()
        //{
        //    var customers = await _customerRepository.GetAsync();
        //    return Ok(customers);
        //}
    }
}
