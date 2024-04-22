using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Dashboard_Api.Models;
using Student_Dashboard_Api.Services;
using System.Net.Mime;

namespace Student_Dashboard_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(StudentService studentService, FileService fileService) : ControllerBase
    {
        private readonly StudentService _studentService = studentService;
        private readonly FileService _fileService = fileService;

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] int Id)
        {
            try
            {
                return Ok(await _studentService.Get(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("photo/{photoName}")]
        public IActionResult GetPhoto([FromRoute] string photoName)
        {
            var filePath =  _fileService.GetFilePath(photoName);

            if (filePath == null || !System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            string contentType = GetContentType(photoName);

            return PhysicalFile(filePath, contentType);
        }

        private static string GetContentType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => MediaTypeNames.Image.Jpeg,
                ".pdf" => MediaTypeNames.Application.Pdf,
                _ => MediaTypeNames.Application.Octet,
            };
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _studentService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] StudentCreateDto studentCreateDto)
        {
            try
            {
                return Ok(await _studentService.Add(studentCreateDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] StudentUpdateDto studentUpdateDto)
        {
            try
            {
                return Ok(await _studentService.Update(studentUpdateDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            try
            {
                return Ok(await _studentService.Remove(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
