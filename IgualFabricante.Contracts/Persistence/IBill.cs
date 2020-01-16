using System;
using System.Collections.Generic;
using System.Text;

namespace IgualFabricante.Contracts.Persistence
{
    public interface IBill : IIdentifiable, ICopyable<IExpense>
    {
        DateTime Date { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Friends { get; set; }
        string Currency { get; set; }
    }
}
