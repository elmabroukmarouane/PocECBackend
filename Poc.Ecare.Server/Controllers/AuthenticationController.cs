using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Poc.Ecare.Infrastructure.Models.Classes;
using Poc.Ecare.Server.DtoModel.Models;
using Pro.Ecare.Business.Services.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Poc.Ecare.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        #region ATTRIBUTES
        private readonly IAuthenticationService _authenticationService;
        private readonly IRedisService _redisService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        #endregion

        #region CONTRUCTOR
        public AuthenticationController(
            IAuthenticationService authenticationService,
            IRedisService redisService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _redisService = redisService;
            _mapper = mapper;
            _configuration = configuration;
        }
        #endregion

        #region ENDPOINTS
        [HttpPost]
        public async Task<IActionResult> Authenticate(UserLogin _user)
        {
            try
            {
                var user = await _authenticationService.Authenticate(_user);
                if (user == null)
                {
                    return StatusCode(401, new
                    {
                        Message = "Email ou mot de passe incorrect !"
                    });
                }
                var token = _authenticationService.CreateToken(
                    user,
                    _configuration.GetSection("Jwt").GetSection("Key").Value ?? "",
                    _configuration.GetSection("Jwt").GetSection("Issuer").Value ?? "",
                    _configuration.GetSection("Jwt").GetSection("Audience").Value ?? "");
                var userViewModel = _mapper.Map<UserViewModel>(user);
                var guidSession = "Client-" + Guid.NewGuid().ToString();
                userViewModel.Token = token;
                userViewModel.UserSessionId = guidSession;
                var userString = JsonConvert.SerializeObject(userViewModel);
                var statusSet = await _redisService.Set(guidSession, userString);
                if(statusSet)
                {
                    return Ok(new
                    {
                        Message = "Hi " + user.FirstName + " " + user.LastName + " !",
                        UserSessionId = guidSession
                    });
                }
                else
                {
                    return StatusCode(401, new
                    {
                        Message = "Session non créée !"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Authentification échouée !",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout(int id, string userSessionId)
        {
            try
            {
                var logout = await _authenticationService.Logout(id);
                var clearSession = await _redisService.Delete(userSessionId);
                if (!logout || !clearSession)
                {
                    return StatusCode(400, new
                    {
                        Message = "Quelque chose s'est produit lors de la tentative de déconnexion !"
                    });
                }
                return Ok(new
                {
                    Message = "à bientôt, j'espère :) !"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Echec de la déconnexion !",
                    Error = ex.Message
                });
            }
        }
        #endregion
    }
}