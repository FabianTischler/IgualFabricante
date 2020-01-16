using IgualFabricante.Logic.DataContext;
using IgualFabricante.Logic.DataContext.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace IgualFabricante.Logic.Controllers.Persistence
{
    internal abstract partial class IgualFabricanteController<E, I> : GenericController<E, I>
       where E : Entities.IdentityObject, I, Contracts.ICopyable<I>, new()
       where I : Contracts.IIdentifiable
    {
        protected DbIgualFabricanteContext DBIgualFabricante => (DbIgualFabricanteContext)Context;

        protected IgualFabricanteController(IContext context)
            : base(context)
        {
        }
        protected IgualFabricanteController(ControllerObject controller)
            : base(controller)
        {

        }
    }
}
