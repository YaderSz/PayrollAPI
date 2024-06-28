using SharedModels.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll
{
    public partial class PayrollForm : Form
    {
        private readonly ApiClient _apiClient;
        public PayrollForm(ApiClient apiClient)
        {
            InitializeComponent();
            _apiClient = apiClient;
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm(_apiClient);
            employeeForm.ShowDialog();
        }

        private async void PayrollForm_Load(object sender, EventArgs e)
        {
            await LoadPayrollForm();
        }

        private async Task LoadPayrollForm()
        {
            try
            {
                var payroll = await _apiClient.Payrolls.GetAllAsync();
                dgvPayroll.DataSource = payroll.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la Nomina: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {







            var newPayroll = new PayrollCreateDto
            {

                FechaPeriodo = DateOnly.Parse(dtpNomina.Text)





            };
            try
            {


                var sucess = await _apiClient.Payrolls.CreateAsync(newPayroll);

                MessageBox.Show("¡Nomina agregado exitosamente!",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
                await LoadPayrollForm();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar Empleado: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            dtpNomina.ResetText();
        }

        private void dgvPayroll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var payroll = (PayrollDto)dgvPayroll.Rows[e.RowIndex].DataBoundItem;


                dtpNomina.Text = payroll.FechaPeriodo.ToString();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (dgvPayroll.SelectedRows.Count > 0)
            {

                var selectedPayroll = (PayrollDto)dgvPayroll.SelectedRows[0].DataBoundItem;

                var updatePayrolls = new PayrollUpdateDto
                {

                    FechaPeriodo = DateOnly.Parse(dtpNomina.Text),
                    PayrollId = selectedPayroll.PayrollId,







                };

                try
                {
                    var success =
                                await _apiClient.Payrolls.UpdateAsync(selectedPayroll.PayrollId, updatePayrolls);


                    if (success)
                    {
                        MessageBox.Show("¡Nomina actualizado exitosamente!",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputFields();
                        await LoadPayrollForm();

                    }
                    else
                    {
                        MessageBox.Show($"Error al actualizar Nomina.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar nomina: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Seleccione un nomina para actualizar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (dgvPayroll.SelectedRows.Count > 0)
            {
                var selectedIncome = (PayrollDto)dgvPayroll.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"¿Está seguro de que desea eliminar el " +
                    $"Nomina con ID'{selectedIncome.PayrollId}'?", "Confirmación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var sucess =
                            await _apiClient.Incomes.DeleteAsync(selectedIncome.PayrollId);

                        if (sucess)
                        {
                            MessageBox.Show("¡Nomina eliminado exitosamente!", "¡Éxito!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadPayrollForm();
                        }
                        else
                        {
                            MessageBox.Show($"Error al eliminar el ingreso.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el ingreso: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un ingreso para eliminar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
