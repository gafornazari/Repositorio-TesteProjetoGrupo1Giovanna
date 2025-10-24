using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteProjetoGrupo1Giovanna
{
    public class Medicine
    {
        public string CDB { get; private set; }
        public string Nome { get; private set; }
        public char Categoria { get; private set; }
        public decimal ValorVenda { get; private set; }
        public DateOnly UltimaVenda { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public char Situacao { get; private set; }

        public Medicine()
        {
        }
        public Medicine(string cdb, string nome, char categoria, decimal valorVenda)
        {
            CDB = cdb;
            Nome = nome;
            Categoria = categoria;
            ValorVenda = valorVenda;
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public void SetValorVenda(decimal valor)
        {
            ValorVenda = valor;
        }

        public void SetSituacao(char sit)
        {
            Situacao = sit;
        }
        public override string ToString()
        {
            return $"CDB: {CDB}, Nome: {Nome}, Categoria: {Categoria}, Valor Venda: {ValorVenda}," +
                $" Ultima Venda: {UltimaVenda}, Data Cadastro: {DataCadastro}, Situação: {Situacao}";
        }

    }
}