using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanMartin.Models.Gallery
{
    public class Order
    {
        public enum OrderStatus
        {
            pending = 0,
            inProcess = 1,
            complete = 2 
        };
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public Guid Number { get; set; }
        public DateTime CreatedDtm { get; set; }
        public int Count { get; set; }
        public OrderStatus Status { get; set; }
        public Order(int orderId, int userId)
        {
            OrderId = orderId;
            UserId = userId;
            Number = Guid.Empty;
            CreatedDtm = DateTime.Now;
            Count = 0;
            Status = OrderStatus.pending;
        }
        public Order(int orderId, int userId, Guid number, DateTime createdDtm, int count, OrderStatus status) : this(orderId, userId)
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
