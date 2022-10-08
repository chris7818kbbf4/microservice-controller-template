using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceTemplate.Repositories.Base;

namespace ServiceTemplate.Controllers;

public abstract class BaseController<TModel, TDto>: ControllerBase
{
    private readonly IBaseRepository<TModel> _baseRepository;
    protected readonly IMapper _mapper;

    protected BaseController(IBaseRepository<TModel> baseRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
    }

    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TDto>> GetById(Guid id)
    {
        var result = await _baseRepository.GetById(id);

        if (result is null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<TDto>(result);

        return Ok(dto);
    }
    
    [HttpPost]
    public async Task<ActionResult<TDto>> Post([FromBody] TDto dto)
    {
        var model = _mapper.Map<TModel>(dto);
        var added = await _baseRepository.Add(model);

        var result = _mapper.Map<TDto>(added);

        return Ok(result);
    }


    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TDto>> Put(Guid id, [FromBody] TDto dto)
    {
        if (await _baseRepository.GetById(id) is null)
        {
            return BadRequest("Invalid Id");
        }
        
        var model = _mapper.Map<TModel>(dto);
        var added = _baseRepository.Update(model);

        var result = _mapper.Map<TDto>(added);

        return Ok(result);
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (await _baseRepository.GetById(id) is null)
        {
            return BadRequest("Invalid Id");
        }

        await _baseRepository.Delete(id);

        return Ok();
    }
}