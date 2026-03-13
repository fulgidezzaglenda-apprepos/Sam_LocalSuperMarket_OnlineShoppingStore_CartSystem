//Add a listener to the broswer window which will run the enclosed code once it finishes loading.
window.addEventListener('load', () => {
    //Find the cart modal button and add a listener which will open the method shown when
    //the button is pressed.
    document.getElementById("btnCartHistory").addEventListener('click', async (e) => {
        showCartHistoryModal()
    });
});

//The method triggered by the modal button event listener.
async function showCartHistoryModal() {
    //Send a request to the endpoint provided inside the fetch request and store the response
    var result = await fetch("/ShoppingCart/CartHistoryPartial");

    //Retrieve the HTML content from the body of the fetch request result.
    //This will be the details of the shopping cart partial view.
    var htmlResult = await result.text();
    //Find the cart modal body by using its ID and put the partial view html
    //between its html tags.
    document.getElementById("cartHistoryModalBody").innerHTML = htmlResult;

    let form = document.querySelector("form[action='/ShoppingCart/CartHistoryPartial']");

    $.validator.unobtrusive.parse(form);

    form.addEventListener('submit', async (e) => {
        e.preventDefault();

        let form = e.target
        let formResult = $(form);

        if (formResult.valid() == false) {
            return;
        }

        let result = await fetch("ShoppingCart/CartHistoryPartial", {
            method: "POST",
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify(form)
        });

        if (result.status != 200) {
            alert("Something went wrong.");
            return;
        }

        location.reload();
    });

    //Use JQUERY to find the modal by its ID (dont forget to use the # symbol before the ID name)
    //Then run the modal-show command on the modal to make it visible.
    $('#cartHistory').modal('show');
}

async function calculateCartTotal() {
    //Use the query selector to find all the line total paragraph elements and stored them in a list. We will use those later to
    //add all the totals together for the cart total.
    let lineItems = document.querySelectorAll(".lineTotal");
    //Create a variable to be a running total of all out line item totals as we add them together.
    let total = 0;
    //Cycle through all the line items in a foreach loop.
    lineItems.forEach((item) => {
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