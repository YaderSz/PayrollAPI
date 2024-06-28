namespace Payroll
{
    partial class DeductionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            mskID = new MaskedTextBox();
            label9 = new Label();
            dgvEgresos = new DataGridView();
            btnEliminar = new Button();
            btnCrear = new Button();
            btnActualizar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEgresos).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Stencil", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(139, 11);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(155, 39);
            label2.TabIndex = 9;
            label2.Text = "EGRESOS";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // mskID
            // 
            mskID.Location = new Point(169, 66);
            mskID.Margin = new Padding(4);
            mskID.Mask = "000";
            mskID.Name = "mskID";
            mskID.Size = new Size(32, 31);
            mskID.TabIndex = 27;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(15, 71);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(143, 25);
            label9.TabIndex = 26;
            label9.Text = "ID Empleado";
            label9.TextAlign = ContentAlignment.TopCenter;
            // 
            // dgvEgresos
            // 
            dgvEgresos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEgresos.Location = new Point(12, 202);
            dgvEgresos.Margin = new Padding(4);
            dgvEgresos.Name = "dgvEgresos";
            dgvEgresos.ReadOnly = true;
            dgvEgresos.RowHeadersWidth = 51;
            dgvEgresos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEgresos.Size = new Size(415, 358);
            dgvEgresos.TabIndex = 28;
            dgvEgresos.CellClick += dgvEgresos_CellClick;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(310, 136);
            btnEliminar.Margin = new Padding(4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(118, 36);
            btnEliminar.TabIndex = 44;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(18, 136);
            btnCrear.Margin = new Padding(4);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(118, 36);
            btnCrear.TabIndex = 43;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(160, 136);
            btnActualizar.Margin = new Padding(4);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(121, 36);
            btnActualizar.TabIndex = 42;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // DeductionForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(445, 641);
            Controls.Add(btnEliminar);
            Controls.Add(btnCrear);
            Controls.Add(btnActualizar);
            Controls.Add(dgvEgresos);
            Controls.Add(mskID);
            Controls.Add(label9);
            Controls.Add(label2);
            Margin = new Padding(4);
            Name = "DeductionForm";
            Text = "DeductionForm";
            Load += DeductionForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEgresos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private MaskedTextBox mskID;
        private Label label9;
        private DataGridView dgvEgresos;
        private Button btnEliminar;
        private Button btnCrear;
        private Button btnActualizar;
    }
}