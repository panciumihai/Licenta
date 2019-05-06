using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusManagement.Business.Article;
using CampusManagement.Business.Article.Models;
using CampusManagement.Business.Student.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CampusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        //[Authorize(Roles="Student")]
        [HttpGet(Name = "GetAllArticles")]
        [EnableQuery()]
        public async Task<IActionResult> Get()
        {
            var articles = await _articleService.GetAllAsync("Admin.Person");

            return Ok(articles);
        }

        [HttpGet("{id}", Name = "GetArticleById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _articleService.GetAsync(id, "Admin");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArticleCreateModel articleCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _articleService.AddAsync(articleCreateModel);

            return CreatedAtRoute("GetArticleById", new { id = result }, result);
        }


        [HttpPost("bulk")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ArticleCreateModel> articleCreateModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = await _articleService.AddAsync(articleCreateModels);

            return CreatedAtRoute("GetAllArticle", results);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ArticleCreateModel articleCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _articleService.UpdateAsync(id, articleCreateModel);

            if (result == Guid.Empty)
                return NotFound();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _articleService.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] IEnumerable<Guid> ids)
        {
            await _articleService.DeleteAsync(ids);
            return NoContent();
        }
    }
}