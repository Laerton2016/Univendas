using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univendas.Model;
namespace Univendas
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Context_S context = new Context_S()) 
            {
                var query = from c in context.TESTOQUE select c;
                foreach (var item in query)
                {
                    Console.WriteLine(item.PRODUTO);
                }
            }
            Console.Read();
        }
        
    }
}
