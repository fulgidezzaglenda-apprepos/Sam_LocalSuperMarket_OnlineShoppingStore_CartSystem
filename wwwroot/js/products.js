//File to manage the Javascript code that is only for the productss page. By utting it here instead of in the
//site.js file, we can setup our website to only run this coder form the products page. If it was in the
//site.js file, it would try to run on every page that opens.

window.addEventListener('load', () => {

    // Is this needed? There's no btnCreateProducts
    //document.getElementById("btnCreateProducts").addEventListener("click", () => showCreateProductsModal())
  

    //Find all the buttons in the page that have the add-item-button class on them
    let addButtons = document.querySelectorAll(".add-item-button");

    addButtons.forEach((button) => {
        //Get the value attribute from the button to get the bookId
        let productsId = parseInt(button.getAttribute("value"));
        //Add a listener to the button for the click action that will trigger the
        //addItemToCart method when it is pressed.
        button.addEventListener('click', () => addItemToCart(productsId));
    });

    /*//Find all the buttons in the page that have the add-item-button class on them
    let viewButtons = document.querySelectorAll(".view-cart-button");

    viewButtons.forEach((button) => {
        //Get the value attribute from the button to get the bookId
        let cartId = parseInt(button.getAttribute("value"));
        //Add a listener to the button for the click action that will trigger the
        //addItemToCart method when it is pressed.
        button.addEventListener('click', () => viewCartDetails(cartId));
    });*/

    //Find all the buttons in the page that have the add-item-button class on them
    let viewButtons = document.querySelectorAll(".view-cart-button");

    viewButtons.forEach((button) => {
        //Get the value attribute from the button to get the bookId
        let cartId = parseInt(button.getAttribute("value"));
        //Add a listener to the button for the click action that will trigger the
        //addItemToCart method when it is pressed.
        button.addEventListener('click', () => addToCartView(cartId));
    });

    //gets all the elements with the 'edit-Button' class applied and stores them.
    let deleteButtons = document.getElementsByClassName("delete-Button")

    //Cycle through the array of buttons
    for (let i = 0; i < deleteButtons.length; i++) {
        //get the Id out of the value atribute or the current button
        let id = deleteButtons[i].getAttribute('value');
        //Attach an event listener to the button and set ti to trigger the specified method
        //when pressed and pass the id to the method.
        deleteButtons[i].addEventListener("click", () => showDeleteProductsModal(id))
    }

    //gets all the elements with the 'edit-Button' class applied and stores them.
    let editButtons = document.getElementsByClassName("edit-Button")

    //Cycle through the array of buttons
    for (let i = 0; i < editButtons.length; i++) {
        //get the Id out of the value atribute or the current button
        let id = editButtons[i].getAttribute('value');
        //Attach an event listener to the button and set ti to trigger the specified method
        //when pressed and pass the id to the method.
        editButtons[i].addEventListener("click", () => showEditProductsModal(id))
    }

    //gets all the elements with the 'details-Button' class applied and stores them.
    let detailsButtons = document.getElementsByClassName("details-Button")

    //Cycle through the array of buttons
    for (let i = 0; i < detailsButtons.length; i++) {
        //get the Id out of the value atribute or the current button
        let id = detailsButtons[i].getAttribute('value');
        //Attach an event listener to the button and set ti to trigger the specified method
        //when pressed and pass the id to the method.
        detailsButtons[i].addEventListener("click", () => showDetailsProductsModal(id))
    }
})

async function addItemToCart(productsId)
{
    //Send the fetch request to add the book to the user's cart. The book
    //Id is added as part of the URL address
    let result = await fetch("/ShoppingCart/AddToCart/" + productsId);
    //If the result is an Unproductsised message, redirect the user to the login page.
    if (result.status == 401) {
        location.href = "/UserLogin/UserLogin";
        return;
    }
    //If the user message is anything other than 200 show an error message
    if (result.status != 200) {
        alert("Add to cart failed")
        return;
    }
    //Refresh the cart modal. Even though this method is in a different file, we can
    //still call it here because it is in the _Layout.cshtml which means it is available
    //wihtin all of ther views.
    showCartModal();
}

//This is working for Partial view cart History -- DO NOT DELETE --
/*async function viewCartDetails(cartId) {
    //Send the fetch request to add the book to the user's cart. The book
    //Id is added as part of the URL address
    let result = await fetch("/ShoppingCart/ViewDetails/" + cartId);
    //If the result is an Unproductsised message, redirect the user to the login page.
    if (result.status == 401) {
        location.href = "/UserLogin/UserLogin";
        return;
    }
    
    //Refresh the cart modal. Even though this method is in a different file, we can
    //still call it here because it is in the _Layout.cshtml which means it is available
    //wihtin all of ther views.
    showCartModal();
}*/

async function addToCartView(cartId) {
    //Send the fetch request to add the book to the user's cart. The book
    //Id is added as part of the URL address
    let result = await fetch("/ShoppingCart/AddToView/" + cartId);
    //If the result is an Unproductsised message, redirect the user to the login page.
    if (result.status == 401) {
        location.href = "/UserLogin/UserLogin";
        return;
    }

    //Refresh the cart modal. Even though this method is in a different file, we can
    //still call it here because it is in the _Layout.cshtml which means it is available
    //wihtin all of ther views.
    showCartHistoryModal();
}

async function showEditProductsModal(id) {
    //Request the partial view from the controller
    var result = await fetch("/Products/Edit/" + id);
    //Convert the response into its plain HTML content
    var htmlResult = await result.text();
    //Access the modal body by its ID and change its internal content
    //to be the partial view content.
    document.getElementById("productsModalBody").innerHTML = htmlResult;
    //Update the title of the modal to reflect the pertial view being shown
    document.getElementById("productsModalTitle").innerHTML = "Edit Products";

    //Uses the Javascript query selector command to find the component of the specified type (form)
    //that has an action connecting to the specified endpoint.
    //NOTE: The word form needs to be lower case and have the endpoint capitalised as per your controller
    //endpoint casing used.
    let form = document.querySelector(`form[action='/Products/Edit/${id}']`);

    //Passes the form to JQUERY(A Javascript library) to parse/interpret the form to work out what
    //rules it needs to follow to class as valid. This needs to be done manually because this stp is normally
    //done when the page loads, but the form in the modal is added to the page at a later stage when we push the 
    //button to open the modal.
    $.validator.unobtrusive.parse($('form'));

    console.log(form);
    //Add listener to form to trigger on submit event.
    form.addEventListener('submit', async (e) => {
        //Cancel the default submit action of this form when the button is pressed.
        e.preventDefault();

        //Get the form that was submitted by the event and store it as a variable.
        //NOTE: At this stage the form will just be HTML text.
        let form = e.target;
        //Pass the form to JQUERY to be translated/retrieved at a javascript object.
        //This step will make the form interactable in our code.
        let formResult = $(form);
        //Checks the status of the form to see if it is valid. If it is not valid, which means
        //it doesn't meet our validation rules for the form fields, don't proceed any further.
        //This will stop the form even being sent to the erver by our browser.
        if (formResult.valid() == false) {
            return;
        }

        let formData = {
            //get the form fields by name (ID or asp-for tag) and store them in properties
            //of the form data
            //NOTE: Your targets will be capitalised as per their naming in C# and will need to be
            //in pascal case in the javascript object in order for it to map to your controller.
            Id: id,
            FirstName: e.target["FirstName"].value,
            LastName: e.target["LastName"].value
        }

        console.log(formData);

        //Get the anti-forgery token from the form and store it.
        var token = $('input[name="__RequestVerificationToken"]').val();

        //Send a fetch request top the desired endpoint to create an products
        let result = await fetch("/Products/Edit/" + id, {
            //Set the HTTP method type 
            method: "POST",
            //Set the headers to specify the content type of the request that will be sent.
            data: {

            },
            headers: {
                "content-type": "application/json",
                "RequestVerificationToken": token
            },
            //Convert the form data to a JSON string and add to to the reqest body
            body: JSON.stringify(formData)
        })

        if (result.status != 200) {
            alert("Something Went wrong")
            return;
        }

        location.reload();
    });

    //Tell the modal to show itself on screen.
    $('#productsModal').modal("show");
}

async function showDetailsProductsModal(id) {
    //Request the partial view from the controller
    var result = await fetch("/Products/Details/" + id);
    //Convert the response into its plain HTML content
    var htmlResult = await result.text();
    //Access the modal body by its ID and change its internal content
    //to be the partial view content.
    document.getElementById("productsModalBody").innerHTML = htmlResult;
    //Tell the modal to show itself on screen.
    //Update the title of the modal to reflect the pertial view being shown
    document.getElementById("productsModalTitle").innerHTML = "Products Details";

    $('#productsModal').modal("show");
}

async function showDeleteProductsModal(id) {
    //Request the partial view from the controller
    var result = await fetch("/Products/Delete/" + id);
    //Convert the response into its plain HTML content
    var htmlResult = await result.text();
    //Access the modal body by its ID and change its internal content
    //to be the partial view content.
    document.getElementById("productsModalBody").innerHTML = htmlResult;
    //Update the title of the modal to reflect the pertial view being shown
    document.getElementById("productsModalTitle").innerHTML = "Delete Products?";

    //Uses the Javascript query selector command to find the component of the specified type (form)
    //that has an action connecting to the specified endpoint.
    //NOTE: The word form needs to be lower case and have the endpoint capitalised as per your controller
    //endpoint casing used.
    let form = document.querySelector(`form[action='/Products/Delete/${id}']`);
    console.log(form);
    //Add listener to form to trigger on submit event.
    form.addEventListener('submit', async (e) => {
        //Cancel the default submit action of this form when the button is pressed.
        e.preventDefault();

        //Get the anti-forgery token from the form and store it.
        var token = $('input[name="__RequestVerificationToken"]').val();

        //Send a fetch request top the desired endpoint to create an products
        let result = await fetch("/Products/Delete/" + id, {
            //Set the HTTP method type 
            method: "POST",
            //Set the headers to specify the content type of the request that will be sent.
            data: {

            },
            headers: {
                "content-type": "application/json",
                "RequestVerificationToken": token
            },
            //Convert the form data to a JSON string and add to to the reqest body
            body: ""
        })

        if (result.status != 200) {
            alert("Something Went wrong")
            return;
        }

        location.reload();
    });

    //Tell the modal to show itself on screen.
    $('#productsModal').modal("show");
}


async function showCreateProductsModal() {
    //Request the partial view from the controller
    var result = await fetch("/Products/Create");
    //Convert the response into its plain HTML content
    var htmlResult = await result.text();
    //Access the modal body by its ID and change its internal content
    //to be the partial view content.
    document.getElementById("productsModalBody").innerHTML = htmlResult;
    //Tell the modal to show itself on screen.
    //Update the title of the modal to reflect the pertial view being shown
    document.getElementById("productsModalTitle").innerHTML = "Create Products";

    //Uses the Javascript query selector command to find the component of the specified type (form)
    //that has an action connecting to the specified endpoint.
    //NOTE: The word form needs to be lower case and have the endpoint capitalised as per your controller
    //endpoint casing used.
    let form = await document.querySelector("form[action='/Products/Create']");

    //Passes the form to JQUERY(A Javascript library) to parse/interpret the form to work out what
    //rules it needs to follow to class as valid. This needs to be done manually because this stp is normally
    //done when the page loads, but the form in the modal is added to the page at a later stage when we push the 
    //button to open the modal.
    $.validator.unobtrusive.parse($('form'));

    //Add listener to form to trigger on submit event.
    form.addEventListener('submit', async (e) => {
        //Cancel the default submit action of this form when the button is pressed.
        e.preventDefault();

        //Get the form that was submitted by the event and store it as a variable.
        //NOTE: At this stage the form will just be HTML text.
        let form = e.target;
        //Pass the form to JQUERY to be translated/retrieved at a javascript object.
        //This step will make the form interactable in our code.
        let formResult = $(form);
        //Checks the status of the form to see if it is valid. If it is not valid, which means
        //it doesn't meet our validation rules for the form fields, don't proceed any further.
        //This will stop the form even being sent to the erver by our browser.
        if (formResult.valid() == false) {
            return;
        }


        //Create a Javascript object to hold our form data
        let formData = {
            //get the form fields by name (ID or asp-for tag) and store them in properties
            //of the form data
            //NOTE: Your targets will be capitalised as per their naming in C# and will need to be 
            //in pascal case in the javascript object in order for it to map to your controller.
            FirstName: e.target["FirstName"].value,
            LastName: e.target["LastName"].value
        }

        //Get the anti-forgery token from the form and store it.
        var token = $('input[name="__RequestVerificationToken"]').val();

        //Send a fetch request top the desired endpoint to create an products
        let result = await fetch("/Products/Create", {
            //Set the HTTP method type 
            method: "POST",
            //Set the headers to specify the content type of the request that will be sent.
            data: {

            },
            headers: {
                "content-type": "application/json",
                "RequestVerificationToken": token
            },
            //Convert the form data to a JSON string and add to to the reqest body
            body: JSON.stringify(formData)
        })

        if (result.status != 200) {
            alert("Something Went wrong")
            return;
        }

        location.reload();
    });

    $('#productsModal').modal("show");
}

