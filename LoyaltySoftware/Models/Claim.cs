using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    public class Claim
    {

        public int Id { get; set; }
        public int claimId { get; set; }
        public int pointsSpent { get; set; }
    }
}
