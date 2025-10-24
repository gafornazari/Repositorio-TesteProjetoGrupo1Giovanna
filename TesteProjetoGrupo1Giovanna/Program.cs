using System.ComponentModel.Design;
using TesteProjetoGrupo1Giovanna;

Farmacia farmacia = new Farmacia();

int op = 0;
int menu;
do
{
    Console.WriteLine("MENU");
    Console.WriteLine("Deseja acessar qual menu?");
    Console.Write("1 - Ingredientes\n2 - Medicamentos\n0 - Sair\nOpção: ");
    menu = int.Parse(Console.ReadLine());

    if (menu == 1)
    {
        Console.WriteLine("1 - Inserir");
        Console.WriteLine("2 - Localizar");
        Console.WriteLine("3 - Alterar");
        Console.WriteLine("4 - Imprimir");
        int ing = int.Parse(Console.ReadLine());

        switch (ing)
        {
            case 1:
                farmacia.IncluirIngredient();
                break;
            case 2:
                Console.WriteLine("Qual o Id do ingrediente que deseja localizar?");
                string idloc = Console.ReadLine();
                farmacia.LocalizarIngridient(idloc);
                break;
            case 3:
                Console.WriteLine("Qual o Id do ingrediente que deseja alterar?");
                string idalt = Console.ReadLine();
                farmacia.AlterarIngridient(idalt);
                break;
            case 4:
                farmacia.ImprimirIngridient();
                break;
            default:
                Console.WriteLine("Invalido");
                break;
        }

    }

    else if (menu == 2)
    {
        Console.WriteLine("1 - Inserir");
        Console.WriteLine("2 - Localizar");
        Console.WriteLine("3 - Alterar");
        Console.WriteLine("4 - Imprimir");
        int ing = int.Parse(Console.ReadLine());

        switch (ing)
        {
            case 1:
                farmacia.IncluirMedicine();
                break;
            case 2:
                Console.WriteLine("Qual o CDB do medicamento que deseja localizar?");
                string idloc = Console.ReadLine();
                Medicine localizar = farmacia.LocalizarMedicine(idloc);
                if(localizar != null)
                    Console.WriteLine(localizar.ToString()); 
                else
                    Console.WriteLine("CDB não encontrado!");
                break;
            case 3:
                Console.WriteLine("Qual o CDB do medicamento que deseja alterar?");
                string idalt = Console.ReadLine();
                Medicine alterar = farmacia.LocalizarMedicine(idalt);
                if (alterar != null)
                    farmacia.AlterarMedicine(idalt);
                else
                    Console.WriteLine("CDB não encontrado!");

                    break;
            case 4:
                farmacia.ImprimirMedicines();
                break;
            default:
                Console.WriteLine("Invalido");
                break;
        }
    }

    else
        break;
} while (menu != 0);