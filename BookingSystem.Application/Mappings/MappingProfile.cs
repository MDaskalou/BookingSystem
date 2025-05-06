namespace BookingSystem.Application.Mappings;

public class MappingProfile // : Profile
{
    public MappingProfile()
    {
        // TODO: Lägg till mappning från Booking till BookingDto och tvärtom (om jag behöver skriva tillbaka)
        //CreateMap<Booking, BookingDto>().ReverseMap();

        // TODO: Lägg till mappning från CreateBookingDto till Booking (när jag skapar en ny bokning)
        //CreateMap<CreateBookingDto, Booking>();

        // TODO: Lägg till fler mappings för andra entiteter och DTOs (t.ex. User, Patient, Document, etc.)
        // Exempel:
        // CreateMap<User, UserDto>().ReverseMap();
        // CreateMap<CreateUserDto, User>();
    }
  
}