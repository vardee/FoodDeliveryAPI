using backendTask.DataBase.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderedDishes
{
    [Key]
    public Guid OrderId { get; set; }
    [Key]
    public Guid DishId { get; set; }

    public int Amount { get; set; }

}
