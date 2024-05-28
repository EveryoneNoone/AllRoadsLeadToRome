using AllRoadsLeadToRome.Core.Enums;

namespace AllRoadsLeadToRome.Service.Order.Application.Dtos;


public record OrderResponseDto(
    int Id,
    string AddressFrom,
    string AddressTo,
    OrderStatus Status,
    int CustomerUserId,
    int DeliveryUserId,
    decimal Weight,
    DateTime CreatedDate,
    DateTime UpdatedDate);