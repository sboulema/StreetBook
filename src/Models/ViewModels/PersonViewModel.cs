using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StreetBook.Models.ViewModels;

public class PersonViewModel
{
	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public int HouseNumber { get; set; }
	
	public string HouseNumberSuffix { get; set; } = string.Empty;

	public string MobilePhoneNumber { get; set; } = string.Empty;

	public bool HasPicture { get; set; }

	public string Name => $"{FirstName}{(!string.IsNullOrEmpty(LastName) ? $" {LastName}" : string.Empty)}";
	
	public bool IsDisabled { get; set; }
	
	public bool IsHidden { get; set; }
	
	public string Status { get; set; } = string.Empty;
	
	public List<string> LicensePlates { get; set; } = [];
}

[JsonSerializable(typeof(PersonViewModel))]
[JsonSerializable(typeof(List<PersonViewModel>))]
public partial class PersonViewModelContext : JsonSerializerContext { }
