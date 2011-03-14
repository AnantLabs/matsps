using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ant.Forms.SelectAlgs
{
    public partial class frmSelectAlgs : Form
    {
        #region Поля

        /// <summary>
        /// Список выбранных алгоритмов
        /// </summary>
        private List<int> _selectList = null;

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
            
            //Создание узлов дерева алгоритмов
            treeViewSelectAlgs.Nodes.Add("Алгоритмы");
            treeViewSelectAlgs.Nodes[0].Nodes.Add("Муравьиной колонии");

            //Инициализация списка выбранных алгоритмов
            _selectList = new List<int>();
            int iCount = treeViewSelectAlgs.Nodes[0].Nodes.Count; //количество алгоритмов
            for (int i = 0; i < iCount; i++)
                _selectList.Add(0);

            //Настройка узлов дерева
            treeViewSelectAlgs.Nodes[0].Expand(); //разворачиваем главный узел
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
                    _selectList[curNode.Index] = 1; //выбран
                else
                    _selectList[curNode.Index] = 0; //не выбран
            }
        }

        /// <summary>
        /// Выбора главного узла
        /// </summary>
        private void treeViewSelectAlgs_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SelectAllSubnodes(e.Node);
        }
        
        #endregion

        #region Методы

        /// <summary>
        /// Возвращает список выбранных алгоритмов
        /// </summary>
        public List<int> getSelectList()
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
