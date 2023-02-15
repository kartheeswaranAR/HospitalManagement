using HospitalManagement.Domain.Context;
using HospitalManagement.Domain.Models;
using HospitalManagement.Domain.Models.Authentication;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AuthenticationController : ControllerBase
    {
        
        
        public static Patient patientmodel = new Patient();
        public static Doctor doctorModel= new Doctor();

        private readonly DBContext _DbContext;
        private  IConfiguration _configuration;

        public AuthenticationController(DBContext DbContext,IConfiguration configuration)
        {
            _DbContext = DbContext;
            _configuration = configuration;
        }

        [HttpPost("Authentication")]
        public async Task<ActionResult<Patient>> GenerateToken(string Email ,string Password )
        {
            if (patientmodel != null && Email != null && Password != null )
            {
                var value = await GetUserInfo(Email,Password);
                var jwtdata = _configuration.GetSection("Jwt").Get<Authentication>();
                if (value.Email ==Email & value.Password == Password)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,jwtdata.Subject),
                        new Claim(JwtRegisteredClaimNames.Iss,jwtdata.Issuer),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("Id",patientmodel.Id.ToString()),
                        new Claim("Email",Email),
                        new Claim("Password",Password)
                    };
                    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtdata.Key));
                    var signIn = new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwtdata.Issuer,
                        jwtdata.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signIn);
                 return Ok(new JwtSecurityTokenHandler().WriteToken(token));   
                }
                else
                {
                    return BadRequest("--->Incorrect Credentials");
                }

            }
            else
            {
                return BadRequest("--->Incorrect Credentials");
            }
        }

        [HttpGet("PatientLoginInfo")]
        public async Task<Patient> GetUserInfo(string Email,string Password)
        {
            return await _DbContext.PatientModel.FirstOrDefaultAsync(x=>x.Email==Email && x.Password==Password);
        }

        
        [HttpGet("DoctorLogininfo")]
        public async Task<Doctor> GetUserInfo1(string Email, string Password)
        {
            return await _DbContext.DoctorModel.FirstOrDefaultAsync(x => x.Email == Email && x.Password == Password);
        }
    }
}
