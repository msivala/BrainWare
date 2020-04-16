$(document).ready(function () {
    var $orders = $('#orders');
    $.ajax({
        'url': '/api/order/1',
        'type': 'GET',
        'success': function (data) {

            var $orderList = $('<ul/>');

            if (data) {
                $.each(data,
                    function (i) {
                        var $li = $('<li/>').text(this.Description + ' (Total: $' + this.OrderTotal + ')')
                            .appendTo($orderList);

                        var $productList = $('<ul/>');

                        $.each(this.OrderProducts, function (j) {
                            var $li2 = $('<li/>').text(this.Product.Name + ' (' + this.Quantity + ' @ $' + this.Price + '/ea)')
                                .appendTo($productList);
                        });

                        $productList.appendTo($li);
                    });

                $orders.append($orderList);
            }
        },
        'error': (err) => {
            var $errMsg = $('<h4/>');

            switch (err.status) {
                case 404:
                    $errMsg.text('No orders were found.');
                    break;
                case 500:
                    $errMsg.text(`An unexpected error occurred on the server: ${err.responseJSON.ExceptionMessage}`);
                    break;
                default:
                    $errMsg.text(`An unknown error occurred: ${err.statusText}.`);
            }
            

            $orders.append($errMsg);
        }
    });
});