﻿using AllRoadsLeadToRome.Core.Enums;
using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Application.Services.Interfaces;
using AuthApi;
using MassTransit;
using OrderApi;

namespace AllRoadsLeadToRome.Service.Order.Application.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderLogRepository _orderLogRepository;
    //private readonly AuthGrpc.AuthGrpcClient _grpcClient;

    public OrderService(IOrderRepository orderRepository, IOrderLogRepository orderLogRepository)
    {
        _orderRepository = orderRepository;
        _orderLogRepository = orderLogRepository;
        //_grpcClient = grpcClient;
    }

    public async Task<int> Create(AddOrderRequestDto request, CancellationToken ct)
    {
        //try
        //{
        //    var response = await _grpcClient.GetOrderAsync(new GetOrderRequest()
        //    {
        //        Id = 1
        //    });
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //    //throw;
        //}

        var order = await _orderRepository.Create(request, ct);
        await _orderLogRepository.Create(order, ct);
        return order.Id;
    }

    public async Task<OrderResponseDto> GetById(int id, CancellationToken ct)
    {
        var order = await _orderRepository.GetById(id, ct);
        return new OrderResponseDto(
            order.Id,
            order.AddressFrom,
            order.AddressTo,
            order.Status,
            order.CustomerUserId,
            order.DeliveryUserId,
            order.Weight,
            order.CreatedDate,
            order.UpdatedDate);
    }

    public async Task<List<OrderResponseDto>> GetAll(CancellationToken ct)
    {
        var orders = await _orderRepository.GetAll(ct);
        return orders.Select(order => new OrderResponseDto(
            order.Id,
            order.AddressFrom,
            order.AddressTo,
            order.Status,
            order.CustomerUserId,
            order.DeliveryUserId,
            order.Weight,
            order.CreatedDate,
            order.UpdatedDate)).ToList();
    }

    public async Task ChangeStatus(int id, OrderStatus newStatus, CancellationToken ct)
    {
        await _orderRepository.ChangeStatus(id, newStatus, ct);
        var order = await _orderRepository.GetById(id, ct);
        await _orderLogRepository.Create(order, ct);
    }
}