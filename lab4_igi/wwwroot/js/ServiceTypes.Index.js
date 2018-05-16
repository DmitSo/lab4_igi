askServerAndRenderList('ServiceTypes/SortedList');

$('#sort-name, #sort-price').click(function () {
    askServerAndRenderList('ServiceTypes/SortedList');
    saveToSession('ServiceTypes/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('ServiceTypes/SaveFiltration');
});

filterList();