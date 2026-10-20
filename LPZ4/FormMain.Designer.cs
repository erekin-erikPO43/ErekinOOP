namespace WinFormsLinqApp
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.gbAggregates = new System.Windows.Forms.GroupBox();
            this.lblAggregate = new System.Windows.Forms.Label();
            this.lblMinMax = new System.Windows.Forms.Label();
            this.lblAvg = new System.Windows.Forms.Label();
            this.lblSum = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnCalcAggregates = new System.Windows.Forms.Button();
            this.btnExceptOnlineOnly = new System.Windows.Forms.Button();
            this.btnIntersectBoth = new System.Windows.Forms.Button();
            this.btnUnionAllUnique = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.gbAggregates.SuspendLayout();
            this.SuspendLayout();

         
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(12, 12);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(760, 220);
            this.dgvResults.TabIndex = 0;

   
            this.gbAggregates.Controls.Add(this.lblAggregate);
            this.gbAggregates.Controls.Add(this.lblMinMax);
            this.gbAggregates.Controls.Add(this.lblAvg);
            this.gbAggregates.Controls.Add(this.lblSum);
            this.gbAggregates.Controls.Add(this.lblCount);
            this.gbAggregates.Location = new System.Drawing.Point(12, 245);
            this.gbAggregates.Name = "gbAggregates";
            this.gbAggregates.Size = new System.Drawing.Size(760, 145);
            this.gbAggregates.TabIndex = 1;
            this.gbAggregates.TabStop = false;
            this.gbAggregates.Text = "Результаты агрегатных функций (OnlineOrders)";

           
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(15, 25);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(70, 15);
            this.lblCount.Text = "Count(): -";

        
            this.lblSum.AutoSize = true;
            this.lblSum.Location = new System.Drawing.Point(15, 48);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(60, 15);
            this.lblSum.Text = "Sum(): -";

       
            this.lblAvg.AutoSize = true;
            this.lblAvg.Location = new System.Drawing.Point(15, 71);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new System.Drawing.Size(75, 15);
            this.lblAvg.Text = "Average(): -";

           
            this.lblMinMax.AutoSize = true;
            this.lblMinMax.Location = new System.Drawing.Point(15, 94);
            this.lblMinMax.Name = "lblMinMax";
            this.lblMinMax.Size = new System.Drawing.Size(90, 15);
            this.lblMinMax.Text = "Min() / Max(): -";

     
            this.lblAggregate.AutoSize = true;
            this.lblAggregate.Location = new System.Drawing.Point(15, 117);
            this.lblAggregate.Name = "lblAggregate";
            this.lblAggregate.Size = new System.Drawing.Size(85, 15);
            this.lblAggregate.Text = "Aggregate(): -";

    
            this.btnCalcAggregates.Location = new System.Drawing.Point(12, 400);
            this.btnCalcAggregates.Name = "btnCalcAggregates";
            this.btnCalcAggregates.Size = new System.Drawing.Size(180, 40);
            this.btnCalcAggregates.TabIndex = 2;
            this.btnCalcAggregates.Text = "Расчет показателей";
            this.btnCalcAggregates.UseVisualStyleBackColor = true;

          
            this.btnExceptOnlineOnly.Location = new System.Drawing.Point(205, 400);
            this.btnExceptOnlineOnly.Name = "btnExceptOnlineOnly";
            this.btnExceptOnlineOnly.Size = new System.Drawing.Size(180, 40);
            this.btnExceptOnlineOnly.TabIndex = 3;
            this.btnExceptOnlineOnly.Text = "Только онлайн (Except)";
            this.btnExceptOnlineOnly.UseVisualStyleBackColor = true;

        
            this.btnIntersectBoth.Location = new System.Drawing.Point(398, 400);
            this.btnIntersectBoth.Name = "btnIntersectBoth";
            this.btnIntersectBoth.Size = new System.Drawing.Size(180, 40);
            this.btnIntersectBoth.TabIndex = 4;
            this.btnIntersectBoth.Text = "В обоих каналах (Intersect)";
            this.btnIntersectBoth.UseVisualStyleBackColor = true;

           
            this.btnUnionAllUnique.Location = new System.Drawing.Point(592, 400);
            this.btnUnionAllUnique.Name = "btnUnionAllUnique";
            this.btnUnionAllUnique.Size = new System.Drawing.Size(180, 40);
            this.btnUnionAllUnique.TabIndex = 5;
            this.btnUnionAllUnique.Text = "Уникальные (Union)";
            this.btnUnionAllUnique.UseVisualStyleBackColor = true;

            
            this.ClientSize = new System.Drawing.Size(784, 455);
            this.Controls.Add(this.btnUnionAllUnique);
            this.Controls.Add(this.btnIntersectBoth);
            this.Controls.Add(this.btnExceptOnlineOnly);
            this.Controls.Add(this.btnCalcAggregates);
            this.Controls.Add(this.gbAggregates);
            this.Controls.Add(this.dgvResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LINQ: Агрегация и Множества (SQLite)";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.gbAggregates.ResumeLayout(false);
            this.gbAggregates.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.GroupBox gbAggregates;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.Label lblAvg;
        private System.Windows.Forms.Label lblMinMax;
        private System.Windows.Forms.Label lblAggregate;
        private System.Windows.Forms.Button btnCalcAggregates;
        private System.Windows.Forms.Button btnExceptOnlineOnly;
        private System.Windows.Forms.Button btnIntersectBoth;
        private System.Windows.Forms.Button btnUnionAllUnique;
    }
}