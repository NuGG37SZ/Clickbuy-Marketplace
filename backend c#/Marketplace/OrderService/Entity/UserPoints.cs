using Microsoft.EntityFrameworkCore;
using System;

namespace OrderService.Entity
{
    [Index(nameof(UserId), IsUnique = true)]
    [Index(nameof(PointsId), IsUnique = true)]
    public class UserPoints
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PointsId { get; set; }

        public bool IsActive { get; set; }
    }
}
