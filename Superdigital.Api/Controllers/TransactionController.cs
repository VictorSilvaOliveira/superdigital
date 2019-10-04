using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Superdigital.Handlers;
using Superdigital.Models;


namespace Superdigital.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransferHandler _transferHandler;

        public TransactionController(TransferHandler transferHandler)
        {
            _transferHandler = transferHandler;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Transfer> Get(Guid identificador)
        {
            return Ok(new Transfer());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Transfer> Post([FromBody] Transfer transaction)
        {
            try
            {
                var processedTransactionId = _transferHandler.Execute(transaction);
                return Created(new Uri($"{HttpContext.Request.Host}/{processedTransactionId}"), transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }


    }
}
