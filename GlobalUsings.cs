global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.Extensions.Options;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Storage;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using System.Data;
global using System.Data.Common;
global using System.Reflection;
global using System.Runtime.Serialization;
global using System.ComponentModel.DataAnnotations;
global using System.Text.Json.Serialization;

global using MediatR;
global using Npgsql;

global using myhero_dotnet.EventBus.Extensions;
global using myhero_dotnet.Infrastructure;
global using myhero_dotnet.Commands;