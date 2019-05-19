using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusManagement.Business.Admin;
using CampusManagement.Business.Article;
using CampusManagement.Business.Article.Models;
using CampusManagement.Business.File;
using CampusManagement.Business.Practice_Things;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IAdminService _adminService;
        private readonly IFileService _fileService;

        public ArticlesController(IArticleService articleService, IFileService fileService, IAdminService adminService)
        {
            _articleService = articleService;
            _fileService = fileService;
            _adminService = adminService;
        }

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
            var result = await _articleService.GetAsync(id, "Admin.Person");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/values
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(
            [ModelBinder(BinderType = typeof(JsonModelBinder))] ArticleLightCreateModel articleLightCreateModel,
            IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var articleCreateModel = new ArticleCreateModel()
            {
                Title = articleLightCreateModel.Title,
                Content = articleLightCreateModel.Content
            };

            var filePath = await _fileService.SaveOnDisk(file);
            articleCreateModel.Image = filePath ?? "Uploads/default_article.png";
            var personId = Guid.Parse(User.Claims.First(c => c.Type == "PersonId").Value);
            var admin = await _adminService.GetAdminByPersonId(personId);
            articleCreateModel.AdminId = admin.Id;

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
        
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFiles(
            [ModelBinder(BinderType = typeof(JsonModelBinder))] ArticleCreateModel value,
            IList<IFormFile> files)
        {
            // Use serialized json object 'value'
            // Use uploaded 'files'
            var result = await _fileService.SaveOnDisk(files);

            return Ok();
        }
    }
}