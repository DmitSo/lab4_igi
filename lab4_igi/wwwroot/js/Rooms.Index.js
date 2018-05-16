askServerAndRenderList('Rooms/SortedList');

$('#sort-name, #sort-price').click(function () {
    askServerAndRenderList('Rooms/SortedList');
    saveToSession('Rooms/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('Rooms/SaveFiltration');
});

filterList();