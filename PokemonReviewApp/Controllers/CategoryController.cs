namespace PokemonReviewApp.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PokemonReviewApp.Dto;
    using PokemonReviewApp.Interfaces;
    using PokemonReviewApp.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;   
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Category>))]
        public IActionResult GetCategories()
        {
            var categories = 
                _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type=typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if(_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }

            var category =  
                _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategory(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("{categoryId}/Pokemon")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonsByCategory(int categoryId)
        {
            if(!_categoryRepository.CategoryExists(categoryId))
            { 
                return NotFound();
            }

            var pokemon =
                _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonsByCategory(categoryId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if(categoryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var category = _categoryRepository.GetCategories()
                                                                .Where(c=> c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                                                                .FirstOrDefault();

            if(category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
                    
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categorytMap = _mapper.Map<Category>(categoryCreate);

            if(!_categoryRepository.CreateCategory(categorytMap))
            {
                ModelState.AddModelError("","Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if(updatedCategory == null) 
            { 
                return BadRequest(ModelState);
            }

            if(categoryId != updatedCategory.Id)
            {
                return BadRequest(ModelState);
            }

            if(!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categoryMap = _mapper.Map<Category>(updatedCategory);

            if(!_categoryRepository.updateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

           return NoContent();
        }

    }
}
