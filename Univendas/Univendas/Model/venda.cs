//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Univendas.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class venda
    {
        public int ID_VENDA { get; set; }
        public int CLIENTE { get; set; }
        public float VALOR_TOTAL { get; set; }
        public string MODALIDADE { get; set; }
        public string OBS { get; set; }
        public System.DateTime DATA { get; set; }
        public int USUARIO_ID_USUARIO { get; set; }
    
        public virtual usuario usuario { get; set; }
        /*
        public static implicit operator venda(venda v)
        {
            throw new NotImplementedException();
        }
        */
    }
}
