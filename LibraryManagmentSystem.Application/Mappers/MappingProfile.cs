using AutoMapper;
using LibraryManagmentSystem.Application.Commands.BookCommands;
using LibraryManagmentSystem.Application.Commands.LoanCommands;
using LibraryManagmentSystem.Application.Commands.MemberCommands;
using LibraryManagmentSystem.Application.Commands.ReservationCommands;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping between Book and BookDto
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<CreateBookCommand, Book>().ReverseMap();
            CreateMap<UpdateBookCommand , Book>().ReverseMap();

            CreateMap<BookDto, UpdateBookCommand>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate))
            .ForMember(dest => dest.NumberOfCopies, opt => opt.MapFrom(src => src.NumberOfCopies));




            // Mapping between Member and MemberDto
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<CreateMemberCommand, Member>().ReverseMap();
            CreateMap<UpdateMemberCommand, Member>().ReverseMap();
            CreateMap<DeleteMemberCommand, Member>().ReverseMap();


            CreateMap<MemberDto, UpdateMemberCommand>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
            .ForMember(dest => dest.DateOfMembership, opt => opt.MapFrom(src => src.DateOfMembership));





            // Mapping between Loan and LoanDto
            CreateMap<Loan, LoanDto>().ReverseMap();
           
            

            // Mapping between Fine and FineDto 
            CreateMap<Fine, FineDto>().ReverseMap();

            // Mapping between Reservation and ReservationDto 
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<CreateReservationCommand, Reservation>().ReverseMap();
            CreateMap<UpdateReservationCommand, Reservation>().ReverseMap();
            CreateMap<CancelReservationCommand, Reservation>().ReverseMap();

            CreateMap<ReservationDto, UpdateReservationCommand>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId))
          //  .ForMember(dest => dest.ReservationDate, opt => opt.MapFrom(src => src.ReservationDate))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate));
  


            CreateMap<User , UserDto>().ReverseMap();
        }

    }
}
