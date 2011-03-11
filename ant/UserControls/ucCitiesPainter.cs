using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

using ant.AntAlgData;
using ant.AntAlgLogic;
using ant.CommonData;

namespace ant.UserControls
{
    /// <summary>
    /// Пользовательский контрол, который прорисовывает Коллекцию городов и путь между ними (по порядку следования в коллекции)
    /// </summary>
    public partial class ucCitiesPainter : UserControl
    {
        #region Конструкторы и Данные
        public ucCitiesPainter()                            
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Состояние режима прорисовки
        /// </summary>
        private enum DrawingState                                   
        {
            /// <summary>
            /// Прорисовка точек городов
            /// </summary>
            Cities, 
            /// <summary>
            /// Прорисовка линий (дуг) маршрута
            /// </summary>
            Route,
            /// <summary>
            /// Прорисовка городов и дуг
            /// </summary>
            CitiesAndRoute
        }
        private DrawingState stateCurrent = DrawingState.Cities;

        private Pen penCities = new Pen(Brushes.Black, 5);
        private Pen penRouteLine = new Pen(Brushes.Purple, 3);
        private Pen penLiteLine = new Pen(Brushes.Silver, 1);
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция городов
        /// </summary>
        private DataCitiesCollection Cities           
        {
            set;
            get;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Прорисовывает коллецию городов
        /// </summary>
        public void PaintCities()                                               
        {
            stateCurrent = DrawingState.Cities;
            PaintObjects();
        }

        /// <summary>
        /// Прорисовывает маршрут между городами
        /// </summary>
        public void PaintRoute()                                                
        {
            stateCurrent = DrawingState.Route;            
            PaintObjects();
        }

        /// <summary>
        /// Прорисовка городов и маршрута между ними
        /// </summary>
        public void PaintCitiesAndRoute()                                       
        {
            stateCurrent = DrawingState.CitiesAndRoute;
            PaintObjects();
        }

        /// <summary>
        /// Перебирает коллекцию городов и прорисовывает объекты по состоянию
        /// </summary>
        /// <param name="state">Режим прорисовки: точки или линии</param>
        /// <param name="pen">Кисть</param>
        private void PaintObjects()                                             
        {
            int picBoxWidth = pbCanvas.Size.Width;
            float fKoefX = (float)picBoxWidth / (float)Cities.MaxDistance;
            int picBoxHeight = pbCanvas.Size.Height;
            float fKoefY = (float)picBoxHeight / (float)Cities.MaxDistance;
  
            pbCanvas.Refresh();
            Image img = new Bitmap(picBoxWidth, picBoxHeight);
            System.Drawing.Graphics g = Graphics.FromImage(img);

            // Прорисовываем линии между всеми городами
            if (stateCurrent == DrawingState.CitiesAndRoute ||
                stateCurrent == DrawingState.Route)
            {                
                for(int i = 0; i < Cities.Count - 1; i++)
                    for (int j = i + 1; j < Cities.Count; j++)
                    {
                        g.DrawLine(penLiteLine, fKoefX * Cities[i].X, fKoefY * Cities[i].Y, fKoefX * Cities[j].X, fKoefY * Cities[j].Y);
                    }
            }

            // Порисовываем путь
            for (int j = 0; j < Cities.Count - 1; j++)
            {
                switch(stateCurrent)
                {
                    case DrawingState.Cities:
                        g.DrawEllipse(penCities, fKoefX * Cities[j].X - 2, fKoefY * Cities[j].Y - 2, 4, 4);                        
                        break;
                    case DrawingState.Route:
                        g.DrawLine(penRouteLine, fKoefX * Cities[j].X, fKoefY * Cities[j].Y, fKoefX * Cities[j + 1].X, fKoefY * Cities[j + 1].Y);
                        break;
                    case DrawingState.CitiesAndRoute:
                        g.DrawEllipse(penCities, fKoefX * Cities[j].X - 2, fKoefY * Cities[j].Y - 2, 4, 4);
                        g.DrawLine(penRouteLine, fKoefX * Cities[j].X, fKoefY * Cities[j].Y, fKoefX * Cities[j + 1].X, fKoefY * Cities[j + 1].Y);
                        break;
                }
            }
            g.DrawEllipse(penCities, fKoefX * Cities[Cities.Count - 1].X - 2, fKoefY * Cities[Cities.Count - 1].Y - 2, 4, 4);
            // Путь от последнего к первому городу
            if( stateCurrent == DrawingState.Route || stateCurrent == DrawingState.CitiesAndRoute)
                g.DrawLine(penRouteLine, fKoefX * Cities[Cities.Count - 1].X, fKoefY * Cities[Cities.Count - 1].Y, fKoefX * Cities[0].X, fKoefY * Cities[0].Y);

            // Очищаем память
            g.Dispose();

            pbCanvas.Image = img;
        }

        /// <summary>
        /// Выводит длинну маршрута
        /// </summary>
        public void RouteLengthTextOut(double value)                            
        {
            if (value == -1)
                txbRouteLength.Text = "";
            else
                txbRouteLength.Text = String.Format("{0:##.00}", value);
        }

        /// <summary>
        /// Установить коллекцию городов для прорисовки
        /// </summary>
        /// <param name="cities">коллекция городов</param>
        internal void SetCities(DataCitiesCollection cities)                    
        {
            Cities = cities;
        }
        #endregion

        #region События
        /// <summary>
        /// Изменение размеров формы и перерисовка городов
        /// </summary>
        private void ucCitiesPainter_Resize(object sender, EventArgs e)     
        {
            if (Cities != null)
            {
                PaintObjects();
                
            }
        }
        private void ucCitiesPainter_(object sender, EventArgs e)
        {
            if (Cities != null)
            {
                PaintObjects();
            }
        }
        #endregion
    }
}
