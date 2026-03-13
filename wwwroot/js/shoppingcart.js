//Add a listener to the broswer window which will run the enclosed code once it finishes loading.
window.addEventListener('load', () => {
    //Find the cart modal button and add a listener which will open the method shown when
    //the button is pressed.
    document.getElementById("btnCartModal").addEventListener('click', () => showCartModal());
});

//The method triggered by the modal button event listener.
async function showCartModal()
{
    //Send a request to the endpoint provided inside the fetch request and store the response
    let result = await fetch("/ShoppingCart/Index");
    //Check if the request was successful or not. It will not be 
    //successful if the user is not logged in.
    if (result.status != 200)
    {
        //Redirect to the login controller and run the login endpoint method.
        location.href = "/UserLogin/UserLogin";
    }
    //Retrieve the HTML content from the body of the fetch request result.
    //This will be the details of the shopping cart partial view.
    let htmlResult = await result.text();
    //Find the cart modal body by using its ID and put the partial view html
    //between its html tags.
    document.getElementById("cartModalBody").innerHTML = htmlResult;

    //Searches through the HTML of the page and finds all the item-qty-form objects.
    let itemQtyForms = document.querySelectorAll(".item-qty-form");
    //Cycle through the colleciotn of forms. For each form and an event listener
    //that will trigger on the form submit action to tell the form not to do its default 
    //action. This means the form will not auto-submit or close the modal/window it is in.
    itemQtyForms.forEach((form) => {
        form.addEventListener('submit', (e) => {
            e.preventDefault();
        });
    });

    setupQuantityButtons();
    setupRemoveButtons();
    setupClearButtons();

    calculateCartTotal();
    //Use the query selector to find the checkout button by using its D.
    let checkoutButton = document.querySelector("#btnCheckOut");
    //Checks if the button was found this will fall if the modal has no valid cart showing.
    if (checkoutButton != null)
    {
        //Add a listener to the button to trigger the finalise cart method.
        checkoutButton.addEventListener('click', (e)=> finaliseCart(e))
    }

    let clearCartButton = document.querySelector("#btnClearCart");
    if (clearCartButton != null)
    {
        clearCartButton.addEventListener('click', (e)=> clearShoppingCart(e))
    }

    //Use JQUERY to find the modal by its ID (dont forget to use the # symbol before the ID name)
    //Then run the modal-show command on the modal to make it visible.
    $('#cartModal').modal('show');
}

async function finaliseCart(e)
{
    if (confirm("Complete CheckOut?") == true)
    {
        //Get the cart ID out of the value attribute of the button.
        let id = parseInt(e.target.getAttribute("value"))
        //Send a fetch request to the contorller using the ID and store the response.
        let result = await fetch("/ShoppingCart/FinaliseCart/" + id);
        //Throw an error alert if the fetch request fails.
        if (result.status != 200)
        {
            alert("Something went wrong! Unable to finalise cart!")
        }
        else
        {
            $('#cartModal').modal('hide');
        }
    }
    else
    {
        return;
    }
}

async function clearShoppingCart(e)
{
    if (confirm("Clear Shopping Cart?") == true) {
        let id = parseInt(e.target.getAttribute("value"))
        let result = await fetch("/ShoppingCart/ClearShoppingCart/" + id);
        if (result.status != 200) {
            alert("Something went wrong! Unable to clear the shopping cart!")
        }
        else {
            $('#cartModal').modal('hide');
        }
    }
    else
    {
        return;
    }
}

async function setupRemoveButtons()
{
    //Searches through the HTML of the page and finds all the objects with the 
    //remove-button class on them.
    let removeButtons = document.querySelectorAll(".remove-button");
    //Cycle through each button and add a listener to each one that will trigger the method
    //called removeItem.
    removeButtons.forEach((button) => {
        //Get the value attribute from the button tags and store its value. This will be
        //the primary key we stored when setting the buttons up.
        let cartItemId = parseInt(button.getAttribute("value"));
        button.addEventListener("click", () => removeItem(cartItemId));
    });
}

async function setupClearButtons()
{
    let clearButtons = document.querySelectorAll(".clear-button");
    clearButtons.forEach((button) => {
        let cartItemId = parseInt(button.getAttribute("value"));
        button.addEventListener("click", () => clearCart(cartItemId));
    });
}

async function removeItem(cartItemId)
{
    //Craete a JSON object to hold the cart ID
    let cartItem = {
        Id: cartItemId
    };
    //Send a fetch request to the controller to request the item to be removed from the
    //database.
    let result = await fetch('/ShoppingCart/RemoveFromCart', {
        method: "DELETE",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(cartItem)
    });
    //If the request fails, alert the user with an error message
    if (result.status != 200) {
        alert("Remove Failed")
        return;
    }
    //Refresh the cart modal.
    showCartModal();
}

async function clearCart(cartItemId)
{
    let cartItem = {
        Id: cartItemId
    };

    let result = await fetch('/ShoppingCart/ClearTheCart', {
        method: "DELETE",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(cartItem)
    });
    if (result.status != 200) {
        alert("Clear Cart Failed")
        return;
    }
    showCartModal();
}

async function setupQuantityButtons()
{
    //Searches through the HTML of the page and finds all the objects with the 
    //minus - button class on them.
    let minusButtons = document.querySelectorAll(".minus-button");
    //Cycle through each button and add a listener to each one that will trigeer the method
    //called decreaseQuantity.
    minusButtons.forEach((button) => {
        button.addEventListener("click", (e) => decreaseQuantity(e));
    });

    //Searches through the HTML of the page and finds all the objects with the 
    //minus - button class on them.
    let plusButtons = document.querySelectorAll(".plus-button");
    //Cycle through each button and add a listener to each one that will trigeer the method
    //called increaseQuantity.
    plusButtons.forEach((button) => {
        button.addEventListener("click", (e) => increaseQuantity(e));
    });
}

async function increaseQuantity(e)
{
    //Get the target of the event. Find the target's parent form then run the query
    //selector on the form to find the first input element and get its value.
    let cartItemId = parseInt(e.target.form.querySelector("input").value);
    //Do the same as above except find the item in the form using the .qty class and grab its
    //innerText which is the text between the tags of the element.
    let qty = parseInt(e.target.form.querySelector(".qty").innerText);
    //Increase the quantity and update the qty field in the form with the new value
    qty++;
    e.target.form.querySelector(".qty").innerText = qty;

    //Retrieve the paragraph tag the line total is stored in by using the query selector on the from of the button that was just pressed.
    let lineItem = e.target.form.querySelector(".lineTotal");
    //Get the contents of the value attributes from the lineItem element and convert it to a float.
    let unitPrice = parseFloat(lineItem.getAttribute("value"));
    //Calculate the total price based upon the unit price times the quantity. We are multiplying the unit price by 100 to avoid floating point errors
    //and wired rounding errors.
    let totalPrice = qty * (unitPrice * 100);
    //Put the new total price back into the paragraph tags. We need to divide the total price by 100 convert.
    lineItem.innerText = "Total: $" + parseFloat(totalPrice / 100).toFixed(2);

    calculateCartTotal();

    //Pass the new qty and cart Item ID to the method that will request the controller to update the
    //value in the database.
    updateQuantity(qty, cartItemId);
}

async function decreaseQuantity(e) {
    //Get the target of the event. Find the target's parent form then run the query
    //selector on the form to find the first input element and get its value.
    let cartItemId = parseInt(e.target.form.querySelector("input").value);
    //Do the same as above except find the item in the form using the .qty class and grab its
    //innerText which is the text between the tags of the element.
    let qty = parseInt(e.target.form.querySelector(".qty").innerText);

    //If the quantity is already at 1, dont decrease it. We will remove it instead. 
    if (qty == 1)
    {
        removeItem(cartItemId);
        return;
    }

    //Increase the quantity and update the qty field in the form with the new value
    qty--;
    e.target.form.querySelector(".qty").innerText = qty;

    //Retrieve the paragraph tag the line total is stored in by using the query selector on the from of the button that was just pressed.
    let lineItem = e.target.form.querySelector(".lineTotal");
    //Get the contents of the value attributes from the lineItem element and convert it to a float.
    let unitPrice = parseFloat(lineItem.getAttribute("value"));
    //Calculate the total price based upon the unit price times the quantity. We are multiplying the unit price by 100 to avoid floating point errors
    //and wired rounding errors.
    let totalPrice = qty * (unitPrice * 100);
    //Put the new total price back into the paragraph tags. We need to divide the total price by 100 convert.
    lineItem.innerText = "Total: $" + parseFloat(totalPrice / 100).toFixed(2);

    calculateCartTotal();

    //Pass the new qty and cart Item ID to the method that will request the controller to update the
    //value in the database.
    updateQuantity(qty, cartItemId);
}

async function updateQuantity(qty, cartItemId)
{
    //Create a Javascript object to hold the cartItemId and updated quantity. The fields of
    //this object need to match the shoppingCartItem model of our project.
    let updatedItem = {
        Quantity: qty,
        Id: cartItemId
    };
    //Send a fetch request to the Shopping Cart controller to run the Update Quanity
    //endpoint. The request will be a PUT (update) method and will pass the updatedItem
    //details in the request body.
    let response = await fetch("/ShoppingCart/UpdatedQuantity", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(updatedItem)
    });
    //If the request fails and sends an error response message. Log the error and reset the modal.
    if (response.status != 200) {
        alert("Update Quantity Failed")
        showCartModal();
    }
}

async function calculateCartTotal()
{
    //Use the query selector to find all the line total paragraph elements and stored them in a list. We will use those later to
    //add all the totals together for the cart total.
    let lineItems = document.querySelectorAll(".lineTotal");
    //Create a variable to be a running total of all out line item totals as we add them together.
    let total = 0;
    //Cycle through all the line items in a foreach loop.
    lineItems.forEach((item) =>
    {
        //Perform a split on the line item's text to retrieve just the price section and store it.
        let linePrice = parseFloat(item.innerText.split("$")[1]) * 100;

        console.log("total is " + total + " and line price is " + linePrice)

        //Add the line price to the current total.
        total += linePrice;
    });
    //Put the total after it has been added up into the text of the price field in the modal footer.
    //This uses their # in the query selector this time becuase it is finding  the element it using an id not a class.
    document.querySelector("#modalTotal").innerText = "Cart Total: $" + (total / 100).toFixed(2);
}