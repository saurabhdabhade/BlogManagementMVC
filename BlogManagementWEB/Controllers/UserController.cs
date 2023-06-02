using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogManagementLibrary.Model.Dto;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Repository.IRepository;
using BlogManagementLibrary.Repository;

namespace UserManagementWEB.Controllers
{
    [ApiController]
    [Route("api/UserController")]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            IEnumerable<User> blogList = await _userRepository.GetAllAsync();
            return Ok(_mapper.Map<List<UserDTO>>(blogList));
        }

        [HttpGet("{Id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }
            var user = await _userRepository.GetAsync(u => u.UserId == Id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserDTO blogCreateDTO)
        {
            User model = _mapper.Map<User>(blogCreateDTO);

            await _userRepository.CreateAsync(model);
            return Ok(model);
        }


        [HttpDelete("{id:int}", Name = "DeleteUser")]
        //[Authorize(Roles = "user")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var blog = await _userRepository.GetAsync(u => u.UserId == id);
            if (blog == null)
            {
                return NotFound();

            }
            await _userRepository.RemoveAsync(blog);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO blogUpdateDTO)
        {
            if (blogUpdateDTO == null || id != blogUpdateDTO.UserId)
            {
                return BadRequest();
            }
            User model = _mapper.Map<User>(blogUpdateDTO);

            await _userRepository.UpdateAsync(model);
            return NoContent();
        }
    }
}
