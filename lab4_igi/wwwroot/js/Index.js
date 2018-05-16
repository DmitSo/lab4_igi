var filterList = function () {
    var key = $('#find').val();
    $('.item-line').toArray().forEach(function (item) {
        var id = item.getAttribute('id');
        if (!id.startsWith(key)) {
            $('#' + id).hide();
        }
        else {
            $('#' + id).show();
        }
    });
};

var saveToSession = function (urlAdress) {
    $.ajax({
        data: {
            find: $('#find').val(),
            first: $('#sort-name').prop('checked'),
            second: $('#sort-price').prop('checked')
        },
        url: urlAdress,
        type: 'POST'
    });
}

var askServerAndRenderList = function (urlAdress) {
    $('#list').text('');
    $.ajax({
        data: {
            name: $('#sort-name').prop('checked'),
            price: $('#sort-price').prop('checked')
        },
        url: urlAdress,
        success: function (response) {
            $('#list').append(response);
            filterList();
        }
    });
}