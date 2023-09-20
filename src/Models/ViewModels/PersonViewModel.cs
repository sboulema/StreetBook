namespace StreetBook.Models.ViewModels;

public class PersonViewModel
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PictureURL { get; set; } = string.Empty;

    public int HouseNumber { get; set; }

    public string MobilePhoneNumber { get; set; } = string.Empty;

    public string Name => $"{FirstName}{(!string.IsNullOrEmpty(LastName) ? $" {LastName}" : string.Empty)}";
}
