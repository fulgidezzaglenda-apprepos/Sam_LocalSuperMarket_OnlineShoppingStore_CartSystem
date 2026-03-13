using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sam_LocalSuperMarket_OnlineShoppingStore1.Data;
using Sam_LocalSuperMarket_OnlineShoppingStore1.Models;

namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly SamOnlineShoppingStoreDBContext _dbContext;

        public ShoppingCartController(SamOnlineShoppingStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //[Authorize (Roles = "Admin, Customer, Guest")]
        public IActionResult Index()
        {
            //Get the user Id out of the session and store it.
            var Id = HttpContext.Session.GetInt32("ID");
            //Check if the user is not logged in and an invalid ID has been stored.
            if (Id == null || Id < 0 || HttpContext.User.Identity.IsAuthenticated == false)
            {
                return BadRequest();
            }

            //Get the shopping cart for the current user from the database.
            var shoppingCart = _dbContext.ShoppingCarts.Include(c => c.CartUser)
                .Where(c => c.UserId == Id && c.IsFinalised == false).FirstOrDefault();
            //If the user doesn't currently have an open cart return to the partial view with
            //no data provided
            if (shoppingCart == null)
            {
                return PartialView("_ShoppingCartPartial");
            }
            //Get the shopping cart lines for the selected cart and add them to its carts items.
            shoppingCart.CartItems = _dbContext.ShoppingCartItems.Include(ci => ci.ProductsItem)
                .Where(ci => ci.ShoppingCartId == shoppingCart.Id).ToList();
            //Return the shopping cart partial view with any data we hand over to it.
            return PartialView("_ShoppingCartPartial", shoppingCart);
        }

       public IActionResult CartHistoryPartial(int cartId)
       {
            // Get the user Id out of the session and store it. //This works. DO NOT DELETE
            var userId = HttpContext.Session.GetInt32("ID");

            // Check if the user is not logged in or has an invalid ID stored.
            if (userId == null || userId < 0 || !HttpContext.User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            var cartView = _dbContext.ShoppingCarts.Include(c => c.CartItems)
                                                   .ThenInclude(c => c.ProductsItem)
                                                   .Include(c => c.CartUser)
                                                   .FirstOrDefault(c => c.UserId == userId && c.Id == cartId);
            if ( cartView == null)
            {
                return NotFound();
            }

            return PartialView("_CartDetailsPartial", cartView);
        }

        public async Task<ActionResult> UpdatedQuantity([FromBody] ShoppingCartItem item)
        {
            //Pass the model to entity framework to be used. By using the attach command we can tell entity framework to only change the field we
            //specify in the next step.
            _dbContext.ShoppingCartItems.Attach(item);
            //Tell entity frameworks to find the quantity property of the item and sets the IsModified property to true.
            //This will mark this property as one that need to be updted in the database.
            _dbContext.Entry(item).Property(x => x.Quantity).IsModified = true;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        public async Task<ActionResult> RemoveFromCart([FromBody] ShoppingCartItem item)
        {
            //Tells entity framework to remove the specified item.
            _dbContext.ShoppingCartItems.Remove(item);
            //Save the changes.
            await _dbContext.SaveChangesAsync();
            //Send a response back to the javascript when this method has been called.
            return Ok();
        }

        public async Task<ActionResult> ClearTheCart([FromBody] ShoppingCartItem items)
        {
            _dbContext.ShoppingCartItems.Remove(items);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        public async Task<ActionResult> AddToCart(int Id)
        {
            //Check if there is a user ID stored in the session data. This will default the ID to -1 if there isn't
            //one saved in the session.
            var userId = HttpContext.Session.GetInt32("ID") ?? -1;
            //Check if the user is logged in and the ID is valid.
            if (userId == 0 || HttpContext.User.Identity.IsAuthenticated == false)
            {
                return Unauthorized();
            }
            //Check if the user has a cart that is still unfinalised and if they do get it from the database.
            var cart = _dbContext.ShoppingCarts.Where(c => c.UserId == userId && c.IsFinalised == false)
                                               .Include(c => c.CartItems).FirstOrDefault();
            //Create a new cart item with the details we will need to add it later when it's needed.
            var cartItem = new ShoppingCartItem
            {
                ProductsId = Id,
                Quantity = 1
            };
            //If the user currently doesn't have an open cart.
            if (cart == null)
            {
                //Create a new cart with the user's Id and start a new cart item list with the created item as it's first entry.
                cart = new ShoppingCart
                {
                    UserId = userId,
                    CartItems = new List<ShoppingCartItem> { cartItem }
                };
                //Add a new cart to entity framework as well as the details of the new cartItem from within the cart.
                //This will also add the cartItem's foreign key reference to the shopping cart.
                _dbContext.ShoppingCarts.Add(cart);

                //Save the change to the database.
                await _dbContext.SaveChangesAsync();
                //Send an Ok response to the user.
                return Ok();
            }
            //If the user already has an open cart.
            else
            {
                //Check if their cart already has a copy of the item in it.
                var item = cart.CartItems.Where(ci => ci.ProductsId == Id).FirstOrDefault();
                //If the cart already has an item.
                if (item != null)
                {
                    //Increase the items quantity by one.
                    item.Quantity++;
                    //Hand the item over to entity framework.
                    _dbContext.ShoppingCartItems.Attach(item);
                    //Tell entity framework to find the Quantity property of the item and sets the IsModified property to true.
                    //This will mark this property as one that needs to be updated in the database.
                    _dbContext.Entry(item).Property(x => x.Quantity).IsModified = true;
                }
                else
                {
                    //Add the cart's ID to the shopping cart Item.
                    cartItem.ShoppingCartId = cart.Id;
                    //Pass the cart item to the entity framework to be used.
                    _dbContext.Add(cartItem);
                }
            }
            //Save the change to the database.
            await _dbContext.SaveChangesAsync();
            //Send an Ok response to the user.
            return Ok();
        }

        /*public async Task<ActionResult> FinaliseCart (int id) //This is working. DO NOT DELETE.
        {
            if (id < 1)
            {
                return BadRequest();
            }
            //Create a new cart object with the ID, total and isFinalised properties set.
            //The total will be set by calling the calculate total method and assigning the result.
            var cart = new ShoppingCart {  Id = id, Total = CalculateCartTotal(id), IsFinalised = true, Date = DateTime.Now };
            
            //Pass the new cart model to the context class for saving.
            _dbContext.ShoppingCarts.Attach(cart);
            //Tell the context class which properties we want to update.
            _dbContext.Entry(cart).Property(c => c.IsFinalised).IsModified = true;
            _dbContext.Entry(cart).Property(c => c.Total).IsModified = true;
            _dbContext.Entry(cart).Property(c => c.Date).IsModified = true;
            //Save the changes to the database.
            await _dbContext.SaveChangesAsync();

            return Ok();
        }*/

        public async Task<ActionResult> FinaliseCart(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var cart = await _dbContext.ShoppingCarts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            cart.Total = CalculateCartTotal(id);
            cart.IsFinalised = true;
            cart.Date = DateTime.Now;

            _dbContext.Entry(cart).Property(c => c.IsFinalised).IsModified = true;
            _dbContext.Entry(cart).Property(c => c.Total).IsModified = true;
            _dbContext.Entry(cart).Property(c => c.Date).IsModified = true;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        /*public async Task<ActionResult> ClearShoppingCart(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var cartToCancel = await _dbContext.ShoppingCarts
                                                 .Include(c => c.CartItems)
                                                 .FirstOrDefaultAsync(c => c.Id == id);
            if (cartToCancel == null)
            {
                return NotFound();
            }

            // Clone the canceled cart to create a new finalized cart
            var finalizedCart = new ShoppingCart
            {
                UserId = cartToCancel.UserId,
                IsFinalised = true,
                Date = DateTime.Now,
                CartItems = new List<ShoppingCartItem>()
                // Add other relevant properties
            };

            foreach (var item in cartToCancel.CartItems)
            {
                finalizedCart.CartItems.Add(new ShoppingCartItem
                {
                    ProductsId = item.ProductsId,
                    Quantity = item.Quantity
                    // Add other relevant properties
                });
            }

            // Add the new finalized cart to the database
            _dbContext.ShoppingCarts.Add(finalizedCart);
            await _dbContext.SaveChangesAsync();

            // Optionally, update the canceled cart properties
            cartToCancel.IsCancelled = true;
            cartToCancel.Date = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }*/


        public async Task<ActionResult> ClearShoppingCart (int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var cart = await _dbContext.ShoppingCarts.Include(c => c.CartItems)
                                                     .FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null)
            {
                return NotFound();
            }
            _dbContext.ShoppingCartItems.RemoveRange(cart.CartItems);
            cart.Total = 0;
            //cart.Id = id;
            cart.IsCancelled = true;
            cart.Date = DateTime.Now;

            //this was just added to check something! will be deleted if this is not needed or not doing anything as expected.
            _dbContext.Entry(cart).Property(c => c.IsCancelled).IsModified = true;
            _dbContext.Entry(cart).Property(c => c.Total).IsModified = true;
            _dbContext.Entry(cart).Property(c => c.Date).IsModified = true;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        public double CalculateCartTotal(int id)
        {
            //Find all the cart items foir the cart with the ID we jave provided. Also get their products so we can get the products price for each item.
            var cartItem = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == id)
                                                       .Include(c => c.ProductsItem).ToList();
            //Create a variable to hold the total of our cart.
            double total = 0;
            //Cycle through all the items for thecart.
            foreach (var item in cartItem)
            {
                //Multiply the products price by the quantity for each item and add it to the current total.
                total += (double)item.ProductsItem.Price * item.Quantity;
            }
            return total;
        }
    }
}

