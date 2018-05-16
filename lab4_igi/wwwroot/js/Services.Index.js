askServerAndRenderList('Services/SortedList');

$('#sort-name, #sort-price').click(function () {
    askServerAndRenderList('Services/SortedList');
    saveToSession('Services/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('Services/SaveFiltration');
});

filterList();