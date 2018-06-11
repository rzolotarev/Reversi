using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Boards.Contracts
{
    public interface IBoardCreator
    {        
        Board Create(string board);
    }
}
