using System.Collections.Generic;

namespace StreetBook.Models.ViewModels;

public class StreetBookViewModel
{
    public List<PersonViewModel> Persons { get; set; } = new();
}
