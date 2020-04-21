using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIMatrixRotateSwagger.Entities;
using APIMatrixRotateSwagger.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIMatrixRotateSwagger.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MatrixRotateController : ControllerBase
    {
        private readonly IMatrixOperation _matrixOperation;

        public MatrixRotateController(IMatrixOperation matrixOperation)
        {
            _matrixOperation = matrixOperation;
        }

        /// <summary>
        /// Método para obtener un mensaje de bienvenida a la API   
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Matrix Rotate v1.0.0" };
        }


        /// <summary>
        /// Método para rotar una matriz 90° las veces que se especifique.
        /// La cantidad de giros de 90° se especifica en la propiedad N como un entero.
        /// La matrix se ingresa como un campo de texto teniendo en cuenta que para agregar
        /// una nueva columna se separa por ',' y para agregar una nueva fila por ';'.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /MatrixRotate
        ///     {
        ///        "N": 1,
        ///        "Matrix": "A,B;C,D;E,F"        
        ///     }
        ///
        /// </remarks>        
        [HttpPost]
        public IActionResult Post([FromBody] InputData inputData)
        {
            // Validando el modelo que viene del cliente
            if (!ModelState.IsValid)
            {
                return BadRequest("Formato JSON erróneo");
            }
            else
            {
                return Ok(_matrixOperation.RotateMatrix(inputData));
            }
        }
    }
}