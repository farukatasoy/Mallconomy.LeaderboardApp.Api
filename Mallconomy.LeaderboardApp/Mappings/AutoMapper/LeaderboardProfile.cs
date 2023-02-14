using System;
using AutoMapper;
using Mallconomy.LeaderboardApp.Data.Entities;
using Mallconomy.LeaderboardApp.Models;

namespace Mallconomy.LeaderboardApp.Mappings.AutoMapper
{
    public class LeaderboardProfile : Profile
    {
        public LeaderboardProfile()
        {
            CreateMap<Leaderboard, LeaderboardCreateModel>().ReverseMap();
            CreateMap<Leaderboard, LeaderboardListModel>().ReverseMap();
            CreateMap<Leaderboard, UserPrizesListModel>().ReverseMap();
        }
    }
}