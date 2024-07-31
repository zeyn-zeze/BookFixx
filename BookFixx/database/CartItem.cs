namespace BookFixx.database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        
        public int CartID { get; set; }

        public int BookID { get; set; }

        public int Quantity { get; set; }

        public virtual Book Book { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
