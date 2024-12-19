using Microsoft.AspNetCore.Mvc;

using PiPlateRelay;

namespace PiPlateRelayServer.Controllers
{
    public class BoardController(PiPlateService piPlateService) : APIBaseController
    {
        private readonly PiPlateService _piPlateService = piPlateService;

        [HttpGet, Route("board/{id}/info")]
        public async Task<IActionResult> Info(int id)
        {
            try
            {
                return JsonSuccess(await _piPlateService.System.GetBoardInfoAsync(id));
            }
            catch (Exception ex)
            {
                return JsonFailure(ex);
            }
        }

        [HttpGet, Route("board/{id}/hardwareVersion")]
        public async Task<IActionResult> HardwareVersion(int id)
        {
            try
            {
                var result = await _piPlateService.System.HardwareVersionAsync(id);
                if (decimal.TryParse(result, out var version))
                    return JsonSuccess(new { version });
                else
                    return JsonFailure(new { message = $"Hardware version \"{result}\" invalid." });
            }
            catch (Exception ex)
            {
                return JsonFailure(ex);
            }
        }

        [HttpGet, Route("board/{id}/firmwareVersion")]
        public async Task<IActionResult> FirmwareVersion(int id)
        {
            try
            {
                var result = await _piPlateService.System.FirmwareVersionAsync(id);
                if (decimal.TryParse(result, out var version))
                    return JsonSuccess(new { version });
                else
                    return JsonFailure(new { message = "Hardware version invalid." });
            }
            catch (Exception ex)
            {
                return JsonFailure(ex.Message);
            }
        }

        [HttpPost, Route("board/{id}/ledOn")]
        public async Task<IActionResult> LEDOn(int id)
        {
            try
            {
                await _piPlateService.System.LEDOnAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return JsonFailure(ex.Message);
            }
        }

        [HttpPost, Route("board/{id}/ledOff")]
        public async Task<IActionResult> LEDOff(int id)
        {
            try
            {
                await _piPlateService.System.LEDOffAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return JsonFailure(ex.Message);
            }
        }

        [HttpPost, Route("board/{id}/ledToggle")]
        public async Task<IActionResult> LEDToggle(int id)
        {
            try
            {
                await _piPlateService.System.ToggleLEDAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return JsonFailure(ex.Message);
            }
        }
    }
}
