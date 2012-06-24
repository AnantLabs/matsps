using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Специальные точки
    /// </summary>
    public enum SpecialPoints 
    {
       /// <summary>
       /// Точка старта маршрута
       /// </summary>
        Start =-1,
        /// <summary>
        /// Точка конца маршрута
        /// </summary>
        Finish = -2
    };

    public static class DoubleExtension
    {
        /// <summary>
        /// Получить линейный размер (количество строк или столбцов для квадратной матрицы)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetLineLength(this double[,] value)
        {
            return (int)Math.Sqrt(value.Length);
        }
    }

    public class PointD
    {
        public PointD()
        {

        }
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X
        {
            set;
            get;
        }

        public double Y
        {
            set;
            get;
        }
    }
}
