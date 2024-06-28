namespace PayrolAPI.Repository.IRepository
{
    public interface ICalculosIRepository
    {
        public double CalcularHorasextra(double Salariobasico, int Horasextra);
        public double CalcularAntiguedad(int Antiguedad, double Salariobasico);
        public double Deducciones( double INSS, double IR);
        public double CalcularIR(double Salariobruto, double INSS);
        public double CalcularINSS(double Salariobasico);
        public double CalcularDeducciones(double INSS, double IR);
        public double CalcularSalarioBruto(double Salariobasico, double Thorasextra, double Nocturnidad, double Antiguedad);
        public double CalcularNocturnidad(int cntdHoras, double Salariobasico);
        public double CalcularSalarioNeto(double Salariobruto, double Deduc);


    }
}
