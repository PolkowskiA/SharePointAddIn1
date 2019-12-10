function AddOrUpdateCar(url) {
    var form = $('#CarsForm');
    var inputValue = $('input#URLHolder').val().toString();
    $(form).append(
        $('<input>').attr('type', 'hidden').attr('name', 'SPHostUrl').val(inputValue)
    );
    form.submit();
}