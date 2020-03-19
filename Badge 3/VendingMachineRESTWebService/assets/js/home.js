// checks the browser fully renders and calls the populate function
$(document).ready(function () {
    'use strict';
    $('#number').val(0); // sets value back/to zero 
    populate();
});

// Calls the buy function passing money and ID
function purchase() {
    'use strict';
    buy(($('#number').val() * 0.01).toFixed(2), parseInt($('#itemID').val()));
}
// Sets an ID
function set(newId) {
    'use strict';
    $('#itemID').val(newId);
}

// Gets data from the API
function buy(money, itemID) {
    'use strict';
    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/money/' + money + '/item/' + itemID,
        success: function (data) {
            $('#messageBox').val("Thank you!!!");
            $('#changeBox').val('' + data.quarters + ' quarter ' + data.dimes + ' dime ' + data.nickels + ' nickel and ' + data.pennies + ' pennies is your change.');
            $('#number').val(0);
            add(0);
        },
        statusCode: {
            422: function (XMLHttpRequest) {
                $('#messageBox').val($.parseJSON(XMLHttpRequest.responseText).message);
            }
        }
    });
}

// Get data from the API and Displays the vending machine buttons
function populate () {
    'use strict';
    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/items',
        success: function (data) {
            $('#itemBoxes').text('');
            $.each(data, function(index,item) {
                var html = '<div class="col-4 border bg-light item-option" onclick="set(';
                html += item.id;
                html += ')">';
                html += '<div class="text-left" style="font-size:10pt;">';
                html += item.id;
                html += '</div>';
                html += '<div class="text-center">';
                html += item.name;
                html += '<br/>$';
                html += parseFloat(item.price).toFixed(2);
                html += '<br/>Quantity Left: ';
                html += item.quantity;
                html += '</div>';
                html += "</div>";
                $('#itemBoxes').append(html);
            });
        },
        // If the server is not available 
        error: function () {
            alert('Busted, come back l8er!');
        }
    });
}

// Collect amount, add to "number" and display results
function add(amount) {
    $('#number').val(parseInt($('#number').val()) + amount);
    $('#money').text(($('#number').val() * 0.01).toFixed(2));
}

// Display change
function change() {
    $('#itemID').val(''); // clear
    $('#messageBox').val(''); // clear
    populate();
    if($('#number').val() == 0) {
        $('#changeBox').val(''); // clear
    } else {
        // Calculate the change
        var quarter = Math.floor(parseInt($('#number').val()) / 25);
        var dime = Math.floor(parseInt($('#number').val()) / 10);
        var nickel = Math.floor(parseInt($('#number').val()) / 5);
        var penny = parseInt($('#number').val()) % 5;
        
        // Display the results
        $('#number').val(parseInt($('#number').val()) % 25);
        $('#number').val(parseInt($('#number').val()) % 10);
        $('#changeBox').val('' + quarter + ' quarter ' + dime + ' dime ' + nickel + ' nickel and ' + penny + ' pennies is your change.');
        $('#number').val(0);
        
        // Call add passing 0 to display the results.
        add(0);
    }
}