using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TesteProjetoGrupo1Giovanna
{
    public class Ingredient
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public DateOnly UltimaCompra { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public char Situacao { get; private set; }

        public Ingredient()
        {

        }
        public Ingredient(string id, string nome)
        {
            Id = id;
            Nome = nome;
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public void SetSituacao(char situacao)
        {
            Situacao = situacao;
        }

        public void SetUltimaCompra(DateOnly ultimacompra)
        {
            UltimaCompra = ultimacompra;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nome: {Nome}, Última compra: {UltimaCompra}, Data cadastro: {DataCadastro}, Situação: {Situacao}";
        }
    }
}