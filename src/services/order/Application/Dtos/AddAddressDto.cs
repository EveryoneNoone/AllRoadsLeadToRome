using System.ComponentModel.DataAnnotations;

namespace AllRoadsLeadToRome.Service.Order.Application.Dtos;

public record AddAddressDto(
    [Required] string City,
    [Required] int Region,
    [Required] string PostalCode,
    [Required] string Street,
    [Required] string House);