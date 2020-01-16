using System;
using System.Collections.Generic;
using System.Text;

namespace IgualFabricante.Logic.Entities
{
    internal class IdentityObject : Contracts.IIdentifiable
    {
        public int id { get; set; }
        public object Id { get; internal set; }
    }
}
