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
    
    public partial class prevenda
    {
        public int ID_PREVENDA { get; set; }
        public int CONTROLE { get; set; }
        public int VENDA_ID_VENDAS { get; set; }
        public string LOJA_ID_LOJA { get; set; }
    
        public virtual loja loja { get; set; }
    }
}
