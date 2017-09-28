using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ReadingClub.Data.Models.Contracts;

namespace ReadingClub.Data.Models.Abstracts
{
    public abstract class BaseDataModel : IAuditable, IDeletable
    {
        public BaseDataModel()
        {
        }

        [Key]
        public int Id { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
