using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Services;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SQLitePCL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO.Compression;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly string _folderPath = @"C:\Users\gorke\Desktop\asp.net\BlogApp\BlogApp\wwwroot\img\";
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;


        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IActionResult> Index(string? tag_url) 
        {

            var claims = User.Claims;

            var posts = _postRepository.Posts.Where(i => i.isActive == true);
            if(!string.IsNullOrEmpty(tag_url))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag_url));
                return View(new PostViewModel{
                    Posts = await posts.Include(x => x.Tags).OrderByDescending(x => x.PublishedOn).ToListAsync()
                });
            }
            else
            {
                return View(new PostViewModel{
                Posts = await posts.Include(x => x.Tags).OrderByDescending(x => x.PublishedOn).ToListAsync()
            });
            }

        }

        public async Task<IActionResult> Details(string url)
        {
            var userTitle = User.FindFirstValue(ClaimTypes.PrimarySid);
            ViewBag.userTitle = userTitle;
            var userPhoto = User.FindFirstValue(ClaimTypes.UserData);
            ViewBag.userPhoto = userPhoto;

            return View(
                await _postRepository.
                Posts.
                Include(x => x.User).
                Include(x => x.Tags).
                Include(y => y.Comments).
                ThenInclude(x => x.User).
                Include(y => y.User).
                FirstOrDefaultAsync(p => p.Url == url));
        }


        [HttpPost]
        public JsonResult AddComment(int PostId, string CommentText, string Url)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var image = User.FindFirstValue(ClaimTypes.UserData);
            var title = User.FindFirstValue(ClaimTypes.PrimarySid);

            var entity = new Comment
            {
            CommentText = CommentText,
            PostId = PostId,    
            PublishedOn = DateTime.Now,
            UserId = int.Parse(userId ?? ""),

            };
            _commentRepository.CreateComment(entity);

            return Json(new {
                userName,
                CommentText,
                entity.PublishedOn,
                image,
                title
            });
        }

        [HttpGet]

        [Authorize]
        public IActionResult Create()
        {

            return View();


        }

        [HttpPost]


        [Authorize]
        public IActionResult Create(IFormFile FormFile,PostCreateViewModel postCreateViewModel)
        {   

            if(!(User.Identity!.IsAuthenticated))
            {
                RedirectToAction ("Register","Users");
            }

            var new_url = $"{postCreateViewModel.Title.ToString().Replace("\"","").Replace("?","").Replace("_","").Replace("=","")}{ExtensionService.GenerateRandomString(10)}";
            postCreateViewModel.Url = new_url;


            if(ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (FormFile.Length == 0)
            {
                return Content("No file selected.");
            }
            try
            {

                if (!Directory.Exists(_folderPath))
                {
                    Directory.CreateDirectory(_folderPath);
                }
                var extension = $"{ExtensionService.GenerateRandomString(6)}{FormFile.FileName}";
                var targetPath = Path.Combine(_folderPath, extension);

                using (var stream = new FileStream(targetPath, FileMode.Create))
                {
                    FormFile.CopyTo(stream);
                }
                
                _postRepository.CreatePost(
                    new Post{
                        Title = postCreateViewModel.Title,
                        Content = postCreateViewModel.Content,
                        Url = postCreateViewModel.Url,
                        UserId = int.Parse(userId ?? ""),
                        PublishedOn = DateTime.Now,
                        Image = (extension ?? "unknown.jpg"),
                        isActive = true,
                    }
                    );

                return RedirectToAction("index","Posts");
                }
            catch (Exception ex)
            {
                return Content($"File upload error: {ex.Message}");
            }

            }
            else
            {
                return View(postCreateViewModel);
            }

        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;
            
            if (string.IsNullOrEmpty(role))
            {
                posts = posts.Where(x => x.UserId == userId);
            }

            return View(await posts.ToListAsync());


        }





        [Authorize]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var post = _postRepository.Posts.Include(x => x.Tags).FirstOrDefault(x => x.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            ViewBag.Tags = _tagRepository.Tags.ToList();

            return View(new PostCreateViewModel()
            {
                PostId = post.PostId,
                Title = post.Title!,
                Content = post.Content,
                Url = post.Url,
                isActive = post.isActive,
                Tags = post.Tags
            }
            );

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(IFormFile FormFile,PostCreateViewModel postCreateViewModel, int[] tagIds)
        {   
            if(FormFile == null)
            {
                ModelState.Remove("FormFile");
            }

            if(ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                
                var entityToUpdate = new Post{
                    PostId = postCreateViewModel.PostId,
                    Title = postCreateViewModel.Title,
                    Content = postCreateViewModel.Content,
                    Url = postCreateViewModel.Url,
                    isActive = postCreateViewModel.isActive
                };
                
                _postRepository.EditPost(entityToUpdate,tagIds);

                return RedirectToAction("list","Posts");
            }
        
                ViewBag.Tags = _tagRepository.Tags.ToList();
                return View(postCreateViewModel);


        }



}
}