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
            antAlgSelect.Value = true;
            antAlgName.Value = "Муравьиной колонии";
            antAlgCalcCount.Value = "1";
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
            nnAlgSelect.Value = true;
            nnAlgName.Value = "Ближайшего соседа";
            nnAlgCalcCount.Value = "1";
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
            bnbAlgSelect.Value = true;
            bnbAlgName.Value = "Ветвей и границ";
            bnbAlgCalcCount.Value = "1";
            //Добавляем ячейки в строку
            bnbAlgRow.Cells.Add(bnbAlgSelect);
            bnbAlgRow.Cells.Add(bnbAlgName);
            bnbAlgRow.Cells.Add(bnbAlgCalcCount);
            //Добавляем строку в DataGridView
            dataGridView.Rows.Add(bnbAlgRow);
            //
            //Создаем Ячейки
            DataGridViewCheckBoxCell genAlgSelect = new DataGridViewCheckBoxCell();
            DataGridViewTextBoxCell genAlgName = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell genAlgCalcCount = new DataGridViewTextBoxCell();
            //Задаем значения ячейкам
            genAlgSelect.Value = true;
            genAlgName.Value = "Генетический";
            genAlgCalcCount.Value = "1";
            //Добавляем ячейки в строку
            genAlgRow.Cells.Add(genAlgSelect);
            genAlgRow.Cells.Add(genAlgName);
            genAlgRow.Cells.Add(genAlgCalcCount);
            //Добавляем строку в DataGridView
            dataGridView.Rows.Add(genAlgRow);

            //Настройка
            dataGridView.AllowUserToResizeRows = false;  //запрещаем растягивать строки
            dataGridView.Rows[3].Cells[0].Value = false; //генетический еще не доступен
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
                        (string)dataGridView.Rows[i].Cells[1].Value, //имя алгоритма
                        Convert.ToInt32((string)dataGridView.Rows[i].Cells[2].Value))); //кол-во запусков
                }                
            }
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
