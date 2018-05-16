askServerAndRenderList('ServiceTypes/SortedList');

$('#sort-first, #sort-second').click(function () {
    askServerAndRenderList('ServiceTypes/SortedList');
    saveToSession('ServiceTypes/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('ServiceTypes/SaveFiltration');
});

filterList();