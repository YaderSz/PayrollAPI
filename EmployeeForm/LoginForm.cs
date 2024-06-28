using Payroll;

namespace EmployeeForm
{
    public partial class LoginForm : Form
    {
        private readonly ApiClient _apiClient;
        public LoginForm()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
           await LoginAsync();


        }
        private async Task LoginAsync()
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;

            var token = await _apiClient.LoginUsers.AuthenticateUserAsync(username, password);

            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Login successful!");

                    // Guardar el token para futuras solicitudes
                    _apiClient.SetAuthToken(token);

                    Hide();
                    var mainForm = new PayrollForm(_apiClient);
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Login failed. Please check your username and password.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Login failed. Please check your username and password." + ex.Message);
            }
        }
            private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
