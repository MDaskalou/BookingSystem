using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookingSystem.Application.Commands.UserCommands.CreateUser;
using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookingSystem.Application.Service.Implementation;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public AuthService(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _mediator = mediator;
    }

    public async Task<OperationResult<int>> RegisterAsync(CreateUserDto dto)
    {
        var result = await _mediator.Send(new CreateUserCommand(dto));
        return result; // Returnera hela OperationResult, inte bara userId
    }

    public string GenerateJwtToken(int userId, string name, string? role)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Role, role ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
    
