﻿using myhero_dotnet.ContentsAPI;

namespace myhero_dotnet.AccountAPI.Infrastructure;

public class SearchUserMappingProfile : Profile
{
    public SearchUserMappingProfile()
    {
        CreateMap<SearchUserRequest, SearchUserByStringCommand>();
        //.ForMember(dest => dest.SearchWord, opt => opt.MapFrom(src => src.SearchWord))
    }
}