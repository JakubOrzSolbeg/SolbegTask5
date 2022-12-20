namespace DataRepository.Enums;

[Flags]
public enum UserType : byte
{
    User = 0,
    Worker = 1,
    Admin = 7
}