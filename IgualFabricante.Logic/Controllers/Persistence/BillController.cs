using IgualFabricante.Logic.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace IgualFabricante.Logic.Controllers.Persistence
{
    internal partial class BillController : IgualFabricanteController<Entities.Persistence.Bill, Contracts.Persistence.IExpense>
    {
        protected override IEnumerable<Entities.Persistence.Bill> Set => IgualFabricanteContext.Bills;

        public BillController(IContext context)
            : base(context)
        {
        }
        public BillController(ControllerObject controller)
            : base(controller)
        {
        }
    }
}
