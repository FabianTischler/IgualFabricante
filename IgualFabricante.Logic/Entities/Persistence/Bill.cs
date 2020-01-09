using System;
using System.Collections.Generic;
using System.Text;
using CommonBase.Extensions;
using IgualFabricante.Contracts.Persistence;

namespace IgualFabricante.Logic.Entities.Persistence
{
    internal class Bill : IdentityObject, Contracts.Persistence.IBill
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Friends { get; set; }
        public string Currency { get; set; }

        public void CopyProperties(IBill other)
        {
            other.CheckArgument(nameof(other));
            id = other.id;
            Date = other.Date;
            Title = other.Title;
            Description = other.Description;
            Currency = other.Currency;
            Friends = other.Friends;
        }
        //Navigation Field
        public IEnumerable<Expense> Expenses { get; set; }
    }
}
