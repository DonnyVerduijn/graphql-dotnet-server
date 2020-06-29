﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Models
{
    public class OrderCreateInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public DateTime Created { get; private set; }

        public string Id { get; private set; }
        public Orders.Schema.OrderStatusesEnum Status { get; private set; }

    }
}
