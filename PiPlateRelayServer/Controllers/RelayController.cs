﻿using PiPlateRelay;
using Microsoft.AspNetCore.Mvc;

namespace PiPlateRelayServer.Controllers
{
    public class RelayController(PiPlateService piPlateService) : APIBaseController
    {
        private readonly PiPlateService _piPlateService = piPlateService;

        [HttpPost, Route("/board/{id}/relay/{relayNumber}/on")]
        public async Task<IActionResult> On(int id, int relayNumber)
        {
            try
            {
                await _piPlateService.Relay.OnAsync(id, relayNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return JsonFailure(ex);
            }
        }

        [HttpPost, Route("/board/{id}/relay/{relayNumber}/off")]
        public async Task<IActionResult> Off(int id, int relayNumber)
        {
            try
            {
                await _piPlateService.Relay.OffAsync(id, relayNumber);

                return Ok();
            }
            catch (Exception ex)
            {
                return JsonFailure(ex);
            }
        }

        [HttpPost, Route("/board/{id}/relay/{relayNumber}/toggle")]
        public async Task<IActionResult> Toggle(int id, int relayNumber)
        {
            try
            {
                await _piPlateService.Relay.ToggleAsync(id, relayNumber);

                return Ok();
            }
            catch (Exception ex)
            {
                return JsonFailure(ex.Message);
            }
        }

        [HttpGet, Route("/board/{id}/relay/status")]
        public async Task<IActionResult> Status(int id)
        {
            try
            {
                var status = await _piPlateService.Relay.StatusAsync(id);

                return JsonSuccess(status.Statuses);
            }
            catch (Exception ex)
            {
                return JsonFailure(ex.Message);
            }
        }

        [HttpGet, Route("/board/{id}/relay/{relayNumber?}/status")]
        public async Task<IActionResult> Status(int id, int? relayNumber)
        {
            try
            {
                var status = await _piPlateService.Relay.StatusAsync(id);

                if (relayNumber.HasValue &&
                    relayNumber > 0 && relayNumber < 8)
                {
                    return JsonSuccess(status.Statuses[relayNumber.Value - 1]);
                }
                else
                {
                    return JsonSuccess(status.Statuses);
                }
            }
            catch (Exception ex)
            {
                return JsonFailure(ex.Message);
            }
        }

        [HttpPost, Route("/board/{id}/relay/reset")]
        public async Task<IActionResult> Reset(int id)
        {
            try
            {
                await _piPlateService.Relay.ResetAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return JsonFailure(ex.Message);
            }
        }
    }
}
