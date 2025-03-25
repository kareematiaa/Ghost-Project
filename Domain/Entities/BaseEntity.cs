using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class BaseEntity
    {
        /// <summary>
        /// The PK of an Entity that inhearts from <see cref="BaseEntity"/> 
        /// <see cref="Guid"/> to make PK has large Range 
        /// </summary>
        public Guid Id { get; set; }
    }
}
