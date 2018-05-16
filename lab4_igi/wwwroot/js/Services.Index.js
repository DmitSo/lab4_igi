askServerAndRenderList('Services/SortedList');

$('#sort-first, #sort-second, #sort-third').click(function () {
    askServerAndRenderList('Services/SortedList');
    saveToSession('Services/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('Services/SaveFiltration');
});

filterList();