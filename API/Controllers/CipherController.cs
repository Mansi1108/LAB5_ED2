using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CipherController : ControllerBase
    {
        private IWebHostEnvironment Environment;

        public CipherController(IWebHostEnvironment env)
        {
            Environment = env;
        }

        // GET: api/<CipherController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("/api/cipher/{method}/{key}")]
        [HttpPost]
        public async Task<IActionResult> Cipher([FromForm] IFormFile file, string method, string key)
        {
            try
            {
                var uploadedFilePath = await FileManager.SaveFileAsync(file, Environment.ContentRootPath);
                if (!KeyHolder.CheckKeyValidness(method, key))
                {
                    return StatusCode(500, "La llave ingresada es incorrecta");
                }
                var returningFile = FileManager.Cipher(uploadedFilePath, method, key);
                return PhysicalFile(returningFile.Path, MediaTypeNames.Text.Plain);
            }
            catch 
            {
                if (method.ToLower() == "ruta")
                {
                    return StatusCode(500, "La forma correcta de enviar una llave para el cifrado de ruta es MxN-V o MxN-E");
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }

        [Route("api/decipher/{key}")]
        [HttpPost]
        public async Task<IActionResult> Decipher([FromForm] IFormFile file, string key)
        {
            try
            {
                var uploadedFilePath = await FileManager.SaveFileAsync(file, Environment.ContentRootPath);
                if (!KeyHolder.CheckKeyFromFileType(uploadedFilePath, key))
                {
                    return StatusCode(500, "La llave ingresada es incorrecta");
                }
                var returningFile = FileManager.Decipher(uploadedFilePath, key);
                return PhysicalFile(returningFile, MediaTypeNames.Text.Plain);
            }
            catch 
            {
                return StatusCode(500, "La forma correcta de enviar una llave para el cifrado de ruta es MxN-V o MxN-E");
            }
        }
    }
}
