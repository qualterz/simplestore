$(':button[name="addToCartButton"]').on('click', function () {
    let inCartClass = 'in-cart'
    let inCart = $(this).hasClass(inCartClass)

    let buttonAddClass = 'btn-outline-success'
    let buttonRemoveClass = 'btn-success'

    if (inCart) {
        $(this).removeClass(buttonRemoveClass)
            .addClass(buttonAddClass)
    } else {
        $(this).removeClass(buttonAddClass)
            .addClass(buttonRemoveClass)
    }

    let icon = $(this).find('i')

    let iconAddClass = 'bi-cart-plus'
    let iconRemoveClass = 'bi-cart-dash-fill'

    if (inCart) {
        icon.removeClass(iconRemoveClass)
            .addClass(iconAddClass)
    } else {
        icon.removeClass(iconAddClass)
            .addClass(iconRemoveClass)
    }

    let id = $(this).find('input').attr('value')

    if (inCart) {
        $.post('/Cart/Remove', { id: id })
    } else {
        $.post('/Cart/Add', { id: id })
    }

    $(this).toggleClass(inCartClass)
})

$('#cart-item-list :button').on('click', function () {
    let id = $(this.closest('.card')).attr('id')

    $.post('/Cart/Remove', { id: id }).done(function () {
        $('#checkout').load('/Cart/CheckoutPartial')
    })

    $(this.closest('.col')).remove()
})

$('#cart-item-list :button').on('click', function () {
    let count = $('#cart-item-list').children().length

    if (count === 0) {
        $.get('/Partial/EmptyMessage', function (data) {
            $('#cart-item-list').replaceWith(data)
        })
    }

    console.log(count)
})

$('#cart-item-list input[name="quantity"]').on('change', function () {
    let id = $(this.closest('.card')).attr('id')
    let quantity = $(this).val()

    $.post('/Cart/Update', { id: id, quantity: quantity }).done(function () {
        $('#checkout').load('/Cart/CheckoutPartial')
    })
})

$('#item-search input').on('change', function () {
    let search = $(this).val()
    let orderType = $('#item-sort select').val()
    
    $('#item-list').load('/Home/ItemList',
        { search: search, orderType: orderType })
})

$('#item-sort select').on('change', function () {
    let search = $('#item-search input').val()
    let orderType = $(this).val()

    $('#item-list').load('/Home/ItemList',
        { search: search, orderType: orderType })
})