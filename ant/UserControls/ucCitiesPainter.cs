using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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

        private Font _fontPointsLabel = new Font("Arial", 10);
        private SolidBrush _brashPointsLabel = new SolidBrush(Color.DarkRed);
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция городов
        /// </summary>
        internal DataCitiesCollection Cities            
        {
            set;
            get;
        }
        /// <summary>
        /// маршрут
        /// </summary>
        internal Route Route                            
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
            //Вывод длинны маршрута           
            RouteLengthTextOut();

            int picBoxWidth = pbCanvas.Size.Width;
            float fKoefX = (float)picBoxWidth / (float)Cities.MaxDistance;
            int picBoxHeight = pbCanvas.Size.Height;
            float fKoefY = (float)picBoxHeight / (float)Cities.MaxDistance;
  
            pbCanvas.Refresh();
            Image img = new Bitmap(picBoxWidth, picBoxHeight);
            System.Drawing.Graphics g = Graphics.FromImage(img);

            // Прорисовываем линии между всеми городами
            if (stateCurrent == DrawingState.CitiesAndRoute ||
                stateCurrent == DrawingState.Route || stateCurrent == DrawingState.Cities)
            {                
                for(int i = 0; i < Cities.Count - 1; i++)
                    for (int j = i + 1; j < Cities.Count; j++)
                    {
                        g.DrawLine(penLiteLine, fKoefX * Cities[i].X, fKoefY * Cities[i].Y, fKoefX * Cities[j].X, fKoefY * Cities[j].Y);
                    }
            }

            // Порисовываем путь
            for (int j = 0; j < Cities.Count; j++)
            {
                switch(stateCurrent)
                {
                    case DrawingState.Cities:
                        g.DrawEllipse(penCities, fKoefX * Cities[j].X - 2, fKoefY * Cities[j].Y - 2, 4, 4);                        
                        break;
                    case DrawingState.Route:
                        g.DrawLine(penRouteLine, fKoefX * Route[j].X, fKoefY * Route[j].Y, fKoefX * Route[j + 1].X, fKoefY * Route[j + 1].Y);
                        break;
                    case DrawingState.CitiesAndRoute:                        
                        g.DrawEllipse(penCities, fKoefX * Route[j].X - 2, fKoefY * Route[j].Y - 2, 4, 4);
                        if( j != Cities.Count - 1 )
                            g.DrawLine(penRouteLine, fKoefX * Route[j].X, fKoefY * Route[j].Y, fKoefX * Route[j + 1].X, fKoefY * Route[j + 1].Y);
                        g.DrawString((j + 1).ToString(), _fontPointsLabel, _brashPointsLabel, fKoefX * Route[j].X + 3, fKoefY * Route[j].Y - 13);
                        break;
                }
            }
            // Путь от последнего к первому городу
            if( stateCurrent == DrawingState.Route || stateCurrent == DrawingState.CitiesAndRoute)
                g.DrawLine(penRouteLine, fKoefX * Route[Route.Count - 1].X, fKoefY * Route[Route.Count - 1].Y, fKoefX * Route[0].X, fKoefY * Route[0].Y);
            
            // Очищаем память
            g.Dispose();

            pbCanvas.Image = img;
        }

        /// <summary>
        /// Выводит длинну маршрута
        /// </summary>
        private void RouteLengthTextOut()                            
        {
            if (Route != null)
            {
                double value = Route.length;
                if (value == -1)
                    txbRouteLength.Text = "";
                else
                    txbRouteLength.Text = String.Format("{0:##.00}", value);
            }
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
