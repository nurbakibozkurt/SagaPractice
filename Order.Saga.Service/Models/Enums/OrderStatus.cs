using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Saga.Service.Models.Enums
{
    public enum OrderStatus
    {
        InProgress,
        Completed,
        Failed
    }
}
