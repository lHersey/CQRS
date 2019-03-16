using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CQRSExample.Domain.Entities
{
    public class Genre
    {
        public Genre()
        {
            this.Movies = new Collection<Movie>();
        }

        public int Id { get; set; }
        public string Description { get; set; }        
        
        public ICollection<Movie> Movies { get; set; }
    }
}