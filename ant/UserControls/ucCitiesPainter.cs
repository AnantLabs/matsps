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
        
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция городов
        /// </summary>
        private AntAlgDataCitiesCollection Cities           
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
            int picBoxWidth = pbCanvas.Size.Width;
            float fKoefX = (float)picBoxWidth / (float)Cities.MaxDistance;
            int picBoxHeight = pbCanvas.Size.Height;
            float fKoefY = (float)picBoxHeight / (float)Cities.MaxDistance;

            pbCanvas.Refresh();
            Bitmap img = new Bitmap(picBoxWidth, picBoxHeight);
            System.Drawing.Graphics g = Graphics.FromImage(img);

            Pen pen = new Pen(Brushes.Purple,2);

            for (int j = 0; j < Cities.Count - 1; j++)
            {
                g.DrawLine(pen, fKoefX * Cities[j].X, fKoefY * Cities[j].Y, fKoefX * Cities[j + 1].X, fKoefY * Cities[j + 1].Y);
                g.DrawEllipse(new Pen(Brushes.Purple, 4), fKoefX * Cities[j].X - 2, fKoefY * Cities[j].Y - 2, 4, 4);
            }
            // Путь от последнего к первому городу
            g.DrawLine(pen, fKoefX * Cities[Cities.Count - 1].X, fKoefY * Cities[Cities.Count - 1].Y, fKoefX * Cities[0].X, fKoefY * Cities[0].Y);
            
            // Очищаем память
            pen.Dispose();
            g.Dispose();

            pbCanvas.Image = img;
        }

        /// <summary>
        /// Установить коллекцию городов для прорисовки
        /// </summary>
        /// <param name="cities">коллекция городов</param>
        internal void SetCities(AntAlgDataCitiesCollection cities)        
        {
            Cities = cities;
        }
        #endregion

        private void pictureBox1_Paint(object sender, PaintEventArgs e)     
        {
            /*
            pictureBox1.Refresh();
            Graphics objGraphic = this.pictureBox1.CreateGraphics();

            Pen pen = new Pen(Color.Black,2);

            for (int j = 0; j < Cities.Count - 1; j++)
            {
                objGraphic.DrawLine(pen, Cities[j].X, Cities[j].Y, Cities[j + 1].X, Cities[j + 1].Y);
            }
            pen.Dispose();
            objGraphic.Dispose();
            */
        }

        private void ucCitiesPainter_Resize(object sender, EventArgs e)
        {
            if (Cities != null)
            {
                this.PaintCities();
            }
        }
    }
}
