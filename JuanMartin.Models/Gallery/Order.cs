using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanMartin.Models.Gallery
{
    public class Order
    {
        public enum OrderStatusType
        {
            [Description("pending")]
            pending = 0,
            [Description("inProcess")]
            inProcess = 1,
            [Description("complete")]
            complete = 2 
        };
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public Guid Number { get; set; }
        public DateTime CreatedDtm { get; set; }
        public int Count { get; set; }
        public OrderStatusType Status { get; set; }
        public Order(int orderId, int userId)
        {
            OrderId = orderId;
            UserId = userId;
            Number = Guid.Empty;
            CreatedDtm = DateTime.Now;
            Count = 0;
            Status = OrderStatusType.pending;
        }
        public Order(int orderId, int userId, Guid number, DateTime createdDtm, int count, OrderStatusType status) : this(orderId, userId)
        {
            OrderId = orderId;
            UserId = userId;
            Number = number;
            CreatedDtm = createdDtm;
            Count= count;
            Status = status;
        }
    }
}
