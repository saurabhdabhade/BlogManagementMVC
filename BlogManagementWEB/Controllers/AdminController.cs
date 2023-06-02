using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BlogManagementLibrary.Model.Dto;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Repository.IRepository;
using BlogManagementLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Azure;
using System.Net;

namespace AdminManagementWEB.Controllers
{
    [ApiController]
    [Route("api/AdminController")]

    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiResponse;
        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDTO>>> GetAdmins()
        {
            IEnumerable<Admin> blogList = await _adminRepository.GetAllAsync();
            return Ok(_mapper.Map<List<AdminDTO>>(blogList));
        }

        [HttpGet("{Id:int}", Name = "GetAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAdmin(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }
            var admin = await _adminRepository.GetAsync(u => u.AdminId == Id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AdminDTO>> CreateAdmin([FromBody] AdminDTO blogCreateDTO)
        {
            Admin model = _mapper.Map<Admin>(blogCreateDTO);

            await _adminRepository.CreateAsync(model);
            return Ok(model);
        }


        [HttpDelete("{id:int}", Name = "DeleteAdmin")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var blog = await _adminRepository.GetAsync(id);
            if (blog == null)
            {
                return NotFound();

            }
            await _adminRepository.RemoveAsync(blog);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] AdminDTO adminUpdateDTO)
        {
            if (adminUpdateDTO == null || id != adminUpdateDTO.AdminId)
            {
                return BadRequest();
            }
            Admin model = _mapper.Map<Admin>(adminUpdateDTO);

            await _adminRepository.UpdateAsync(model);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {

            APIResponse _apiResponse = new APIResponse();
            var loginResponse = await _adminRepository.Login(model);
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Username or Password incorrect");
                return BadRequest(_apiResponse);
            }
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.Result = loginResponse;
            return Ok(_apiResponse);
        }


    }
}
