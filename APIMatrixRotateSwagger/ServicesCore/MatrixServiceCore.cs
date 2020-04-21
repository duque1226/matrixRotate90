using APIMatrixRotateSwagger.Entities;
using APIMatrixRotateSwagger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMatrixRotateSwagger.ServicesCore
{
    public class MatrixServiceCore : IMatrixOperation
    {
        public IQueryable RotateMatrix(InputData textData)
        {
            List<string> arrStrRows = new List<string>();
            List<string> arrStrCols = new List<string>();
            List<string> res = new List<string>();
            // Conversión del JSON a una cadena de texto
            string inputMat = createDataFormat(textData);
            // Extrayendo Las filas del texto de entrada
            arrStrRows = inputMat.Split("\r\n").ToList();
            arrStrRows.RemoveAt(arrStrRows.Count - 1);
            // Almacenando la cantidad de giros que va a realizar la matriz del texto de entrada
            int varN = Convert.ToInt32(arrStrRows[0]);
            // Eliminando el primer valor del texto de entrada
            arrStrRows.RemoveAt(0);


            if (validateMatrix(arrStrRows))
            {
                // Calculando el angulo de giro            
                double angulo = Math.PI * (varN * 90) / 180.0;
                // Almacenando el coseno y seno del angulo de giro
                int angleSin = Convert.ToInt16((Math.Sin(angulo)));
                int angleCos = Convert.ToInt16((Math.Cos(angulo)));
                // Almacenando la cantidad de columnas de la matriz
                int colssQty = arrStrRows[0].Split(',').Length;
                // Inicializando el arreglo que almacena las columnas
                for (int i = 0; i < colssQty; i++)
                {
                    arrStrCols.Add("");
                }
                // Recorriendo el arreglo de filas para almacenar las columnas
                arrStrRows.ForEach((x) =>
                {
                    for (int i = 0; i < colssQty; i++)
                    {
                        arrStrCols[i] += x.Split(",")[i] + " ";
                    }
                });

                arrStrCols.ForEach(x =>
                   x = x.Remove(0, x.Length - 1)
                );

                // Evaluando que tipo de giro se debe realizar
                switch (angleSin)
                {
                    // Giro de 90°
                    case 1:
                        arrStrCols.ForEach(x =>
                            res.Add(Reverse(x))
                        );
                        break;
                    case 0:
                        // Giro de 180°
                        if (angleCos == -1)
                        {
                            arrStrRows.Reverse();
                            arrStrRows.ForEach(x =>
                                res.Add(Reverse(x))
                            );
                        }
                        // Giro de 0°
                        else
                        {
                            arrStrRows.ForEach(x =>
                               res.Add(x)
                            );
                        }
                        break;
                    // Giro de 270°
                    case -1:
                        arrStrCols.Reverse();
                        arrStrCols.ForEach(x =>
                           res.Add(x)
                        );
                        break;
                }
                return (res.AsQueryable());
            }
            else
            {
                return "La matriz no tiene el formato adecuado".AsQueryable();
            }
        }

        public bool validateMatrix(List<string> rows)
        {
            // Contando la cantidad de columnas de las filas
            List<int> rowsQty = new List<int>();
            rows.ForEach(x =>
                rowsQty.Add(x.Split(',').Length)
            );

            // Almacenando las celdas de la matriz
            List<string[]> dataRow = new List<string[]>();
            rows.ForEach(x =>
                dataRow.Add(x.Split(","))
            );
            // Contando la cantidad de columnas
            int rowQty = rows[0].Split(',').Length;
            // Verificando si todas las filas tienen las mismas columnas
            bool isEqual = rowsQty.All(x => x == rowQty);
            bool isEmpty = false;

            // Verificando si hay algun campo nulo en las filas
            dataRow.ForEach(x =>
               x.ToList().ForEach(e =>
               {
                   if (String.IsNullOrEmpty(e))
                   {
                       isEmpty = true;
                   }
               }
            ));
            return isEqual && !isEmpty;
        }

        public string createDataFormat(InputData inputData)
        {
            List<string> outString = new List<string>();
            outString = inputData.Matrix.Split(';').ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(inputData.N.ToString());
            outString.ForEach(x =>
                sb.AppendLine(x)
            );
            return sb.ToString();
        }

        // Método para invertir la posicion de la cadena de texto
        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
