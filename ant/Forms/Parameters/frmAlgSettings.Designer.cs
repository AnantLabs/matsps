namespace matsps.Forms.Parameters
{
    partial class frmSelectAlgs
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txbMaxDistance = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbAlgStopConvergenceCount = new System.Windows.Forms.TextBox();
            this.rbtnAntEndConvergence = new System.Windows.Forms.RadioButton();
            this.txbAlgStopIterCount = new System.Windows.Forms.TextBox();
            this.rbtnAntEndIter = new System.Windows.Forms.RadioButton();
            this.txbQVal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txbRho = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbBetta = new System.Windows.Forms.TextBox();
            this.txbAlpha = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbMaxAnts = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblNewAgentMutation = new System.Windows.Forms.Label();
            this.lblIsolatedChainMutation = new System.Windows.Forms.Label();
            this.lblSwitchCityMutation = new System.Windows.Forms.Label();
            this.tbNewAgentMutation = new System.Windows.Forms.TrackBar();
            this.tbIsolatedChainMutation = new System.Windows.Forms.TrackBar();
            this.tbCitySwitchMutation = new System.Windows.Forms.TrackBar();
            this.lblMutationsPecent = new System.Windows.Forms.Label();
            this.txbMutationsPecent = new System.Windows.Forms.TextBox();
            this.txbSurviversCount = new System.Windows.Forms.TextBox();
            this.lblSurviversCount = new System.Windows.Forms.Label();
            this.txbAgetsCount = new System.Windows.Forms.TextBox();
            this.lblAgetsCount = new System.Windows.Forms.Label();
            this.txbGenCout = new System.Windows.Forms.TextBox();
            this.lblGenCout = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbCitiesCount = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.gbMutationsRatio = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNewAgentMutation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIsolatedChainMutation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCitySwitchMutation)).BeginInit();
            this.gbMutationsRatio.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(1, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(407, 279);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txbMaxDistance);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(399, 253);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Программа";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txbMaxDistance
            // 
            this.txbMaxDistance.Location = new System.Drawing.Point(199, 11);
            this.txbMaxDistance.Name = "txbMaxDistance";
            this.txbMaxDistance.Size = new System.Drawing.Size(50, 20);
            this.txbMaxDistance.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Макс. дистанция между городами:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.txbQVal);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txbRho);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.txbBetta);
            this.tabPage2.Controls.Add(this.txbAlpha);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txbMaxAnts);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(399, 253);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Алгоритм муравья";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(276, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "(рекомендуется задать равным количеству городов)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txbAlgStopConvergenceCount);
            this.groupBox1.Controls.Add(this.rbtnAntEndConvergence);
            this.groupBox1.Controls.Add(this.txbAlgStopIterCount);
            this.groupBox1.Controls.Add(this.rbtnAntEndIter);
            this.groupBox1.Location = new System.Drawing.Point(9, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 70);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Завершение алгоритма";
            // 
            // txbAlgStopConvergenceCount
            // 
            this.txbAlgStopConvergenceCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txbAlgStopConvergenceCount.Location = new System.Drawing.Point(324, 43);
            this.txbAlgStopConvergenceCount.Name = "txbAlgStopConvergenceCount";
            this.txbAlgStopConvergenceCount.Size = new System.Drawing.Size(50, 20);
            this.txbAlgStopConvergenceCount.TabIndex = 17;
            // 
            // rbtnAntEndConvergence
            // 
            this.rbtnAntEndConvergence.AutoSize = true;
            this.rbtnAntEndConvergence.Location = new System.Drawing.Point(7, 44);
            this.rbtnAntEndConvergence.Name = "rbtnAntEndConvergence";
            this.rbtnAntEndConvergence.Size = new System.Drawing.Size(300, 17);
            this.rbtnAntEndConvergence.TabIndex = 1;
            this.rbtnAntEndConvergence.Text = "По сходимости (количество повторений лучшего пути)";
            this.rbtnAntEndConvergence.UseVisualStyleBackColor = true;
            this.rbtnAntEndConvergence.CheckedChanged += new System.EventHandler(this.rbtnAntEndIter_CheckedChanged);
            // 
            // txbAlgStopIterCount
            // 
            this.txbAlgStopIterCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txbAlgStopIterCount.Location = new System.Drawing.Point(324, 17);
            this.txbAlgStopIterCount.Name = "txbAlgStopIterCount";
            this.txbAlgStopIterCount.Size = new System.Drawing.Size(50, 20);
            this.txbAlgStopIterCount.TabIndex = 3;
            // 
            // rbtnAntEndIter
            // 
            this.rbtnAntEndIter.AutoSize = true;
            this.rbtnAntEndIter.Location = new System.Drawing.Point(7, 20);
            this.rbtnAntEndIter.Name = "rbtnAntEndIter";
            this.rbtnAntEndIter.Size = new System.Drawing.Size(228, 17);
            this.rbtnAntEndIter.TabIndex = 0;
            this.rbtnAntEndIter.Text = "По итерациям (по количеству проходов)";
            this.rbtnAntEndIter.UseVisualStyleBackColor = true;
            this.rbtnAntEndIter.CheckedChanged += new System.EventHandler(this.rbtnAntEndIter_CheckedChanged);
            // 
            // txbQVal
            // 
            this.txbQVal.Location = new System.Drawing.Point(253, 213);
            this.txbQVal.Name = "txbQVal";
            this.txbQVal.Size = new System.Drawing.Size(50, 20);
            this.txbQVal.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(241, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Количество фермента, оставляемого на пути:";
            // 
            // txbRho
            // 
            this.txbRho.Location = new System.Drawing.Point(67, 185);
            this.txbRho.Name = "txbRho";
            this.txbRho.Size = new System.Drawing.Size(50, 20);
            this.txbRho.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Rho:";
            // 
            // txbBetta
            // 
            this.txbBetta.Location = new System.Drawing.Point(67, 160);
            this.txbBetta.Name = "txbBetta";
            this.txbBetta.Size = new System.Drawing.Size(50, 20);
            this.txbBetta.TabIndex = 10;
            // 
            // txbAlpha
            // 
            this.txbAlpha.Location = new System.Drawing.Point(67, 137);
            this.txbAlpha.Name = "txbAlpha";
            this.txbAlpha.Size = new System.Drawing.Size(50, 20);
            this.txbAlpha.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Beta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Alpha:";
            // 
            // txbMaxAnts
            // 
            this.txbMaxAnts.Location = new System.Drawing.Point(132, 8);
            this.txbMaxAnts.Name = "txbMaxAnts";
            this.txbMaxAnts.Size = new System.Drawing.Size(50, 20);
            this.txbMaxAnts.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Количество муравьев:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gbMutationsRatio);
            this.tabPage3.Controls.Add(this.lblMutationsPecent);
            this.tabPage3.Controls.Add(this.txbMutationsPecent);
            this.tabPage3.Controls.Add(this.txbSurviversCount);
            this.tabPage3.Controls.Add(this.lblSurviversCount);
            this.tabPage3.Controls.Add(this.txbAgetsCount);
            this.tabPage3.Controls.Add(this.lblAgetsCount);
            this.tabPage3.Controls.Add(this.txbGenCout);
            this.tabPage3.Controls.Add(this.lblGenCout);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(399, 253);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Генетический";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblNewAgentMutation
            // 
            this.lblNewAgentMutation.AutoSize = true;
            this.lblNewAgentMutation.Location = new System.Drawing.Point(226, 27);
            this.lblNewAgentMutation.Name = "lblNewAgentMutation";
            this.lblNewAgentMutation.Size = new System.Drawing.Size(75, 13);
            this.lblNewAgentMutation.TabIndex = 13;
            this.lblNewAgentMutation.Text = "Новый агент:";
            // 
            // lblIsolatedChainMutation
            // 
            this.lblIsolatedChainMutation.AutoSize = true;
            this.lblIsolatedChainMutation.Location = new System.Drawing.Point(116, 27);
            this.lblIsolatedChainMutation.Name = "lblIsolatedChainMutation";
            this.lblIsolatedChainMutation.Size = new System.Drawing.Size(88, 13);
            this.lblIsolatedChainMutation.TabIndex = 12;
            this.lblIsolatedChainMutation.Text = "В цепи городов:";
            // 
            // lblSwitchCityMutation
            // 
            this.lblSwitchCityMutation.AutoSize = true;
            this.lblSwitchCityMutation.Location = new System.Drawing.Point(6, 27);
            this.lblSwitchCityMutation.Name = "lblSwitchCityMutation";
            this.lblSwitchCityMutation.Size = new System.Drawing.Size(89, 13);
            this.lblSwitchCityMutation.TabIndex = 11;
            this.lblSwitchCityMutation.Text = "Перестановкой:";
            // 
            // tbNewAgentMutation
            // 
            this.tbNewAgentMutation.Location = new System.Drawing.Point(229, 43);
            this.tbNewAgentMutation.Maximum = 1000;
            this.tbNewAgentMutation.Name = "tbNewAgentMutation";
            this.tbNewAgentMutation.Size = new System.Drawing.Size(104, 45);
            this.tbNewAgentMutation.TabIndex = 10;
            // 
            // tbIsolatedChainMutation
            // 
            this.tbIsolatedChainMutation.LargeChange = 10;
            this.tbIsolatedChainMutation.Location = new System.Drawing.Point(119, 43);
            this.tbIsolatedChainMutation.Maximum = 1000;
            this.tbIsolatedChainMutation.Name = "tbIsolatedChainMutation";
            this.tbIsolatedChainMutation.Size = new System.Drawing.Size(104, 45);
            this.tbIsolatedChainMutation.TabIndex = 9;
            // 
            // tbCitySwitchMutation
            // 
            this.tbCitySwitchMutation.LargeChange = 10;
            this.tbCitySwitchMutation.Location = new System.Drawing.Point(9, 43);
            this.tbCitySwitchMutation.Maximum = 1000;
            this.tbCitySwitchMutation.Name = "tbCitySwitchMutation";
            this.tbCitySwitchMutation.Size = new System.Drawing.Size(104, 45);
            this.tbCitySwitchMutation.TabIndex = 8;
            this.tbCitySwitchMutation.Scroll += new System.EventHandler(this.tbMutations_Scroll);
            // 
            // lblMutationsPecent
            // 
            this.lblMutationsPecent.AutoSize = true;
            this.lblMutationsPecent.Location = new System.Drawing.Point(6, 91);
            this.lblMutationsPecent.Name = "lblMutationsPecent";
            this.lblMutationsPecent.Size = new System.Drawing.Size(98, 13);
            this.lblMutationsPecent.TabIndex = 7;
            this.lblMutationsPecent.Text = "Процент мутации:";
            // 
            // txbMutationsPecent
            // 
            this.txbMutationsPecent.Location = new System.Drawing.Point(259, 88);
            this.txbMutationsPecent.Name = "txbMutationsPecent";
            this.txbMutationsPecent.Size = new System.Drawing.Size(76, 20);
            this.txbMutationsPecent.TabIndex = 6;
            // 
            // txbSurviversCount
            // 
            this.txbSurviversCount.Location = new System.Drawing.Point(259, 62);
            this.txbSurviversCount.Name = "txbSurviversCount";
            this.txbSurviversCount.Size = new System.Drawing.Size(76, 20);
            this.txbSurviversCount.TabIndex = 5;
            // 
            // lblSurviversCount
            // 
            this.lblSurviversCount.AutoSize = true;
            this.lblSurviversCount.Location = new System.Drawing.Point(6, 65);
            this.lblSurviversCount.Name = "lblSurviversCount";
            this.lblSurviversCount.Size = new System.Drawing.Size(249, 13);
            this.lblSurviversCount.TabIndex = 4;
            this.lblSurviversCount.Text = "Количество выживающих в каждом поколении:";
            // 
            // txbAgetsCount
            // 
            this.txbAgetsCount.Location = new System.Drawing.Point(259, 36);
            this.txbAgetsCount.Name = "txbAgetsCount";
            this.txbAgetsCount.Size = new System.Drawing.Size(76, 20);
            this.txbAgetsCount.TabIndex = 3;
            // 
            // lblAgetsCount
            // 
            this.lblAgetsCount.AutoSize = true;
            this.lblAgetsCount.Location = new System.Drawing.Point(6, 39);
            this.lblAgetsCount.Name = "lblAgetsCount";
            this.lblAgetsCount.Size = new System.Drawing.Size(174, 13);
            this.lblAgetsCount.TabIndex = 2;
            this.lblAgetsCount.Text = "Количество особей в поколении:";
            // 
            // txbGenCout
            // 
            this.txbGenCout.Location = new System.Drawing.Point(259, 10);
            this.txbGenCout.Name = "txbGenCout";
            this.txbGenCout.Size = new System.Drawing.Size(76, 20);
            this.txbGenCout.TabIndex = 1;
            // 
            // lblGenCout
            // 
            this.lblGenCout.AutoSize = true;
            this.lblGenCout.Location = new System.Drawing.Point(6, 13);
            this.lblGenCout.Name = "lblGenCout";
            this.lblGenCout.Size = new System.Drawing.Size(233, 13);
            this.lblGenCout.TabIndex = 0;
            this.lblGenCout.Text = "Количесво поколений(число циклов отбора):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Количество городов:";
            // 
            // txbCitiesCount
            // 
            this.txbCitiesCount.Location = new System.Drawing.Point(128, 6);
            this.txbCitiesCount.Name = "txbCitiesCount";
            this.txbCitiesCount.ReadOnly = true;
            this.txbCitiesCount.Size = new System.Drawing.Size(50, 20);
            this.txbCitiesCount.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackgroundImage = global::matsps.Properties.Resources.cancel_48;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(38, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(32, 32);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.BackgroundImage = global::matsps.Properties.Resources.accepted_48;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStart.Location = new System.Drawing.Point(1, 317);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(32, 32);
            this.btnStart.TabIndex = 6;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbMutationsRatio
            // 
            this.gbMutationsRatio.Controls.Add(this.lblSwitchCityMutation);
            this.gbMutationsRatio.Controls.Add(this.lblNewAgentMutation);
            this.gbMutationsRatio.Controls.Add(this.tbCitySwitchMutation);
            this.gbMutationsRatio.Controls.Add(this.lblIsolatedChainMutation);
            this.gbMutationsRatio.Controls.Add(this.tbIsolatedChainMutation);
            this.gbMutationsRatio.Controls.Add(this.tbNewAgentMutation);
            this.gbMutationsRatio.Location = new System.Drawing.Point(9, 114);
            this.gbMutationsRatio.Name = "gbMutationsRatio";
            this.gbMutationsRatio.Size = new System.Drawing.Size(344, 119);
            this.gbMutationsRatio.TabIndex = 15;
            this.gbMutationsRatio.TabStop = false;
            this.gbMutationsRatio.Text = "Соотношение видов мутаций";
            // 
            // frmSelectAlgs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 352);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbCitiesCount);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 379);
            this.Name = "frmSelectAlgs";
            this.ShowIcon = false;
            this.Text = "Настройки";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNewAgentMutation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIsolatedChainMutation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCitySwitchMutation)).EndInit();
            this.gbMutationsRatio.ResumeLayout(false);
            this.gbMutationsRatio.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txbMaxDistance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbMaxAnts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbCitiesCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbAlgStopIterCount;
        private System.Windows.Forms.TextBox txbBetta;
        private System.Windows.Forms.TextBox txbAlpha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbRho;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbQVal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnAntEndConvergence;
        private System.Windows.Forms.RadioButton rbtnAntEndIter;
        private System.Windows.Forms.TextBox txbAlgStopConvergenceCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblAgetsCount;
        private System.Windows.Forms.TextBox txbGenCout;
        private System.Windows.Forms.Label lblGenCout;
        private System.Windows.Forms.Label lblSurviversCount;
        private System.Windows.Forms.TextBox txbAgetsCount;
        private System.Windows.Forms.TextBox txbSurviversCount;
        private System.Windows.Forms.Label lblMutationsPecent;
        private System.Windows.Forms.TextBox txbMutationsPecent;
        private System.Windows.Forms.TrackBar tbCitySwitchMutation;
        private System.Windows.Forms.TrackBar tbNewAgentMutation;
        private System.Windows.Forms.TrackBar tbIsolatedChainMutation;
        private System.Windows.Forms.Label lblNewAgentMutation;
        private System.Windows.Forms.Label lblIsolatedChainMutation;
        private System.Windows.Forms.Label lblSwitchCityMutation;
        private System.Windows.Forms.GroupBox gbMutationsRatio;
    }
}