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
            //Подписка на события
            treeViewSelectAlgs.AfterCheck += new TreeViewEventHandler(treeViewSelectAlgs_AfterCheck);
            treeViewSelectAlgs.AfterSelect += new TreeViewEventHandler(treeViewSelectAlgs_AfterSelect);
            //Создание узлов дерева алгоритмов
            treeViewSelectAlgs.Nodes.Add("Алгоритмы");
            treeViewSelectAlgs.Nodes[0].Nodes.Add("Муравьиной колонии");
            treeViewSelectAlgs.Nodes[0].Nodes.Add("Ближайший сосед");
            treeViewSelectAlgs.Nodes[0].Nodes.Add("Ветви и Границы");


            //Инициализация списка выбранных алгоритмов
            _selectList = new List<algStartParam>();
            int iCount = treeViewSelectAlgs.Nodes[0].Nodes.Count; //количество алгоритмов
            for (int i = 0; i < iCount; i++)
            {
                // по умолчанию не выбран, 0 раз запустить
                _selectList.Add(new algStartParam(false,0));
            }

            //Настройка узлов дерева
            treeViewSelectAlgs.Nodes[0].Expand(); //разворачиваем главный узел

            //Настройка текстбокса
            txbInstCount.Text = "0";      //ко-во запусков
            txbInstCount.Visible = false; //видимость
        }

        /// <summary>
        /// Нажатие кнопки "Расчет"
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Проверяем, какие узлы выбранны и заносим их в список
            foreach (TreeNode curNode in treeViewSelectAlgs.Nodes[0].Nodes)
            {
                if (curNode.Checked)
                    _selectList[curNode.Index].selected = true; //выбран
                else
                    _selectList[curNode.Index].selected = false; //не выбран
            }
        }

        /// <summary>
        /// Выбора главного узла
        /// </summary>
        private void treeViewSelectAlgs_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SelectAllSubnodes(e.Node);
            //считывание данных из текстбокса
            _selectList[e.Node.Index].instCount = Convert.ToInt16(txbInstCount.Text);
        }
        private void treeViewSelectAlgs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Настройка текстбокса
            txbInstCount.Visible = true; //видимость 
            txbInstCount.Text = _selectList[e.Node.Index].instCount.ToString();
            //Перемещение текстбокса
            if(e.Node.Index == 0)
                txbInstCount.Location = new Point(195,35);
            if (e.Node.Index == 1)
                txbInstCount.Location = new Point(195, 50);
            if (e.Node.Index == 2)
                txbInstCount.Location = new Point(195, 65);
            //Считывание данных из тексбокса
            _selectList[e.Node.Index].instCount = Convert.ToInt16(txbInstCount.Text);
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
        /// <summary>
        /// Выделить все подузлы дерева
        /// </summary>
        private void SelectAllSubnodes(TreeNode treeNode)
        {
            foreach (TreeNode treeSubNode in treeNode.Nodes)
            {
                treeSubNode.Checked = treeNode.Checked;
            }
        }

        #endregion
    }
}
