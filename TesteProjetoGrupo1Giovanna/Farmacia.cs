using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TesteProjetoGrupo1Giovanna
{
    public class Farmacia
    {

        public List<Ingredient> ListaIngredients { get; set; }
        public List<Medicine> ListaMedicines { get; set; }

        public Farmacia()
        {
            this.ListaIngredients = new List<Ingredient>();
            this.ListaMedicines = new List<Medicine>();
        }


        //método para verificar se já existe algum ingrediente com o mesmo id
        public bool BuscarIngridientId(string id)
        {
            if (ListaIngredients == null || ListaIngredients.Count == 0)
                return false;
            return ListaIngredients.Any(aux => aux.Id == id);// true - existe | false - não existe
        }


        //Retorna um ingrediente novo para ser inserido na lista
        public void IncluirIngredient()
        {
            int auxId = 0, auxNome = 0;
            string nome, id;
            Console.WriteLine("INGREDIENTE:");

            //vai ficar no laço de repetição até digitar um Id válido
            do
            {
                Console.WriteLine("Qual o Id (Lembrete do formato obrigatório: AI + 4 dígitos, exemplo: AI0000)?");
                id = Console.ReadLine();
                if (id.Length == 6)
                {
                    string sufixo = id.Substring(0, 2);
                    string prefixo = id.Substring(2);
                    if (BuscarIngridientId(id))
                        Console.WriteLine("Erro! Esse Id já é cadastrado!");
                    else if ((!sufixo.Equals("AI", StringComparison.OrdinalIgnoreCase)) || (!prefixo.All(c => char.IsDigit(c))))
                        Console.WriteLine("Erro! O formato do Id está incorreto!\nEle precisa ser composto por AI + 4 dígitos, exemplo: AI0000");
                    else
                        auxId = 1;
                }
                else
                {
                    Console.WriteLine("Erro! O Id que digitou está no formato errado, não contendo apenas 6 caracteres");
                }
            } while (auxId == 0);

            do
            {
                Console.WriteLine("Qual o nome do ingrediente (Lembrete do formato obrigatório: até 20 caracteres, e apenas alfanuméricos)?");
                nome = Console.ReadLine();
                if (nome.Length <= 20)
                {
                    if (!Regex.IsMatch(nome, @"^[a-zA-Z0-9 ]+$"))
                        Console.WriteLine("O nome deve conter apenas caracteres alfanuméricos!");
                    else
                    {
                        auxNome = 1;
                        nome = nome.PadRight(20);
                    }
                }
                else
                {
                    Console.WriteLine("O nome pode ter ATÉ 20 caracteres");
                }
            } while (auxNome == 0);

            //Data UltimaCompra será colocada apenas quando for realizado uma compra
            //DataCadstro será atribuída no próprio construtor com a data atual
            //Situação será atribuída no prórpio construtor como Ativa

            //return new Ingredient(id, nome);
            // Ingredient novo = ;
            ListaIngredients.Add(new Ingredient(id, nome));
            Console.WriteLine("Ingrediente adicionado com sucesso!");
        }

        //Mostra uma mensagem com as informações do ingrediente, caso não ache o Id mostra uma mensagem que não achou
        public void LocalizarIngridient(string id)
        {
            foreach (var ing in ListaIngredients)
            {
                if (ing.Id == id)
                {
                    Console.WriteLine("Ingrediente localizado:\n");
                    Console.WriteLine(ing.ToString());
                    return;
                }
            }
            Console.WriteLine("Ingrediente não localizado!");
        }

        //Altera a situação do ingrediente de Ativo para Inativo
        public void AlterarIngridient(string id)
        {
            foreach (var ing in ListaIngredients)
            {
                if (ing.Id == id)
                {
                    if (ing.Situacao == 'I')
                    {
                        Console.WriteLine("Situação do ingrediente alterada com sucesso! De inativo para ativo!");
                        ing.SetSituacao('A');
                        return;
                    }
                    else if (ing.Situacao == 'A')
                    {
                        Console.WriteLine("Situação do ingrediente alterada com sucesso! De ativo para inativo!");
                        ing.SetSituacao('I');
                        return;
                    }
                }
            }
            Console.WriteLine("Ingrediente não localizado!");
        }

        //É chamada quando há uma nova compra, alterando a data da última compra
        public void AlterarIngridientUltimaCompra(DateOnly ultimacompra, string id)
        {
            foreach (var ing in ListaIngredients)
            {
                if (ing.Id == id)
                {
                    ing.SetUltimaCompra(ultimacompra);
                }
            }
        }

        //Imprime a lista de ingredientes
        public void ImprimirIngridient()
        {
            if (ListaIngredients == null || !ListaIngredients.Any())
                Console.WriteLine("A lista está vazia!");
            else
            {
                foreach (var ing in ListaIngredients)
                {
                    Console.WriteLine(ing.ToString());
                }
            }
        }




        //começo medicine

        //Mostra uma mensagem com as informações do medicamento, caso não ache o Id mostra uma mensagem que não achou
        public Medicine LocalizarMedicine(string cdb)
        {
            foreach (var med in ListaMedicines)
            {
                if (med.CDB == cdb)
                    return med;
            }
            return null;
        }

        //verifica se o CDB passado existe
        public bool VerificacaoCDB(string cdb)
        {
            int[] vetoraux = new int[13];
            vetoraux = cdb.Select(c => int.Parse(c.ToString())).ToArray();
            int somaImpar = 0, somaPar = 0;

            for (int i = 1; i < 13; i++)
            {
                if (i % 2 != 0)
                    somaImpar += vetoraux[i - 1];
                else
                    somaPar += vetoraux[i - 1];
            }

            int somaTotal = somaImpar + somaPar * 3;
            int verificador = 10 - (somaTotal % 10);
            return verificador == vetoraux[12];
        }

        //inclui o novo medicamento
        public void IncluirMedicine()
        {
            string nome, cdb;
            char categoria = ' ';
            int opCategoria;
            int auxNome = 0, auxVenda = 0, auxCategoria = 1, auxCdb = 0;
            decimal valorVenda;
            Console.WriteLine("MEDICAMENTO");

            do
            {
                Console.WriteLine("Qual o código de barras. (Lembrete do formato obrigatório: 13 caracteres e ínicio '789')?");
                cdb = Console.ReadLine();
                if (cdb.Length != 13)
                    Console.WriteLine("Código de barras com tamanho diferente de 13 caracteres!");
                else if((cdb.Substring(0,3))!= "789")
                    Console.WriteLine("Começo do código diferente de '789'");
                else
                {
                    if (VerificacaoCDB(cdb))
                    {
                        if (LocalizarMedicine(cdb) == null)
                            auxCdb = 1;
                        else
                            Console.WriteLine("Código de barras já existente!");
                    }
                    else
                        Console.WriteLine("Código de barras inválido!");
                }
            } while (auxCdb == 0);

            do
            {
                Console.WriteLine("Qual o nome do medicamento (Lembrete do formato obrigatório: até 40 caracteres, e apenas alfanuméricos)?");
                nome = Console.ReadLine();
                if (nome.Length <= 40)
                {
                    if (!Regex.IsMatch(nome, @"^[a-zA-Z0-9 ]+$"))
                        Console.WriteLine("O nome deve conter apenas caracteres alfanuméricos!");
                    else
                    {
                        nome = nome.PadRight(40);
                        auxNome = 1;
                    }
                }
                else
                {
                    Console.WriteLine("O nome pode ter ATÉ 40 caracteres");
                }
            } while (auxNome == 0);

            do
            {
                Console.WriteLine("Qual a categoria do medicamento?\n1 - Analgésico\n2 - Antibiótico\n3 - Anti-inflamatório\n4 - Vitamina");
                
                opCategoria = int.Parse(Console.ReadLine());
                switch (opCategoria)
                {
                    case 1:
                        categoria = 'A';
                        auxCategoria = 1;
                        break;
                    case 2:
                        categoria = 'B';
                        auxCategoria = 1;
                        break;
                    case 3:
                        categoria = 'I';
                        auxCategoria = 1;
                        break;
                    case 4:
                        categoria = 'V';
                        auxCategoria = 1;
                        break;
                    default:
                        Console.WriteLine("Categoria inválida!");
                        auxCategoria = 0;
                        break;

                }
            } while (auxCategoria == 0);

            do
            {
                Console.WriteLine("Qual o valor da venda do medicamento?");
                valorVenda = decimal.Parse(Console.ReadLine());
                if (valorVenda > 0 && valorVenda < 10000)
                {
                    string formatadoValorVenda = valorVenda.ToString("F2").PadLeft(7);
                    auxVenda = 1;
                }
                else
                    Console.WriteLine("Valor inválido! Deve ser > 0 e < 10.000");
            } while (auxVenda == 0);


            ListaMedicines.Add(new Medicine(cdb, nome, categoria, valorVenda));
            Console.WriteLine("Medicamento adicionado com sucesso!");
        }

        //altera as informações do medicamento
        public void AlterarMedicine(string cdb)
        {
            decimal valorVenda;
            int auxVenda = 0;
            Medicine medicine = LocalizarMedicine(cdb);
            Medicine medicine1 = medicine;

            Console.WriteLine("O que deseja alterar?\n1 - Valor da Venda\n2 - Situação do medicamento\n3 - As duas opções");
            int op = int.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    do
                    {
                        Console.WriteLine("Qual o novo valor da venda do medicamento?");
                        valorVenda = decimal.Parse(Console.ReadLine());
                        if (valorVenda > 0 && valorVenda < 10000)
                            auxVenda = 1;
                        else
                            Console.WriteLine("Valor inválido! Deve ser > 0 e < 10.000");
                    } while (auxVenda == 0);
                    medicine.SetValorVenda(valorVenda);
                    Console.WriteLine("Valor alterado com sucesso!");
                    break;
                case 2:
                    foreach (var med in ListaMedicines)
                    {
                        if (med.CDB == cdb)
                        {
                            if (med.Situacao == 'I')
                            {
                                Console.WriteLine("Situação do medicamento alterada com sucesso! De inativo para ativo!");
                                med.SetSituacao('A');
                            }
                            else if (med.Situacao == 'A')
                            {
                                Console.WriteLine("Situação do medicamento alterada com sucesso! De ativo para inativo!");
                                med.SetSituacao('I');
                            }
                            else
                                Console.WriteLine("Medicamento não localizado!");
                        }
                    }
                    break;
                case 3:
                    do
                    {
                        Console.WriteLine("Qual o novo valor da venda do medicamento?");
                        valorVenda = decimal.Parse(Console.ReadLine());
                        if (valorVenda > 0 && valorVenda < 10000)
                            auxVenda = 1;
                        else
                            Console.WriteLine("Valor inválido! Deve ser > 0 e < 10.000");
                    } while (auxVenda == 0);
                    medicine1.SetValorVenda(valorVenda);

                    foreach (var med in ListaMedicines)
                    {
                        if (med.CDB == cdb)
                        {
                            if (med.Situacao == 'I')
                            {
                                Console.WriteLine("Situação do medicamento e valor alteradas com sucesso! De inativo para ativo!");
                                med.SetSituacao('A');
                            }
                            else if (med.Situacao == 'A')
                            {
                                Console.WriteLine("Situação do medicamento e valor alteradas com sucesso! De ativo para inativo!");
                                med.SetSituacao('I');
                            }
                            else
                                Console.WriteLine("Medicamento não localizado!");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }

        //
        public void ImprimirMedicines()
        {
            if (ListaMedicines == null || !ListaMedicines.Any())
                Console.WriteLine("A lista está vazia!");
            else
            {
                foreach (var med in ListaMedicines)
                {
                    Console.WriteLine(med.ToString());
                }
            }
        }





    }
}
