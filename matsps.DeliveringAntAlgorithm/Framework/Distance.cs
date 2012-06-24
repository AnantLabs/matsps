using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matsps.DeliveringAntAlgorithm
{
    public class Distance
    {
        #region Поля
        /// <summary>
        /// Матрица расстояний
        /// </summary>
        private Double[,] _matrix;
        private Double[,] _specialMatrix;
        #endregion Поля

        #region Свойства
        /// <summary>
        /// Возвращает или задает коллекцию координат точек
        /// </summary>
        public List<PointD> Points
        {
            set;
            get;
        }
        /// <summary>
        /// Возвращает или задает точку старта
        /// </summary>
        public PointD StartPoint
        {
            set;
            get;
        }
        #endregion Свойства

        #region Методы
        public Double GetDistance(int ind1,int ind2)
        {
            if (ind1 < 0 || ind2 < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
             return _matrix[ind1,ind2];
        }
        public Double GetDistance(SpecialPoints specialPoint, int ind)
        {
            if (ind < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _specialMatrix[0,ind];
        }
        public Double GetDistance(int ind,SpecialPoints specialPoint)
        {
            if (ind < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _specialMatrix[1, ind];
        }

        public void CalcDistanceLine(IList<Client> clients, PointD startPoint)
        {
            Points = clients.Select( c => c.Position).ToList();
            StartPoint = startPoint;

            int count = clients.Count;
            double[,] dist = new double[count, count];
            for (int from = 0; from < count; from++)
                for (int to = 0; to < count; to++)
                {
                    if (to != from && dist[from, to] == 0.0)
                    {
                        dist[from, to] = DistanceTwoPointLine(clients[from].Position, clients[to].Position);
                        dist[to, from] = dist[from, to];
                    }
                }
            _matrix = dist;

            if (startPoint != null)
            {
                // расстояния до/от старта
                _specialMatrix = new Double[2, count];
                for (int i = 0; i < count; i++)
                {
                    _specialMatrix[0, i] = DistanceTwoPointLine(startPoint, clients[i].Position);
                    _specialMatrix[1, i] = _specialMatrix[0, i];
                }
            }
        }

        public void CalcDiatanceRoad(List<Client> clients, PointD startPoint)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Расстояние между двумя точками по прямой 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private double DistanceTwoPointLine(PointD from, PointD to)
        {
            double xd = Math.Abs(from.X - to.X);
            double yd = Math.Abs(from.Y - to.Y);

            return Math.Sqrt(xd * xd + yd * yd);
        }


        /// <summary>
        /// Получить объединенную матрицу с расстоянием до старта
        /// </summary>
        /// <returns></returns>
        public Double[,] GetMergeMatrix()
        {
            if (StartPoint == null)
                return GetMatrix();

            int mergeMatrixLength = _matrix.GetLineLength() + 1;

            Double[,] mergeMatrix = new Double[mergeMatrixLength, mergeMatrixLength];

            for (int i = 0; i < mergeMatrixLength - 1; i++)
            {
                mergeMatrix[i + 1, 0] = _specialMatrix[0, i]; // расстояния от Старта до клиента
                mergeMatrix[0, i + 1] = _specialMatrix[i, 0];
            }

            for (int i = 0; i < mergeMatrixLength - 1; i++)
            {
                for (int j = 0; j < mergeMatrixLength - 1; j++)
                {
                    mergeMatrix[i + 1, j  +1] = _matrix[i, j];
                }
            }

            return mergeMatrix;
        }

        public Double[,] GetMatrix()
        {
            return _matrix;
        }

        public Distance FullClone()
        {
            Distance clone = new Distance();
            clone.StartPoint = this.StartPoint;
            foreach (PointD point in this.Points)
            {
                clone.Points.Add(point);
            }
            return clone;
        }

        #endregion

        public Distance()
        { 
            
        }
    }
}
