using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeBookLibrary.DAL;

namespace HomeBookLibrary.Controllers
{
    public class BaseController : ApiController
    {
        protected IUnitOfWork UnitOfWork;

        protected BaseController()
        {
            UnitOfWork = new UnitOfWork();
        }

        //protected virtual IQueryable<TDto> GetAll<T, TDto>(GenericRepository<T> repository, TDto dto)
        //    where T : class, IEntityWithId
        //{
        //    return repository.Get().ProjectTo<TDto>();
        //}

        //protected async Task<IHttpActionResult> GetById<T, TDetailDto>(GenericRepository<T> repository,
        //    TDetailDto tDetailDto, int id, string includeProperties = "")
        //    where T : class, IEntityWithId
        //{
        //    var item = await Task.Run(() => repository.GetById(id, includeProperties));

        //    TDetailDto dtoDetail;

        //    if (item != null)
        //    {
        //        dtoDetail = Mapper.Map<TDetailDto>(item);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //    return Ok(dtoDetail);
        //}


        //protected IHttpActionResult Put<T>(GenericRepository<T> repository, int id, T entity)
        //    where T : class, IEntityWithId
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != entity.Id)
        //    {
        //        return BadRequest();
        //    }

        //    repository.Update(entity);

        //    try
        //    {
        //        UnitOfWork.Save();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (repository.GetById(id) == null)
        //        {
        //            return NotFound();
        //        }
        //        throw;
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //protected IHttpActionResult Post<T, TDto>(GenericRepository<T> repository, TDto dto, T entity)
        //    where T : class, IEntityWithId
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    repository.Insert(entity);
        //    UnitOfWork.Save();

        //    var outputDto = Mapper.Map<TDto>(entity);

        //    return CreatedAtRoute("DefaultApi", new {id = entity.Id}, outputDto);
        //}

        //protected async Task<IHttpActionResult> Delete<T, TDto>(GenericRepository<T> repository,
        //    TDto tDto, int id)
        //    where T : class, IEntityWithId
        //{
        //    var item = await Task.Run(() => repository.GetById(id));
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    repository.Delete(id);
        //    UnitOfWork.Save();

        //    var dto = Mapper.Map<TDto>(item);

        //    return Ok(dto);
        //}
    }
}