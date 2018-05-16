askServerAndRenderList('Clients/SortedList');

$('#sort-name, #sort-price').click(function () {
    askServerAndRenderList('Clients/SortedList');
    saveToSession('Clients/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('Clients/SaveFiltration');
});

filterList();