using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringGame.Application.Dtos;
using HiringGame.Common;
using Microsoft.AspNetCore.Http;

namespace HiringGame.Application.Interfaces
{
    public interface IPlayerServices
    {
        List<PlayerForReturnDto> GetList();

        PlayerForReturnDto GetById(int id);
        ClientMessage<PlayerScoreDto> GetPlayerScore(int playerId);
        ClientMessage<RegistrationResult> Register(PlayerDto dto);
        ClientMessage<bool> SubmitTotalScore(SubmitScoreDto dto);
        ClientMessage<int> CreateLevel2Transaction(TransactionDto dto);
        ClientMessage<bool> CreateLevel3Transaction(List<TransactionDto> dto);
        ClientMessage<List<QuestionForReturnDto>> GetLevel2Questions();
        ClientMessage<List<QuestionForReturnDto>> GetLevel3Questions();


    }
}
