$('#item-search input').on('change', function () {
    let value = $(this).val()

    $('#item-table').load('/Administration/Items/ItemTable', { search: value })
})

$('#order-search input').on('change', function () {
    let value = $(this).val()

    $('#order-table').load('/Administration/Orders/OrderTable', { search: value })
})