using System;
using System.Collections.Generic;
using System.IO;
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
        [Route("/cipher")]
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
                var returningFile = FileManager.Cipher(Environment.ContentRootPath,uploadedFilePath, method, key);
                return PhysicalFile(returningFile.Path, MediaTypeNames.Text.Plain, $"{Path.GetFileNameWithoutExtension(uploadedFilePath)}{returningFile.FileType}");
            }
            catch 
            {
                if (method.ToLower() == "ruta")
                {
                    return StatusCode(500, "La forma correcta de enviar una llave para el cifrado de ruta es MxN-V o MxN-E");
                }
                else if (method.ToLower() == "zizag")
                {
                    return StatusCode(500, "La forma correcta de enviar una llave para el cifrado de zizag es un número entero positivo.");
                }           
                else
                {
                    return StatusCode(500);
                }
            }
        }

        [Route("/api/decipher/{key}")]
        [HttpPost]
        public async Task<IActionResult> Decipher([FromForm] IFormFile file, string key)
        {
            try
            {
                var uploadedFilePath = await FileManager.SaveFileAsync(file, Environment.ContentRootPath);
                var returningFile = FileManager.Decipher(Environment.ContentRootPath, uploadedFilePath, key);
                return PhysicalFile(returningFile, MediaTypeNames.Text.Plain);
            }
            catch 
            {
                return StatusCode(500, "La forma correcta de enviar una llave para el descifrado de ruta es MxN-V o MxN-E");
            }
        }
    }
}
