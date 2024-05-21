using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class WishListEntity
    {
       public int WishListId {  get; set; }
       public int BookId {  get; set; }
       public int UserId {  get; set; }
    }
}
