﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Warehouse.Application.Features.Customers.Commands.CreateCustomer;
using Warehouse.Application.Features.Customers.Commands.DeleteCustomerById;
using Warehouse.Application.Features.Customers.Commands.UpdateCustomer;
using Warehouse.Application.Features.Customers.Queries.GetCustomerById;
using Warehouse.Application.Features.Customers.Queries.GetCustomers;
using Warehouse.WebApi.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Warehouse.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CustomersController : BaseApiController
    {

        public CustomersController()
        {
        }

        /// <summary>
        /// Gets a list of positions based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter used to query the positions.</param>
        /// <returns>A list of positions.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCustomersQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        /// Gets a position by its Id.
        /// </summary>
        /// <param name="id">The Id of the position.</param>
        /// <returns>The position with the specified Id.</returns>
        [HttpGet("{id}")]
        // [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCustomerByIdQuery { Id = id }));
        }

        /// <summary>
        /// Creates a new position.
        /// </summary>
        /// <param name="command">The command containing the data for the new position.</param>
        /// <returns>A 201 Created response containing the newly created position.</returns>
        [HttpPost]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateCustomerCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        /// <summary>
        /// Sends an InsertMockCustomerCommand to the mediator.
        /// </summary>
        /// <param name="command">The command to be sent.</param>
        /// <returns>The result of the command.</returns>
        [HttpPost]
        [Route("AddMock")]
        //[Authorize]
        public async Task<IActionResult> AddMock(InsertMockCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Retrieves a paged list of positions.
        /// </summary>
        /// <param name="query">The query parameters for the paged list.</param>
        /// <returns>A paged list of positions.</returns>
        [HttpPost]
        //[Authorize]
        [Route("Paged")]
        public async Task<IActionResult> Paged(PagedCustomersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Updates a position with the given id using the provided command.
        /// </summary>
        /// <param name="id">The id of the position to update.</param>
        /// <param name="command">The command containing the updated information.</param>
        /// <returns>The updated position.</returns>
        [HttpPut("{id}")]
        //[Authorize(Policy = AuthorizationConsts.AdminPolicy)]
        public async Task<IActionResult> Put(Guid id, UpdateCustomerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes a position by its Id.
        /// </summary>
        /// <param name="id">The Id of the position to delete.</param>
        /// <returns>The result of the deletion.</returns>
        [HttpDelete("{id}")]
        //[Authorize(Policy = AuthorizationConsts.AdminPolicy)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCustomerByIdCommand { Id = id }));
        }
    }
}