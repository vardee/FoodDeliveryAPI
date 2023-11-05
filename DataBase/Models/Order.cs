using backendTask.DataBase.Models;
using backendTask.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    public Guid OrderId { get; set; }
    [Key]
    public Guid UserId { get; set; }
    public DateTime OrderTime { get; set; }
    public DateOnly DeliveryTime { get; set; }
    public OrderStatus Status { get; set; }
    public double Price { get; set; }
    public string Address { get; set; }
}