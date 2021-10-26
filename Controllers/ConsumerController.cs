using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedBagerApi.Data;
using RedBagerApi.Models;
using System;

namespace RedBagerApi.Controllers
{
    [ApiController]
    [Route("v1/consumers")]
    public class ConsumerController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Consumer>>> Get([FromServices] DataContext context)
        {
            var consumers = await context.Consumers.ToListAsync();
            return consumers; 
        }

        [HttpGet]
        [Route("{cpfcnpj:long}")]
        public async Task<ActionResult<Consumer>> GetById([FromServices] DataContext context, long cpfcnpj)
        {
            try
            {
                var consumer = await context.Consumers.
                AsNoTracking()
                .FirstAsync(x => x.CpfCnpj == cpfcnpj);
                return consumer; 
            }
            catch(Exception ex)
            {
                var str_ex = ex.ToString();
                return NotFound();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<string>> Post(
            [FromServices] DataContext context,
            [FromBody] Consumer model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    context.Consumers.Add(model);
                    await context.SaveChangesAsync();
                    return "OK";
                }
                catch(Exception ex)
                {
                    var str_ex = ex.ToString();
                    return BadRequest("ERROR");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        } 

        [HttpPut]
        [Route("{cpfcnpj:long}")]
        public async Task<ActionResult<string>> PutById(
            [FromServices] DataContext context, 
            [FromBody] Consumer model,
            [FromRoute] long cpfcnpj)
        {
            var consumer = await context.Consumers.FirstOrDefaultAsync(x => x.CpfCnpj == cpfcnpj);

            if(consumer == null)
                return NotFound();

            consumer.TotCallFree = model.TotCallFree;
            consumer.TotCall = model.TotCall;
            consumer.ChavePixCpfCnpj = model.ChavePixCpfCnpj;
            consumer.ChavePixCelular = model.ChavePixCelular;
            consumer.ChavePixEmail = model.ChavePixEmail;
            consumer.ContatoResponsavel = model.ContatoResponsavel;

            context.Consumers.Update(consumer);
            await context.SaveChangesAsync();
            return "OK"; 
        }       
    }
}