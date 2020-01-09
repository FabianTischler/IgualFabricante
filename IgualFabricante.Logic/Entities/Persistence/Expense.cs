using System;
using System.Collections.Generic;
using System.Text;
using CommonBase.Extensions;
using IgualFabricante.Contracts.Persistence;

namespace IgualFabricante.Logic.Entities.Persistence
{
    internal class Expense : IdentityObject, Contracts.Persistence.IExpense
    {
        public int BillId { get; set; }
        public string Designation { get; set; }
        public double Amount { get; set; }
        public string Friend { get; set; }

        public void CopyProperties(IExpense other)
        {
            other.CheckArgument(nameof(other));

            id = other.id;
            BillId = other.BillId;
            Designation = other.Designation;
            Amount = other.Amount;
            Friend = other.Friend;
        }
        //Navigation Property
        public Bill Bill { get; set; }
    }
}
