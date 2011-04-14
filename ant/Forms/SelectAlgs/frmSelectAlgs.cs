using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace matsps.Forms.SelectAlgs
{   
    public partial class frmSelectAlgs : Form
    {
        #region Поля
        /// <summary>
        /// Список выбранных алгоритмов
        /// </summary>
        private List<AlgStartParam> _selectList = null;
        /// <summary>
        /// Список выбранных алгоритмов по умолчанию
        /// </summary>
        private static List<AlgStartParam> _defaultParams = null;

        #endregion

        #region Конструкторы

        public frmSelectAlgs()
        {
            InitializeComponent();
        }

        #endregion

        #region События формы

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void frmSelectAlgs_Load(object sender, EventArgs e)
        {
            //инициализация списка алгоритмов по умолчанию
            if (_defaultParams == null )//если не инициализирован
            {
                _defaultParams = new List<AlgStartParam>();
            }

            //инициализируем список алгоритмов для запуска
            _selectList = new List<AlgStartParam>();
            //Убираем заголовок
            dataGridView.RowHeadersVisible = false;
            
            //Новые стоки
            DataGridViewRow antAlgRow = new DataGridViewRow();
            DataGridViewRow nnAlgRow = new DataGridViewRow();
            DataGridViewRow bnbAlgRow = new DataGridViewRow();
            DataGridViewRow genAlgRow = new DataGridViewRow();

            //Создаем Ячейки
            DataGridViewCheckBoxCell antAlgSelect = new DataGridViewCheckBoxCell();
            DataGridViewTextBoxCell antAlgName = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell antAlgCalcCount = new DataGridViewTextBoxCell();

            //Задаем значения ячейкам
            antAlgSelect.Value = false;
            antAlgName.Value = "Муравьиной колонии";
            antAlgCalcCount.Value = "0";
            //Добавляем ячейки в строку
            antAlgRow.Cells.Add(antAlgSelect);
            antAlgRow.Cells.Add(antAlgName);
            antAlgRow.Cells.Add(antAlgCalcCount);           
            //Добавляем строку в DataGridView
            dataGridView.Rows.Add(antAlgRow);
            //
            //Создаем Ячейки
            DataGridViewCheckBoxCell nnAlgSelect = new DataGridViewCheckBoxCell();
            DataGridViewTextBoxCell nnAlgName = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell nnAlgCalcCount = new DataGridViewTextBoxCell();           
            //Задаем значения ячейкам
            nnAlgSelect.Value = false;
            nnAlgName.Value = "Ближайшего соседа";
            nnAlgCalcCount.Value = "0";
            //Добавляем ячейки в строку
            nnAlgRow.Cells.Add(nnAlgSelect);
            nnAlgRow.Cells.Add(nnAlgName);
            nnAlgRow.Cells.Add(nnAlgCalcCount);
            //Добавляем строку в DataGridView
            dataGridView.Rows.Add(nnAlgRow);
            //
            //Создаем Ячейки
            DataGridViewCheckBoxCell bnbAlgSelect = new DataGridViewCheckBoxCell();
            DataGridViewTextBoxCell bnbAlgName = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell bnbAlgCalcCount = new DataGridViewTextBoxCell();
            //Задаем значения ячейкам
            bnbAlgSelect.Value = false;
            bnbAlgName.Value = "Ветвей и границ";
            bnbAlgCalcCount.Value = "0";
            //Добавляем ячейки в строку
            bnbAlgRow.Cells.Add(bnbAlgSelect);
            bnbAlgRow.Cells.Add(bnbAlgName);
            bnbAlgRow.Cells.Add(bnbAlgCalcCount);
            //Добавляем строку в DataGridView
            dataGridView.Rows.Add(bnbAlgRow);
            //

            //Загрузка значений по умолчанию
            for (int i = 0; i < _defaultParams.Count; i++)
            {
                if (_defaultParams[i].name == "Муравьиной колонии")
                {
                    antAlgSelect.Value = true;
                    antAlgCalcCount.Value = Convert.ToString(_defaultParams[i].InstCount);
                }
                if (_defaultParams[i].name == "Ближайшего соседа")
                {
                    nnAlgSelect.Value = true;
                    nnAlgCalcCount.Value = Convert.ToString(_defaultParams[i].InstCount);
                }
                if (_defaultParams[i].name == "Ветвей и границ")
                {
                    bnbAlgSelect.Value = true;
                    bnbAlgCalcCount.Value = Convert.ToString(_defaultParams[i].InstCount);
                }
            }

            //Настройка
            dataGridView.AllowUserToResizeRows = false;  //запрещаем растягивать строки
        }

        /// <summary>
        /// Нажатие кнопки "Расчет"
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                bool algChecked = (bool)dataGridView.Rows[i].Cells[0].Value;
                //если алгоритм выбран, то добавляем в список
                if (algChecked == true)
                { 
                    _selectList.Add(new AlgStartParam(
                        (string)(dataGridView.Rows[i].Cells[1].Value), //имя алгоритма
                        Convert.ToInt32((string)dataGridView.Rows[i].Cells[2].Value))); //кол-во запусков
                }                
            }
            _defaultParams = _selectList;
        }
        
        #endregion

        #region Методы

        /// <summary>
        /// Возвращает список выбранных алгоритмов
        /// </summary>
        public List<AlgStartParam> getSelectList()
        {
            return _selectList;
        }

        #endregion
    }
}
