using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IceGate_Demo.Dbcontext;
using IceGate_Demo.Entities;
using Azure.Core;

namespace IceGate_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationRequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IntegrationRequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IntegrationRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntegrationRequest>>> GetIntegrationRequest()
        {
            return await _context.IntegrationRequest.ToListAsync();
        }

        // GET: api/IntegrationRequests/5
        [HttpGet("byAckId/{ackId}")]
        public async Task<ActionResult<IntegrationResponse>> GetIntegrationRequest(Guid ackId)
        {
            var integrationRequest = await _context.IntegrationRequest.FirstOrDefaultAsync(x => x.AckId == ackId);

            if (integrationRequest == null)
            {
                return NotFound();
            }
                var response = new IntegrationResponse()
                {
                    Status = "Success",
                    ErrorCode = "000",
                    CommonRefNumber = "2019031955489454",
                    Message = "Data Integrated",
                    RequestorId = integrationRequest.RequestorId,
                    AckId = integrationRequest.AckId.ToString(),
                    ManifestNumberOrRotationNumber = integrationRequest.ManifestNumberOrRotationNumber,
                    ManifestDateOrRotationDate = integrationRequest.ManifestDateOrRotationDate
                };
            return response;
        }

        //[HttpGet("byAckId/{ackId}")]
        //public async Task<ActionResult<IntegrationRequest>> GetIntegrationRequestByAckId(Guid ackId)
        //{
        //var integrationRequest = await _context.IntegrationRequest.FirstOrDefaultAsync(ir => ir.AckId == ackId);
        //    if (integrationRequest == null)
        //    {
        //        return NotFound();
        //    }
        //    return integrationRequest;
        //}

        // PUT: api/IntegrationRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntegrationRequest(int id, IntegrationRequest integrationRequest)
        {
            if (id != integrationRequest.IntegrationRequestId)
            {
                return BadRequest();
            }

            _context.Entry(integrationRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntegrationRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<IntegrationRequest>> AddIntegrationRequest(DemoIntegrationRequestJson integrationRequest)
        {
            var request = integrationRequest.Text;
            var addIntegrationRequest = new IntegrationRequest()
            {
                AckId = Guid.NewGuid(),
                RequestorId = request.RequestorId,
                RecordType = request.RecordType,
                VoyageCallNumber = request.VoyageCallNumber,
                ModeOfTransport = request.ModeOfTransport,
                TypeOfTransportMeans = request.TypeOfTransportMeans,
                IdentityOfTransportMeans = request.IdentityOfTransportMeans,
                VesselCode = request.VesselCode,
                VoyageNumber = request.VoyageNumber,
                TypeOfVessel = request.TypeOfVessel,
                PurposeOfCall = request.PurposeOfCall,
                ShippingAgentCode = request.ShippingAgentCode,
                LineCode = request.LineCode,
                TerminalOperatorCode = request.TerminalOperatorCode,
                PortCode = request.PortCode,
                ExpectedDateTimeOfArrival = request.ExpectedDateTimeOfArrival,
                ExpectedDateTimeOfDeparture = request.ExpectedDateTimeOfDeparture,
                RequestePostedDate = request.RequestePostedDate,
                ServiceName = request.ServiceName,
                AllotmentDate = request.AllotmentDate,
                ManifestNumberOrRotationNumber = request.ManifestNumberOrRotationNumber,
                ManifestDateOrRotationDate = request.ManifestDateOrRotationDate
            };

            _context.IntegrationRequest.Add(addIntegrationRequest);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetIntegrationRequest", new { id = addIntegrationRequest.AckId }, addIntegrationRequest);
        }

        // DELETE: api/IntegrationRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntegrationRequest(int id)
        {
            var integrationRequest = await _context.IntegrationRequest.FindAsync(id);
            if (integrationRequest == null)
            {
                return NotFound();
            }

            _context.IntegrationRequest.Remove(integrationRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IntegrationRequestExists(int id)
        {
            return _context.IntegrationRequest.Any(e => e.IntegrationRequestId == id);
        }
    }
}
