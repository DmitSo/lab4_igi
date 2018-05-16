askServerAndRenderList('RoomTypes/SortedList');

$('#sort-name, #sort-price').click(function () {
    askServerAndRenderList('RoomTypes/SortedList');
    saveToSession('RoomTypes/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('RoomTypes/SaveFiltration');
});

filterList();