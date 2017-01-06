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
            try
            {
                Usuario user = new Usuario("daniel", "1234");
                Console.WriteLine(user.GetTipo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var query = context.usuario.ToList();
                foreach (var item in query)
                {
                    Console.WriteLine(item.LOGIN);
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            */
            Usuario user = new Usuario("daniel", "1234");
            Cliente cliente = new Cliente();
            Venda venda = new Venda(user, cliente, "06012017", "modalidade");
            Console.WriteLine(CProduto.Lista("azul"));



            Console.Read();
        }
    }
}
