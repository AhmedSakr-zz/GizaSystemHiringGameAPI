using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HiringGame.Application.Dtos;
using HiringGame.DataModel;
using Microsoft.Extensions.Configuration;

namespace HiringGame.MappingProfile
{
    public class MappingProfile : Profile
    {


        public MappingProfile()
        {

            CreateMap<Job, JobDto>();
            CreateMap<JobDto, Job>();

            CreateMap<Video, VideoDto>();
            CreateMap<VideoDto, Video>();
            CreateMap<VideoDto, VideoForReturnDto>()
                .ForMember(t => t.Url, opt => opt.MapFrom(s => s.PhysicalName));

            CreateMap<Question, QuestionDto>();

            CreateMap<QuestionDto, Question>();
            CreateMap<Question, QuestionForReturnDto>()
                .ForMember(t => t.Question, opt => opt.MapFrom(s => s.QuestionString));

            CreateMap<Answer, AnswerForReturnDto>()
                .ForMember(t => t.Answer, opt => opt.MapFrom(s => s.AnswerString))
                .ForMember(t => t.IsCorrect, opt => opt.MapFrom(s => s.IsCorrectAnswer ?? false
                 ));
            CreateMap<AnswerDto, Answer>();
            CreateMap<Answer, AnswerDto>()
                .ForMember(t => t.IsCorrectAnswer, opt => opt.MapFrom(s => s.IsCorrectAnswer ?? false));


            CreateMap<PlayerDto, Player>();
            CreateMap<Player, PlayerDto>();
            CreateMap<Player, PlayerScoreDto>();


            CreateMap<PlayerForReturnDto, Player>();
            CreateMap<Player, PlayerForReturnDto>()
                .ForMember(t => t.JobName, opt => opt.MapFrom(s => s.Job.Name))
                .ForMember(t => t.TotalScore, opt => opt.MapFrom(s => Convert.ToDecimal(s.Score)));


            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
            CreateMap<Transaction, TransactionForReturnDto>()
                .ForMember(t => t.Answer, opt => opt.MapFrom(s => s.Answer.AnswerString))
                .ForMember(t => t.IsCorrect, opt => opt.MapFrom(s => s.Answer.IsCorrectAnswer))
                .ForMember(t => t.Question, opt => opt.MapFrom(s => s.Question.QuestionString))
                .ForMember(t => t.Score, opt => opt.MapFrom(s => s.Score));

            CreateMap<AppUser, AppUserForReturnDto>();
            CreateMap<AppUserForReturnDto, AppUser>();
        }

    }
}
