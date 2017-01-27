using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univendas.Model;
using Univendas.Controle;
namespace Univendas
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            using (Context_S context = new Context_S())
            {
                var query = from c in context.TESTOQUE select c;
                foreach (var item in query)
                {
                    Console.WriteLine(item.PRODUTO+"cod: "+item.CODBARRAS);
                }
            }
            */

            Context_L _contextL = new Context_L(); //Banco SGBR Laerton
            Context_S _contextS = new Context_S(); //Banco SGBR SIL
            Context_Venda _contextV = new Context_Venda(); //Banco MySQL

            // Adicionando usuário e cliente para poder realizar a venda
            Console.WriteLine("Adicionando usuário e cliente...");

            CUsuario user = new CUsuario("daniel", "1234");
            Cliente cliente = new Cliente();


            // Passando dados das vendas 
            Console.WriteLine("Criando objeto venda...");

            CVenda venda = new CVenda(user, cliente, DateTime.Now, "modalidade");

            // Validando produto
            Console.WriteLine("Validando os produtos...");

            CProduto cp = new CProduto("1000000006520");
            CProduto cp2 = new CProduto("1000000005554");

            // Passando o objeto do produto e a quantidade
            Console.WriteLine("Passando as quantidades...");

            ItemVenda item = new ItemVenda(cp, 2);
            ItemVenda item2 = new ItemVenda(cp2, 1);

            // Imprimindo as quantidades passadas
            Console.WriteLine("\nQuantidade passada :");
            Console.WriteLine("Item1: " + item._quant);
            Console.WriteLine("Item2: " + item2._quant);

            Console.WriteLine("-----------------------------------");

            // Imprimindo o estoque antes de retirar os produtos
            Console.WriteLine("Estoque Antes:");
            Console.WriteLine("Item1:  S-{0} L-{1}", cp.EstoqueS, cp.EstoqueL);
            Console.WriteLine("Item2:  S-{0} L-{1}", cp2.EstoqueS, cp2.EstoqueL);

            Console.WriteLine("----------------------------------");

            // Calculando o estoque
            Console.WriteLine("Calculando o estoque...");

            item.CalculaEstoque();
            item2.CalculaEstoque();

            // Adicionando itens à venda
            Console.WriteLine("Adicionando itens ao carrinho...");

            venda.AdicionaItem(item);
            venda.AdicionaItem(item2);

            // Somando os itens 
            Console.WriteLine("Somando os produtos...");

            item.SomaProduto();
            item2.SomaProduto();

            // Imprimindo estoque após a retirada dos produtos
            Console.WriteLine("\nComo o estoque deve ficar ao retirar os produtos: ");
            Console.WriteLine("Estoque item1 depois: S-{0} L-{1}", cp.EstoqueS, cp.EstoqueL);
            Console.WriteLine("Estoque item2 depois: S-{0} L-{1}", cp2.EstoqueS, cp2.EstoqueL);

            Console.WriteLine("---------------------------");

            // Imprime a lista dos itens passados 
            Console.WriteLine("\nItens passados, quantidade e valor total de cada item:\n");
            foreach (var prod in venda.GetLista())
            {
                Console.WriteLine(prod._cp.Descricao() + " * " + prod._quant + " = " + prod._soma);
            }

            Console.WriteLine("---------------------------");

            // Estoques que foram inseridos na tabela itens venda
            Console.WriteLine("\nQuantidades que foram inseridos na tabela itens venda: ");
            Console.WriteLine("item1: S-{0} L-{1}", item._nEstoqueS, item._nEstoqueL);
            Console.WriteLine("item2: S-{0} L-{1}", item2._nEstoqueS, item2._nEstoqueL);

            Console.WriteLine("---------------------------");

            // Calcula total da venda
            Console.WriteLine("\nValor total da venda: ");
            Console.WriteLine("Total: " + venda.CalculaTotal());

            // Insere na tabela itens_de_venda
            Console.WriteLine("\nInserindo os dados na tabela venda...");
            Console.WriteLine("Inserindo os dados na tabela itens_de_venda...");
            venda.InsertVenda();
            venda.InsertItemVenda();

            string a = cp.GetEAN();
            string b = cp2.GetEAN();
            decimal? s1 = _contextS.TESTOQUE.Where(p => p.CODBARRAS == a).First().QTDEREAL;
            decimal? l1 = _contextL.TESTOQUE.Where(p => p.CODBARRAS == a).First().QTDEREAL;
            decimal? s2 = _contextS.TESTOQUE.Where(p => p.CODBARRAS == b).First().QTDEREAL;
            decimal? l2 = _contextL.TESTOQUE.Where(p => p.CODBARRAS == b).First().QTDEREAL;

            Console.WriteLine("-----------------------------------");

            // Mostra se o estoque foi atualizado
            Console.WriteLine("\nEstoque após o update: ");
            Console.WriteLine("item1: S-{0} L-{1}", s1, l1);
            Console.WriteLine("item2: S-{0} L-{1}", s2, l2);

            // Insere na tabela prevenda
            Console.WriteLine("\nInserindo dados na tabela prevenda");
            venda.InsertPrevenda();

            Console.Read();
        }
    }
}
