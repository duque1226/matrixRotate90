using APIMatrixRotateSwagger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMatrixRotateSwagger.Interfaces
{
    public interface IMatrixOperation
    {
        /// <summary>
        /// Método para rotar la matriz ingresada
        /// </summary>
        /// <param name="textData"> Datos de cantidad de giros de 90° y la matriz en una cadena de texto
        /// </param>
        IQueryable RotateMatrix(InputData textData);

        /// <summary>
        /// Método para validar el formato de la matriz de entrada
        /// </summary>
        /// <param name="matrix">
        /// Cadena de texto con la matriz
        /// </param>
        /// <returns></returns>
        bool validateMatrix(List<string> rows);

        /// <summary>
        /// Método para crear el formato correcto para realizar la rotación de la matriz
        /// </summary>
        /// <param name="inputData">
        /// JSON que viene del cliente
        /// </param>
        /// <returns></returns>
        string createDataFormat(InputData inputData);
    }
}
