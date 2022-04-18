using AuthProject.Data;
using AuthProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace AuthProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassListController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("getAllClassList")]
        public async Task<IActionResult> GetAllClassLlist()
        {
            var classlist =await _context.ListOfClasses.ToListAsync();
            var data =new { CLASSLIST = classlist };
            return Ok(data);

        }


        [HttpPost("saveClassList")]

        public async Task<IActionResult> SaveClassList(ListOfClasses data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (data.ListOfClassesID == null)
                {
                    data.ListOfClassesID = Guid.NewGuid().ToString();

                    await _context.ListOfClasses.AddAsync(data);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Entry(data).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }

                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest(ex.InnerException.Message);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

        }

        [HttpGet("getClassById")]
        public async Task<IActionResult> GetClassById(string Id)
        {
            try
            {
                var classlist = await (from cl in _context.ListOfClasses
                                      where cl.ListOfClassesID == Id
                                      select new
                                      {
                                          cl.ListOfClassesID,
                                          cl.Image,
                                          cl.Time,
                                          cl.Location,
                                          cl.Link,
                                          cl.ClassName
                                         

                    }).FirstOrDefaultAsync();
                var data = new { CLASSLIST = classlist };
                return Ok(data);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest(ex.InnerException.Message);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("deleteClassList")]
        public async Task<IActionResult> DeleteClassList(string Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid");
                }

                try
                {
                    _context.ListOfClasses.RemoveRange(_context.ListOfClasses.Where(x => x.ListOfClassesID == Id));
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        return BadRequest(ex.InnerException.Message);
                    }
                    else
                    {
                        return BadRequest(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest(ex.InnerException.Message);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
