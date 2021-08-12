using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.CreateClave;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.UpdateClave;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.DeleteClave;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.UpdateGlobal;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Queries.GetClave;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Queries.GetKeyValues;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Queries.GetKeyEnum;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Queries.GetParameters;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.AlterClave;
using CleanArchitecture.WebApiFramework.Application.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArchitecture.WebApiFramework.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RegistroSistemaController : BaseApiController
    {
      

      
        // GET api/<controller>/5
        [HttpGet("Load/{clave}")]
        public async Task<IActionResult> Load(string clave)
        {
            return Ok(await Mediator.Send(new  GetClaveRegistroByClaveQuery { Clave =  clave }));
        }

        // GET api/<controller>/5
        [HttpGet("GetParameters/{clave}")]
        public async Task<IActionResult> GetParameters(string clave)
        {
            return Ok(await Mediator.Send(new  GetParametersQuery { Clave = clave }));
        }

        // GET api/<controller>/5
        [HttpGet("GetKeyEnum/{clave}")]
        public async Task<IActionResult> GetKeyEnum(string clave)
        {
            return Ok(await Mediator.Send(new  GetKeyEnumQuery { Clave = clave }));
        }


        // POST api/<RegistroSistemaController>
        [HttpPost("CreateClave")]
        public async Task<IActionResult> Post([FromBody] CreateClaveCommand command)
        {
            //var productToReturn = await Mediator.Send(command);
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<RegistroSistemaController>/5
        [HttpPut("UpdateKey")]
        public async Task<IActionResult> UpdateKey([FromBody] UpdateClaveCommand command)
        {
           
                     
            return Ok(await Mediator.Send(command));
            
        }

        // PUT api/<RegistroSistemaController>/5
        [HttpPut("AlterKey")]
        public async Task<IActionResult> AlterKey([FromBody] AlterClaveCommand command)
        {

            if (command.newKey == command.oldKey)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(command));

        }


        [HttpPut("GetKeyValues")]
        public async Task<IActionResult> GetKeyValues([FromBody] GetKeyValuesQuery clavesToRead)
        {
            return Ok(await Mediator.Send(clavesToRead));
        }

        [HttpPut("UpdateGlobal")]
        public async Task<IActionResult> UpdateGlobal([FromBody] UpdateGlobalCommand clavesToUpdate)
        {
            return Ok(await Mediator.Send(clavesToUpdate));
        }


        //DELETE api/<RegistroSistemaController>/5
        [HttpDelete("{clave}")]
        public async Task<IActionResult> Delete(string clave)
        {
            return Ok(await Mediator.Send(new DeleteClaveCommand { Clave = clave }));
        }
    }
}
