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
            

            try
            {
                List<TESTOQUE> lista = CProduto.Lista("SSD");

                CProduto cp = new CProduto(lista[0].CODBARRAS);
                Console.WriteLine(cp.Descricao());
            }
            catch (Exception e)
            {

                Console.WriteLine (e.Message);
            }
            
            Console.Read();
        }
        
    }
}
