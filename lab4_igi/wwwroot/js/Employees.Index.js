askServerAndRenderList('Employees/SortedList');

$('#sort-name, #sort-price').click(function () {
    askServerAndRenderList('Employees/SortedList');
    saveToSession('Employees/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('Employees/SaveFiltration');
});

filterList();