using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ant.Forms.Parameters
{
    public partial class frmAlgSettings : Form
    {
        #region Конструкторы и Данные
        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public frmAlgSettings()                                                         
        {
            InitializeComponent();
        }

        internal frmAlgSettings(ant.AntAlgData.AntAlgDataParameters param):this()
        {
            _parameters = param;

            // Заносим данные в контроллы
            SetDataToControls();
        }

        internal frmAlgSettings(ant.AntAlgData.AntAlgDataParameters param, int citiesCount):this(param)
        {
            _iCitiesCount = citiesCount;

            // Заносим данные в контроллы
            SetDataToControls();
        }

        /// <summary>
        /// Изменяемые параметры алгоритма
        /// </summary>
        private ant.AntAlgData.AntAlgDataParameters _parameters;

        /// <summary>
        /// Количество городов
        /// </summary>
        private int _iCitiesCount = 0;
        #endregion

        #region События
        /// <summary>
        /// Принимаем настройки. Закрываем форму.
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)                            
        {

        }

        /// <summary>
        /// Нажатие на чек бокс "Количесво муравьев равно количеству городов"
        /// </summary>
        private void chbAntEqualCities_CheckedChanged(object sender, EventArgs e)       
        {
            txbMaxAnts.Enabled = !chbAntEqualCities.Checked;
        }
        #endregion

        #region Внутренние методы
        /// <summary>
        /// Устанавливает данные из объекта параметров в контроллы формы
        /// </summary>
        private void SetDataToControls()                                                
        {
            // Количество городов
            if (_iCitiesCount != 0)
                txbCitiesCount.Text = _iCitiesCount.ToString();

            txbMaxDistance.Text = _parameters.MaxDistance.ToString();
            txbMaxAnts.Text = _parameters.MaxAnts.ToString();
            txbAlgStopCount.Text = _parameters.CountRepeatCуcles.ToString();
            txbAlpha.Text = _parameters.ALPHA.ToString();
            txbBetta.Text = _parameters.BETA.ToString();
            txbRho.Text = _parameters.RHO.ToString();
            txbQVal.Text = _parameters.QVAL.ToString();
        }

        /// <summary>
        /// Заносит данные из контроллов в параметры
        /// </summary>
        private void SetControlsToData()                                                
        {
            string strErrorStart = "Ошибка конвертации: ";

            try{
                _parameters.MaxDistance = Convert.ToInt32(txbMaxDistance.Text);
            }catch (Exception ex){
                MessageBox.Show(strErrorStart + "Максимальная дистанция");
            }

            try{
            _parameters.MaxAnts = Convert.ToInt32( txbMaxAnts.Text );
            }catch(Exception ex){
                MessageBox.Show(strErrorStart + "Количество муравьев");
            }

            try{
                _parameters.CountRepeatCуcles = Convert.ToInt32(txbAlgStopCount.Text);
            }catch (Exception ex){
                MessageBox.Show(strErrorStart + "Количество проходов алгоритма");
            }

            try{
                _parameters.ALPHA = Convert.ToInt32(txbAlpha.Text);
            }catch (Exception ex){
                MessageBox.Show(strErrorStart + "Коэффициент Альфа");
            }

            try{
                _parameters.BETA = Convert.ToInt32(txbBetta.Text);
            }catch (Exception ex){
                MessageBox.Show(strErrorStart + "Коэффициент Бета");
            }

            try{
                _parameters.RHO = Convert.ToDouble(txbRho.Text);
            }catch (Exception ex){
                MessageBox.Show(strErrorStart + "Коэффициент Ро");
            }

            try{
                _parameters.QVAL = Convert.ToInt32(txbQVal.Text);
            }catch (Exception ex){
                MessageBox.Show(strErrorStart + "Количество фермента, оставляемого на пути");
            }
        }
        #endregion

        #region Внешние методы
        /// <summary>
        /// Получить параметры
        /// </summary>
        /// <returns></returns>
        internal ant.AntAlgData.AntAlgDataParameters GetParameters()                    
        {
            SetControlsToData();

            return _parameters;
        }
        #endregion
    }
}
