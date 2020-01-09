using System;
using System.Collections.Generic;
using System.Text;

namespace IgualFabricante.Contracts
{
    public partial interface ICopyable<T>
    {
        void CopyProperties(T other);
    }
}
