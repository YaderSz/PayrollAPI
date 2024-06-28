using PayrolAPI.Repository;
using SharedModels.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Payroll
{
    public partial class IncomesForm : Form
    {
        private readonly ApiClient _apiClient;
        public CalculosRepository _calculosRepo = new CalculosRepository();
     
        public IncomesForm(ApiClient apiClient)
        {
            InitializeComponent();
            _apiClient = apiClient;
        }

        private async void IncomesForm_Load(object sender, EventArgs e)
        {
            await LoadIncomesAsync();
        }

        private async Task LoadIncomesAsync()
        {
            try
            {
                var incomes = await _apiClient.Incomes.GetAllAsync();
                dgvIncomes.DataSource = incomes.ToList();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los ingresos: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void btnCrear_Click(object sender, EventArgs e)
        {
            int antiguedad = int.Parse(txtAñosDeAntiguedad.Text);
            double salario = int.Parse(txtSalario.Text);
            int horasextras = int.Parse(txtHorasExtras.Text);
          
            double buffe1 = _calculosRepo.CalcularAntiguedad(antiguedad, salario);
            double buffer2 = _calculosRepo.CalcularHorasextra(salario, horasextras);
            double buffer3;
         if(cmbNocturnidad.Text.ToLower() == "si")
            {
                buffer3 = salario * 0.2;
            }
         else { buffer3 = 0; }
            double buffer4;
            if (cmbRiesgoLaboral.Text.ToLower() == "si")
            {
                buffer4 = salario * 0.2;
            }
            else { buffer4 = 0; }

            var newIncome = new IncomeCreateDto
            {
                CntdHorasExtras = int.Parse(txtHorasExtras.Text),
                Antiguedad = buffe1,
                EmployeeID = int.Parse(txtIdEmpleado.Text),
                SalarioExtraOrdinario = buffer2,
                SalarioOrdinario = salario,
                Nocturnidad = buffer3,
                RiesgoLaboral = buffer4,
                AñosAntiguedad = int.Parse(txtAñosDeAntiguedad.Text),
                TotalIncomes = await _apiClient.Incomes.CalculateTotal(int.Parse(txtIdEmpleado.Text))





        };
            try
            {
                
                
                var sucess = await _apiClient.Incomes.CreateAsync(newIncome);
              
                MessageBox.Show("¡Ingreso agregado exitosamente!",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
                await LoadIncomesAsync();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar ingreso: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtAñosDeAntiguedad.Clear();
            txtHorasExtras.Clear();
          cmbRiesgoLaboral.ResetText();
            cmbNocturnidad.ResetText();
            txtIdEmpleado.Clear();
         
            txtSalario.Clear();
        }

        private void dgvIncomes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var income = (IncomeDto)dgvIncomes.Rows[e.RowIndex].DataBoundItem;
           
              
                double horasNocturnas = ((income.CntdHorasExtras * 30 * 8) / (income.SalarioOrdinario * 0.2));
                txtAñosDeAntiguedad.Text = income.AñosAntiguedad.ToString();
                txtHorasExtras.Text = income.CntdHorasExtras.ToString();
                cmbNocturnidad.Text = income.Nocturnidad.ToString();
                cmbRiesgoLaboral.Text = income.RiesgoLaboral.ToString();
                    
                txtIdEmpleado.Text = income.EmployeeID.ToString();
                
                txtSalario.Text = income.SalarioOrdinario.ToString();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvIncomes.SelectedRows.Count > 0)
            {
                var selectedIncome = (IncomeDto)dgvIncomes.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"¿Está seguro de que desea eliminar el " +
                    $"ingreso con ID'{selectedIncome.IncomeId}'?", "Confirmación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var sucess =
                            await _apiClient.Incomes.DeleteAsync(selectedIncome.IncomeId);

                        if (sucess)
                        {
                            MessageBox.Show("¡Ingreso eliminado exitosamente!", "¡Éxito!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadIncomesAsync();
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

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvIncomes.SelectedRows.Count > 0)
            {
                int antiguedad = int.Parse(txtAñosDeAntiguedad.Text);
                int salario = int.Parse(txtSalario.Text);
                int horasextras = int.Parse(txtHorasExtras.Text);
                double buffer3;
                if (cmbNocturnidad.Text.ToLower() == "si")
                {
                    buffer3 = salario * 0.2;
                }
                else { buffer3 = 0; }
                double buffer4;
                if (cmbRiesgoLaboral.Text.ToLower() == "si")
                {
                    buffer4 = salario * 0.2;
                }
                else { buffer4 = 0; }
                var selectedIncomes = (IncomeDto)dgvIncomes.SelectedRows[0].DataBoundItem;
                var updateIncomes = new IncomeUpdateDto
                {

                    IncomeId = selectedIncomes.IncomeId,
                    CntdHorasExtras = int.Parse(txtHorasExtras.Text),
                    Antiguedad = _calculosRepo.CalcularAntiguedad(antiguedad, salario),
                    EmployeeID = int.Parse(txtIdEmpleado.Text),
                    SalarioExtraOrdinario = _calculosRepo.CalcularHorasextra(salario, horasextras),
                    SalarioOrdinario = salario,
                    Nocturnidad = buffer3,
                    RiesgoLaboral = buffer4,
                    AñosAntiguedad = int.Parse(txtAñosDeAntiguedad.Text),
                    TotalIncomes = await _apiClient.Incomes.CalculateTotal(selectedIncomes.EmployeeID)




                };

                try
                {
                    var success =
                                await _apiClient.Incomes.UpdateAsync(selectedIncomes.IncomeId, updateIncomes);
                    

                    if (success)
                    {
                        MessageBox.Show("¡Ingreso actualizado exitosamente!",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputFields();
                        await LoadIncomesAsync();
                        
                    }
                    else
                    {
                        MessageBox.Show($"Error al actualizar Ingreso.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar ingreso: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Seleccione un ingreso para actualizar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
