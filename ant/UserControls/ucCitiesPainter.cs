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
            for (int i = 0; i < Cities.Count; i++)
            {
                rtxbCities.AppendText(Cities[i].Index + " X:" + Cities[i].X + " Y:" + Cities[i].Y + Environment.NewLine);
            }

            //int picBoxWidth = pictureBox1.Size.Width;
            //int picBoxHeight = pictureBox1.Size.Height;

            pictureBox1.Refresh();
            Graphics objGraphic = this.pictureBox1.CreateGraphics();

            Pen pen = new Pen(Color.Black);

            for (int j = 0; j < Cities.Count - 1; j++)
            {
                objGraphic.DrawLine(pen, Cities[j].X,  Cities[j].Y, Cities[j + 1].X, Cities[j + 1].Y );
            }
            pen.Dispose();
            objGraphic.Dispose();
            //pictureBox1.Refresh();
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
            pictureBox1.Refresh();
            Graphics objGraphic = this.pictureBox1.CreateGraphics();

            Pen pen = new Pen(Color.Black);

            for (int j = 0; j < Cities.Count - 1; j++)
            {
                objGraphic.DrawLine(pen, Cities[j].X, Cities[j].Y, Cities[j + 1].X, Cities[j + 1].Y);
            }
            pen.Dispose();
            objGraphic.Dispose();
        }
    }
}
