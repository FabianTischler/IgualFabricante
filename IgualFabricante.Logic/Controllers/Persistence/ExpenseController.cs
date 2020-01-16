using IgualFabricante.Logic.DataContext;
using IgualFabricante.Logic.Entities.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace IgualFabricante.Logic.Controllers.Persistence
{
    internal partial class ExpenseController : IgualFabricanteController<Entities.Persistence.Expense, Contracts.Persistence.IExpense>
    {
        protected override IEnumerable<Entities.Persistence.Expense> Set => IgualFabricanteContext.Expense;

        public ExpenseController(IContext context)
            : base(context)
        {
        }
        public ExpenseController(ControllerObject controller)
            : base(controller)
        {
        }
    }
}
