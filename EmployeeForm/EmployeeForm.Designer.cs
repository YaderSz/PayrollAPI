namespace Payroll
{
    partial class EmployeeForm
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
            txtRUC = new TextBox();
            label3 = new Label();
            txtPrimerNombre = new TextBox();
            label4 = new Label();
            txtSecondNombre = new TextBox();
            label5 = new Label();
            txtSecondApellido = new TextBox();
            label6 = new Label();
            txtPrimerApellido = new TextBox();
            label7 = new Label();
            dtpNacimiento = new DateTimePicker();
            label8 = new Label();
            label9 = new Label();
            mskCelular = new MaskedTextBox();
            mskTelefono = new MaskedTextBox();
            label10 = new Label();
            cmbSexo = new ComboBox();
            label11 = new Label();
            label12 = new Label();
            cmbEstado = new ComboBox();
            label13 = new Label();
            dtpContrato = new DateTimePicker();
            label14 = new Label();
            dtpFinContrato = new DateTimePicker();
            chkActivo = new CheckBox();
            txtDireccion = new TextBox();
            label15 = new Label();
            btnEliminar = new Button();
            btnCrear = new Button();
            btnActualizar = new Button();
            dgvEmpleados = new DataGridView();
            btnDeducciones = new Button();
            btnIngresos = new Button();
            label16 = new Label();
            txtCedula = new TextBox();
            txtPayrollId = new TextBox();
            label17 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvEmpleados).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Stencil", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(424, 11);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(356, 39);
            label2.TabIndex = 7;
            label2.Text = "Datos De Empleados";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtRUC
            // 
            txtRUC.Location = new Point(318, 78);
            txtRUC.Margin = new Padding(4);
            txtRUC.Multiline = true;
            txtRUC.Name = "txtRUC";
            txtRUC.Size = new Size(134, 42);
            txtRUC.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(256, 88);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(53, 25);
            label3.TabIndex = 10;
            label3.Text = "RUC";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtPrimerNombre
            // 
            txtPrimerNombre.Location = new Point(162, 134);
            txtPrimerNombre.Margin = new Padding(4);
            txtPrimerNombre.Multiline = true;
            txtPrimerNombre.Name = "txtPrimerNombre";
            txtPrimerNombre.Size = new Size(180, 42);
            txtPrimerNombre.TabIndex = 13;
            txtPrimerNombre.TextChanged += textBox3_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(20, 144);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(131, 25);
            label4.TabIndex = 12;
            label4.Text = "First Name";
            label4.TextAlign = ContentAlignment.TopCenter;
            label4.Click += label4_Click;
            // 
            // txtSecondNombre
            // 
            txtSecondNombre.Location = new Point(515, 134);
            txtSecondNombre.Margin = new Padding(4);
            txtSecondNombre.Multiline = true;
            txtSecondNombre.Name = "txtSecondNombre";
            txtSecondNombre.Size = new Size(180, 42);
            txtSecondNombre.TabIndex = 15;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(356, 144);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(149, 25);
            label5.TabIndex = 14;
            label5.Text = "Second Name";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtSecondApellido
            // 
            txtSecondApellido.Location = new Point(1204, 130);
            txtSecondApellido.Margin = new Padding(4);
            txtSecondApellido.Multiline = true;
            txtSecondApellido.Name = "txtSecondApellido";
            txtSecondApellido.Size = new Size(180, 42);
            txtSecondApellido.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(1001, 139);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(190, 25);
            label6.TabIndex = 16;
            label6.Text = "Second Surname";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtPrimerApellido
            // 
            txtPrimerApellido.Location = new Point(814, 134);
            txtPrimerApellido.Margin = new Padding(4);
            txtPrimerApellido.Multiline = true;
            txtPrimerApellido.Name = "txtPrimerApellido";
            txtPrimerApellido.Size = new Size(180, 42);
            txtPrimerApellido.TabIndex = 19;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(702, 140);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(108, 25);
            label7.TabIndex = 18;
            label7.Text = "Surname";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // dtpNacimiento
            // 
            dtpNacimiento.Format = DateTimePickerFormat.Short;
            dtpNacimiento.Location = new Point(164, 202);
            dtpNacimiento.Margin = new Padding(4);
            dtpNacimiento.MaxDate = new DateTime(2999, 6, 26, 0, 0, 0, 0);
            dtpNacimiento.MinDate = new DateTime(1940, 1, 1, 0, 0, 0, 0);
            dtpNacimiento.Name = "dtpNacimiento";
            dtpNacimiento.Size = new Size(152, 31);
            dtpNacimiento.TabIndex = 20;
            dtpNacimiento.Value = new DateTime(1940, 1, 1, 0, 0, 0, 0);
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(16, 206);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(134, 25);
            label8.TabIndex = 21;
            label8.Text = "Nacimiento";
            label8.TextAlign = ContentAlignment.TopCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(471, 88);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(100, 25);
            label9.TabIndex = 22;
            label9.Text = "Celular";
            label9.TextAlign = ContentAlignment.TopCenter;
            // 
            // mskCelular
            // 
            mskCelular.Location = new Point(581, 82);
            mskCelular.Margin = new Padding(4);
            mskCelular.Mask = "00000000";
            mskCelular.Name = "mskCelular";
            mskCelular.Size = new Size(76, 31);
            mskCelular.TabIndex = 23;
            // 
            // mskTelefono
            // 
            mskTelefono.Location = new Point(786, 79);
            mskTelefono.Margin = new Padding(4);
            mskTelefono.Mask = "00000000";
            mskTelefono.Name = "mskTelefono";
            mskTelefono.Size = new Size(76, 31);
            mskTelefono.TabIndex = 25;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(666, 84);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(111, 25);
            label10.TabIndex = 24;
            label10.Text = "Telefono";
            label10.TextAlign = ContentAlignment.TopCenter;
            // 
            // cmbSexo
            // 
            cmbSexo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSexo.FormattingEnabled = true;
            cmbSexo.Items.AddRange(new object[] { "Masculino", "Femenino", "No identificado" });
            cmbSexo.Location = new Point(936, 78);
            cmbSexo.Margin = new Padding(4);
            cmbSexo.Name = "cmbSexo";
            cmbSexo.Size = new Size(122, 33);
            cmbSexo.TabIndex = 27;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(871, 82);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(62, 25);
            label11.TabIndex = 28;
            label11.Text = "Sexo";
            label11.TextAlign = ContentAlignment.TopCenter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.Location = new Point(1075, 81);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(145, 25);
            label12.TabIndex = 30;
            label12.Text = "Estado Civil";
            label12.TextAlign = ContentAlignment.TopCenter;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Items.AddRange(new object[] { "Casado/a", "Soltero/a", "Viudo/a", "Divorciado/a" });
            cmbEstado.Location = new Point(1231, 76);
            cmbEstado.Margin = new Padding(4);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(122, 33);
            cmbEstado.TabIndex = 29;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.Location = new Point(324, 206);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(163, 25);
            label13.TabIndex = 32;
            label13.Text = "Contratacion";
            label13.TextAlign = ContentAlignment.TopCenter;
            // 
            // dtpContrato
            // 
            dtpContrato.Format = DateTimePickerFormat.Short;
            dtpContrato.Location = new Point(499, 202);
            dtpContrato.Margin = new Padding(4);
            dtpContrato.MaxDate = new DateTime(2999, 6, 26, 0, 0, 0, 0);
            dtpContrato.MinDate = new DateTime(1940, 1, 1, 0, 0, 0, 0);
            dtpContrato.Name = "dtpContrato";
            dtpContrato.Size = new Size(152, 31);
            dtpContrato.TabIndex = 31;
            dtpContrato.Value = new DateTime(1940, 1, 1, 0, 0, 0, 0);
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(666, 206);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(202, 25);
            label14.TabIndex = 34;
            label14.Text = "Fin Contratacion";
            label14.TextAlign = ContentAlignment.TopCenter;
            // 
            // dtpFinContrato
            // 
            dtpFinContrato.Format = DateTimePickerFormat.Short;
            dtpFinContrato.Location = new Point(878, 202);
            dtpFinContrato.Margin = new Padding(4);
            dtpFinContrato.MaxDate = new DateTime(2999, 6, 26, 0, 0, 0, 0);
            dtpFinContrato.MinDate = new DateTime(1940, 1, 1, 0, 0, 0, 0);
            dtpFinContrato.Name = "dtpFinContrato";
            dtpFinContrato.Size = new Size(152, 31);
            dtpFinContrato.TabIndex = 33;
            dtpFinContrato.Value = new DateTime(1940, 1, 1, 0, 0, 0, 0);
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Location = new Point(1069, 204);
            chkActivo.Margin = new Padding(4);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(88, 29);
            chkActivo.TabIndex = 36;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(162, 259);
            txtDireccion.Margin = new Padding(4);
            txtDireccion.Multiline = true;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(410, 42);
            txtDireccion.TabIndex = 38;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.Location = new Point(20, 269);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(120, 25);
            label15.TabIndex = 37;
            label15.Text = "Direccion";
            label15.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(581, 350);
            btnEliminar.Margin = new Padding(4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(118, 36);
            btnEliminar.TabIndex = 41;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(226, 350);
            btnCrear.Margin = new Padding(4);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(118, 36);
            btnCrear.TabIndex = 40;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(405, 350);
            btnActualizar.Margin = new Padding(4);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(121, 36);
            btnActualizar.TabIndex = 39;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // dgvEmpleados
            // 
            dgvEmpleados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmpleados.Location = new Point(15, 408);
            dgvEmpleados.Margin = new Padding(4);
            dgvEmpleados.Name = "dgvEmpleados";
            dgvEmpleados.ReadOnly = true;
            dgvEmpleados.RowHeadersWidth = 51;
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.Size = new Size(1370, 255);
            dgvEmpleados.TabIndex = 42;
            dgvEmpleados.CellClick += dgvEmpleados_CellClick;
            // 
            // btnDeducciones
            // 
            btnDeducciones.Location = new Point(958, 350);
            btnDeducciones.Margin = new Padding(4);
            btnDeducciones.Name = "btnDeducciones";
            btnDeducciones.Size = new Size(136, 36);
            btnDeducciones.TabIndex = 44;
            btnDeducciones.Text = "Deducciones";
            btnDeducciones.UseVisualStyleBackColor = true;
            btnDeducciones.Click += btnDeducciones_Click;
            // 
            // btnIngresos
            // 
            btnIngresos.Location = new Point(764, 350);
            btnIngresos.Margin = new Padding(4);
            btnIngresos.Name = "btnIngresos";
            btnIngresos.Size = new Size(121, 36);
            btnIngresos.TabIndex = 43;
            btnIngresos.Text = "Ingresos";
            btnIngresos.UseVisualStyleBackColor = true;
            btnIngresos.Click += button5_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label16.Location = new Point(624, 269);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(88, 25);
            label16.TabIndex = 45;
            label16.Text = "Cedula";
            label16.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtCedula
            // 
            txtCedula.Location = new Point(735, 269);
            txtCedula.Margin = new Padding(4);
            txtCedula.Multiline = true;
            txtCedula.Name = "txtCedula";
            txtCedula.Size = new Size(410, 42);
            txtCedula.TabIndex = 46;
            // 
            // txtPayrollId
            // 
            txtPayrollId.Location = new Point(144, 8);
            txtPayrollId.Margin = new Padding(4);
            txtPayrollId.Multiline = true;
            txtPayrollId.Name = "txtPayrollId";
            txtPayrollId.Size = new Size(155, 42);
            txtPayrollId.TabIndex = 47;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label17.Location = new Point(13, 13);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(123, 25);
            label17.TabIndex = 48;
            label17.Text = "PayrollId";
            label17.TextAlign = ContentAlignment.TopCenter;
            // 
            // EmployeeForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1396, 678);
            Controls.Add(label17);
            Controls.Add(txtPayrollId);
            Controls.Add(txtCedula);
            Controls.Add(label16);
            Controls.Add(btnDeducciones);
            Controls.Add(btnIngresos);
            Controls.Add(dgvEmpleados);
            Controls.Add(btnEliminar);
            Controls.Add(btnCrear);
            Controls.Add(btnActualizar);
            Controls.Add(txtDireccion);
            Controls.Add(label15);
            Controls.Add(chkActivo);
            Controls.Add(label14);
            Controls.Add(dtpFinContrato);
            Controls.Add(label13);
            Controls.Add(dtpContrato);
            Controls.Add(label12);
            Controls.Add(cmbEstado);
            Controls.Add(label11);
            Controls.Add(cmbSexo);
            Controls.Add(mskTelefono);
            Controls.Add(label10);
            Controls.Add(mskCelular);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(dtpNacimiento);
            Controls.Add(txtPrimerApellido);
            Controls.Add(label7);
            Controls.Add(txtSecondApellido);
            Controls.Add(label6);
            Controls.Add(txtSecondNombre);
            Controls.Add(label5);
            Controls.Add(txtPrimerNombre);
            Controls.Add(label4);
            Controls.Add(txtRUC);
            Controls.Add(label3);
            Controls.Add(label2);
            Margin = new Padding(4);
            Name = "EmployeeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EmployeeForm";
            Load += EmployeeForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEmpleados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        #endregion

        private Label label2;
        private TextBox txtRUC;
        private Label label3;
        private TextBox txtPrimerNombre;
        private Label label4;
        private TextBox txtSecondNombre;
        private Label label5;
        private TextBox txtSecondApellido;
        private Label label6;
        private TextBox txtPrimerApellido;
        private Label label7;
        private DateTimePicker dtpNacimiento;
        private Label label8;
        private Label label9;
        private MaskedTextBox mskCelular;
        private MaskedTextBox mskTelefono;
        private Label label10;
        private ComboBox cmbSexo;
        private Label label11;
        private Label label12;
        private ComboBox cmbEstado;
        private Label label13;
        private DateTimePicker dtpContrato;
        private Label label14;
        private DateTimePicker dtpFinContrato;
        private CheckBox chkActivo;
        private TextBox txtDireccion;
        private Label label15;
        private Button btnEliminar;
        private Button btnCrear;
        private Button btnActualizar;
        private DataGridView dgvEmpleados;
        private Button btnDeducciones;
        private Button btnIngresos;
        private Label label16;
        private TextBox txtCedula;
        private TextBox txtPayrollId;
        private Label label17;
    }
}