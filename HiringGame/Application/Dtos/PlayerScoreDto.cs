using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class PlayerScoreDto : DtoBase
    {
        public PlayerScoreDto()
        {
            Transactions = new List<TransactionForReturnDto>();
        }
        public PlayerForReturnDto Player { get; set; }
        public IList<TransactionForReturnDto> Transactions { get; set; }

        public IList<DiscResult> DiscResults { get; set; }
    }
}
