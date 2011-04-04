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
        private  List<algStartParam> _selectList = null;

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
            //инициализируем список
            _selectList = new List<algStartParam>();
            //Убираем заголовок
            dataGridView.RowHeadersVisible = false;
            //Новые стоки
            DataGridViewRow antAlgRow = new DataGridViewRow();
            DataGridViewRow nnAlgRow = new DataGridViewRow();
            DataGridViewRow bnbAlgRow = new DataGridViewRow();

            //Создаем Ячейки
            DataGridViewCheckBoxCell antAlgCalcSatatus = new DataGridViewCheckBoxCell();
            DataGridViewTextBoxCell antAlgName = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell antAlgCalcCount = new DataGridViewTextBoxCell();           
            //Задаем значения ячейкам
            antAlgCalcSatatus.Value = true;
            antAlgName.Value = "Муравьиной колонии";
            antAlgCalcCount.Value = "1";
            //Добавляем ячейки в строку
            antAlgRow.Cells.Add(antAlgCalcSatatus);
            antAlgRow.Cells.Add(antAlgName);
            antAlgRow.Cells.Add(antAlgCalcCount);           
            //Добавляем строку в DataGridView
            dataGridView.Rows.Add(antAlgRow);
            //
            //Создаем Ячейки
            DataGridViewCheckBoxCell nnAlgCalcSatatus = new DataGridViewCheckBoxCell();
            DataGridViewTextBoxCell nnAlgName = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell nnAlgCalcCount = new DataGridViewTextBoxCell();           
            //Задаем значения ячейкам
            nnAlgCalcSatatus.Value = true;
            nnAlgName.Value = "Ближайшего соседушки";
            nnAlgCalcCount.Value = "1";
            //Добавляем ячейки в строку
            nnAlgRow.Cells.Add(nnAlgCalcSatatus);
            nnAlgRow.Cells.Add(nnAlgName);
            nnAlgRow.Cells.Add(nnAlgCalcCount);
            //Добавляем строку в DataGridView
            dataGridView.Rows.Add(nnAlgRow);
            //
            //Создаем Ячейки
            DataGridViewCheckBoxCell bnbAlgCalcSatatus = new DataGridViewCheckBoxCell();
            DataGridViewTextBoxCell bnbAlgName = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell bnbAlgCalcCount = new DataGridViewTextBoxCell();
            //Задаем значения ячейкам
            bnbAlgCalcSatatus.Value = true;
            bnbAlgName.Value = "Ветвей и границ";
            bnbAlgCalcCount.Value = "1";
            //Добавляем ячейки в строку
            bnbAlgRow.Cells.Add(bnbAlgCalcSatatus);
            bnbAlgRow.Cells.Add(bnbAlgName);
            bnbAlgRow.Cells.Add(bnbAlgCalcCount);
            //Добавляем строку в DataGridView
            dataGridView.Rows.Add(bnbAlgRow);

            //Настройка
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.Rows[2].Cells[0].Value = false;
        }

        /// <summary>
        /// Нажатие кнопки "Расчет"
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            int Count = dataGridView.Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                bool algChecked = (bool)dataGridView.Rows[i].Cells[0].Value;
                if (algChecked == true)
                { 
                    int CalcCount = Convert.ToInt32((string)dataGridView.Rows[i].Cells[2].Value);
                    _selectList.Add(new algStartParam(true,CalcCount));
                }                
            }
        }
        
        #endregion

        #region Методы

        /// <summary>
        /// Возвращает список выбранных алгоритмов
        /// </summary>
        public List<algStartParam> getSelectList()
        {
            return _selectList;
        }

        #endregion
    }
}
