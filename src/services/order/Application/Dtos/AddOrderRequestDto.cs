using System.ComponentModel.DataAnnotations;

namespace AllRoadsLeadToRome.Service.Order.Application.Dtos;

public record AddOrderRequestDto(
    [Required] AddAddressDto AddressFrom,
    [Required] AddAddressDto AddressTo,
    [Required] int CustomerUserId,
    [Required] int DeliveryUserId,
    [Required] decimal Weight);