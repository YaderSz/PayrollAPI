using PayrolAPI.Repository;
using SharedModels.Dto;
using System.Windows.Forms;

namespace Payroll
{
    public partial class DeductionForm : Form
    {
        private readonly ApiClient _apiClient;

        public CalculosRepository _calculosRepo = new CalculosRepository();


        public DeductionForm(ApiClient apiClient)
        {
            InitializeComponent();
            _apiClient = apiClient;
        }

        private async void DeductionForm_Load(object sender, EventArgs e)
        {
            await LoadDeductionAsync();
        }

        private async Task LoadDeductionAsync()
        {
            try
            {
                var deduction = await _apiClient.Deductions.GetAllAsync();
                dgvEgresos.DataSource = deduction.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los egresos: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm(_apiClient);
            double salario = await form.funcion(int.Parse(mskID.Text));

            var newDeduction = new DeductionCreateDto
            {
                EmployeeID = int.Parse(mskID.Text),
                Salario = salario,
                TotalDeductions = await _apiClient.Deductions.CalculateTotal(int.Parse(mskID.Text))

            };
            try
            {


                var sucess = await _apiClient.Deductions.CreateAsync(newDeduction);

                MessageBox.Show("¡Egreso agregado exitosamente!",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
                await LoadDeductionAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar Egreso: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            mskID.Clear();

        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvEgresos.SelectedRows.Count > 0)
            {
                EmployeeForm form = new EmployeeForm(_apiClient);
                var selectedDeduction = (DeductionDto)dgvEgresos.SelectedRows[0].DataBoundItem;
                double salario = await form.funcion(int.Parse(mskID.Text));
                var updateDeductions = new DeductionUpdateDto
                {

                    DeductionID = selectedDeduction.DeductionID,
                    EmployeeID = int.Parse(mskID.Text),
                    INSS = _calculosRepo.CalcularINSS(salario),
                    IR = _calculosRepo.CalcularIR(salario, _calculosRepo.CalcularINSS(salario)),
                    TotalDeductions = await _apiClient.Deductions.CalculateTotal(int.Parse(mskID.Text))






                };

                try
                {
                    var success =
                                await _apiClient.Deductions.UpdateAsync(selectedDeduction.DeductionID, updateDeductions);


                    if (success)
                    {
                        MessageBox.Show("¡Egreso actualizado exitosamente!",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputFields();
                        await LoadDeductionAsync();

                    }
                    else
                    {
                        MessageBox.Show($"Error al actualizar egreso.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar egreso: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Seleccione un ingreso para actualizar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvEgresos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var deduction = (DeductionDto)dgvEgresos.Rows[e.RowIndex].DataBoundItem;


                mskID.Text = deduction.EmployeeID.ToString();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEgresos.SelectedRows.Count > 0)
            {
                var selectedDeduction = (DeductionDto)dgvEgresos.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"¿Está seguro de que desea eliminar el " +
                    $"egreso con ID'{selectedDeduction.DeductionID}'?", "Confirmación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var sucess =
                            await _apiClient.Deductions.DeleteAsync(selectedDeduction.DeductionID);

                        if (sucess)
                        {
                            MessageBox.Show("¡Egreso eliminado exitosamente!", "¡Éxito!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadDeductionAsync();
                        }
                        else
                        {
                            MessageBox.Show($"Error al eliminar el Egreso.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el Egreso: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un egreso para eliminar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
