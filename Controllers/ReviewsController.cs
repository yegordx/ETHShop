using Microsoft.AspNetCore.Mvc;
using ETHShop.Entities;
using ETHShop.Contracts;
using Microsoft.EntityFrameworkCore;


namespace ETHShop.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly ShopDbContext _context;

    public ReviewsController(ShopDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> MakeReview(MakeReviewRequest request)
    {
        var productId = Guid.Parse(request.ProductId);
        var userId = Guid.Parse(request.UserId);

        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == productId);
        var user = await _context.Users
            .Include(u => u.Orders)
            .ThenInclude(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null || product == null)
        {
            return BadRequest();
        }

        bool hasOrderedProduct = user.Orders
            .SelectMany(o => o.OrderItems)
            .Any(oi => oi.Product.ProductID == productId);

        if (!hasOrderedProduct)
        {
            return BadRequest(new { message = "User hasn`t ordered this product." });
        }

        var review = new Review(Guid.NewGuid(), request.Rating, request.Comment, user, product);
        product.AddReview(review);
        user.AddReview(review);
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{userId}/reviews")]
    public async Task<IActionResult> GetReviews(string userId)
    {
        var userID = Guid.Parse(userId);

        var reviews = await _context.Reviews
            .Where(r => r.UserID == userID)
            .Select(r => new ReviewDto(r.ReviewID, r.Rating, r.Comment, r.UserID))
            .ToListAsync();

        return Ok(reviews);
    }

    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(Guid reviewId, string userId)
    {
        var userID = Guid.Parse(userId);

        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.ReviewID == reviewId);

        if (review == null)
        {
            return NotFound(new { message = "Review not found." });
        }

        if (review.UserID != userID)
        {
            return BadRequest(new { message = "User is not authorized to delete this review." });
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Review deleted successfully." });
    }

    [HttpGet("product/{productId}/reviews")]
    public async Task<IActionResult> GetReviewsByProduct(Guid productId)
    {
        var reviews = await _context.Reviews
            .Where(r => r.ProductID == productId)
            .Select(r => new ReviewDto(r.ReviewID, r.Rating, r.Comment, r.UserID))
            .ToListAsync();

        if (!reviews.Any())
        {
            return NotFound(new { message = "No reviews found for this product." });
        }

        return Ok(reviews);
    }
}
