using AutoMapper;
using BookStore.API.Dtos.Category;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    //we are defining the route to access this controller.
    //Besides the URL from the application, we need to add ‘/api/controller-name’ to make the calls.
    //For example, when we start the application, we can call the get books using this URL: https://localhost:5001/api/books.
    //Because we will use the Get HTTP verb, the API understands that need to call the GetAll method.
    [Route("api/[controller]")]
    public class CategoriesController : MainController
    {
        //we have the private properties, that we are going to use to call the service method, and do the mapping from our model(entity) to the DTO object.
        //Those properties are private because it only be used on this controller.
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        //we can do our injections to make the calls to the service class and to do the mapping of the entity.
        public CategoriesController(IMapper mapper,
                                    ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        //Note that above each method, the attribute [ProducesResponseType(StatusCodes.xxx)] was added.
        //This is because a method can have more than one type of return,
        //for example, can return a NotFound in case if the recorded doesn’t exist, or can return an Ok if the value is returned.
        //Adding this attribute will produce more descriptive response details for the Web API, so when we check the API in Swagger,
        //we will see more descriptive details about which status codes can be returned by the API.

        //Note that there is a HttpGet attribute above the name of the method, this means that when we execute a GET operation it will reach this method.
        //On this method we have a call to the service class, to get all categories.If not found any category, it will return 404 (Not Found)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();

            return Ok(_mapper.Map<IEnumerable<CategoryResultDto>>(categories));
        }

        // We also have a HttpGet attribute, but also have the parameter id and the type of the parameter.
        //On this method, we have a similar situation from the GetAll method, but in this case, we filter by the category id.
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null) return NotFound();

            return Ok(_mapper.Map<CategoryResultDto>(category));
        }


        // a HttpPost attribute.This method will receive an object as a parameter.
        // We do not have a validation to check if already exists a category with the informed name, because this logic is in the service class.
        // We just have a validation to check if the properties were sent correctly, based on the rules that we added on the Dto class.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CategoryAddDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = _mapper.Map<Category>(categoryDto);
            var categoryResult = await _categoryService.Add(category);

            if (categoryResult == null) return BadRequest();

            return Ok(_mapper.Map<CategoryResultDto>(categoryResult));
        }

        //has a HttpPut attribute. This method received an id and an object as a parameter.
        //The reason why we have these two parameters, it’s because this is a way to confirm if the parameter id is the same id used in the updated object.
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, CategoryEditDto categoryDto)
        {
            if (id != categoryDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var categoryResult = await _categoryService.Update(_mapper.Map<Category>(categoryDto));

            if (categoryResult == null) return BadRequest();

            return Ok(categoryDto);
        }

        // a HttpDelete attribute and will call the method to delete the category.
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound();

            var result = await _categoryService.Remove(category);

            if (!result) return BadRequest();

            return Ok();
        }

        [HttpGet]
        [Route("search/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Category>>> Search(string category)
        {
            var categories = _mapper.Map<List<Category>>(await _categoryService.Search(category));

            if (categories == null || categories.Count == 0)
                return NotFound("None category was founded");

            return Ok(categories);
        }
    }
}
