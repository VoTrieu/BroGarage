using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    public class BaseEntity
    {
        public int CreatedUserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public TimeSpan CreatedTime { get; set; }

        public long CreatedTimeStamp { get; set; }

        public int? UpdatedUserId { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        private readonly DateTime FromDate = new(1970, 1, 1);

        public BaseEntity()
        {
            DateTime now = DateTime.Now;

            CreatedUserId = 0;
            CreatedDate = now.Date;
            CreatedTime = now.TimeOfDay;
            CreatedTimeStamp = Convert.ToInt64(now.Subtract(FromDate).TotalSeconds);
            UpdatedUserId = 0;
        }
    }
}
