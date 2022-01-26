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
using System.Text;

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AdminsController : ControllerBase
    {

        //Dependencies
        private readonly JwtSettings _jwtSettings;

        private readonly IAdminRepository _adminsRepository;
        private readonly IMapper _mapper;
        public AdminsController(JwtSettings jwtSettings, IMapper mapper, AdminRepository adminsRepository)
        {
            this._jwtSettings = jwtSettings;
            this._adminsRepository = adminsRepository;
            _mapper = mapper;
        }

        // GET api/<AdminsController>/
        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
        public async Task<ActionResult<AdminDto>> Get()
        {
            var adminModels = await _adminsRepository.GetAllAsync();

            if (adminModels is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<AdminDto>>(adminModels));
        }


        // GET api/<AdminsController>/5
        [HttpGet("{adminId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult<AdminDto>> Get(string adminId)
        {
            var adminModel = await _adminsRepository.GetOneAsync(adminId);

            if (adminModel is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AdminDto>(adminModel));
        }


        // PUT api/<AdminsController>/5
        [HttpPut("{adminId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Update(string adminId, [FromBody] AdminCreateDto updatedAdmin)
        {
            var adminModel = await _adminsRepository.GetOneAsync(adminId);

            if (adminModel is null)
            {
                return NotFound();
            }
            var adminToSave = _mapper.Map<Admin>(updatedAdmin);

            adminToSave.Id = adminModel.Id;
            adminToSave.CreatedAt = adminModel.CreatedAt;
            adminToSave.UpdatedAt = DateTime.Now;
            if (adminToSave is null || adminToSave.Id is null)
            {
                return NotFound();
            }
            await _adminsRepository.UpdateAsync(adminToSave.Id, adminToSave);

            return NoContent();
        }

        // DELETE api/<CustomersController>/5
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
        [HttpDelete("{adminId:length(24)}")]
        public async Task<IActionResult> Delete(string adminId)
        {
            var adminModel = await _adminsRepository.GetOneAsync(adminId);

            if (adminModel is null || adminModel.Id is null)
            {
                return NotFound();
            }

            await _adminsRepository.RemoveAsync(adminModel.Id);

            return NoContent();
        }

        [HttpPost("login")]
        [APIKey]
        public async Task<ActionResult<UserToken>> Post([FromBody] LoginDto loginDetails)
        {
            //Check admin logging in is not null
            if (loginDetails is null)
            {
                return NoContent();
            }

            //check admin exists in db by email
            var adminModel = await _adminsRepository.GetByEmailAsync(loginDetails.Email);

            byte[] salt = new byte[128 / 8];
            
            salt = Encoding.ASCII.GetBytes(adminModel.PasswordSalt);

            //If no admin matches the email or the password does not match the returned admin return unauthorized
            if (adminModel is null || adminModel.PasswordHash.Equals(EncryptPassword.HashPassword(loginDetails.Password, salt)))
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
                    Id = adminModel.Id,
                    Email = adminModel.Email,
                    Role = adminModel.Role

                }, _jwtSettings);

                return Token;
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
        public async Task<IActionResult> Post([FromBody] AdminCreateDto newAdmin)
        {
            if (ModelState.IsValid)
            {
                //Check pottential admin is not null
                if (newAdmin is null || newAdmin.Email is null)
                {
                    return NotFound();
                }

                var adminModel = await _adminsRepository.GetByEmailAsync(newAdmin.Email);


                if (adminModel is not null)
                {
                    return Unauthorized("This email is already associated with an account");
                }



                //map the createDTO to the model
                var adminToSave = _mapper.Map<Admin>(newAdmin);

                //salt and hash newAdmin password and assign to the model salt as a string
                if(newAdmin.Password is not null)
                {
                    //Generate salt 
                    var salt = EncryptPassword.GenerateSalt(newAdmin.Password);
                    //then convert to string for saving in db
                    adminToSave.PasswordSalt = Convert.ToBase64String(salt);
                    //hash password&salt
                    adminToSave.PasswordHash = EncryptPassword.HashPassword(newAdmin.Password, salt);

                }




                //Save the model in the database
                await _adminsRepository.CreateAsync(adminToSave);

                //Map the model to output dto
                var adminToReturn = _mapper.Map<AdminDto>(adminToSave);


                return CreatedAtAction(nameof(Get), new { id = adminToReturn.Id }, adminToReturn);
            }

            return NotFound();
            
        }



    }
}
