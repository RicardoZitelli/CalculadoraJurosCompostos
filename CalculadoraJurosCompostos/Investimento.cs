using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraJurosCompostos
{
    public sealed class Investimento
    {
        public double ValorMensal { get; set; }
        public double TaxaDeJurosMensal { get; set; }
        public int Periodo { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public double TotalInvestido { get; set; }
        public double TotalJuros { get; set; }
        public double TotalAcumulado { get; set; }
        public double Juros { get; set; }

        public Investimento(double valorMensal
            , double taxaDeJurosMensal
            , int periodo
            , double totalInvestido
            , double totalJuros
            , double totalAcumulado
            , double juros
            , int ano
            , int mes)
        {
            ValorMensal = valorMensal;
            TaxaDeJurosMensal = taxaDeJurosMensal;
            Periodo = periodo;
            TotalInvestido = totalInvestido;
            TotalJuros = totalJuros;
            TotalAcumulado = totalAcumulado;
            Juros = juros;
            Ano = ano;
            Mes = mes;
        }

        /// <summary>
        /// Processes the investment over a given period, calculating and adding the monthly investment, interest, and total accumulated amount to a DataGridView.
        /// </summary>
        public Investimento Processar()
        {
            try
            {
                Juros = Math.Round(TotalAcumulado * TaxaDeJurosMensal / 100, 2);
                TotalJuros += Juros;
                TotalInvestido += ValorMensal;
                TotalAcumulado = TotalInvestido + TotalJuros;
            }
            catch (Exception)
            {
                throw;
            }

            return new Investimento(ValorMensal, TaxaDeJurosMensal, Periodo, TotalInvestido, TotalJuros, TotalAcumulado, Juros, Ano, Mes);
        }

        /// <summary>
        /// Verifies and updates the month, resetting to 1 if it exceeds 12.
        /// </summary>
        public void IdentificarMes()
        {
            Mes++;

            if (Mes > 12)
                Mes = 1;
        }

        /// <summary>
        /// Identifies the year based on the given month count.
        /// If the month count is less than 12, the year is set to 1.
        /// Otherwise, the year is calculated by dividing the month count by 12 and rounding up to the nearest integer.
        /// </summary>
        /// <param name="i">The month count.</param>
        public void IdentificarAno(int i)
        {            
            Ano = i < 12 ? 1 : (int)Math.Ceiling((i / 12d));
        }
    }
}