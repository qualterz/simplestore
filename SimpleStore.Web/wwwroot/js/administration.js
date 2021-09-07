$('#item-search input').on('change', function () {
    let value = $(this).val()

    console.log(value);

    $('#item-table').load('/Administration/Items/ItemTable', { search: value })
})