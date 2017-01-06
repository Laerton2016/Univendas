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
            Context_Venda context = new Context_Venda();
            context.usuario.Add(new usuario { LOGIN = "asdf", SENHA = "12332", TIPO = 2 });

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

            Console.Read();
        }

    }
}
