using PayrolAPI.Repository;
using PayrollAPI.Data;
using SharedModels;
using SharedModels.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll
{
    public partial class EmployeeForm : Form
    {
        private readonly ApiClient _apiClient;
        
        private CalculosRepository _calculosRepo = new CalculosRepository();
       
        public EmployeeForm(ApiClient apiClient)
        {
            InitializeComponent();
            _apiClient = apiClient;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private async void EmployeeForm_Load(object sender, EventArgs e)
        {
            await LoadEmployeeAsync();
        }

        private async Task LoadEmployeeAsync()
        {
            try
            {
                var employee = await _apiClient.Employees.GetAllAsync();
                dgvEmpleados.DataSource = employee.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los empleados: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IncomesForm incomesForm = new IncomesForm(_apiClient);
            incomesForm.ShowDialog();
        }

        private void btnDeducciones_Click(object sender, EventArgs e)
        {
            DeductionForm deductionForm = new DeductionForm(_apiClient);
            deductionForm.ShowDialog();
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            var newEmployee = new EmployeeCreateDto
            {
                Activo = chkActivo.Checked,
                Cedula = txtCedula.Text,
                Celular = int.Parse(mskCelular.Text),
                Contratacion = DateOnly.Parse(dtpContrato.Text),
                Direccion = txtDireccion.Text,
                EstadoCivil = cmbEstado.Text,
                FinContratacion = DateOnly.Parse(dtpFinContrato.Text),
                FirstName = txtPrimerNombre.Text,
                LastName = txtPrimerApellido.Text,
                LastName2 = txtSecondApellido.Text,
                Nacimiento = DateOnly.Parse(dtpNacimiento.Text),
                PayrollId = int.Parse(txtPayrollId.Text),
                RUC = int.Parse(txtRUC.Text),
                SecondName = txtSecondNombre.Text,
                Sexo = cmbSexo.Text,
                Telefono = int.Parse(mskTelefono.Text)
                
                



            };
            try
            {


                var sucess = await _apiClient.Employees.CreateAsync(newEmployee);

                MessageBox.Show("¡Empleado agregado exitosamente!",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
                await LoadEmployeeAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar Empleado: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            chkActivo.Checked = false;
            txtCedula.Clear();
            mskCelular.Clear();
            dtpContrato.ResetText();
            txtDireccion.Clear();
            cmbEstado.ResetText();
            dtpFinContrato.ResetText();
            txtPrimerNombre.Clear();
            txtPrimerApellido.Clear();
            txtSecondApellido.Clear();
            dtpNacimiento.ResetText();
            txtRUC.Clear();
            txtSecondNombre.Clear();
            cmbSexo.ResetText();
            mskTelefono.Clear();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                var selectedEmployee = (EmployesDto)dgvEmpleados.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"¿Está seguro de que desea eliminar el " +
                    $"empleado con ID'{selectedEmployee.EmployeeID}'?", "Confirmación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var sucess =
                            await _apiClient.Employees.DeleteAsync(selectedEmployee.EmployeeID);

                        if (sucess)
                        {
                            MessageBox.Show("¡Empleado eliminado exitosamente!", "¡Éxito!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadEmployeeAsync();
                        }
                        else
                        {
                            MessageBox.Show($"Error al eliminar el empleado.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el empleado: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un empleado para eliminar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                IncomesForm incomesForm = new IncomesForm(_apiClient);
                var selectedEmployees = (EmployesDto)dgvEmpleados.SelectedRows[0].DataBoundItem;
                var updateEmployee = new EmployeeUpdateDto
                {
                    EmployeeID = selectedEmployees.EmployeeID,
                    Activo = chkActivo.Checked,
                    Cedula = txtCedula.Text,
                    Celular = int.Parse(mskCelular.Text),
                    Contratacion = DateOnly.Parse(dtpContrato.Text),
                    Direccion = txtDireccion.Text,
                    EstadoCivil = cmbEstado.Text,
                    FinContratacion = DateOnly.Parse(dtpFinContrato.Text),
                    FirstName = txtPrimerNombre.Text,
                    LastName = txtPrimerApellido.Text,
                    LastName2 = txtSecondApellido.Text,
                    Nacimiento = DateOnly.Parse(dtpNacimiento.Text),
                    PayrollId = int.Parse(txtPayrollId.Text),
                    RUC = int.Parse(txtRUC.Text),
                    SecondName = txtSecondNombre.Text,
                    Sexo = cmbSexo.Text,
                    Telefono = int.Parse(mskTelefono.Text),
                    SalarioNeto = await _apiClient.Employees.CalculateTotal(selectedEmployees.EmployeeID),
                 
                    




                };

                try
                {
                    var success =
                                await _apiClient.Employees.UpdateAsync(selectedEmployees.EmployeeID, updateEmployee);
                    

                    if (success)
                    {
                        MessageBox.Show("¡Empleado actualizado exitosamente!",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputFields();
                        await LoadEmployeeAsync();

                    }
                    else
                    {
                        MessageBox.Show($"Error al actualizar empleado.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar empleado: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Seleccione un empleado para actualizar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public async Task<double> funcion(int id)
        {
            
            return await _apiClient.Employees.CalculateTotal(id);
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                var employee = (EmployesDto)dgvEmpleados.Rows[e.RowIndex].DataBoundItem;

                txtCedula.Text = employee.Cedula;
                txtDireccion.Text = employee.Direccion;
                txtPayrollId.Text = employee.PayrollId.ToString();
                txtPrimerApellido.Text = employee.LastName;
                txtPrimerNombre.Text = employee.FirstName;
                txtRUC.Text = employee.RUC.ToString();
                txtSecondApellido.Text = employee.LastName2;
                txtSecondNombre.Text = employee.SecondName;
                cmbSexo.Text= employee.Sexo;
                cmbEstado.Text = employee.EstadoCivil;
                dtpContrato.Text = employee.Contratacion.ToString();
                dtpFinContrato.Text = employee.FinContratacion.ToString();
                dtpNacimiento.Text=employee.Nacimiento.ToString();
                chkActivo.Checked= employee.Activo;
                mskCelular.Text = employee.Cedula.ToString();
                mskTelefono.Text=employee.Telefono.ToString();


                
                
            }
        }
    }
}
