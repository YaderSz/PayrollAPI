using PayrolAPI.Repository.IRepository;

namespace PayrolAPI.Repository
{
    public class CalculosRepository : ICalculosIRepository
    {
        public double CalcularAntiguedad(int Antiguedad, Double Salariobasico)
        {
            double buffer ;
            if (Antiguedad <= 3 )
            {
                buffer = Salariobasico * Antiguedad;
                return buffer;
            }

                if(Antiguedad > 3)
                     {
                     if (Antiguedad > 5)
                         {
                    buffer = (Salariobasico / 30) * 20 * 5;
                    return buffer;

                         }
                buffer = (Salariobasico / 30) * 20 * Antiguedad;
                return buffer;
            }
            return 1;
        }

        public double CalcularDeducciones(double INSS, double IR)
        {
            double Deducciones = INSS + IR;
            return Deducciones;
        }



        public double CalcularINSS(double Salariobasico)
        {
            double CINSS = Salariobasico * 0.07;
            return CINSS;
        }

        public double CalcularIR(double Salariobruto, double INSS)
        {
            double buffersalario;
            double buffersalario2;

            //Tasas de aplicacion
            double tasa;

            //sobreExceso
            double sobreexceso;

            double Ir;
            buffersalario = Salariobruto - INSS;



            if (buffersalario >= 0.01 && buffersalario <= 8833.33)
            {

                return 0;
            }


            else if (buffersalario >= 8333.33 && buffersalario <= 16666.66)
            {

                sobreexceso = 8333.33;

                buffersalario = buffersalario - sobreexceso;

                buffersalario2 = (buffersalario * 0.15);

                Ir = buffersalario2;

                return Ir;
            }

            else if (buffersalario >= 16666.67 && buffersalario <= 29166.66)
            {

                sobreexceso = 16666.66;
                tasa = 1250;

                buffersalario = buffersalario - sobreexceso;

                buffersalario2 = (buffersalario * 0.2) + tasa;

                Ir = buffersalario2;

                return Ir;
            }

            else if (buffersalario >= 29166.67 && buffersalario <= 41666.66)
            {

                sobreexceso = 29166.66;
                tasa = 3750;

                buffersalario = buffersalario - sobreexceso;

                buffersalario2 = (buffersalario * 0.25) + tasa;

                Ir = buffersalario2;

                return Ir;
            }

            else if (buffersalario >= 41666.67)
            {

                sobreexceso = 41666.66;
                tasa = 6875;

                buffersalario = buffersalario - sobreexceso;

                buffersalario2 = (buffersalario * 0.3) + tasa;

                Ir = buffersalario2;

                return Ir;
            }

            return 0;
        }
        public double Deducciones(double INSS, double IR)
        {
            double Deduc =  INSS + IR;
            return Deduc;
        }
        public double CalcularHorasextra(double Salariobasico, int Horasextra)
        {
            double buffer;
            buffer = ((Salariobasico) / 30) / 8;
            double Thorasextra = (Horasextra * buffer) * 2;

            return Thorasextra;
        }
        public double CalcularNocturnidad(int cntdHoras, double Salariobasico)
        {
            double buffer = ((Salariobasico*0.2) / 30) ;
            double Nocturnidad = (buffer * cntdHoras) ;
            return Nocturnidad;
        }

        public double CalcularSalarioBruto(double Salariobasico, double Thorasextra, double Nocturnidad, double Antiguedad)
        {
            double Salariobruto = Salariobasico + Thorasextra + Nocturnidad + Antiguedad;
            return Salariobruto;
        }

        public double CalcularSalarioNeto(double Salariobruto, double Deduc)
        {
            double SalarioNeto = Salariobruto - Deduc;

            return SalarioNeto;
        }
    }


}

