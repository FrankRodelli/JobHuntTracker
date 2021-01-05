using JobHuntApi.Models;
using JobHuntApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobHuntApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;

        public UserController(IUserRepository productRepository)
        {
            _UserRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GellAll()
        {
            var users = await _UserRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var user = await _UserRepository.GetById(id);
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(User entity)
        {
            await _UserRepository.AddUser(entity);
            return Ok(entity);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(User entity, string id)
        {
            await _UserRepository.UpdateUser(entity, id);
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _UserRepository.RemoveUser(id);
            return Ok();
        }
    }
}
