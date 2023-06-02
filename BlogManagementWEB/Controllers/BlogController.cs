using AutoMapper;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Model.Dto;
using BlogManagementLibrary.Repository;
using BlogManagementLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlogManagementWEB.Controllers
{
    [ApiController]
    [Route("api/BlogController")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        APIResponse _apiResponse=new APIResponse();
        public BlogController(IBlogRepository userRepository, IMapper mapper)
        {
            _blogRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogs()
        {
            IEnumerable<Blog> blogList = await _blogRepository.GetAllAsync();
            _apiResponse.Result= blogList;
            return Ok(_apiResponse);
        }

        [HttpGet("{Id:int}", Name = "GetBlog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBlog(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }
            var blog = await _blogRepository.GetAsync(u => u.BlogId == Id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BlogDTO>> CreateBlog([FromBody] BlogDTO blogCreateDTO)
        {
           Blog model = _mapper.Map<Blog>(blogCreateDTO);

            await _blogRepository.CreateAsync(model);
            return Ok(model);
        }


        [HttpDelete("{id:int}", Name = "DeleteBlog")]
       // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var blog = await _blogRepository.GetAsync(u => u.BlogId == id);
            if (blog == null)
            {
                return NotFound();

            }
            await _blogRepository.RemoveAsync(blog);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogDTO blogUpdateDTO)
        {
            if (blogUpdateDTO == null || id != blogUpdateDTO.BlogId)
            {
                return BadRequest();
            }
            Blog model = _mapper.Map<Blog>(blogUpdateDTO);

            await _blogRepository.UpdateAsync(model);
            return NoContent();
        }
    }
}
