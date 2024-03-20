global using System.Data;
global using System.Data.Common;
global using System.Text;
global using System.Linq;
global using System.Text.Json;
global using System.Reflection;
global using System.Runtime.Serialization;
global using System.ComponentModel.DataAnnotations;
global using System.Text.Json.Serialization;
global using System.Globalization;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.IdentityModel.Tokens.Jwt;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.AspNetCore.Authentication.BearerToken;
global using Microsoft.Extensions.Options;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Storage;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;

global using FluentValidation;
global using MediatR;
global using AutoMapper;
global using Serilog;
global using Npgsql;
global using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

global using Util.Infrastructure;
global using Util.Infrastructure.DependencyInjection;

global using Shared.Features;
global using Shared.Featrues.Auth;
global using Shared.Featrues.Crypt;
global using Shared.Featrues.Extensions;
global using Shared.Features.Constants;
global using Shared.Features.DatabaseCore;
