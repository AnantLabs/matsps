using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using matsps.Parameters;           // параметры алгоритмов

namespace matsps.Forms.Parameters
{
    public partial class frmSelectAlgs : Form
    {
        #region Конструкторы и Данные

        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public frmSelectAlgs()                                                         
        {
            InitializeComponent();
        }

        internal frmSelectAlgs(AntParameters param)
            : this()                                                                    
        {
            _parameters = param;

            // Заносим данные в контроллы
            SetDataToControls();
        }

        internal frmSelectAlgs(AntParameters param, int citiesCount):this(param)        
        {
            _iCitiesCount = citiesCount;

            // Заносим данные в контроллы
            SetDataToControls();
        }

        /// <summary>
        /// Изменяемые параметры алгоритма
        /// </summary>
        private AntParameters _parameters;

        /// <summary>
        /// Количество городов
        /// </summary>
        private int _iCitiesCount = 0;



        #endregion Конструкторы и Данные

        #region События
        /// <summary>
        /// Принимаем настройки. Закрываем форму.
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)                            
        {

        }

        /// <summary>
        /// Изменение Типа конца алгоиртма
        /// </summary>
        private void rbtnAntEndIter_CheckedChanged(object sender, EventArgs e)          
        {
            if (rbtnAntEndIter.Checked)
            {
                txbAlgStopIterCount.Enabled = true;
                txbAlgStopConvergenceCount.Enabled = false;
            }

            if (rbtnAntEndConvergence.Checked)
            {
                txbAlgStopIterCount.Enabled = false;
                txbAlgStopConvergenceCount.Enabled = true;
            }
        }

        /// <summary>
        /// Изменение соотношения мутаций
        /// </summary>
        private void tbMutations_Scroll(object sender, EventArgs e)
        {
            List<TrackBar> tbList = new List<TrackBar>();
            tbList.Add(this.tbCitySwitchMutation);
            tbList.Add(this.tbIsolatedChainMutation);
            tbList.Add(this.tbNewAgentMutation);

            int iSumm = 0;
            for (int i = 0; i < tbList.Count; i++)
            {
                iSumm += tbList[i].Value;
            }
            // Сумма значений трекбаров должна быть меньше 1000(100 %)
            if (iSumm > 1000)
            {
                // Избыток суммы значений трекбаров
                int iSurplus = iSumm - 1000;

                iSumm -= ((TrackBar)sender).Value;

                tbList.Remove((TrackBar)sender);

                // Коэффициент уменьшения избыточных значений для каждого из трекбаров
                List<double> _liCoeff = new List<double>();
                for (int i = 0; i < tbList.Count; i++)
                {
                    double dCoeff = ((double)tbList[i].Value / (double)iSumm);
                    _liCoeff.Add(dCoeff);
                }

                // Уменьшаем значения трекбаров
                for (int i = 0; i < tbList.Count; i++)
                {
                    int iCurSurplus = (int)(iSurplus * _liCoeff[i]);
                    // if (iCurSurplus < 0)
                    //      iCurSurplus = 0;
                    tbList[i].Value = tbList[i].Value - iCurSurplus;
                    // определить коэффициент для каждого tb
                }
            }

            txbCitySwitchMutation.Text = ((double)tbCitySwitchMutation.Value / 10.0).ToString();
            txbIsolatedChainMutation.Text = ((double)tbIsolatedChainMutation.Value / 10.0).ToString();
            txbNewAgentMutation.Text = ((double)tbNewAgentMutation.Value / 10.0).ToString();
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

            switch (_parameters.EndType)
            {
                case AntAlgorithmEndType.Iteration:
                    {
                        rbtnAntEndIter.Checked = true;
                        rbtnAntEndConvergence.Checked = false;
                    };
                    break;
                case AntAlgorithmEndType.Convergence:
                    {
                        rbtnAntEndIter.Checked = false;
                        rbtnAntEndConvergence.Checked = true;
                    };break;
            }
            txbAlgStopConvergenceCount.Text = _parameters.CountConvergence.ToString();
            txbAlgStopIterCount.Text = _parameters.CountRepeatCуcles.ToString();

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

            if (rbtnAntEndConvergence.Checked)
                _parameters.EndType = AntAlgorithmEndType.Convergence;
            if (rbtnAntEndIter.Checked)
                _parameters.EndType = AntAlgorithmEndType.Iteration;
            try{
                _parameters.CountRepeatCуcles = Convert.ToInt32(txbAlgStopIterCount.Text);
            }catch (Exception ex){
                MessageBox.Show(strErrorStart + "Количество проходов алгоритма");
            }
            try
            {
                _parameters.CountConvergence = Convert.ToInt32(txbAlgStopConvergenceCount.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strErrorStart + "Количество для сходимости");
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

        private void LoadGAParametersFromIni(string _pathToIni)
        {
            // Настройки
            matsps.Parameters.Ini.IniFile ini = new matsps.Parameters.Ini.IniFile(_pathToIni);

            string sGenCount, sAgentsCount, sSurviversCount;

            sGenCount = ini.IniReadValue("count", "generations");
            sAgentsCount = ini.IniReadValue("count", "agents");
            sSurviversCount = ini.IniReadValue("count", "survivers");

            if (sGenCount != "")
                try
                {
                    this.txbGenCout.Text = (sGenCount);
                }
                catch { }
            if (sAgentsCount != "")
                try
                {
                    this.txbAgentsCount.Text = (sAgentsCount);
                }
                catch { }
            if (sSurviversCount != "")
                try
                {
                    this.txbSurviversCount.Text = (sSurviversCount);
                }
                catch { }

            string sMutationsPecent;

            sMutationsPecent = ini.IniReadValue("percent", "mutations");

            if (sMutationsPecent != "")
                try
                {
                    this.txbMutationsPecent.Text = (sMutationsPecent);
                }
                catch { }

            string sCitySwitch, sIsolatedChain, sNewAgent;
            sCitySwitch = ini.IniReadValue("probability", "cityswitch");
            sIsolatedChain = ini.IniReadValue("probability", "isolatedchain");
            sNewAgent = ini.IniReadValue("probability", "newagent");
            if (sCitySwitch != "")
                try
                {
                    int iCitySwitch;
                    Int32.TryParse(sCitySwitch, out iCitySwitch);

                    this.txbCitySwitchMutation.Text = (sCitySwitch);
                    this.tbCitySwitchMutation.Value = iCitySwitch * 10;
                }
                catch { }
            if (sIsolatedChain != "")
                try
                {
                    int iIsolatedChain;
                    Int32.TryParse(sIsolatedChain, out iIsolatedChain);

                    this.txbIsolatedChainMutation.Text = (sIsolatedChain);
                    this.tbIsolatedChainMutation.Value = iIsolatedChain * 10;
                }
                catch { }
            if (sNewAgent != "")
                try
                {
                    int iNewAgent;
                    Int32.TryParse(sNewAgent, out iNewAgent);

                    this.txbNewAgentMutation.Text = (sNewAgent);
                    this.tbNewAgentMutation.Value = iNewAgent * 10;
                }
                catch { }
            //if (strHeight != "")
            //    try
            //    {
            //        this.Height = Convert.ToInt32(strHeight);
            //    }
            //    catch { }
            //if (strDgvFileHeight != "")
            //    try
            //    {
            //        splitContainer1.Panel1.Height = Convert.ToInt32(strDgvFileHeight);
            //    }
            //    catch { }
        }

        private void SaveGAParametersToIni(string _pathToIni)
        {
            matsps.Parameters.Ini.IniFile ini = new matsps.Parameters.Ini.IniFile(_pathToIni);
           // ini.IniWriteValue("files", "openPath", _openPath);
           // ini.IniWriteValue("files", "saveToExcelPath", _saveToExcelPath);

            ini.IniWriteValue("count", "generations", this.txbGenCout.Text);
            ini.IniWriteValue("count", "agents", this.txbAgentsCount.Text);
            ini.IniWriteValue("count", "survivers", this.txbSurviversCount.Text);
            
            ini.IniWriteValue("percent", "mutations", this.txbMutationsPecent.Text);

            ini.IniWriteValue("probability", "cityswitch", this.txbCitySwitchMutation.Text);
            ini.IniWriteValue("probability", "isolatedchain", this.txbIsolatedChainMutation.Text);
            ini.IniWriteValue("probability", "newagent", this.txbNewAgentMutation.Text);


           // ini.IniWriteValue("form", "height", this.Height.ToString());
           // ini.IniWriteValue("form", "dgvFileHeight", splitContainer1.Panel1.Height.ToString());
        }
        #endregion

        #region Внешние методы
        /// <summary>
        /// Получить параметры
        /// </summary>
        /// <returns></returns>
        internal AntParameters GetParameters()                    
        {
            SetControlsToData();

            return _parameters;
        }
        #endregion

        /// <summary>
        /// Событие загрузки формы
        /// </summary>
        private void frmSelectAlgs_Load(object sender, EventArgs e)
        {
            try
            {
                /// <summary>
                /// Путь к INI-файлу настроек
                /// </summary>
                string _pathToGAParameters = Environment.CurrentDirectory + "\\Settings\\GAParameters.ini";
                this.LoadGAParametersFromIni(_pathToGAParameters);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }



        /// <summary>
        /// Событие закрытия формы
        /// </summary>
        private void frmSelectAlgs_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                /// <summary>
                /// Путь к INI-файлу настроек
                /// </summary>
                string _pathToGAParameters = Environment.CurrentDirectory + "\\Settings\\GAParameters.ini";
                this.SaveGAParametersToIni(_pathToGAParameters);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


    }
}
